using Core;
using Enums;
using Interfaces;
using UnityEngine;

namespace Place.Core
{
    public class PlaceController : EntityController, IInitialize
    {
        [SerializeField] private PlaceState _currentState = PlaceState.Build;
        [SerializeField] private int _currentLevel;
        
        public PlaceState CurrentState => _currentState;
        public int CurrentLevel => _currentLevel;
        
        public override void Initialize(params object[] objects)
        {
            
        }

        public override void ActivateEntity()
        {
            throw new System.NotImplementedException();
        }

        public override void DiactivateEntity()
        {
            throw new System.NotImplementedException();
        }

        public bool BuildPlace()
        {
            _currentState = PlaceState.Upgrade;

            return true;
        }

        public bool UpgradePlace()
        {
            _currentLevel++;
            
            return true;
        }
    }
}