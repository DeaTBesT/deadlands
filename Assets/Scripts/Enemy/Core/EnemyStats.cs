using Data;
using DL.CoreRuntime;
using UnityEngine;

namespace DL.StructureRuntime.UIPanels.Core
{
    public class EnemyStats : EntityStats
    {
        public override int TeamId => Teams.EnemyTeamId;
        
        public override bool TryApplyDamage(int teamId, float amount)
        {
            if ((!IsEnable) || (teamId == TeamId))
            {
                return false;
            }
            
            _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, float.MaxValue);

            if (_currentHealth > 0)
            {
                return true;
            }
            
            DestroyEntity();
            
            return true;
        }

        public override void DestroyEntity() => 
            Destroy(gameObject);
    }
}