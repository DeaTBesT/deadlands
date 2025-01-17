using System.Collections.Generic;
using GameResources.Core;
using Place.UpgradePlace;
using UI.PlacePanels.Core;
using UnityEngine.Events;
using Utils;

namespace UI.PlacePanels
{
    public class BuildPanelUI : AdvancedPlacePanelUI
    {
        private UpgradePlaceController _placeController;
        
        public override void Initialize(params object[] objects)
        {
            _placeController = objects[0] as UpgradePlaceController;
            _placeController.OnBuildStart += GenerateResourcesPanel;
        }
        
        public void AddOnClickEvent(UnityAction onClickAction) => 
            _button.onClick.AddListener(onClickAction);
        
        private void GenerateResourcesPanel(List<ResourceData> dataList)
        {
            ClearRequiredResourcesPanel();

            dataList.SortResources();

            foreach (var data in dataList)
            {
                var resourceDataUI = Instantiate(_requiredResourcesPrefab, _requiredResourcesParent);
                resourceDataUI.ChangeResourceData(data);
                _resourcesDataUI.Add(resourceDataUI);
            }
        }
    }
}