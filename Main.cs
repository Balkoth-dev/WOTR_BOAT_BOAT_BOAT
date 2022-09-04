using HarmonyLib;
using System;
using System.IO;
using UnityModManagerNet;
using WOTR_BOAT_BOAT_BOAT.Utilities;
using ModKit;

namespace WOTR_BOAT_BOAT_BOAT
{
    public class Main
    {
        public class Settings : UnityModManager.ModSettings
        {
            public override void Save(UnityModManager.ModEntry modEntry)
            {
                Save(this, modEntry);
            }
        }
        public static UnityModManager.ModEntry modInfo = null;
        public static Settings settings; 
        private static bool enabled;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            var harmony = new Harmony(modEntry.Info.Id);
            AssetLoader.ModEntry = modEntry;
            modInfo = modEntry;
            settings = Settings.Load<Settings>(modEntry);
            var settingsFile = Path.Combine(modEntry.Path, "Settings.bak");
            var copyFile = Path.Combine(modEntry.Path, "Settings.xml");
            if (File.Exists(settingsFile) && !File.Exists(copyFile))
            {
                File.Copy(settingsFile, copyFile, false);
            }
            settings = Settings.Load<Settings>(modEntry);
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
            harmony.PatchAll();
            return true;
        }

        private static void OnGUI(UnityModManager.ModEntry obj)
        {
            UI.AutoWidth(); UI.Div(0, 15);
            using (UI.VerticalScope())
            {
                UI.Label("SETTINGS WILL NOT BE UPDATED UNTIL YOU RESTART YOUR GAME.".grey().bold().size(20));
                /*   UI.Toggle("Gold Dragon Spell Damage Fix".bold(), ref settings.PatchGoldDragonSpellDamage);
                   if(settings.PatchGoldDragonSpellDamage)
                   {
                       UI.Label("Spell Damage Dice Progression is changed to work as written. In addition, if an enemy has any energy vulnerability they'll be vulnerable to the attack at mythic rank 10.".green().size(10));
                   }
                   else
                   {
                       UI.Label("Spell Damage Dice Progression is unchanged.".red().size(10));
                   }*/

            }
        }

        private static bool Unload(UnityModManager.ModEntry arg)
        {
            throw new NotImplementedException();
        }
        public static void Log(string msg)
        {
#if DEBUG
            modInfo.Logger.Log(msg);
#endif
        }

        public bool GetSettingValue(string b)
        {
            return true;
        }
        public static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            settings.Save(modEntry);
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            enabled = value;

            return true;
        }

    }
}
