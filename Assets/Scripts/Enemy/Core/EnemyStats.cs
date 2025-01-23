using DL.CoreRuntime;
using UnityEngine;

namespace DL.StructureRuntime.UIPanels.Core
{
    public class EnemyStats : EntityStats
    {
        public override int TeamId => 2;
        
        public override void TakeDamage(int teamId, float amount)
        {
            if ((!IsEnable) || (teamId == TeamId))
            {
                return;
            }
            
            _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, float.MaxValue);

            if (_currentHealth <= 0)
            {
                DestroyEntity();
            }
        }

        public override void DestroyEntity() => 
            Destroy(gameObject);
    }
}