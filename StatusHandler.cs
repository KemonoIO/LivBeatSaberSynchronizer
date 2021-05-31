using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace LivBeatSaberSynchronizer
{
    class StatusHandler
    {
        readonly static double DoubleHitMilliseconds = 40;

        readonly List<Material> materials;

        readonly Color colorA;

        readonly Color colorB;

        readonly Color colorNeutral;

        long lastNoteCut = 0;

        string lastSaberType = "";

        public StatusHandler(Settings s, List<Material> materials)
        {
            colorA = new Color(s.colorLeft[0], s.colorLeft[1], s.colorLeft[2]) * s.colorLeft[3];
            colorB = new Color(s.colorRight[0], s.colorRight[1], s.colorRight[2]) * s.colorRight[3];
            colorNeutral = new Color(s.colorNeutral[0], s.colorNeutral[1], s.colorNeutral[2]) * s.colorNeutral[3];
            this.materials = materials;
        }

        public void Handle(MessageEventArgs e)
        {
            JObject data = JObject.Parse(e.Data);
            switch (data["event"].ToString())
            {
                case "hello":
                case "menu":
                    HandleReset();
                    break;
                case "noteCut":
                    HandleNoteCut(data);
                    break;
            }
        }

        void HandleNoteCut(JObject data)
        {
            string saberType = data["noteCut"]["saberType"].ToString();
            if (saberType.StartsWith("Saber"))
            {
                long now = Utility.GetMilliseconds();
                Color color = saberType == "SaberA" ? colorA : colorB;
                bool isDoubleHit = now - lastNoteCut < DoubleHitMilliseconds && lastSaberType != saberType;
                Color emissionColor = isDoubleHit ? colorB : color;
                Color rimColor = isDoubleHit ? colorA : color;
                SetColors(emissionColor, rimColor);
                lastNoteCut = now;
                lastSaberType = saberType;
            }
        }

        void HandleReset()
        {
            SetColors(colorNeutral, colorNeutral);
        }

        void SetColors(Color emissionColor, Color rimColor)
        {
            materials.ForEach(material =>
            {
                material.SetColor("_EmissionColor", emissionColor);
                material.SetColor("_RimColor", rimColor);
            });
        }
    }
}