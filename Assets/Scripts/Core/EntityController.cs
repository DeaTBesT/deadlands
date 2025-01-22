using System;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.CoreRuntime
{
    public abstract class EntityController : MonoBehaviour, IInitialize
    {
        public virtual bool IsEnable { get; set; } = true;
        
        public abstract void Initialize(params object[] objects);
        
        public abstract void ActivateEntity();

        public abstract void DiactivateEntity();
        
        public virtual void ActivateMoveEntity() => 
            throw new NotImplementedException();

        public virtual void DiactivateMoveEntity() => 
            throw new NotImplementedException();
    }
}