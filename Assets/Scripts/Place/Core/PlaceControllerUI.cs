using System;
using Interfaces;
using UnityEngine;

namespace Place.Core
{
    public abstract class PlaceControllerUI : MonoBehaviour, IInitialize
    {
        public bool IsEnable { get; set; } = true;
        
        public virtual void Initialize(params object[] objects)
        {
            throw new NotImplementedException();
        }
    }
}