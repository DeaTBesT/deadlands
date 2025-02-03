using Data;
using DL.CoreRuntime;

namespace DL.PlayersRuntime
{
    public class PlayerStats : EntityStats
    {
        public override int TeamId => Teams.PlayerTeamId;

        public override void TakeDamage(int teamId, float amount)
        public override bool TryApplyDamage(int teamId, float amount)
        {
            if (!IsEnable)
            {
                return false;
            }
            return true;
        }
    }
}