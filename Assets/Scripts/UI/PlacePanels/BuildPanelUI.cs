using UI.PlacePanels.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.PlacePanels
{
    public class BuildPanelUI : PlacePanelUI
    {
        [SerializeField] private GameObject _renderer;
        [SerializeField] private Button _buildButton;

        public override void Show() => 
            _renderer.SetActive(true);

        public override void Hide() => 
            _renderer.SetActive(false);
        
        public void AddOnClickEvent(UnityAction onClickAction) => 
            _buildButton.onClick.AddListener(onClickAction);
    }
}