using DL.CoreRuntime;
using DL.PrefabsPoolingRuntime;
using DL.RaidRuntime;
using DL.SelectingLevel;
using DL.WardrobeRuntime;
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

        [Header("Wardrobe")] [SerializeField] private WardrobeManager _wardrobeManager;
        [SerializeField] private WardrobeControllerUI _wardrobeControllerUI;

        [Header("SelectLevel")] [SerializeField] private SelectLevelManager _selectLevelManager;
        [SerializeField] private SelectLevelControllerUI _selectLevelControllerUI;

        private void OnValidate()
        {
            _gameManager ??= GetComponent<GameManager>();
            _resourceManager ??= GetComponent<ResourcesManager>();
            _prefabPoolManager ??= GetComponent<PrefabPoolManager>();
            _raidManager ??= GetComponent<RaidManager>();
            _raidControllerUI ??= GetComponent<RaidControllerUI>();
            _wardrobeManager ??= GetComponent<WardrobeManager>();
            _wardrobeControllerUI ??= GetComponent<WardrobeControllerUI>();
            _selectLevelManager ??= GetComponent<SelectLevelManager>();
            _selectLevelControllerUI ??= GetComponent<SelectLevelControllerUI>();
        }

        public override void Initialize(params object[] objects)
        {
            _gameManager.Initialize(_raidManager, _resourceManager, _prefabPoolManager, _wardrobeManager);
            _raidManager.Initialize(_raidControllerUI);
            _wardrobeManager.Initialize(_wardrobeControllerUI);
            _selectLevelManager.Initialize(_selectLevelControllerUI);
        }

        public override void Deinitialize(params object[] objects)
        {
            _raidManager.Deinitialize();
            _wardrobeManager.Deinitialize();
            _selectLevelManager.Deinitialize();
        }
    }
}