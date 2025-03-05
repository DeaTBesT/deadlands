using DL.StructureRuntime.Core;
using UnityEngine;

namespace DL.CommonStructure.SimpleStructureRuntime
{
    public class SimpleStructureInitializer : StructureInitializer
    {
        [SerializeField] private SimpleStructureController _structureController;
        [SerializeField] private SimpleStructureControllerUI _placeControllerUI;

        private void OnValidate()
        {
            _structureController ??= GetComponent<SimpleStructureController>();
            _placeControllerUI ??= GetComponent<SimpleStructureControllerUI>();
            _interactableStructure ??= GetComponent<InteractableStructure>();
        }
        
        public override void Initialize(params object[] objects)
        {
            _interactableStructure?.Initialize(_structureController);
            _placeControllerUI?.Initialize();
            _structureController.Initialize(_placeControllerUI);
        }
    }
}