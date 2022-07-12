using System;
using System.Windows;
using Newtonsoft.Json;
using ValoAfk.Properties;

namespace ValoAfk.controllers
{
    public static class ConfigStore
    {
        public static void Store(Config config)
        {
            try
            {
                Settings.Default.config = JsonConvert.SerializeObject(config);
                Settings.Default.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static Config Load(
        )
        {
            return JsonConvert.DeserializeObject<Config>(Settings.Default.config);
        }
    }
}