using System.Collections.Generic;
using GameResources.UI;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlacePanels.Core
{
    public abstract class AdvancedPlacePanelUI : MonoBehaviour, IInitialize, IPlacePanel
    {
        [SerializeField] protected GameObject _renderer;
        [SerializeField] protected Button _button;
        [SerializeField] protected Transform _requiredResourcesParent;
        [SerializeField] protected ResourceDataUI _requiredResourcesPrefab;
        
        protected List<ResourceDataUI> _resourcesDataUI = new();

        public bool IsEnable { get; set; }

        public abstract void Initialize(params object[] objects);
        
        public virtual void Show() =>
            _renderer.SetActive(true);

        public virtual void Hide() => 
            _renderer.SetActive(false);
        
        protected void ClearRequiredResourcesPanel()
        {
            if (_requiredResourcesParent == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Resource panels is null");
#endif
                return;
            }

            foreach (Transform child in _requiredResourcesParent)
            {
                Destroy(child.gameObject);
            }

            _resourcesDataUI.Clear();
        }
    }
}