using System.Collections.Generic;
using System.Linq;
using DL.CoreRuntime;
using DL.Data.Resource;
using DL.ManagersRuntime;
using UnityEngine;

namespace DL.PlayersRuntime
{
    public class PlayerInventoryController : EntityInventoryController
    {
        private GameResourcesManager _gameResourcesManager;

        private List<ResourceDataModel> _resourcesData = new();

        public List<ResourceDataModel> ResourcesData => _resourcesData;

        public override void Initialize(params object[] objects) =>
            _gameResourcesManager = objects[0] as GameResourcesManager;

        public override void Deinitialize(params object[] objects) =>
            DropAllResources();

        public override void AddResource(ResourceDataModel resource)
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
                _resourcesData.Add(new ResourceDataModel(resource.ResourceConfig, resource.AmountResource));
            }
        }
        
        public override void RemoveResource(ResourceDataModel resource)
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
        
        public override void DropResource(ResourceDataModel resource)
        {
            ResourceDataModel.InstantiateResource(resource, transform);

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