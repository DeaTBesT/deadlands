using GameResources.Core;
using Player;
using UnityEngine;

namespace GameResources
{
    public class GameResource : MonoBehaviour
    {
        [SerializeField] private ResourceData _resourceData;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerInventoryController playerInventory))
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