using DL.Data.Core;
using UnityEngine;

namespace DL.Data.Wardrobe
{
    public class WardrobeItemConfig : ItemConfig
    {
        [SerializeField] private int _maxParts;//Макс. кол-во частей, нужное для сбора предмета
        
        public int MaxParts => _maxParts;
    }
}