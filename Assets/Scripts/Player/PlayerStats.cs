using Mono.Cecil;
using UnityEngine;
using WizardGame.Core;
using WizardGame.Player;

namespace WizardGame.Stats
{
    public class PlayerStats : BaseStats
    {
        public int Level { get; private set; }

        public Stat MovementSpeed { get; private set; }
        public Stat DamageResistance { get; private set; }
        public Stat Health { get; private set; }
        public Stat Experience { get; private set; }

        private PlayerDataSO baseData;

        public PlayerStats(PlayerDataSO baseData)
        {
            this.baseData = baseData;

            var allStats = new[]
            {
                baseData.MovementSpeed,
                baseData.DamageResistance,
                baseData.Health,
                baseData.Experience
            };
            InitializeFromSO(allStats);
            Level = 1;
        }

        // public static PlayerStats CopyFrom(PlayerDataSO playerData)
        // {
        //     return new PlayerStats
        //     {
        //         baseData = playerData,

        //         Level = 1,

        //         MovementSpeed = new Stat(playerData.MovementSpeed),
        //         Health = new Stat(playerData.Health),
        //         Experience = new Stat(playerData.Experience)
        //     };
        // }
    }
}
