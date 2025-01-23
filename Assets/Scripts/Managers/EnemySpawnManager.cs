using System.Collections;
using DL.CoreRuntime;
using DL.UtilsRuntime;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace DL.ManagersRuntime
{
    public class EnemySpawnManager : Singleton<EnemySpawnManager>
    {
        [SerializeField] private float _spawnRadius = 10f;
        [SerializeField] private float _spawnInterval = 2f;

        [SerializeField] private EntityInitializer _enemyPrefab;
        [SerializeField] private Transform _player;

        [SerializeField] private LayerMask _groundLayer;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
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

            var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var xOffset = Mathf.Cos(angle);
            var zOffset = Mathf.Sin(angle);

            var spawnPoint = cameraPos + new Vector3(xOffset, 0, zOffset) * spawnDistance;
            spawnPoint.y = GetGroundHeight(spawnPoint);

            return spawnPoint;
        }

        private float GetGroundHeight(Vector3 position)
        {
            return Physics.Raycast(new Vector3(position.x, 100f, position.z), Vector3.down, out var hit, Mathf.Infinity,
                _groundLayer)
                ? hit.point.y
                : 0f;
        }

        private bool IsInsideNavMesh(Vector3 position)
        {
            return NavMesh.SamplePosition(position, out _, 1.0f, NavMesh.AllAreas);
        }

        private float GetCameraViewRadius()
        {
            var screenCorner = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));
            return Vector3.Distance(_player.transform.position, screenCorner);
        }
    }
}