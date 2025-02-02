using DL.CoreRuntime;
using DL.RaidRuntime;
using UnityEngine;

namespace DL.ManagersRuntime
{
    public class ManagersInitializer : EntityInitializer
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private ResourcesManager _resourceManager;
        [SerializeField] private PrefabPoolManager _prefabPoolManager;

        [Header("Raid")] [SerializeField] private RaidManager _raidManager;
        [SerializeField] private RaidControllerUI _raidControllerUI;

        private void OnValidate()
        {
            _gameManager ??= GetComponent<GameManager>();
            _resourceManager ??= GetComponent<ResourcesManager>();
            _prefabPoolManager ??= GetComponent<PrefabPoolManager>();
            _raidManager ??= GetComponent<RaidManager>();
            _raidControllerUI ??= GetComponent<RaidControllerUI>();
        }

        public override void Initialize(params object[] objects)
        {
            _gameManager.Initialize(_raidManager, _resourceManager, _prefabPoolManager);
            _raidManager.Initialize(_raidControllerUI);
        }

        public override void Deinitialize(params object[] objects) => 
            _raidManager.Deinitialize();
    }
}