using DL.StructureRuntime.Model;
using DL.StructureRuntime.UIPanels.Core;
using DL.UtilsRuntime;
using TMPro;
using UnityEngine;

namespace DL.StructureRuntime.UIPanels.StructuresPanels
{
    public class UpgradePanelUI : AdvancedStructurePanelUI
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;

        public override void UpdatePanelView(params object[] objects)
        {
            var requiredRequiredResources = objects[0] as RequiredResourcesModel;
            
            GenerateResourcesPanel(requiredRequiredResources);
        }

        private void GenerateResourcesPanel(RequiredResourcesModel requiredRequiredResources)
        {
            ClearRequiredResourcesPanel();

            if (requiredRequiredResources == null)
            {
                return;
            }
            
            requiredRequiredResources.RequiredResources.SortResources();

            foreach (var data in requiredRequiredResources.RequiredResources)
            {
                var resourceDataUI = Instantiate(_requiredResourcesPrefab, _requiredResourcesParent);
                resourceDataUI.ChangeResourceData(data);
                _currentLevelText.text = $"{requiredRequiredResources.Level}";
                _resourcesDataUI.Add(resourceDataUI);
            }
        }
    }
}