using System.Collections.Generic;
using System.Linq;
using Core;
using GameResources.Core;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerInventoryController : EntityInventoryController
    {
        private GameResourcesManager _gameResourcesManager;

        private List<ResourceData> _resourcesData = new();

        public List<ResourceData> ResourcesData => _resourcesData;

        public override void Initialize(params object[] objects) =>
            _gameResourcesManager = objects[0] as GameResourcesManager;

        public override void Deinitialize(params object[] objects) =>
            DropAllResources();

        public override void AddResource(ResourceData resource)
        {
            _gameResourcesManager.AddResource(resource.ResourceConfig, resource.AmountResource);

            var resourceData = _resourcesData.FirstOrDefault(x =>
                x.ResourceConfig.TypeResource == resource.ResourceConfig.TypeResource);

            if (resourceData != null)
            {
                resourceData.AddResource(resource.AmountResource);
            }
            else
            {
                _resourcesData.Add(new ResourceData(resource.ResourceConfig, resource.AmountResource));
            }
        }
        
        public override void RemoveResource(ResourceData resource)
        {
            _gameResourcesManager.RemoveResource(resource.ResourceConfig, resource.AmountResource);

            var resourceData = _resourcesData.FirstOrDefault(x =>
                x.ResourceConfig.TypeResource == resource.ResourceConfig.TypeResource);
            
            if (resourceData.AmountResource - resource.AmountResource > 0)
            {
                resourceData.RemoveResource(resource.AmountResource);
            }
            else
            {
                _resourcesData.Remove(resourceData);
            }
        }
        
        public override void DropResource(ResourceData resource)
        {
            ResourceData.InstantiateResource(resource, transform);

            _gameResourcesManager.RemoveResource(resource.ResourceConfig, resource.AmountResource);

            var resourceData = _resourcesData.FirstOrDefault(x =>
                x.ResourceConfig.TypeResource == resource.ResourceConfig.TypeResource);

            if (resourceData == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resource data isn't exist");
#endif
                return;
            }

            _resourcesData.Remove(resourceData);
        }

        public override void DropAllResources()
        {
            for (var i = _resourcesData.Count - 1; i >= 0; i--)
            {
                DropResource(_resourcesData[i]);
            }
        }
    }
}