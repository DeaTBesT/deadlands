using DL.CoreRuntime;
using DL.WorldResourceRuntime.Core;
using UnityEngine;

namespace DL.WorldResourceRuntime.ResourceVein
{
    public class ResourceVeinInitializer : EntityInitializer
    {
        [SerializeField] private ResourceVeinStats _resourceVeinStats;
        [SerializeField] private ResourceSpawner _resourceSpawner;

        private void OnValidate()
        {
            if (_resourceVeinStats == null)
            {
                _resourceVeinStats = GetComponent<ResourceVeinStats>();
            }
            
            if (_resourceSpawner == null)
            {
                _resourceSpawner = GetComponent<ResourceSpawner>();
            }
        }

        public override void Initialize(params object[] objects) => 
            _resourceVeinStats.Initialize(_resourceSpawner);
    }
}