using System.Collections.Generic;
using System.Linq;
using DL.Data.Wardrobe;
using DL.EnumsRuntime;
using DL.InterfacesRuntime;
using DL.UIRuntime;
using DL.UtilsRuntime;
using UI.Core;
using UnityEngine;
using Wardrobe.UI;

namespace DL.WardrobeRuntime
{
    public class WardrobeControllerUI : MonoBehaviour, IInitialize, IDeinitialize
    {
        [SerializeField] private WardrobeItemPanelUI _itemPanelPrefab;

        [Header("Panels")]
        [SerializeField] private UIPanel _generalPanel;
        [SerializeField] private UIPanel _waaponsPanel;
        [SerializeField] private UIPanel _armorPanel;
        
        [Header("Weapons")]
        [SerializeField] private List<PivotItemConfig> _weaponsPivots = new();
        
        [Header("Armor")]
        [SerializeField] private List<PivotItemConfig> _armorPivots = new();

        private List<WardrobeItemPanelUI> _itemPanels = new();
        
        private UIPanelList _panels = new();
        
        private WardrobeManager _wardrobeManager;
        
        public bool IsEnable { get; set; }
     
        [System.Serializable]
        public class PivotItemConfig
        {
            public Transform pivot;
            public RareType rareType;
        }
        
        public void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }

            _itemPanels = new List<WardrobeItemPanelUI>();
            
            _wardrobeManager = objects[0] as WardrobeManager;

            if (_wardrobeManager != null)
            {
                _wardrobeManager.OnItemChanged += OnItemChanged;
                _wardrobeManager.OnGenerateWeaponItems += OnGenerateWeaponItems;
                _wardrobeManager.OnGenerateArmorItems += OnGenerateArmorItems;
            }

            _panels.Add(_generalPanel);
            _panels.Add(_waaponsPanel);
            _panels.Add(_armorPanel);
            
            _panels.ClosePanels();
            
            IsEnable = true;
        }

        public void Deinitialize(params object[] objects)
        {
            if (!IsEnable)
            {
                return;
            }
            
            if (_wardrobeManager != null)
            {
                _wardrobeManager.OnItemChanged -= OnItemChanged;
                _wardrobeManager.OnGenerateWeaponItems -= OnGenerateWeaponItems;
                _wardrobeManager.OnGenerateArmorItems -= OnGenerateArmorItems;
            }
            
            _itemPanels.Clear();
            
            IsEnable = false;
        }
        
        private void OnItemChanged(WardrobeItemModel item)
        {
            var itemPanel = _itemPanels.FirstOrDefault(x => x.ItemModel.ItemType == item.ItemType);

            if (itemPanel == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Panel isn't exist");
#endif
                return;
            }
            
            itemPanel.UpdatePanel(item);
        }
        
        private void OnGenerateWeaponItems(List<WardrobeItemModel> wardrobeItems) => 
            GenerateWardrobe(_weaponsPivots, wardrobeItems);

        private void OnGenerateArmorItems(List<WardrobeItemModel> wardrobeItems) => 
            GenerateWardrobe(_armorPivots, wardrobeItems);

        private void GenerateWardrobe(List<PivotItemConfig> pivots, List<WardrobeItemModel> wardrobeItems)
        {
            ClearPivotsChildren(pivots);
            
            foreach (var item in wardrobeItems)
            {
                var pivotItemConfig = pivots.FirstOrDefault(x => x.rareType == item.TypeRare);

                if (pivotItemConfig == null)
                {
                    continue;
                }

                var pivot = pivotItemConfig.pivot;
                var newPanel = Instantiate(_itemPanelPrefab.gameObject, pivot);

                if (!newPanel.TryGetComponent(out WardrobeItemPanelUI wardrobeItemPanelUI))
                {
                    continue;
                }
                
                wardrobeItemPanelUI.Initialize(item);
                _itemPanels.Add(wardrobeItemPanelUI);
            }
        }

        private void ClearPivotsChildren(List<PivotItemConfig> pivots)
        {
            foreach (var child in pivots.SelectMany(pivotConfig => pivotConfig.pivot.transform.Cast<Transform>()))
            {
                Destroy(child.gameObject);
            }
        }
    }
}