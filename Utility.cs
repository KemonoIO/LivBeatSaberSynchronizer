using System;
using System.Collections.Generic;
using UnityEngine;

namespace LivBeatSaberSynchronizer
{
    class Utility
    {
        public static void GetEmissionMaterials(List<Material> materials)
        {
            if (materials.Count == 0)
            {
                foreach (Material material in UnityEngine.Object.FindObjectsOfType<Material>())
                {
                    if (material.shader.name == "VRM/MToon")
                    {
                        Color emissionColor = material.GetColor("_EmissionColor");
                        if (emissionColor.r != 0 || emissionColor.g != 0 || emissionColor.b != 0)
                        {
                            materials.Add(material);
                        }
                    }
                }
            }
        }

        public static long GetMilliseconds() => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        public static string GetUserProfilePath() => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
}