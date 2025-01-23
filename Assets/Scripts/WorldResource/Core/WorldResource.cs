using DL.CoreRuntime;
using DL.Data.Resource;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.GameResourcesRuntime.Core
{
    public class WorldResource : MonoBehaviour, IInitialize, IWorldResource
    {
        [SerializeField] private ResourceDataModel _resourceData;
        [SerializeField] private Collider _collider;

        public bool IsEnable { get; set; }

        private void OnValidate()
        {
            if (_collider == null)
            {
                _collider = GetComponent<Collider>();

                if (_collider != null)
                {
                    _collider.enabled = false;
                }
            }
        }

        public void Initialize(params object[] objects)
        {
            IsEnable = true;
            _collider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((!other.TryGetComponent(out EntityInventoryController playerInventory)) || (!IsEnable))
            {
                return;
            }
            
            playerInventory.AddResource(_resourceData);
            Destroy(gameObject);
        }
        
        public void SetAmount(int amount) =>
            _resourceData.SetAmount(amount);
    }
}