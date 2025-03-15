using System;
using System.Collections.Generic;
using DG.Tweening;
using DL.Data.Wardrobe;
using DL.InterfacesRuntime;
using DL.RouletteSystemRuntime.UI;
using DL.UIRuntime;
using DL.UtilsRuntime;
using DL.WardrobeRuntime.UI;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DL.RouletteSystemRuntime
{
    public class RouletteControllerUI : MonoBehaviour, IInitialize, IDeinitialize
    {
        private const int PreviousItems = 4;//Кол-во предметов, который спавняться в левую сторону от центра
        private const int StopOffsetItem = 3; //Остановка рулетки на указанном предмете с конца
        
        [Header("Buttons")] [SerializeField] private Button _closePanelButton;
        [SerializeField] private Button _buttonStartSpin;

        [Header("Panels")] [SerializeField] private UIPanel _generalPanel;

        [SerializeField] private RouletteItemPreviewPanelUI _itemPreviewPanel;

        [Header("Roulette")] [SerializeField] private RectTransform _itemContainer;
        [SerializeField] private RectTransform _itemPrefab;
        [SerializeField] private float _itemWidth = 100f;
        [SerializeField] private float _spinDuration = 3f;
        [SerializeField] private AnimationCurve _spinCurve;

        private readonly UIPanelList _panels = new();
        
        private int _totalItems;

        private WardrobeItemConfig _winningItem;

        private RouletteManager _rouletteManager;

        private Vector2 _itemContainerStartPosition;

        private Action _completeAction;

        private List<WardrobeItemConfig> _itemConfigs = new();

        private readonly List<RouletteItemPanelUI> _itemPanels = new();

        public bool IsEnable { get; set; } = true;

        public void Initialize(params object[] objects)
        {
            _rouletteManager = objects[0] as RouletteManager;
            _itemConfigs = objects[1] as List<WardrobeItemConfig>;
            _totalItems = (int)objects[2];

            if (_rouletteManager != null)
            {
                _rouletteManager.OnShowPanel += ShowPanel;
            }

            if (_closePanelButton != null)
            {
                _closePanelButton.onClick.AddListener(HidePanel);
            }

            if (_buttonStartSpin != null)
            {
                _buttonStartSpin.onClick.AddListener(StartSpin);
            }

            _itemContainerStartPosition = _itemContainer.anchoredPosition;
            _itemPreviewPanel.Initialize((Action)RefreshPanel);
            
            GeneratePanels();

            _panels.Add(_generalPanel);
            _panels.Add(_itemPreviewPanel);

            HidePanel();
        }

        public void Deinitialize(params object[] objects)
        {
            if (_rouletteManager != null)
            {
                _rouletteManager.OnShowPanel -= ShowPanel;
            }

            if (_closePanelButton != null)
            {
                _closePanelButton.onClick.AddListener(HidePanel);
            }

            if (_buttonStartSpin != null)
            {
                _buttonStartSpin.onClick.RemoveListener(StartSpin);
            }
        }

        private void ShowPanel() =>
            _generalPanel.Show();

        private void HidePanel() =>
            _panels.ClosePanels();

        private void GeneratePanels()
        {
            //Добавление предметов справа
            for (var i = 0; i < PreviousItems; i++)
            {
                var newItem = Instantiate(_itemPrefab.gameObject, _itemContainer);
            
                if (newItem.TryGetComponent(out RouletteItemPanelUI rouletteItemPanelUI))
                {
                    var itemConfig = _itemConfigs[Random.Range(0, _itemConfigs.Count)];
                    rouletteItemPanelUI.Initialize(itemConfig);
                }
            
                if (newItem.TryGetComponent(out RectTransform rectTransform))
                {
                    rectTransform.anchoredPosition = new Vector2(i * -_itemWidth, 0);
                }
                
                _itemPanels.Add(rouletteItemPanelUI);
            }
            
            for (var i = 0; i < _totalItems; i++)
            {
                var newItem = Instantiate(_itemPrefab.gameObject, _itemContainer);

                if (newItem.TryGetComponent(out RouletteItemPanelUI rouletteItemPanelUI))
                {
                    var itemConfig = _itemConfigs[Random.Range(0, _itemConfigs.Count)];
                    rouletteItemPanelUI.Initialize(itemConfig);
                }

                if (newItem.TryGetComponent(out RectTransform rectTransform))
                {
                    rectTransform.anchoredPosition = new Vector2(i * _itemWidth, 0);
                }

                _itemPanels.Add(rouletteItemPanelUI);
            }
        }

        private void RefreshPanel()
        {
            //Генерация новых предметов
            _itemPanels.ForEach(itemPanel =>
            {
                var itemConfig = _itemConfigs[Random.Range(0, _itemConfigs.Count)];
                itemPanel.UpdatePanel(itemConfig);
            });

            //Сброс позиции контейнера предметов
            _itemContainer.anchoredPosition = _itemContainerStartPosition;

            _buttonStartSpin.interactable = true;
        }

        [Button]
        private void StartSpin()
        {
            _buttonStartSpin.interactable = false;

            _winningItem = _rouletteManager.GetWinningItem();
            
            var targetIndex = _totalItems - StopOffsetItem;
            var targetPosition = -targetIndex * _itemWidth;

            _itemPanels[targetIndex + PreviousItems].UpdatePanel(_winningItem);
            
            _itemContainer.DOLocalMoveX(targetPosition, _spinDuration).SetEase(Ease.OutQuint).OnComplete(CompleteSpin);
        }

        private void CompleteSpin()
        {
            _itemPreviewPanel.ShowPreview(_winningItem);
            _rouletteManager.TryAddItemToWardrobe(_winningItem);
        }
    }
}