using System;
using System.Collections.Generic;
using DL.CoreRuntime;
using DL.Data.Resource;
using UnityEngine;

namespace DL.InventorySystem.Core
{
    public abstract class InventoryController : MonoBehaviour, IInventoryController
    {
        protected bool _isInfinityInventory;

        protected List<ResourceDataModel> _resourcesData = new();

        public List<ResourceDataModel> ResourcesData => _resourcesData;

        public Action<ResourceDataModel> OnAddResource { get; set; }
        public Action<List<ResourceDataModel>> OnChangedResourcesData { get; set; }
        public Action<ResourceDataModel> OnRemoveResource { get; set; }
        public Action<ResourceDataModel> OnDropResource { get; set; }
        public Action OnDropAllResources { get; set; }

        public bool IsEnable { get; set; }

        public virtual void Initialize(params object[] objects)
        {
        }

        public virtual void Deinitialize(params object[] objects)
        {
            throw new System.NotImplementedException();
        }

        public void SetInfinityInventory(bool isInfinity) =>
            _isInfinityInventory = isInfinity;

        public abstract bool TryAddResource(ResourceDataModel resourceData);

        public abstract void RemoveResource(ResourceDataModel resourceData);

        public abstract void DropResource(ResourceDataModel resourceData);

        public abstract void DropAllResources();

        public virtual void AddResources(List<ResourceDataModel> resourcesData)
        {
        }

        public virtual List<ResourceDataModel> TakeResources()
        {
            return null;
        }
    }
}