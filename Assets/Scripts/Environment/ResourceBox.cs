using GameResources.Core;
using Player;
using UnityEngine;

namespace Environment
{
    public class ResourceBox : MonoBehaviour
    {
        [SerializeField] private int _minDropItems, _maxDropItems;
        [SerializeField] private WeightedList<ResourceData> _droppableResources;

        public bool IsActive { get; private set; } = true;

        private void OnTriggerEnter(Collider other)
        {
            if ((!other.TryGetComponent(out PlayerStats _)) || (!IsActive))
            {
                return;
            }

            IsActive = false;
            DropAllItems();
        }

        private void DropAllItems()
        {
            var countItems = Random.Range(_minDropItems, _maxDropItems);

            for (var i = 0; i < countItems; i++) 
            {
                var resource = _droppableResources.GetRandomItem();
                DropItem(resource);
            }
        }

        private void DropItem(ResourceData resource) => 
            ResourceData.InstantiateResource(resource, transform.position + transform.forward);
    }
}