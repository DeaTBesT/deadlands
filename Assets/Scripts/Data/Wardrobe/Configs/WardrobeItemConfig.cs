using DL.Data.Core;
using DL.EnumsRuntime;
using UnityEngine;

namespace DL.Data.Wardrobe
{
    public class WardrobeItemConfig : ItemConfig
    {
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _maxParts; //Макс. кол-во частей, нужное для сбора предмета
        [SerializeField] private RareType _rareType;
        [SerializeField] private WardrobeItemType _wardrobeItemType;

        public string Title => _title;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int MaxParts => _maxParts;
        public RareType TypeRare => _rareType;
        public WardrobeItemType ItemType => _wardrobeItemType;
    }
}