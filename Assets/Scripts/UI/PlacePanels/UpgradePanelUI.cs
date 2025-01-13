using UI.PlacePanels.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.PlacePanels
{
    public class UpgradePanelUI : PlacePanelUI
    {
        [SerializeField] private GameObject _renderer;
        [SerializeField] private Button _upgradeButton;
        
        public override void Show() => 
            _renderer.SetActive(true);

        public override void Hide() => 
            _renderer.SetActive(false);

        public void AddOnClickEvent(UnityAction onClickAction) => 
            _upgradeButton.onClick.AddListener(onClickAction);
    }
}