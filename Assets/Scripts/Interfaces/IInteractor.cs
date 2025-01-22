using UnityEngine;

namespace DL.InterfacesRuntime
{
    public interface IInteractor
    {
        Transform Interactor { get; }
        
        void OnInteract();
        void OnInteractUp();
        void OnEndInteract();
    }
}