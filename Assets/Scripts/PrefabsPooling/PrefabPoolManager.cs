using System.Collections.Generic;
using System.Linq;
using DL.EnumsRuntime;
using DL.InterfacesRuntime;
using DL.UtilsRuntime;
using UnityEngine;

namespace DL.PrefabsPoolingRuntime
{
    public class PrefabPoolManager : Singleton<PrefabPoolManager>, IInitialize
    {
        [SerializeField] private List<PrefabPool> _prefabPools = new();

        public bool IsEnable { get; set; }

        private void OnValidate()
        {
            if (transform.childCount == _prefabPools.Count)
            {
                return;
            }
            
            _prefabPools.Clear();

            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out PrefabPool prefabPool))
                {
                    _prefabPools.Add(prefabPool);
                }
                else
                {
                    Debug.LogWarning($"{child.name} isn't prefab pool");
                }
            }
        }

        public void Initialize(params object[] objects) => 
            _prefabPools.ForEach(prefabPool => prefabPool.Initialize(objects));

        public PrefabPool GetPool(PoolType poolType)
        {
            var pool = _prefabPools.First(x => x.TypePool == poolType);

            if (pool == null)
            {
#if UNITY_EDITOR
                Debug.LogError($"Pool isn't exist: {poolType}");
#endif
            }

            return pool;
        }
    }
}