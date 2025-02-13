using System;
using DL.Data.Wardrobe;
using DL.UIRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DL.WardrobeRuntime.UI
{
    public class WardrobeItemPanelUI : UIPanel
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _itemParts;
        [SerializeField] private Button _buttonOpenPreview;
        //[SerializeField] private Image _rareImage;

        private WardrobeItemModel _wardrobeItemModel;

        private string GetParts =>
            $"{_wardrobeItemModel.CurrentParts}/{_wardrobeItemModel.ItemWardrobeConfig.MaxParts}";

        public WardrobeItemModel ItemModel => _wardrobeItemModel;

        public Action<WardrobeItemModel> OnItemPreviewClicked { get; set; }
        
        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            _wardrobeItemModel = objects[0] as WardrobeItemModel;

            UpdatePanel(_wardrobeItemModel);

            _buttonOpenPreview.onClick.AddListener(OnButtonOpenPreviewClicked);
            
            IsEnable = true;
        }

        public void UpdatePanel(WardrobeItemModel wardrobeItemModel)
        {
            _wardrobeItemModel = wardrobeItemModel;

            _itemIcon.sprite = _wardrobeItemModel.ItemWardrobeConfig.ItemSprite;
            _itemParts.text = GetParts;
        }

        private void OnButtonOpenPreviewClicked() => 
            OnItemPreviewClicked?.Invoke(_wardrobeItemModel);
    }
}