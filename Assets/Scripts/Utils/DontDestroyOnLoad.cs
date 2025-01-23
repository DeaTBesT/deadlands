using UnityEngine;

namespace DL.UtilsRuntime
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Start() => 
            DontDestroyOnLoad(gameObject);
    }
}