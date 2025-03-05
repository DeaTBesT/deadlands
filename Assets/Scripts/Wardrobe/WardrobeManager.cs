using System;
using System.Collections.Generic;
using System.Linq;
using DL.Data.Scene;
using DL.Data.Wardrobe;
using DL.EnumsRuntime;
using DL.InterfacesRuntime;
using DL.SceneTransitionRuntime;
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
        
        private readonly List<IButtonClick> _clickHandlers = new();
        
        public Action OnShowPanel { get; set; }
        public Action<WardrobeItemModel> OnItemChanged { get; set; }
        public Action<List<WardrobeItemModel>> OnGenerateWeaponItems { get; set; }
        public Action<List<WardrobeItemModel>> OnGenerateArmorItems { get; set; }
        
        public bool IsEnable { get; set; }

        private void ResetStatics()
        {
            // reset all statics

            foreach (var handler in _clickHandlers)
            {
                handler.OnButtonClick -= OnButtonClickHandler;
            }
        }
        
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
            
            SceneLoader.OnStartLoadScene += OnStartLoadScene;
            
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

            SceneLoader.OnStartLoadScene -= OnStartLoadScene;
            
            IsEnable = false;
        }

        private void OnStartLoadScene(SceneConfig sceneConfig) =>
            ResetStatics();
        
        /// <summary>
        /// Для регистрации нажания на кнопку, для открытия панели
        /// </summary>
        public void RegisterButtonClick(IButtonClick clickHandler)
        {
            if ((clickHandler == null) || (_clickHandlers.Contains(clickHandler)))
            {
                return;
            }
            
            clickHandler.OnButtonClick += OnButtonClickHandler;
            _clickHandlers.Add(clickHandler);
        }

        /// <summary>
        /// Для удаления регистрации нажания на кнопку, для открытия панели
        /// </summary>
        public void UnRegisterButtonClick(IButtonClick clickHandler)
        {
            if ((clickHandler == null) || (!_clickHandlers.Contains(clickHandler)))
            {
                return;
            }
            
            clickHandler.OnButtonClick -= OnButtonClickHandler;
            _clickHandlers.Remove(clickHandler);
        }
        
        private void OnButtonClickHandler() =>
            OnShowPanel?.Invoke();
        
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