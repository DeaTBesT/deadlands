using DL.CoreRuntime;

namespace DL.PlayersRuntime
{
    public class PlayerStats : EntityStats
    {
        public override int TeamId => 1;

        public override void TakeDamage(int teamId, float amount)
        {
            if (!IsEnable)
            {
                return;
            }
        }

        public override void DestroyEntity()
        {
            
        }
    }
}