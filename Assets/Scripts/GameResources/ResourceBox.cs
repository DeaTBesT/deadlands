using System.Collections;
using Animations;
using GameResources.Core;
using Player;
using UnityEngine;

namespace GameResources
{
    public class ResourceBox : MonoBehaviour
    {
        private const float DropDelay = 0.3f;
        private const float DropItemRadius = 1.5f;
        private const float DropItemHeight = 1.5f;
        private const float DropItemDuration = 1f;
        
        [SerializeField] private int _minDropItems, _maxDropItems;
        [SerializeField] private WeightedList<ResourceData> _droppableItems;

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

        private void DropAllItems() => 
            StartCoroutine(DropAllItemsRoutine());

        private IEnumerator DropAllItemsRoutine()
        {
            var countItems = Random.Range(_minDropItems, _maxDropItems);
            
            for (var i = 0; i < countItems; i++) 
            {
                var resource = _droppableItems.GetRandomItem();
                DropItem(resource);

                yield return new WaitForSeconds(DropDelay);
            }
        }

        private void DropItem(ResourceData resource)
        {
            var item = ResourceData.InstantiateResource(resource, transform.position + transform.forward);
            item.AnimateItemDrop(transform.position, DropItemRadius, DropItemHeight, DropItemDuration, () =>
            {
                ActivateItemCollider(item);
            });
        }

        private void ActivateItemCollider(GameObject item)
        {
            if (!item.TryGetComponent(out GameResource gameResource))
            {
                return;
            }
            
            gameResource.Initialize();
        }
    }
}