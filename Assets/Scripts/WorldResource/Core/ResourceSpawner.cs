using DL.Data.Resource;
using UnityEngine;

namespace DL.WorldResourceRuntime.Core
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private ResourceDataModel _resourceDataModel;

        public void InstantiateResource() => 
            InstantiateResourceInternal();

        protected virtual void InstantiateResourceInternal() => 
            ResourceDataModel.InstantiateResource(_resourceDataModel, transform);
    }
}