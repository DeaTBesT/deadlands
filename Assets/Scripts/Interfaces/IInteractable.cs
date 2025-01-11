using System;
using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        public InteractType TypeInteract { get; }
        public Transform Interactable { get; }
        
        public Action OnInteract { get; set; }
        public Action OnFinishInteract { get; set; }
        
        bool TryInteract(IInteractor interactor, Action onFinishInteract = null);
        void StartHolding(IInteractor interactor);
        void StopHolding();
        void FinishInteract(IInteractor interactor);
        void ForceFinishInteract(IInteractor interactor);
    }
}