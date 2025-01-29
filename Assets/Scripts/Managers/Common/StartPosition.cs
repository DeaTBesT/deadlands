using UnityEngine;

namespace DL.ManagersRuntime.Common
{
    [DisallowMultipleComponent]
    public class StartPosition : MonoBehaviour
    {
        private void Awake() => 
            GameManager.RegisterStartPosition(transform);

        private void OnDestroy() => 
            GameManager.UnRegisterStartPosition(transform);
    }
}