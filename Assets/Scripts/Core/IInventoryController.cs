using System;
using System.Collections.Generic;
using DL.Data.Resource;
using DL.InterfacesRuntime;

namespace DL.CoreRuntime
{
    public interface IInventoryController : IInitialize, IDeinitialize
    {
        public Action<ResourceDataModel> OnAddResource { get; set; }
        public Action<List<ResourceDataModel>> OnChangedResourcesData { get; set; }
        public Action<ResourceDataModel> OnRemoveResource { get; set; }
        public Action<ResourceDataModel> OnDropResource { get; set; }
        public Action OnDropAllResources { get; set; }

        void SetInfinityInventory(bool isInfinity);
        
        bool TryAddResource(ResourceDataModel resourceData);
        void RemoveResource(ResourceDataModel resourceData);
        
        void DropResource(ResourceDataModel resourceData);
        void DropAllResources();

        void AddResources(List<ResourceDataModel> resourcesData);
        List<ResourceDataModel> TakeResources();
    }
}