using Data;
using DL.CoreRuntime;

namespace DL.PlayersRuntime
{
    public class PlayerStats : EntityStats
    {
        public override int TeamId => Teams.PlayerTeamId;

        public override void TakeDamage(int teamId, float amount)
        {
            if (!IsEnable)
            {
                return;
            }
        }
    }
}