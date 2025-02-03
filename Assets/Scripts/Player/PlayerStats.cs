using Data;
using DL.CoreRuntime;
using UnityEngine;

namespace DL.PlayersRuntime
{
    public class PlayerStats : EntityStats
    {
        private IInventoryController _inventoryController;
        
        public override int TeamId => Teams.PlayerTeamId;

        public override void Initialize(params object[] objects)
        {
            base.Initialize(objects);
            
            _inventoryController = objects[0] as IInventoryController;
        }

        public override bool TryApplyDamage(int teamId, float amount)
        {
            if (!IsEnable)
            {
                return false;
            }

            if (teamId == Teams.PlayerTeamId)
            {
                return false;
            }
            
            _currentHealth -= Mathf.Clamp(_currentHealth - amount, 0, float.MaxValue);

            if (_currentHealth > 0)
            {
                return true;
            }
            
            DestroyEntity();

            return true;
        }

        public override void DestroyEntity()
        {
            _inventoryController.DropAllResources();
            base.DestroyEntity();
        }
    }
}