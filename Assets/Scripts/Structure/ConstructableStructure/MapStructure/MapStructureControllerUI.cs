using DL.ConstructableStructureRuntime.Core;
using DL.StructureRuntime.UIPanels.Core;
using UnityEngine;

namespace DL.ConstructableStructureRuntime.MapStructure
{
    public class MapStructureControllerUI : ConstructableStructureControllerUI
    {
        [SerializeField] private SimpleStructurePanelUI _constructiblePanel;
        [SerializeField] private SimpleStructurePanelUI _openMapPanel;
        [SerializeField] private SimpleStructurePanelUI _selectLevelPanel;

        public override void Initialize(params object[] objects)
        {
            base.Initialize(objects);
            
            _constructiblePanel.Initialize();
            _selectLevelPanel.Initialize();
            
            _openMapPanel.Initialize(_selectLevelPanel);
            _generalPanel.Initialize(_openMapPanel, _constructiblePanel);
            _structurePanels.Add(_constructiblePanel);
            _structurePanels.Add(_openMapPanel);
            _structurePanels.Add(_selectLevelPanel);
            
            _structurePanels.ClosePanels();
        }
    }
}