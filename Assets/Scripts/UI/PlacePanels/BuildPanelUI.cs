using System.Collections.Generic;
using GameResources.Core;
using UI.PlacePanels.Core;
using UnityEngine.Events;
using Utils;

namespace UI.PlacePanels
{
    public class BuildPanelUI : AdvancedPlacePanelUI
    {
        public override void Initialize(params object[] objects)
        {
            base.Initialize(objects);

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