using DL.CoreRuntime;
using DL.RaidRuntime;
using UnityEngine;

namespace DL.ManagersRuntime
{
    public class ManagersInitializer : EntityInitializer
    {
        [SerializeField] private GameManager _gameManager;
        
        [Header("Raid")]
        [SerializeField] private RaidManager _raidManager;
        [SerializeField] private RaidControllerUI _raidControllerUI; 
            
        private void OnValidate()
        {
            if (_gameManager == null)
            {
                _gameManager = GetComponent<GameManager>();
            } 
            
            if (_raidManager == null)
            {
                _raidManager = GetComponent<RaidManager>();
            } 
            
            if (_raidControllerUI == null)
            {
                _raidControllerUI = GetComponent<RaidControllerUI>();
            }
        }
        
        public override void Initialize(params object[] objects)
        {
            _gameManager.Initialize(_raidManager);
            _raidManager.Initialize(_raidControllerUI);
        }

        public override void Deinitialize(params object[] objects) => 
            _raidManager.Deinitialize(_raidControllerUI);
    }
}