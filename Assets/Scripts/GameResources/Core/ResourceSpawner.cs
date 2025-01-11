using UnityEngine;

namespace GameResources.Core
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private ResourceData _resourceData;

        public void InstantiateResource() => 
            InstantiateResourceInternal();

        protected virtual void InstantiateResourceInternal() => 
            ResourceData.InstantiateResource(_resourceData, transform);
    }
}