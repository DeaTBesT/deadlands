using Interfaces;
using Player;
using UnityEngine;

namespace Place.Core
{
    [RequireComponent(typeof(Collider))]
    public class InteractablePlace : MonoBehaviour, IInitialize
    {
        private PlaceController _placeController;
        
        public bool IsEnable { get; set; }
        
        public void Initialize(params object[] objects) => 
            _placeController = objects[0] as PlaceController;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerInitializer _))
            {
                return;
            }
            
            InteractPlace();            
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out PlayerInitializer _))
            {
                return;
            }

            FinishInteract();
        }

        protected virtual void InteractPlace() =>
            _placeController.Interact();

        protected virtual void FinishInteract() =>
            _placeController.FinishInteract();
    }
}