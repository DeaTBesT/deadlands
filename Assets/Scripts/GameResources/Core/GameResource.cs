using System;
using Enums;
using GameResources.Core;
using Interfaces;
using Player;
using UnityEngine;

namespace GameResources
{
    public class GameResource : MonoBehaviour, IInteractable
    {
        [SerializeField] private ResourceData _resourceData;

        public InteractType TypeInteract => InteractType.OneTime;
        public Transform Interactable => transform;

        public Action OnInteract { get; set; }
        public Action OnFinishInteract { get; set; }

        public bool TryInteract(IInteractor interactor, Action onFinishInteract = null)
        {
            if (interactor.Interactor.TryGetComponent(out PlayerInventoryController playerInventory))
            {
                playerInventory.AddResource(_resourceData);
                DestorySelf();
                return true;
            }

            return false;
        }

        public void StartHolding(IInteractor interactor)
        {
            throw new NotImplementedException();
        }

        public void StopHolding()
        {
            throw new NotImplementedException();
        }

        public void FinishInteract(IInteractor interactor)
        {
            throw new NotImplementedException();
        }

        public void ForceFinishInteract(IInteractor interactor)
        {
            throw new NotImplementedException();
        }

        private void DestorySelf() =>
            Destroy(gameObject);

        public void SetAmount(int amount) =>
            _resourceData.SetAmount(amount);
    }
}