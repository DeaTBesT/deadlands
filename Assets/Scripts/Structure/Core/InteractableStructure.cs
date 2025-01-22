using DL.CoreRuntime;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.StructureRuntime.Core
{
    [RequireComponent(typeof(Collider))]
    public sealed class InteractableStructure : MonoBehaviour, IInitialize
    {
        private const int PlayerTeam = 1;
        
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

            if (entityStats.TeamId != PlayerTeam)
            {
                return;
            }
            
            InteractPlace();            
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out EntityStats entityStats))
            {
                return;
            }

            if (entityStats.TeamId != PlayerTeam)
            {
                return;
            }

            FinishInteract();
        }

        private void InteractPlace() =>
            _placeController.Interact();

        private void FinishInteract() =>
            _placeController.FinishInteract();
    }
}