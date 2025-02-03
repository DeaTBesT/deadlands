using System;
using System.Collections.Generic;
using DL.Data.Resource;
using DL.InterfacesRuntime;

namespace DL.CoreRuntime
{
    public interface IInventoryController : IInitialize, IDeinitialize
    {
        List<ResourceDataModel> ResourcesData { get; }

        Action<ResourceDataModel> OnAddResource { get; set; }
        Action<List<ResourceDataModel>> OnChangedResourcesData { get; set; }
        Action<ResourceDataModel> OnRemoveResource { get; set; }
        Action<ResourceDataModel> OnDropResource { get; set; }
        Action OnDropAllResources { get; set; }

        void SetInfinityInventory(bool isInfinity);

        bool TryAddResource(ResourceDataModel resourceData);
        void RemoveResource(ResourceDataModel resourceData);

        void DropResource(ResourceDataModel resourceData);
        void DropAllResources();

        void AddResources(List<ResourceDataModel> resourcesData);
        List<ResourceDataModel> TakeResources();
    }
}