using DL.Data.Wardrobe;
using DL.UIRuntime;
using UnityEngine;
using UnityEngine.UI;

namespace DL.RouletteSystemRuntime.UI
{
    public class RouletteItemPanelUI : UIPanel
    {
        [SerializeField] private Image _itemIcon;
        //[SerializeField] private Image _rareImage;

        public WardrobeItemConfig ItemConfig { get; private set; }

        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }

            ItemConfig = objects[0] as WardrobeItemConfig;

            UpdatePanel(ItemConfig);

            IsEnable = true;
        }

        public void UpdatePanel(WardrobeItemConfig wardrobeItemConfig)
        {
            ItemConfig = wardrobeItemConfig;

            _itemIcon.sprite = ItemConfig.ItemSprite;
        }
    }
}