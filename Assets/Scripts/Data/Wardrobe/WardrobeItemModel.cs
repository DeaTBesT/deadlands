using DL.Data.Core;
using DL.EnumsRuntime;
using UnityEngine;

namespace DL.Data.Wardrobe
{
    [System.Serializable]
    public class WardrobeItemModel : ItemModel
    {
        [SerializeField] private WardrobeItemConfig _wardrobeItemConfig;
        [SerializeField] private int _currentParts;
        [SerializeField] private RareType _rareType;
        [SerializeField] private WardrobeItemType _wardrobeItemType;

        public WardrobeItemConfig ItemWardrobeConfig => _wardrobeItemConfig;
        public int CurrentParts => _currentParts;
        public WardrobeItemType ItemType => _wardrobeItemType;
        public RareType TypeRare => _rareType;
        public override GameObject ItemPrefab => null;
        
        public void AddParts(int amount) => 
            _currentParts = Mathf.Clamp(_currentParts + amount, 0, _wardrobeItemConfig.MaxParts);

        public void RemoveParts(int amount) => 
            _currentParts = Mathf.Clamp(_currentParts - amount, 0, _wardrobeItemConfig.MaxParts);
    }
}