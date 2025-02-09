using Data.Core;
using DL.EnumsRuntime;
using UnityEngine;
using UnityEngine.Serialization;

namespace DL.Data.Resource
{
    [CreateAssetMenu(menuName = "World resources/New resource")]
    public class ResourceConfig : ItemConfig
    {
        [SerializeField] private int _sortPriority = 0;
        [SerializeField] private RareType _rareType;
        [SerializeField] private GameObject _resourcePrefab;

        public int SortPriority => _sortPriority;
        public RareType TypeRare => _rareType;
        public GameObject ResourcePrefab => _resourcePrefab;
    }
}