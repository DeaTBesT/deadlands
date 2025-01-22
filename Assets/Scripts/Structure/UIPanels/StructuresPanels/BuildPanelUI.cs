using System.Collections.Generic;
using DL.Data.Resource;
using DL.StructureRuntime.UIPanels.Core;
using DL.UtilsRuntime;

namespace DL.StructureRuntime.UIPanels.StructuresPanels
{
    public class BuildPanelUI : AdvancedStructurePanelUI
    {
        public override void UpdatePanelView(params object[] objects)
        {
            var dataList = objects[0] as List<ResourceDataModel>;
            
            GenerateResourcesPanel(dataList);
        }

        private void GenerateResourcesPanel(List<ResourceDataModel> dataList)
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