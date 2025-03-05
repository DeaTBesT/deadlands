using System.Collections.Generic;
using DL.Data.Resource;
using DL.Data.Scene;
using DL.EnumsRuntime;
using DL.RaidRuntime;
using DL.SceneTransitionRuntime;
using DL.UtilsRuntime;
using DL.WardrobeRuntime;
using NaughtyAttributes;
using UnityEngine;

namespace DL.ManagersRuntime
{
    public class DebugManager : Singleton<DebugManager>
    {
        [BoxGroup("Wardrobe")] [SerializeField]
        private WardrobeItemType _wardrobeItemType;

        [BoxGroup("Wardrobe")] [SerializeField]
        private int _itemPartAmout;

        [Button]
        public void AddPartItemWardrobe()
        {
            if (!WardrobeManager.Instance.TryAddItemPart(_wardrobeItemType, _itemPartAmout))
            {
                Debug.Log("Failed add part");

                return;
            }

            Debug.Log("Success add part");
        }

        [Button]
        public void RemovePartItemWardrobe()
        {
            if (!WardrobeManager.Instance.TryRemoveItemPart(_wardrobeItemType, _itemPartAmout))
            {
                Debug.Log("Failed remove part");

                return;
            }

            Debug.Log("Success remove part");
        }

        [BoxGroup("Scene loading")] [SerializeField]
        private SceneConfig _loadScene;

        [Button]
        public void LoadScene()
        {
            if (_loadScene == null)
            {
                Debug.LogWarning("Set load scene");

                return;
            }

            SceneLoader.Instance.LoadScene(_loadScene);
        }

        [Button]
        public void FinishSuccessRaid() =>
            RaidManager.Instance.OnPlayerEscapedSuccess();

        [Button]
        public void FinishFailRaid() =>
            RaidManager.Instance.OnPlayerEscapedFail();

        [BoxGroup("Resources")] [SerializeField]
        private List<ResourceDataModel> _resources = new();

        [BoxGroup("Resources")] [SerializeField] [InfoBox("Clear resources list after add or remove")]
        private bool _isClearOnChanged = true;

        [Button]
        public void AddResources()
        {
            if (_resources.Count == 0)
            {
                Debug.LogWarning("Nothing to add");

                return;
            }

            ResourcesManager.Instance.AddPlayerResources(_resources);

            if (_isClearOnChanged)
            {
                _resources.Clear();
            }
        }

        [Button]
        public void RemoveResources()
        {
            if (_resources.Count == 0)
            {
                Debug.LogWarning("Nothing to add");

                return;
            }

            ResourcesManager.Instance.RemovePlayerResources(_resources);

            if (_isClearOnChanged)
            {
                _resources.Clear();
            }
        }
    }
}