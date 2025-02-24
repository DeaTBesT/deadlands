using UnityEngine;

namespace DL.Data.Core
{
    public abstract class ItemModel
    {
        public abstract GameObject ItemPrefab { get; }
        
        public static GameObject InstantiateItem(ItemModel itemModel, Transform spawnPosition)
        {
            if (itemModel == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resource data isn't exist");
#endif
                return null;
            }
            
            if (itemModel.ItemPrefab == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Item prefab isn't exist");
#endif
                return null;
            }
            
            var newResource = Object.Instantiate(itemModel.ItemPrefab, spawnPosition);
            newResource.transform.parent = null;
            newResource.transform.position = spawnPosition.position;
            newResource.transform.rotation = spawnPosition.rotation;


            return newResource;
        }

        public static GameObject InstantiateItem(ItemModel itemModel, Vector3 spawnPosition, Quaternion spawnRotation = default)
        {
            if (itemModel == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Item data isn't exist");
#endif
                return null;
            }
            
            if (itemModel.ItemPrefab == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Item prefab isn't exist");
#endif
                return null;
            }

            var newResource = Object.Instantiate(itemModel.ItemPrefab, spawnPosition, spawnRotation);
            newResource.transform.position = spawnPosition;
            newResource.transform.rotation = spawnRotation;

            return newResource;
        }
    }
}