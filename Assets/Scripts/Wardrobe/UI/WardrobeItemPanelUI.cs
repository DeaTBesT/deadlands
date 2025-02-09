using DL.Data.Wardrobe;
using DL.UIRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wardrobe.UI
{
    public class WardrobeItemPanelUI : UIPanel
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _itemParts;
        //[SerializeField] private Image _rareImage;

        private WardrobeItemModel _wardrobeItemModel;

        private string GetParts =>
            $"{_wardrobeItemModel.CurrentParts}/{_wardrobeItemModel.ItemWardrobeConfig.MaxParts}";

        public WardrobeItemModel ItemModel => _wardrobeItemModel;
        
        public override void Initialize(params object[] objects)
        {
            _wardrobeItemModel = objects[0] as WardrobeItemModel;

            UpdatePanel(_wardrobeItemModel);
        }

        public void UpdatePanel(WardrobeItemModel wardrobeItemModel)
        {
            _wardrobeItemModel = wardrobeItemModel;

            _itemIcon.sprite = _wardrobeItemModel.ItemWardrobeConfig.ItemSprite;
            _itemParts.text = GetParts;
        }
    }
}