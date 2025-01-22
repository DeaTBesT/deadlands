using System;
using System.Collections;
using DL.EnumsRuntime;
using DL.InterfacesRuntime;
using DL.UtilsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DL.SceneTransitionRuntime
{
    public class SceneLoader : Singleton<SceneLoader>, IInitialize
    {
        [SerializeField] private GameObject _canvasLoad;
        [SerializeField] private Slider _loadingBar;
        
        [SerializeField] private Canvas _canvasPrefab;
        
        private Coroutine _sceneLoadingRoutine;

        public static Action<SceneName> OnStartLoadScene { get; set; }
        public static Action OnLoadingScene { get; set; }
        public static Action<SceneName> OnFinishLoadScene { get; set; }

        public bool IsEnable { get; set; } = true;
        
        public void Initialize(params object[] objects)
        {
            if (_canvasLoad == null)
            {
                _canvasLoad = Instantiate(_canvasPrefab.gameObject);
                _loadingBar = _canvasLoad.GetComponentInChildren<Slider>();
            }

            _canvasLoad.SetActive(false);
        }

        public void LoadScene(SceneName newScene)
        {
            if (_sceneLoadingRoutine != null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Player transitioning already");
#endif
                return;
            }

#if UNITY_EDITOR
            Debug.Log($"Start transition new scene: {newScene}");
#endif
            _sceneLoadingRoutine = StartCoroutine(LoadSceneRoutine(newScene));
        }

        private IEnumerator LoadSceneRoutine(SceneName newScene)
        {
            _canvasLoad.SetActive(true);
            OnStartLoadScene?.Invoke(newScene);
            var operation = SceneManager.LoadSceneAsync(newScene.ToString());

            yield return new WaitUntil(() =>
            {
                _loadingBar.value = operation.progress;
                OnLoadingScene?.Invoke();
                return operation.isDone;
            });
            
            OnFinishLoadScene?.Invoke(newScene);
            _canvasLoad.SetActive(false);
        }
    }
}