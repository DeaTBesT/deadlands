using System;
using System.Collections.Generic;
using System.Linq;
using DL.Data.Resource;
using DL.UtilsRuntime;
using UnityEngine;

namespace DL.ManagersRuntime
{
    public class GameResourcesManager : Singleton<GameResourcesManager>
    {
        private List<ResourceDataModel> _resourcesData = new();

        public Action<ResourceDataModel> OnAddResource { get; set; }
        public Action<ResourceDataModel> OnRemoveResource { get; set; }
        public Action<List<ResourceDataModel>> OnChangeResourcesData { get; set; }

        public void AddResource(ResourceConfig resourceConfig, int amount = 1)
        {
            var resourceData = _resourcesData.FirstOrDefault(x =>
                x.ResourceConfig.TypeResource == resourceConfig.TypeResource);

            if (resourceData != null)
            {
                resourceData.AddResource(amount);
                OnAddResource?.Invoke(resourceData);
            }
            else
            {
                _resourcesData.Add(new ResourceDataModel(resourceConfig, amount));
                OnChangeResourcesData?.Invoke(_resourcesData);
            }
        }

        public void RemoveResource(ResourceConfig resourceConfig, int amount = 1)
        {
            var resourceData = _resourcesData.FirstOrDefault(x =>
                x.ResourceConfig.TypeResource == resourceConfig.TypeResource);

            if (resourceData == null)
            {
#if UNITY_EDITOR
                Debug.LogError($"ResourceData with type {resourceConfig.TypeResource} isn't exist");
#endif
                return;
            }

            if (resourceData.AmountResource - amount > 0)
            {
                resourceData.RemoveResource(amount);
                OnRemoveResource?.Invoke(resourceData);
            }
            else
            {
                _resourcesData.Remove(resourceData);
                OnChangeResourcesData?.Invoke(_resourcesData);
            }
        }
    }
}