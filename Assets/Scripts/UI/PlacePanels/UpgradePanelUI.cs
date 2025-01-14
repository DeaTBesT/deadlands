using Place.Models;
using TMPro;
using UI.PlacePanels.Core;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace UI.PlacePanels
{
    public class UpgradePanelUI : AdvancedPlacePanelUI
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        
        public override void Initialize(params object[] objects)
        {
            base.Initialize(objects);

            _placeController.OnUpgrade += GenerateResourcesPanel;
        }

        public void AddOnClickEvent(UnityAction onClickAction) => 
            _button.onClick.AddListener(onClickAction);

        private void GenerateResourcesPanel(ResourcesUpgradeModel requiredResources)
        {
            ClearRequiredResourcesPanel();

            if (requiredResources == null)
            {
                return;
            }
            
            requiredResources.RequiredResources.SortResources();

            foreach (var data in requiredResources.RequiredResources)
            {
                var resourceDataUI = Instantiate(_requiredResourcesPrefab, _requiredResourcesParent);
                resourceDataUI.ChangeResourceData(data);
                _currentLevelText.text = $"{requiredResources.Level}";
                _resourcesDataUI.Add(resourceDataUI);
            }
        }
    }
}