using System;
using DL.InterfacesRuntime;
using DL.RouletteSystemRuntime;
using UnityEngine;
using UnityEngine.UI;

namespace DL.StructureRuntime.UIPanels.StructuresPanels
{
    public class RouletteGeneralPanelUI : GeneralPanelUI, IButtonClick
    {
        [SerializeField] private Button _buttonOpenRouletteanel;
        
        public Action OnButtonClick { get; set; }
        
        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            _buttonOpenRouletteanel.onClick.AddListener(OpenRoulettePanel);

            RouletteManager.Instance.RegisterButtonClick(this);
            
            IsEnable = true;
        }

        private void OpenRoulettePanel() => 
            OnButtonClick?.Invoke();
    }
}