using System;
using System.Collections.Generic;
using System.Linq;
using DL.CoreRuntime;
using DL.Data.Scene;
using DL.EnumsRuntime;
using DL.InterfacesRuntime;
using DL.RaidRuntime;
using DL.SceneTransitionRuntime;
using DL.UtilsRuntime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DL.ManagersRuntime
{
    public sealed class GameManager : Singleton<GameManager>, IInitialize
    {
        [Header("Prefabs")] 
        [SerializeField] private bool _spawnPlayerOnStart = true;
        [SerializeField] private EntityInitializer _playerPrefab;

        [Space, SerializeField] private SceneLoader _sceneLoaderPrefab;

        private RaidManager _raidManager;

        private SceneLoader _sceneLoader;
        private Camera _camera;

        private GameObject _currentPlayer;
        
        private static List<Transform> _startPositions = new();

        public bool IsEnable { get; set; } = true;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ResetStatics()
        {
            // reset all statics
            _startPositions.Clear();
        }

        public void Initialize(params object[] objects)
        {
            _raidManager = objects[0] as RaidManager;

            InitializeCamera();
            InitializeSceneLoader();
            SpawnPlayer();
        }

        private void OnDestroy() =>
            SceneLoader.OnFinishLoadScene -= OnChangedScene;

        private void InitializeCamera() =>
            _camera = Camera.main;

        private void InitializeSceneLoader()
        {
            var sceneLoaderObj = Instantiate(_sceneLoaderPrefab, transform);
            sceneLoaderObj.TryGetComponent(out _sceneLoader);
            _sceneLoader.Initialize();
            SceneLoader.OnFinishLoadScene += OnChangedScene;
        }

        public static void RegisterStartPosition(Transform start)
        {
            _startPositions.Add(start);

            _startPositions = _startPositions.OrderBy(transform => transform.GetSiblingIndex()).ToList();
        }

        public static void UnRegisterStartPosition(Transform start) =>
            _startPositions.Remove(start);

        private Transform GetStartPosition()
        {
            _startPositions.RemoveAll(t => t == null);

            return _startPositions.Count == 0
                ? null
                : _startPositions[Random.Range(0, _startPositions.Count)];
        }

        private GameObject SpawnPlayer(bool isInitialize = true)
        {
            if (!_spawnPlayerOnStart)
            {
                return null;
            }

            if (_playerPrefab == null)
            {
                Debug.LogError("Player prefab is null");
                return null;
            }

            var startPos = GetStartPosition();
            var player = startPos != null
                ? Instantiate(_playerPrefab.gameObject, startPos.position, startPos.rotation)
                : Instantiate(_playerPrefab.gameObject, transform.position, transform.rotation);

            if (isInitialize)
            {
                InitializePlayer(player.transform);
            }

            return player;
        }

        private void InitializePlayer(Transform player)
        {
            if (!player.TryGetComponent(out EntityInitializer entityInitializer))
            {
                return;
            }
            
            entityInitializer.Initialize(_prefabPoolManager);
        }
        
        private void OnStartLoadScene(SceneConfig sceneConfig)
        {
            switch (sceneConfig.TypeScene)
            {
                case SceneType.Lobby:
                case SceneType.SafeZone:
                    _raidManager.StopRaid();
                    _resourcesManager.StopRaid();
                    break;
                case SceneType.Raid:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnChangedScene(SceneConfig sceneConfig)
        {
            InitializeCamera();

            _currentPlayer = SpawnPlayer();
            switch (sceneConfig.TypeScene)
            {
                case SceneType.Lobby:
                    _raidManager.StopRaid();
                    break;
                case SceneType.SafeZone:
                    _raidManager.StopRaid();
                    break;
                case SceneType.Raid:
                {
                    _raidManager.StartRaid(player.transform, _camera);
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}