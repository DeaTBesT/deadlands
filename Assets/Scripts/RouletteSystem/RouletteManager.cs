using System;
using System.Collections.Generic;
using DL.Data.Scene;
using DL.Data.Wardrobe;
using DL.InterfacesRuntime;
using DL.SceneTransitionRuntime;
using DL.UtilsRuntime;
using DL.WardrobeRuntime;
using UnityEngine;

namespace DL.RouletteSystemRuntime
{
    public class RouletteManager : Singleton<RouletteManager>, IInitialize, IDeinitialize
    {
        private const int MinWinningIndexOffset = 2;
        private const int MaxWinningIndexOffset = 3;

        [SerializeField] private List<WardrobeItemConfig> _itemConfigs = new();
        [SerializeField] private int _totalItems = 50;

        private WeightedList<WardrobeItemConfig> _weightedItemConfigs = new();
        private RouletteControllerUI _rouletteControllerUI;
        
        private WardrobeManager _wardrobeManager;

        private readonly List<IButtonClick> _clickHandlers = new();

        public Action OnShowPanel { get; set; }

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

            _rouletteControllerUI = objects[0] as RouletteControllerUI;
            _wardrobeManager = objects[1] as WardrobeManager;

            _rouletteControllerUI.Initialize(this, _itemConfigs, _totalItems);

            SceneLoader.OnStartLoadScene += OnStartLoadScene;

            foreach (var itemConfig in _itemConfigs)
            {
                var chance = RareChanceConverter.GetChance(itemConfig.TypeRare);
                _weightedItemConfigs.Add(itemConfig, chance);
            }

            IsEnable = true;
        }

        public void Deinitialize(params object[] objects)
        {
            if (!IsEnable)
            {
                return;
            }

            _rouletteControllerUI.Deinitialize();

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

        public WardrobeItemConfig GetWinningItem() => 
            _weightedItemConfigs.GetRandomItem();

        public bool TryAddItemToWardrobe(WardrobeItemConfig wardrobeItemConfig) => 
            _wardrobeManager.TryAddItemPart(wardrobeItemConfig.ItemType);
    }
}