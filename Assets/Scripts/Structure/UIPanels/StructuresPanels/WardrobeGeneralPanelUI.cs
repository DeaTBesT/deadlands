using System;
using DL.InterfacesRuntime;
using DL.WardrobeRuntime;
using UnityEngine;
using UnityEngine.UI;

namespace DL.StructureRuntime.UIPanels.StructuresPanels
{
    public class WardrobeGeneralPanelUI : GeneralPanelUI, IButtonClick
    {
        [SerializeField] private Button _buttonOpenWardrobePanel;
        
        public Action OnButtonClick { get; set; }
        
        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            _buttonOpenWardrobePanel.onClick.AddListener(OpenWardrobePanel);

            WardrobeManager.Instance.RegisterButtonClick(this);
            
            IsEnable = true;
        }

        private void OpenWardrobePanel() => 
            OnButtonClick?.Invoke();
    }
}