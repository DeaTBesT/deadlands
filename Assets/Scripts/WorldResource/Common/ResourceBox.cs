using System.Collections;
using Data;
using DL.AnimationsRuntime;
using DL.CoreRuntime;
using DL.Data.Resource;
using DL.GameResourcesRuntime.Core;
using DL.UtilsRuntime;
using UnityEngine;

namespace DL.WorldResourceRuntime.Common
{
    public class ResourceBox : MonoBehaviour
    {
        private const float DropDelay = 0.3f;
        private const float DropItemRadius = 1.5f;
        private const float DropItemHeight = 1.5f;
        private const float DropItemDuration = 1f;

        [SerializeField] private int _minDropItems, _maxDropItems;
        [SerializeField] private WeightedList<ResourceDataModel> _droppableItems;

        public bool IsActive { get; private set; } = true;

        private void OnTriggerEnter(Collider other)
        {
            if ((!other.TryGetComponent(out EntityStats entityStats)) || (!IsActive))
            {
                return;
            }

            if (entityStats.TeamId != Teams.PlayerTeamId)
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

        private void DropItem(ResourceDataModel resource)
        {
            var item = ResourceDataModel.InstantiateResource(resource, transform.position + transform.forward);
            item.AnimateItemDrop(transform.position, DropItemRadius, DropItemHeight, DropItemDuration,
                () => { ActivateItemCollider(item); });
        }

        private void ActivateItemCollider(GameObject item)
        {
            if (!item.TryGetComponent(out WorldResource gameResource))
            {
                return;
            }

            gameResource.Initialize();
        }
    }
}