using Place.Models;
using Place.UpgradePlace;
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

        private UpgradePlaceController _placeController;
        
        public override void Initialize(params object[] objects)
        {
            _placeController = objects[0] as UpgradePlaceController;
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