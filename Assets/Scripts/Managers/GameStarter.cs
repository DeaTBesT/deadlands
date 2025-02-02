using UnityEngine;

namespace DL.ManagersRuntime
{
    public static class GameStarter
    {
        private const string ManagersPath = "Managers";

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
    }
}