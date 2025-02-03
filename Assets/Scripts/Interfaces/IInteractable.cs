using UnityEngine;

namespace DL.InterfacesRuntime
{
    public interface IInteractable
    {
        bool TryInteract(Transform interactor);
        void FinishInteract();
    }
}