using Data.Core;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.Data.Resource
{
    [System.Serializable]
    public class ResourceDataModel : ItemModel
    {
        [SerializeField] private ResourceConfig _resourceConfig;
        [SerializeField] private int _amount;

        public ResourceConfig ResourceConfig => _resourceConfig;
        public int AmountResource => _amount;
        public override GameObject ItemPrefab => _resourceConfig.ResourcePrefab;

        public ResourceDataModel(ResourceConfig resourceConfig, int amount)
        {
            _resourceConfig = resourceConfig;
            _amount = amount;
        }

        public void SetAmount(int amount) =>
            _amount = amount;

        public void AddResource(int amount) =>
            _amount += amount;

        public void RemoveResource(int amount) =>
            _amount = Mathf.Clamp(_amount - amount, 0, int.MaxValue);
        
        public static GameObject InstantiateResource(ResourceDataModel itemModel, Transform spawnPosition)
        {
            var newItem = InstantiateItem(itemModel, spawnPosition);
            
            if (newItem.TryGetComponent(out IWorldResource gameResource))
            {
                gameResource.SetAmount(itemModel.AmountResource);
            }

            return newItem;
        }
        
        public static GameObject InstantiateResource(ResourceDataModel itemModel, Vector3 spawnPosition, Quaternion spawnRotation = default)
        {
            var newItem = InstantiateItem(itemModel, spawnPosition, spawnRotation);
            
            if (newItem.TryGetComponent(out IWorldResource gameResource))
            {
                gameResource.SetAmount(itemModel.AmountResource);
            }

            return newItem;
        }
    }
}