using System;
using System.Collections.Generic;
using System.Linq;
using DL.CoreRuntime;
using DL.Data.Resource;
using DL.InterfacesRuntime;
using DL.UtilsRuntime;
using UnityEngine;

namespace DL.ManagersRuntime
{
    public class ResourcesManager : Singleton<ResourcesManager>, IInitialize
    {
        private IInventoryController _inventoryController;

        private List<ResourceDataModel> _resourcesData = new();

        public Action<ResourceDataModel> OnAddResource { get; set; }
        public Action<ResourceDataModel> OnRemoveResource { get; set; }
        public Action<List<ResourceDataModel>> OnChangeResourcesData { get; set; }

        public bool IsEnable { get; set; }

        public void Initialize(params object[] objects)
        {
            var player = objects[0] as Transform;
            if (player != null)
            {
                player.TryGetComponent(out _inventoryController);
            }
        }

        /// <summary>
        /// Заполняет инвентарь игрока на сейф уровнях(база)
        /// </summary>
        public void FillPlayerInventory()
        {
            _inventoryController.SetInfinityInventory(true);
            _inventoryController.AddResources(_resourcesData);
            _resourcesData.Clear();
        }
        
        public void StartRaid()
        {
            if (_inventoryController == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Inventory controller is null");
#endif
                return;
            }

            var tookResources = _inventoryController.TakeResources();
            AddResources(tookResources);
        }

        public void StopRaid()
        {
            if (_inventoryController == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Inventory controller is null");
#endif
                return;
            }

            var tookResources = _inventoryController.TakeResources();
            AddResources(tookResources);
        }
        
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

        public void AddResources(List<ResourceDataModel> resourcesData) =>
            resourcesData.ForEach(resource => AddResource(resource.ResourceConfig, resource.AmountResource));

        public void RemoveResources(List<ResourceDataModel> resourcesData) =>
            resourcesData.ForEach(resource => RemoveResource(resource.ResourceConfig, resource.AmountResource));
    }
}