using UnityEngine;
using WizardGame.Player;

namespace WizardGame.Stats
{
    public class PlayerStats
    {
        public int Level { get; private set; }

        public Stat MovementSpeed { get; private set; }
        public Stat Health { get; private set; }
        public Stat Experience { get; private set; }

        private PlayerDataSO data;

        public static PlayerStats CopyFrom(PlayerDataSO playerData)
        {
            return new PlayerStats
            {
                data = playerData,

                Level = 1,

                MovementSpeed = new Stat(playerData.MovementSpeed),
                Health = new Stat(playerData.Health),
                Experience = new Stat(playerData.Experience)
            };
        }
    }
}
