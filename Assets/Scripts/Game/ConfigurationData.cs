using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class ConfigurationData
    {
        const string ConfigurationDataFileName = "ConfigEnemy.csv";

        public int HealthBee { get; set; }
        public int AttackPowerBee { get; set; }
        public float MoveSpeedBee { get; set; }        
        public int HealthSlug { get; set; }
        public int AttackPowerSlug { get; set; }
        public float MoveSpeedSlug { get; set; }

        public ConfigurationData()
        {
            // read and save configuration data from file

            using (StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName)))
            {
                try
                {
                    // Read the second line of the file, which contains the values.
                    string line = reader.ReadLine();
                    line = reader.ReadLine();
                    line = reader.ReadLine().Trim();
                    // Set the configuration data fields using the extracted values.
                    SetConfigurationDataFields(line.Substring(1, line.Length - 2));
                }
                catch (Exception)
                {
                    // configuration data with default values
                    HealthBee = 2;
                    AttackPowerBee = 1;
                    MoveSpeedBee = 0.6f;      
                    
                    HealthSlug = 3;
                    AttackPowerSlug = 2;
                    MoveSpeedSlug = 0.3f;
                }
                finally
                {
                    // Close the file if it's not null.
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
            }
        }

        void SetConfigurationDataFields(string csvValues)
        {
            // Split the line into an array of strings.
            string[] values = csvValues.Split('|');
            string[] valueBee = values[0].Split(",");
            string[] valueSlug = values[1].Split(",");

            HealthBee = int.Parse(valueBee[0]);
            AttackPowerBee = int.Parse(valueBee[1]);
            MoveSpeedBee = float.Parse(valueBee[2]);

            HealthSlug = int.Parse(valueSlug[0]);
            AttackPowerSlug = int.Parse(valueSlug[1]);
            MoveSpeedSlug = float.Parse(valueSlug[2]);
        }
    }
}
