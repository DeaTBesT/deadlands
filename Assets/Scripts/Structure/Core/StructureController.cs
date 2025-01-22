using DL.CoreRuntime;
using DL.InterfacesRuntime;

namespace DL.StructureRuntime.Core
{
    public abstract class StructureController : EntityController, IInteractable
    {
        public abstract void Interact();
        public abstract void FinishInteract();
    }
}