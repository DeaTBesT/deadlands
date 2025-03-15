using System;
using DL.Data.Wardrobe;
using DL.UIRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DL.WardrobeRuntime.UI
{
    public class RouletteItemPreviewPanelUI : UIPanel
    {
        [SerializeField] private Button _buttonClosePreview;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _itemTitle;
        
        private WardrobeItemConfig _wardrobeItemConfig;

        private Action _refreshAction;

        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            _refreshAction = objects[0] as Action;
            _buttonClosePreview.onClick.AddListener(Hide);

            IsEnable = true;
        }

        public void ShowPreview(WardrobeItemConfig itemConfig)
        {
            _wardrobeItemConfig = itemConfig;
            
            _itemIcon.sprite = _wardrobeItemConfig.ItemSprite;
            _itemTitle.text = _wardrobeItemConfig.Title;
            
            Show();
        }

        public override void Hide()
        {
            _refreshAction?.Invoke();
            base.Hide();
        }
    }
}