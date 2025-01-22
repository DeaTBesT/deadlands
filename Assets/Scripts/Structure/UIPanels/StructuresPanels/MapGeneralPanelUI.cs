using DL.StructureRuntime.UIPanels.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DL.StructureRuntime.UIPanels.StructuresPanels
{
    public class MapGeneralPanelUI : GeneralPanelUI
    {
        [SerializeField] private Button _buttonOpenMapPanel;
        [SerializeField] private Button _buttonOpenUgpradePanel;
        
        private SimpleStructurePanelUI _panelMap;
        private SimpleStructurePanelUI _panelUpgrade;

        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            _panelMap = objects[0] as SimpleStructurePanelUI;
            _panelUpgrade = objects[1] as SimpleStructurePanelUI;
            
            _buttonOpenMapPanel.onClick.AddListener(OpenMapPanel);
            _buttonOpenUgpradePanel.onClick.AddListener(OpenUpgradePanel);

            IsEnable = true;
        }

        private void OpenMapPanel()
        {
            _panelMap.Show();
            _panelUpgrade.Hide();
        }

        private void OpenUpgradePanel()
        {
            _panelUpgrade.Show();
            _panelMap.Hide();
        }
    }
}