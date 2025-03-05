using DL.Data.Wardrobe;
using DL.UIRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DL.WardrobeRuntime.UI
{
    public class WardrobeItemPreviewPanelUI : UIPanel
    {
        [SerializeField] private Button _buttonClosePreview;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _itemTitle;
        [SerializeField] private TextMeshProUGUI _itemParts;
        
        private WardrobeItemModel _wardrobeItemModel;
        
        private string GetParts =>
            $"{_wardrobeItemModel.CurrentParts}/{_wardrobeItemModel.ItemWardrobeConfig.MaxParts}";
        
        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            _buttonClosePreview.onClick.AddListener(Hide);

            IsEnable = true;
        }

        public void ShowPreview(WardrobeItemModel itemModel)
        {
            _wardrobeItemModel = itemModel;
            
            _itemIcon.sprite = _wardrobeItemModel.ItemWardrobeConfig.ItemSprite;
            _itemTitle.text = _wardrobeItemModel.ItemWardrobeConfig.Title;
            _itemParts.text = GetParts;
            
            Show();
        }
    }
}