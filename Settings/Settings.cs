using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Dungeon.Blueprints;
using ModMenu.Settings;
using System;
using System.Collections.Generic;
using WOTR_BOAT_BOAT_BOAT.Other;
using WOTR_BOAT_BOAT_BOAT.Utilities;

namespace WOTR_BOAT_BOAT_BOAT.Settings
{
    static class Settings
    {
        public static readonly string RootKey = "wotr-boat-boat-boat.settings";
        public static T GetSetting<T>(string key)
        {
            return ModMenu.ModMenu.GetSettingValue<T>(GetKey("noshame"));
        }
        private static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}";
        }
    }
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    static class BlueprintsCache_Init_Patch
    {
        static bool Initialized;
        private static Dictionary<BlueprintGuid, string> blueprintBoonGuids = new Dictionary<BlueprintGuid, string>();

        static void Postfix()
        {
            if (Initialized) return;
            Initialized = true;

            SettingsUI.Initialize();

        }
        class SettingsUI
        {
            private static readonly string RootKey = Settings.RootKey;
            public static void Initialize()
            {
                var dungeonRoot = BlueprintTool.Get<BlueprintDungeonRoot>("096f36d4e55b49129ddd2211b2c50513");
                var sb = SettingsBuilder.New(RootKey, Helpers.CreateString(GetKey("title"), "BOAT BOAT BOAT"));

                sb.AddImage(AssetLoader.LoadInternal("Settings", "boatboatboat.png",512,212),212);
                sb.AddSubHeader(Helpers.CreateString(GetKey("subheader1"), "Settings"));
                sb.AddToggle(Toggle.New(GetKey("noshame"), defaultValue: false, Helpers.CreateString(GetKey("noshametoggle-desc"), "No Shame")).ShowVisualConnection().OnValueChanged(OnToggle).WithLongDescription(Helpers.CreateString(GetKey("noshametoggle-long-desc"), "Toggling this option on will cause your number of attempts to always be displayed as zero.")));
                sb.AddToggle(Toggle.New(GetKey("deadisdead"), defaultValue: false, Helpers.CreateString(GetKey("deadisdeadtoggle-desc"), "Dead Is Dead")).ShowVisualConnection().OnValueChanged(OnToggle).WithLongDescription(Helpers.CreateString(GetKey("deadisdeadtoggle-long-desc"), "When a party member dies, your save is copied to a backup file called \"BBB_Backup.bak\" and then deleted.")));
                sb.AddToggle(Toggle.New(GetKey("cheevos"), defaultValue: false, Helpers.CreateString(GetKey("cheevostoggle-desc"), "Cheevos")).ShowVisualConnection().OnValueChanged(OnToggle).WithLongDescription(Helpers.CreateString(GetKey("cheevostoggle-long-desc"), "RESTART REQUIRED!\nTaken from ToyBox, allows achievements to be used with a modded game. If you use ToyBox you don't need this.")));

                sb.AddSubHeader(Helpers.CreateString(GetKey("subheader2"), "Cheats"));

                foreach (var v in dungeonRoot.m_Boons)
                {
                    var bdb = (BlueprintDungeonBoon)v.GetBlueprint();
                    blueprintBoonGuids.Add(bdb.AssetGuid, bdb.Name);
                }
                
                ModMenu.ModMenu.AddSettings(sb);
            }

            private static Action CheatBoon(string blueprintGuid)
            {
                var blueprintBoon = BlueprintTool.Get<BlueprintDungeonBoon>("blueprintGuid");
                ApplyBoon.Apply(blueprintBoon);
                Main.Log("CheatBoon Clicked");
                return null;
            }

            private static string GetKey(string partialKey)
            {
                return $"{RootKey}.{partialKey}";
            }
            private static void OnToggle(bool value)
            {
                
            }
        }
    }
}
