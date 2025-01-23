using System;
using System.Collections.Generic;
using System.Linq;
using DL.CoreRuntime;
using DL.EnumsRuntime;
using DL.SceneTransitionRuntime;
using DL.UtilsRuntime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DL.ManagersRuntime
{
    public sealed class GameManager : Singleton<GameManager>
    {
        [SerializeField] private EntityInitializer _playerPrefab;
        [SerializeField] private SceneLoader _sceneLoaderPrefab;

        private SceneLoader _sceneLoader;
        
        private static List<Transform> _startPositions = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ResetStatics()
        {
            // reset all statics
            _startPositions.Clear();
        }

        private void Start()
        {
            InitializeSceneLoader();
            SpawnPlayer();
        }

        private void OnDestroy()
        {
            SceneLoader.OnFinishLoadScene -= OnChangedScene;
        }

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

        public Transform GetStartPosition()
        {
            _startPositions.RemoveAll(t => t == null);

            return _startPositions.Count == 0
                ? null
                : _startPositions[Random.Range(0, _startPositions.Count)];
        }

        private void SpawnPlayer()
        {
            if (_playerPrefab == null)
            {
                Debug.LogError("Player prefab is null");
                return;
            }
            
            var startPos = GetStartPosition();
            var player = startPos != null
                ? Instantiate(_playerPrefab.gameObject, startPos.position, startPos.rotation)
                : Instantiate(_playerPrefab.gameObject, transform.position, transform.rotation);
        }
        
        private void OnChangedScene(SceneName sceneName) => 
            SpawnPlayer();
    }
}