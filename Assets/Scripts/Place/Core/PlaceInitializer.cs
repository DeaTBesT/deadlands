using Core;
using UnityEngine;

namespace Place.Core
{
    public class PlaceInitializer : EntityInitializer
    {
        [SerializeField] private PlaceController _placeController;
        [SerializeField] private PlaceControllerUI _placeControllerUI;
        [SerializeField] private InteractablePlace _interactablePlace;

        private void OnValidate()
        {
            if (_placeController == null)
            {
                _placeController = GetComponent<PlaceController>();
            } 
            
            if (_placeControllerUI == null)
            {
                _placeControllerUI = GetComponent<PlaceControllerUI>();
            }
            
            if (_interactablePlace == null)
            {
                _interactablePlace = GetComponent<InteractablePlace>();
            }
        }

        public override void Initialize(params object[] objects)
        {
            _placeController.Initialize();
            _placeControllerUI?.Initialize(_placeController);
            _interactablePlace?.Initialize(_placeControllerUI);
        }
    }
}