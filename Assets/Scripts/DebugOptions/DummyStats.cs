using Data;
using DL.CoreRuntime;
using UnityEngine;

namespace DL.DebugOptionsRuntime
{
    public class DummyStats : EntityStats
    {
        public override int TeamId => Teams.DummyTeamId;

        public override bool TryApplyDamage(int teamId, float amount)
        {
            if (teamId == TeamId)
            {
                return false;
            }
            
            Debug.Log($"Take damage {amount} from team {teamId}");

            return true;
        }
    }
}