using DL.Data.Core;
using DL.EnumsRuntime;
using UnityEngine;

namespace DL.Data.Wardrobe
{
    public class WardrobeItemConfig : ItemConfig
    {
        [SerializeField] private int _maxParts; //Макс. кол-во частей, нужное для сбора предмета
        [SerializeField] private RareType _rareType;
        [SerializeField] private WardrobeItemType _wardrobeItemType;

        public int MaxParts => _maxParts;
        public RareType TypeRare => _rareType;
        public WardrobeItemType ItemType => _wardrobeItemType;
    }
}