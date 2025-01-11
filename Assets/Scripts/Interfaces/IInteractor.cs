using UnityEngine;

namespace Interfaces
{
    public interface IInteractor
    {
        Transform Interactor { get; }
        
        void OnInteract();
        void OnInteractUp();
        void OnEndInteract();
    }
}