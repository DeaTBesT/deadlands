using System.Collections;
using DL.CoreRuntime;
using DL.InterfacesRuntime;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace DL.RaidRuntime.Spawners
{
    public class EnemySpawnController : MonoBehaviour, IInitialize, IDeinitialize
    {
        private const float MinSpawnAngleRadius = 0f;
        private const float MaxSpawnAngleRadius = 360f;
        private const float MaxHeightRayCast = 100f;

        [SerializeField] private EntityInitializer _enemyPrefab;

        [SerializeField] private float _spawnRadius = 10f;
        [SerializeField] private float _spawnInterval = 2f;
        [SerializeField] private LayerMask _groundLayer;

        [ShowNonSerializedField] private Transform _player;

        private Camera _camera;

        private Coroutine _spawnRoutine;

        public bool IsEnable { get; set; } = false;

        public void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }

            IsEnable = true;

            _player = objects[0] as Transform;
            _camera = objects[1] as Camera;

            InitializeSpawn();
        }

        public void Deinitialize(params object[] objects)
        {
            if (!IsEnable)
            {
                return;
            }

            IsEnable = false;

            DeinitializeSpawn();
        }

        private void InitializeSpawn()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }

            _spawnRoutine = StartCoroutine(SpawnEnemies());
        }

        private void DeinitializeSpawn()
        {
            if (_spawnRoutine == null)
            {
                return;
            }

            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }

        private IEnumerator SpawnEnemies()
        {
            while (IsEnable)
            {
                var spawnPos = GetValidSpawnPosition();
                if (spawnPos != Vector3.zero)
                {
                    var enemyObj = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);

                    if (enemyObj.TryGetComponent(out EntityInitializer initializer))
                    {
                        initializer.Initialize(_player);
                    }
                }

                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private Vector3 GetValidSpawnPosition()
        {
            var maxAttempts = 10;
            while (maxAttempts > 0)
            {
                var spawnPoint = GetSpawnPosition();

                if (IsInsideNavMesh(spawnPoint))
                {
                    return spawnPoint;
                }

                maxAttempts--;
            }

            return Vector3.zero;
        }

        private Vector3 GetSpawnPosition()
        {
            var cameraPos = _camera.transform.position;
            var camDistance = GetCameraViewRadius();
            var spawnDistance = camDistance + _spawnRadius;

            var angle = Random.Range(MinSpawnAngleRadius, MaxSpawnAngleRadius) * Mathf.Deg2Rad;
            var xOffset = Mathf.Cos(angle);
            var zOffset = Mathf.Sin(angle);

            var spawnPoint = cameraPos + new Vector3(xOffset, 0, zOffset) * spawnDistance;
            spawnPoint.y = GetGroundHeight(spawnPoint);

            return spawnPoint;
        }

        private float GetGroundHeight(Vector3 position)
        {
            return Physics.Raycast(new Vector3(position.x, MaxHeightRayCast, position.z), Vector3.down, out var hit,
                Mathf.Infinity,
                _groundLayer)
                ? hit.point.y
                : 0f;
        }

        private bool IsInsideNavMesh(Vector3 position) =>
            NavMesh.SamplePosition(position, out _, 1.0f, NavMesh.AllAreas);

        private float GetCameraViewRadius()
        {
            var screenCorner = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));
            return Vector3.Distance(_player.transform.position, screenCorner);
        }
    }
}