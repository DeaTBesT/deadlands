using System.Collections.Generic;
using System.Linq;
using DL.CoreRuntime;
using DL.Data.Resource;
using DL.InterfacesRuntime;
using DL.UtilsRuntime;
using DL.WorldResourceRuntime.UI;
using UnityEngine;

namespace DL.PlayersRuntime
{
    public class PlayerUIController : MonoBehaviour, IInitialize, IDeinitialize
    {
        [SerializeField] private GameObject _canvas;

        [Header("Resources data")] [SerializeField]
        private Transform _resourcesParent;

        [SerializeField] private ResourceDataUI _resourcePrefab;

        private List<ResourceDataUI> _resourcesDataUI = new();

        private IInventoryController _inventoryController;

        public bool IsEnable { get; set; }

        public void Initialize(params object[] objects)
        {
            _inventoryController = objects[0] as IInventoryController;

            if (_inventoryController == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Inventory Controller is null");
#endif
                return;
            }

            _canvas.SetActive(true);

            _inventoryController.OnAddResource += OnAddResource;
            _inventoryController.OnChangedResourcesData += OnChangeResources;
            _inventoryController.OnRemoveResource += OnRemoveResource;
        }

        public void Deinitialize(params object[] objects)
        {
            if (_inventoryController == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resources Manager is null");
#endif
                return;
            }

            _inventoryController.OnAddResource -= OnAddResource;
            _inventoryController.OnChangedResourcesData -= OnChangeResources;
            _inventoryController.OnRemoveResource -= OnRemoveResource;
        }

        private void OnAddResource(ResourceDataModel data)
        {
            var resourceDataUI = _resourcesDataUI.FirstOrDefault(x =>
                x.ResourceData.ResourceConfig.TypeResource == data.ResourceConfig.TypeResource);

            if (resourceDataUI != null)
            {
                resourceDataUI.UpdateResource();
                return;
            }

#if UNITY_EDITOR
            Debug.LogError($"None resource data UI. Resource type: {data.ResourceConfig.TypeResource}");
#endif
        }

        private void OnChangeResources(List<ResourceDataModel> dataList) =>
            GenerateResourcesPanel(dataList);

        private void OnRemoveResource(ResourceDataModel data)
        {
            var resourceDataUI = _resourcesDataUI.FirstOrDefault(x =>
                x.ResourceData.ResourceConfig.TypeResource == data.ResourceConfig.TypeResource);

            if (resourceDataUI != null)
            {
                resourceDataUI.UpdateResource();
                return;
            }

#if UNITY_EDITOR
            Debug.LogError("None resource data UI");
#endif
        }

        private void GenerateResourcesPanel(List<ResourceDataModel> dataList)
        {
            ClearResourcesPanel();

            dataList.SortResources();

            foreach (var data in dataList)
            {
                var resourceDataUI = Instantiate(_resourcePrefab, _resourcesParent);
                resourceDataUI.ChangeResourceData(data);
                _resourcesDataUI.Add(resourceDataUI);
            }
        }

        private void ClearResourcesPanel()
        {
            if (_resourcesParent == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resource panels is null");
#endif
                return;
            }

            foreach (Transform child in _resourcesParent)
            {
                Destroy(child.gameObject);
            }

            _resourcesDataUI.Clear();
        }
    }
}