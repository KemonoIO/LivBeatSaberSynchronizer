using System;
using System.IO;
using Newtonsoft.Json;

namespace LivBeatSaberSynchronizer
{
    class SettingsPersistence
    {
        readonly Logger logger;

        readonly static string path = Utility.GetUserProfilePath() +
            @"\Documents\LIV\Plugins\CameraBehaviours\LivBeatSaberSynchronizer.json";

        public SettingsPersistence(Logger logger)
        {
            this.logger = logger;
        }

        public Settings Load()
        {
            bool fileExists = File.Exists(path);
            if (fileExists)
            {
                try
                {
                    string json = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<Settings>(json);
                }
                catch (Exception e)
                {
                    logger.Log("Settings deserialization failed with the following message:");
                    logger.Log(e.Message);
                }
            }
            Settings settings = new Settings();
            if (!fileExists)
            {
                Save(settings);
            }
            return settings;
        }

        void Save(Settings settings)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}