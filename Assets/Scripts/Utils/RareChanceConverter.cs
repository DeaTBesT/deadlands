using System;
using DL.EnumsRuntime;

namespace DL.UtilsRuntime
{
    public static class RareChanceConverter
    {
        private const int GreyChance = 50;
        private const int BlueChance = 30;
        private const int PurpleChance = 20;
        private const int GoldChance = 10;

        public static int GetChance(RareType rareType)
        {
            return rareType switch
            {
                RareType.Grey => GreyChance,
                RareType.Blue => BlueChance,
                RareType.Purple => PurpleChance,
                RareType.Gold => GoldChance,
                _ => throw new ArgumentOutOfRangeException(nameof(rareType), rareType, null)
            };
        }
    }
}