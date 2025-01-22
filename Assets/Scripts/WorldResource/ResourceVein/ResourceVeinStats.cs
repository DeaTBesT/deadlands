using DL.CoreRuntime;
using DL.WorldResourceRuntime.Core;
using UnityEngine;

namespace DL.WorldResourceRuntime.ResourceVein
{
    public class ResourceVeinStats : EntityStats
    {
        private ResourceSpawner _resourceSpawner;
        
        public override int TeamId => 100;

        public override void Initialize(params object[] objects) => 
            _resourceSpawner = objects[0] as ResourceSpawner;

        public override void TakeDamage(int teamId, float amount)
        {
            if (teamId == TeamId)
            {
                return;
            }

            if (_health - amount > 0)
            {
                TakeDamage(amount);
                return;
            }
            
            DestroyEntity();
        }

        private void TakeDamage(float amount) => 
            _health -= Mathf.Clamp(_health - amount, 0, float.MaxValue);

        public override void DestroyEntity()
        {
            if (_resourceSpawner != null)
            {
                _resourceSpawner.InstantiateResource();
            }

            Destroy(gameObject);
        }
    }
}