using System.Collections.Generic;
using System.Linq;
using DL.Data.Resource;
using DL.InventorySystem.Core;
using UnityEngine;

namespace DL.PlayersRuntime
{
    public class PlayerInventoryController : InventoryController
    {
        private const int InventoryLimit = 20;

        public override bool TryAddResource(ResourceDataModel resource)
        {
            if ((!_isInfinityInventory) && (CountResources() + resource.AmountResource > InventoryLimit))
            {
                return false;
            }

            var resourceData = _resourcesData.FirstOrDefault(x =>
                x.ResourceConfig.TypeRare == resource.ResourceConfig.TypeRare);

            if (resourceData != null)
            {
                resourceData.AddResource(resource.AmountResource);
                OnAddResource?.Invoke(resource);
            }
            else
            {
                _resourcesData.Add(new ResourceDataModel(resource.ResourceConfig, resource.AmountResource));
                OnChangedResourcesData?.Invoke(_resourcesData);
            }

            return true;
        }

        public override void RemoveResource(ResourceDataModel resource)
        {
            var resourceData = _resourcesData.FirstOrDefault(x =>
                x.ResourceConfig.TypeRare == resource.ResourceConfig.TypeRare);

            if (resourceData == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resource data isn't exist");
#endif
                return;
            }

            if (resourceData.AmountResource - resource.AmountResource > 0)
            {
                resourceData.RemoveResource(resource.AmountResource);
                OnRemoveResource?.Invoke(resource);
            }
            else
            {
                _resourcesData.Remove(resourceData);
                OnChangedResourcesData?.Invoke(_resourcesData);
            }
        }

        public override void DropResource(ResourceDataModel resource)
        {
            var resourceData = _resourcesData.FirstOrDefault(x =>
                x.ResourceConfig.TypeRare == resource.ResourceConfig.TypeRare);

            if (resourceData == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resource data isn't exist");
#endif
                return;
            }
            
            ResourceDataModel.InstantiateResource(resource, transform);
            _resourcesData.Remove(resourceData);
            OnChangedResourcesData?.Invoke(_resourcesData);
        }

        public override void DropAllResources()
        {
            for (var i = _resourcesData.Count - 1; i >= 0; i--)
            {
                DropResource(_resourcesData[i]);
            }
        }

        public override void AddResources(List<ResourceDataModel> resourcesData)
        {
            resourcesData.ForEach(resource => TryAddResource(resource));
            OnChangedResourcesData?.Invoke(_resourcesData);
        }

        public override void RemoveResources(List<ResourceDataModel> resourcesData)
        {
            resourcesData.ForEach(RemoveResource);
            OnChangedResourcesData?.Invoke(_resourcesData);
        }

        public override List<ResourceDataModel> TakeResources()
        {
            var tookResourcesData = new List<ResourceDataModel>();
            tookResourcesData.AddRange(_resourcesData);

            _resourcesData.Clear();

            return tookResourcesData;
        }

        private int CountResources() =>
            _resourcesData.Sum(resource => resource.AmountResource);
    }
}