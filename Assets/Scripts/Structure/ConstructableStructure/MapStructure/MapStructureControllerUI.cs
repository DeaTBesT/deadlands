using DL.ConstructableStructureRuntime.Core;
using DL.StructureRuntime.UIPanels.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace DL.ConstructableStructureRuntime.MapStructure
{
    public class MapStructureControllerUI : ConstructableStructureControllerUI
    {
        [FormerlySerializedAs("_constructiblePanel")] [SerializeField] private SimpleStructurePanelUI _constructablePanel;

        public override void Initialize(params object[] objects)
        {
            base.Initialize(objects);
            
            _constructablePanel.Initialize();
            
            _generalPanel.Initialize(_constructablePanel);
            _structurePanels.Add(_constructablePanel);
            
            _structurePanels.ClosePanels();
        }
    }
}