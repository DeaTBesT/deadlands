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
            _structureController ??= GetComponent<ConstructibleStructureController>();
            _placeControllerUI ??= GetComponent<ConstructibleStructureControllerUI>();
            _interactableStructure ??= GetComponent<InteractableStructure>();
        }

        public override void Initialize(params object[] objects)
        {
            _placeControllerUI?.Initialize(_structureController);
            _interactableStructure?.Initialize(_structureController);
            _structureController.Initialize(_placeControllerUI);
        }
    }
}