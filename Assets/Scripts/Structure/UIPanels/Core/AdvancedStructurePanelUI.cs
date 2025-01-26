using System.Collections.Generic;
using DL.WorldResourceRuntime.UI;
using UI.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DL.StructureRuntime.UIPanels.Core
{
    public abstract class AdvancedStructurePanelUI : MonoBehaviour, IPanelUI
    {
        [SerializeField] protected GameObject _renderer;
        [SerializeField] protected Button _button;
        [SerializeField] protected Transform _requiredResourcesParent;
        [SerializeField] protected ResourceDataUI _requiredResourcesPrefab;
        
        protected List<ResourceDataUI> _resourcesDataUI = new();

        public bool IsEnable { get; set; }

        public virtual void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }   
            
            IsEnable = true;
        }
        
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
        
        public void AddOnClickEvent(UnityAction onClickAction) => 
            _button.onClick.AddListener(onClickAction);

        public void RemoveOnClickEvent(UnityAction onClickAction) => 
            _button.onClick.RemoveListener(onClickAction);

        public abstract void UpdatePanelView(params object[] objects);
    }
}