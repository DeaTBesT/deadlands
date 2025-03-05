using System;
using DL.InterfacesRuntime;
using DL.SelectingLevel;
using DL.StructureRuntime.UIPanels.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DL.StructureRuntime.UIPanels.StructuresPanels
{
    public class MapGeneralPanelUI : GeneralPanelUI, IButtonClick
    {
        [SerializeField] private Button _buttonOpenMapPanel;
        [SerializeField] private Button _buttonOpenUgpradePanel;
        
        private SimpleStructurePanelUI _panelUpgrade;

        public Action OnButtonClick { get; set; }
        
        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            _panelUpgrade = objects[0] as SimpleStructurePanelUI;
            
            _buttonOpenMapPanel.onClick.AddListener(OpenMapPanel);
            _buttonOpenUgpradePanel.onClick.AddListener(OpenUpgradePanel);

            SelectLevelManager.Instance.RegisterButtonClick(this);
            
            IsEnable = true;
        }

        private void OpenMapPanel() => 
            OnButtonClick?.Invoke();

        private void OpenUpgradePanel() => 
            _panelUpgrade.Show();
    }
}