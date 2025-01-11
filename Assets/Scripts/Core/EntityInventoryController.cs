using GameResources.Core;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class EntityInventoryController : MonoBehaviour, IInitialize, IDeinitialize
    {
        public bool IsEnable { get; set; }
        
        public virtual void Initialize(params object[] objects)
        {
            
        }

        public virtual void Deinitialize(params object[] objects)
        {
            throw new System.NotImplementedException();
        }
        
        public virtual void AddResource(ResourceData resourceData)
        {
            
        }

        public virtual void RemoveResource(ResourceData resourceData)
        {
            
        }
        
        public virtual void DropResource(ResourceData resourceData)
        {
            
        }

        public virtual void DropAllResources()
        {
            
        }
    }
}