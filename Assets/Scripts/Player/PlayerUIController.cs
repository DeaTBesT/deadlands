using System.Collections.Generic;
using System.Linq;
using DL.Data.Resource;
using DL.InterfacesRuntime;
using DL.ManagersRuntime;
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

        private GameResourcesManager _resourcesManager;

        public bool IsEnable { get; set; }

        public void Initialize(params object[] objects)
        {
            _resourcesManager = objects[0] as GameResourcesManager;

            if (_resourcesManager == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resources Manager is null");
#endif
                return;
            }

            _canvas.SetActive(true);

            _resourcesManager.OnAddResource += OnAddResource;
            _resourcesManager.OnChangeResourcesData += OnChangeResources;
            _resourcesManager.OnRemoveResource += OnRemoveResource;
        }

        public void Deinitialize(params object[] objects)
        {
            if (_resourcesManager == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resources Manager is null");
#endif
                return;
            }

            _resourcesManager.OnAddResource -= OnAddResource;
            _resourcesManager.OnChangeResourcesData -= OnChangeResources;
            _resourcesManager.OnRemoveResource -= OnRemoveResource;
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