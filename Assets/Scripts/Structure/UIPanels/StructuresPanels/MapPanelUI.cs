using DL.StructureRuntime.UIPanels.Core;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DL.StructureRuntime.UIPanels.StructuresPanels
{
    public class MapPanelUI : SimpleStructurePanelUI
    {
        [SerializeField] private Button _buttonOpenMap;

        private IPanelUI _structurePanel;
        
        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            _structurePanel = objects[0] as IPanelUI;
            _buttonOpenMap.onClick.AddListener(OnClickButtonOpenMap);
            
            IsEnable = true;
        }

        private void OnClickButtonOpenMap() => 
            _structurePanel.Show();
    }
}