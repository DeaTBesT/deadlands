using System.Collections.Generic;
using GameResources.UI;
using Place.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlacePanels.Core
{
    public class AdvancedPlacePanelUI : SimplePlacePanelUI
    {
        [SerializeField] protected Button _button;
        [SerializeField] protected Transform _requiredResourcesParent;
        [SerializeField] protected ResourceDataUI _requiredResourcesPrefab;
        
        protected List<ResourceDataUI> _resourcesDataUI = new();

        protected PlaceController _placeController;
        
        public override void Initialize(params object[] objects) => 
            _placeController = objects[0] as PlaceController;
        
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