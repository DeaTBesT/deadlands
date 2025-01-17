using Interfaces;
using Place.UpgradePlace;
using Player;
using UnityEngine;

namespace Place.Core
{
    [RequireComponent(typeof(Collider))]
    public class InteractablePlace : MonoBehaviour, IInitialize
    {
        private UpgradePlaceControllerUI _placeControllerUI;
        
        public bool IsEnable { get; set; }
        
        public void Initialize(params object[] objects) => 
            _placeControllerUI = objects[0] as UpgradePlaceControllerUI;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerStats _))
            {
                return;
            }
            
            InteractPlace();            
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out PlayerStats _))
            {
                return;
            }

            FinishInteract();
        }

        protected virtual void InteractPlace() =>
            _placeControllerUI.OpenPanel();

        protected virtual void FinishInteract() => 
            _placeControllerUI.ClosePanels();
    }
}