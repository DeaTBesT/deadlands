using DL.CoreRuntime;
using UnityEngine;

namespace DL.DebugOptionsRuntime
{
    public class DummyStats : EntityStats
    {
        public override int TeamId => 999;
        public override bool TryApplyDamage(int teamId, float amount)
            Debug.Log($"Take damage {amount} from team {teamId}");
            return true;
    }
}