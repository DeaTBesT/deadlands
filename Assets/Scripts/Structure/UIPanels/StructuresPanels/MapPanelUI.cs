using DL.StructureRuntime.UIPanels.Core;
using DL.StructureRuntime.UIPanels.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace DL.StructureRuntime.UIPanels.StructuresPanels
{
    public class MapPanelUI : SimpleStructurePanelUI
    {
        [SerializeField] private Button _buttonOpenMap;

        private IStructurePanel _structurePanel;
        
        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            _structurePanel = objects[0] as IStructurePanel;
            _buttonOpenMap.onClick.AddListener(OnClickButtonOpenMap);
            
            IsEnable = true;
        }

        private void OnClickButtonOpenMap() => 
            _structurePanel.Show();
    }
}