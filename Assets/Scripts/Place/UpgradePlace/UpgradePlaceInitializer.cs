using Place.Core;
using UnityEngine;

namespace Place.UpgradePlace
{
    public class UpgradePlaceInitializer : PlaceInitializer
    {
        [SerializeField] private UpgradePlaceController _placeController;
        [SerializeField] private UpgradePlaceControllerUI _placeControllerUI;
        [SerializeField] private InteractablePlace _interactablePlace;

        private void OnValidate()
        {
            if (_placeController == null)
            {
                _placeController = GetComponent<UpgradePlaceController>();
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
            _placeController.Initialize(_placeControllerUI);
            _placeControllerUI?.Initialize(_placeController);
            _interactablePlace?.Initialize(_placeController);
        }
    }
}