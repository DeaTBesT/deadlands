using DL.StructureRuntime.Core;
using UnityEngine;

namespace DL.ConstructableStructureRuntime.Core
{
    public class ConstructableStructureInitializer : StructureInitializer
    {
        [SerializeField] private ConstructableStructureController _structureController;
        [SerializeField] private ConstructableStructureControllerUI _placeControllerUI;

        private void OnValidate()
        {
            _structureController ??= GetComponent<ConstructableStructureController>();
            _placeControllerUI ??= GetComponent<ConstructableStructureControllerUI>();
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