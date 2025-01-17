using Place.Core;
using UnityEngine;

namespace Place.UpgradePlace
{
    public class UpgradePlaceInitializer : PlaceInitializer
    {
        [SerializeField] private UpgradePlaceController upgradePlaceController;
        [SerializeField] private UpgradePlaceControllerUI _placeControllerUI;
        [SerializeField] private InteractablePlace _interactablePlace;

        private void OnValidate()
        {
            if (upgradePlaceController == null)
            {
                upgradePlaceController = GetComponent<UpgradePlaceController>();
            } 
            
            if (_placeControllerUI == null)
            {
                _placeControllerUI = GetComponent<UpgradePlaceControllerUI>();
            }
            
            if (_interactablePlace == null)
            {
                _interactablePlace = GetComponent<InteractablePlace>();
            }
        }

        public override void Initialize(params object[] objects)
        {
            _placeControllerUI?.Initialize(upgradePlaceController);
            _interactablePlace?.Initialize(_placeControllerUI);
            upgradePlaceController.Initialize();
        }
    }
}