using Data;
using DL.CoreRuntime;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.StructureRuntime.Core
{
    [RequireComponent(typeof(Collider))]
    public sealed class InteractableStructure : MonoBehaviour, IInitialize
    {
        private IInteractable _placeController;
        
        public bool IsEnable { get; set; }
        
        public void Initialize(params object[] objects) => 
            _placeController = objects[0] as IInteractable;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out EntityStats entityStats))
            {
                return;
            }

            if (entityStats.TeamId != Teams.PlayerTeamId)
            {
                return;
            }
            
            if (!other.TryGetComponent(out IInventoryController inventoryController))
            {
                return;
            }
            
            InteractPlace(other.transform);            
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out EntityStats entityStats))
            {
                return;
            }

            if (entityStats.TeamId != Teams.PlayerTeamId)
            {
                return;
            }
            
            FinishInteract();
        }

        private void InteractPlace(Transform interactor) => 
            _placeController.TryInteract(interactor);

        private void FinishInteract() =>
            _placeController.FinishInteract();
    }
}