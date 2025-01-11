using UnityEngine;

namespace GameResources.Core
{
    [System.Serializable]
    public class ResourceData
    {
        [SerializeField] private ResourceConfig _resourceConfig;
        [SerializeField] private int _amount;

        public ResourceConfig ResourceConfig => _resourceConfig;
        public int AmountResource => _amount;

        public ResourceData(ResourceConfig resourceConfig, int amount)
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

        public static void InstantiateResource(ResourceData resourceData, Transform spawnPosition)
        {
            if (resourceData == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resource data isn't exist");
#endif
                return;
            }
            
            var newResource = Object.Instantiate(resourceData.ResourceConfig.ResourcePrefab, spawnPosition);
            newResource.transform.parent = null;
            newResource.transform.position = spawnPosition.position;
            newResource.transform.rotation = spawnPosition.rotation;

            if (newResource.TryGetComponent(out GameResource gameResource))
            {
                gameResource.SetAmount(resourceData.AmountResource);
            }
        }
    }
}