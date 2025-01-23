using DL.Data.Resource;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.CoreRuntime
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
        
        public virtual void AddResource(ResourceDataModel resourceData)
        {
            
        }

        public virtual void RemoveResource(ResourceDataModel resourceData)
        {
            
        }
        
        public virtual void DropResource(ResourceDataModel resourceData)
        {
            
        }

        public virtual void DropAllResources()
        {
            
        }
    }
}