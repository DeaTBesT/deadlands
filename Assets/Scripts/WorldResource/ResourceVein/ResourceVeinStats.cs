using Data;
using DL.CoreRuntime;
using DL.WorldResourceRuntime.Core;
using UnityEngine;

namespace DL.WorldResourceRuntime.ResourceVein
{
    public class ResourceVeinStats : EntityStats
    {
        private ResourceSpawner _resourceSpawner;
        
        public override int TeamId => Teams.ResourceVeinTeamId;

        public override void Initialize(params object[] objects) => 
            _resourceSpawner = objects[0] as ResourceSpawner;

        public override bool TryApplyDamage(int teamId, float amount)
        {
            if (teamId == TeamId)
            {
                return false;
            }

            _currentHealth -= Mathf.Clamp(_currentHealth - amount, 0, float.MaxValue);
            
            if (_currentHealth  > 0)
            {
                return true;
            }
            
            DestroyEntity();

            return true;
        }

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