using DL.CoreRuntime;
using UnityEngine;

namespace DL.DebugOptionsRuntime
{
    public class DummyStats : EntityStats
    {
        public override int TeamId => 999;
        public override void TakeDamage(int teamId, float amount) => 
            Debug.Log($"Take damage {amount} from team {teamId}");
    }
}