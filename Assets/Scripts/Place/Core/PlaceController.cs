using Core;

namespace Place.Core
{
    public abstract class PlaceController : EntityController
    {
        public abstract void Interact();
        public abstract void FinishInteract();
    }
}