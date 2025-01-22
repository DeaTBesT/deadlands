using DL.ConstructibleStructureRuntime.Core;
using DL.StructureRuntime.UIPanels.Core;
using UnityEngine;

namespace DL.ConstructibleStructureRuntime.MapStructure
{
    public class MapStructureControllerUI : ConstructibleStructureControllerUI
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
            _placePanels.Add(_constructiblePanel);
            _placePanels.Add(_openMapPanel);
            _placePanels.Add(_selectLevelPanel);
            
            ClosePanels();
        }
    }
}