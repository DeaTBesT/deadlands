using UnityEngine;

namespace DL.UtilsRuntime
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool _dontDestroyOnLoad;

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    return _instance;
                }

                _instance = FindObjectOfType<T>();

                if (_instance != null)
                {
                    return _instance;
                }

                var newInstance = new GameObject("GameManager");
                _instance = newInstance.AddComponent<T>();

                return _instance;
            }
        }

        private void Awake()
        {
            // if (_instance == null)
            // {
            //     _instance = this as T;
            // }
            // else if (_instance != null)
            // {
            //     Destroy(gameObject);
            // }

            InitializeSingleton();
        }

        private void InitializeSingleton()
        {
            if (_dontDestroyOnLoad)
            {
                if (_instance != null)
                {
                    Destroy(gameObject);
                    return;
                }

                _instance = this as T;

                if (!Application.isPlaying)
                {
                    return;
                }

                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                _instance = this as T;
            }
        }
    }
}