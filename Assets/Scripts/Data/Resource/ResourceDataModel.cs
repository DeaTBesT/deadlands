using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.Data.Resource
{
    [System.Serializable]
    public class ResourceDataModel
    {
        [SerializeField] private ResourceConfig _resourceConfig;
        [SerializeField] private int _amount;

        public ResourceConfig ResourceConfig => _resourceConfig;
        public int AmountResource => _amount;

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

        public static GameObject InstantiateResource(ResourceDataModel resourceDataModel, Transform spawnPosition)
        {
            if (resourceDataModel == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resource data isn't exist");
#endif
                return null;
            }
            
            var newResource = Object.Instantiate(resourceDataModel.ResourceConfig.ResourcePrefab, spawnPosition);
            newResource.transform.parent = null;
            newResource.transform.position = spawnPosition.position;
            newResource.transform.rotation = spawnPosition.rotation;

            if (newResource.TryGetComponent(out IWorldResource gameResource))
            {
                gameResource.SetAmount(resourceDataModel.AmountResource);
            }
            
            return newResource;
        }

        public static GameObject InstantiateResource(ResourceDataModel resourceDataModel, Vector3 spawnPosition, Quaternion spawnRotation = default)
        {
            if (resourceDataModel == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resource data isn't exist");
#endif
                return null;
            }

            var newResource = Object.Instantiate(resourceDataModel.ResourceConfig.ResourcePrefab, spawnPosition, spawnRotation);
            newResource.transform.position = spawnPosition;
            newResource.transform.rotation = spawnRotation;

            if (newResource.TryGetComponent(out IWorldResource gameResource))
            {
                gameResource.SetAmount(resourceDataModel.AmountResource);
            }

            return newResource;
        }
    }
}