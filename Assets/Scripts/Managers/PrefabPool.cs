using System.Linq;
using DL.EnumsRuntime;
using DL.InterfacesRuntime;
using DL.UtilsRuntime.ObjectPoolRuntime;
using UnityEngine;

namespace DL.ManagersRuntime
{
    public class PrefabPool : MonoBehaviour, IInitialize
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _preloadCount;
        [SerializeField] private PoolType _poolType;

        private GameObjectPool _objectPool;

        public PoolType TypePool => _poolType;

        public bool IsEnable { get; set; }

        private void OnValidate()
        {
            var str = _prefab == null ? "Prefab pool" : $"Prefab pool: {_prefab.name}";
            gameObject.name = str;
        }

        public void Initialize(params object[] objects)
        {
            if (_objectPool != null)
            {
                ClearPool();
            }

            _objectPool = new GameObjectPool(_prefab, _preloadCount, transform);
        }

        public GameObject Get(Transform owner)
        {
            var @object = _objectPool.Get();
            @object.transform.SetParent(owner.transform);
            @object.transform.parent = null;

            return @object;
        }

        public void Return(GameObject @object) =>
            _objectPool.Return(@object);

        private void ClearPool()
        {
            foreach (var @object in _objectPool.Pool.Where(@object => @object != null))
            {
                Destroy(@object);
            }

            _objectPool.Pool.Clear();
        }
    }
}