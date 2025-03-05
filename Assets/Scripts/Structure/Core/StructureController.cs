using DL.CoreRuntime;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.StructureRuntime.Core
{
    public abstract class StructureController : EntityController, IInteractable
    {
        public abstract bool TryInteract(Transform interactor);
        public abstract void FinishInteract();
    }
}