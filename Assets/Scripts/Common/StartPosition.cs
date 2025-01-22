using DL.ManagersRuntime;
using UnityEngine;

namespace DL.CommonRuntime
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