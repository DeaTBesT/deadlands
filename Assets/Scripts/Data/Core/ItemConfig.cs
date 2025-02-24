using NaughtyAttributes;
using UnityEngine;

namespace DL.Data.Core
{
    public abstract class ItemConfig : ScriptableObject
    {
        [SerializeField] protected string _title = "empty title";
        [SerializeField] protected string _description = "empty description";
        [ShowAssetPreview, SerializeField] private Sprite _itemSprite;

        public virtual string Title => _title;
        public virtual string Description => _description;
        public virtual Sprite ItemSprite => _itemSprite;
    }
}