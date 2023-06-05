namespace Assets.Scripts.Game
{
    public static class ConfigurationUtils
    {
        private static ConfigurationData configurationData;
        public static int HealthBee { get; set; }
        public static int AttackPowerBee { get; set; }
        public static float MoveSpeedBee { get; set; }
        public static int HealthSlug { get; set; }
        public static int AttackPowerSlug { get; set; }
        public static float MoveSpeedSlug { get; set; }
        public static void Initialize()
        {
            configurationData = new ConfigurationData();
            // setting data
            HealthBee = configurationData.HealthBee;
            AttackPowerBee = configurationData.AttackPowerBee;
            MoveSpeedBee = configurationData.MoveSpeedBee;

            HealthSlug = configurationData.HealthSlug;
            AttackPowerSlug = configurationData.AttackPowerSlug;
            MoveSpeedSlug = configurationData.MoveSpeedSlug;
        }
    }
}
