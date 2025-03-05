using UnityEngine;

namespace DL.ManagersRuntime
{
    public static class GameStarter
    {
        private const string ManagersPath = "Managers";
        private const string DebugManagersPath = "DebugManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeManagers()
        {
            var managers = Object.FindObjectOfType<GameManager>();

            if (managers != null)
            {
                return;
            }

            var managersObj = Resources.Load(ManagersPath);
            Object.Instantiate(managersObj);
        }

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeDebugManager()
        {
            var manager = Object.FindObjectOfType<DebugManager>();

            if (manager != null)
            {
                return;
            }

            var managerObj = Resources.Load(DebugManagersPath);
            Object.Instantiate(managerObj);
        }
#endif
    }
}