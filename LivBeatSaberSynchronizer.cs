using System;
using System.Collections.Generic;
using UnityEngine;

namespace LivBeatSaberSynchronizer
{
    public class PluginSettings : IPluginSettings {}

    public class LivBeatSaberSynchronizer : IPluginCameraBehaviour
    {
        PluginCameraHelper helper;
        
        readonly List<Material> materials = new List<Material>();

        readonly PluginSettings pluginSettings = new PluginSettings();

        readonly StatusListener statusListener;

        public IPluginSettings settings => pluginSettings;

        readonly Settings s;

        public event EventHandler ApplySettings;

        public string author => "Kemono";

        public string ID => "LivBeatSaberSynchronizer";

        public string name => "LivBeatSaberSynchronizer";

        public string version => "0";

        public LivBeatSaberSynchronizer()
        {
            Logger logger = new Logger();
            s = (new SettingsPersistence(logger)).Load();
            statusListener = new StatusListener(logger, new StatusHandler(s, materials));
        }
        
        public void OnActivate(PluginCameraHelper helper)
        {
            this.helper = helper;
            materials.Clear();
            statusListener.Connect();
        }

        public void OnDeactivate()
        {
            statusListener.Disconnect();
            materials.Clear();
            ApplySettings?.Invoke(this, EventArgs.Empty);
        }

        public void OnDestroy() {}

        public void OnFixedUpdate() {}

        public void OnLateUpdate() {}

        public void OnSettingsDeserialized() {}

        [Obsolete]
        public void OnUpdate()
        {
            Utility.GetEmissionMaterials(materials);
            helper.UpdateCameraPose(
                new Vector3(s.cameraPosition[0], s.cameraPosition[1], s.cameraPosition[2]),
                new Quaternion(s.cameraRotation[0], s.cameraRotation[1], s.cameraRotation[2], s.cameraRotation[3]),
                s.cameraFov
            );
        }
    }
}