using System;
using System.Collections.Generic;
using System.Linq;
using DL.Data.Wardrobe;
using DL.EnumsRuntime;
using DL.InterfacesRuntime;
using DL.UtilsRuntime;
using UnityEngine;

namespace DL.WardrobeRuntime
{
    public class WardrobeManager : Singleton<WardrobeManager>, IInitialize, IDeinitialize
    {
        [SerializeField] private List<WardrobeItemModel> _weaponItems = new();
        [SerializeField] private List<WardrobeItemModel> _armorItems = new();

        private List<WardrobeItemModel> _wardrobeItems = new(); //Общий список

        private WardrobeControllerUI _wardrobeControllerUI;

        private Transform _player;

        public Action<WardrobeItemModel> OnItemChanged { get; set; }
        public Action<List<WardrobeItemModel>> OnGenerateWeaponItems { get; set; }
        public Action<List<WardrobeItemModel>> OnGenerateArmorItems { get; set; }
        public Action OnStartRaid { get; set; }

        public bool IsEnable { get; set; }

        public void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }

            _wardrobeControllerUI = objects[0] as WardrobeControllerUI;
            
            _wardrobeItems = new List<WardrobeItemModel>();
            _wardrobeItems.AddRange(_weaponItems);
            _wardrobeItems.AddRange(_armorItems);

            _wardrobeControllerUI.Initialize(this);
            
            OnGenerateWeaponItems?.Invoke(_weaponItems);
            OnGenerateArmorItems?.Invoke(_armorItems);
            
            IsEnable = true;
        }

        public void Deinitialize(params object[] objects)
        {
            if (!IsEnable)
            {
                return;
            }

            _wardrobeControllerUI.Deinitialize(this);
            IsEnable = false;
        }

        public bool TryAddItemPart(WardrobeItemType itemType, int amount = 1)
        {
            var item = _wardrobeItems.FirstOrDefault(x => x.ItemType == itemType);

            if (item == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Wardrobe item isn't exist");
#endif
                return false;
            }

            item.AddParts(amount);
            OnItemChanged?.Invoke(item);
            
            return true;
        }
        
        public bool TryRemoveItemPart(WardrobeItemType itemType, int amount = 1)
        {
            var item = _wardrobeItems.FirstOrDefault(x => x.ItemType == itemType);

            if (item == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Wardrobe item isn't exist");
#endif
                return false;
            }

            item.RemoveParts(amount);
            OnItemChanged?.Invoke(item);
            
            return true;
        }
    }
}