using System;
using GameResources.Core;
using Interfaces;
using Player;
using UnityEngine;

namespace GameResources
{
    public class GameResource : MonoBehaviour, IInitialize
    {
        [SerializeField] private ResourceData _resourceData;
        [SerializeField] private Collider _collider;
        
        public bool IsEnable { get; set; }

        private void OnValidate()
        {
            if (_collider == null)
            {
                _collider = GetComponent<Collider>();

                if (_collider != null)
                {
                    _collider.enabled = false;
                }
            }
        }

        public void Initialize(params object[] objects)
        {
            IsEnable = true;
            _collider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((!other.TryGetComponent(out PlayerInventoryController playerInventory)) || (!IsEnable))
            {
                return;
            }
            
            playerInventory.AddResource(_resourceData);
            Destroy(gameObject);
        }
        
        public void SetAmount(int amount) =>
            _resourceData.SetAmount(amount);
    }
}