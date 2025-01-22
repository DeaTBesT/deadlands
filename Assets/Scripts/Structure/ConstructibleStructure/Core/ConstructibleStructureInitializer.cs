using DL.StructureRuntime.Core;
using UnityEngine;

namespace DL.ConstructibleStructureRuntime.Core
{
    public class ConstructibleStructureInitializer : StructureInitializer
    {
        [SerializeField] private ConstructibleStructureController _structureController;
        [SerializeField] private ConstructibleStructureControllerUI _placeControllerUI;

        private void OnValidate()
        {
            if (_structureController == null)
            {
                _structureController = GetComponent<ConstructibleStructureController>();
            } 
            
            if (_placeControllerUI == null)
            {
                _placeControllerUI = GetComponent<ConstructibleStructureControllerUI>();
            }
            
            if (_interactableStructure == null)
            {
                _interactableStructure = GetComponent<InteractableStructure>();
            }
        }

        public override void Initialize(params object[] objects)
        {
            _placeControllerUI?.Initialize(_structureController);
            _interactableStructure?.Initialize(_structureController);
            _structureController.Initialize(_placeControllerUI);
        }
    }
}