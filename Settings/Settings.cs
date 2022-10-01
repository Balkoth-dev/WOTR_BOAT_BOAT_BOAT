using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Dungeon;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.Localization;
using Kingmaker.UI;
using ModMenu.Settings;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WOTR_BOAT_BOAT_BOAT.Other;
using WOTR_BOAT_BOAT_BOAT.Utilities;

namespace WOTR_BOAT_BOAT_BOAT.Settings
{
    public static class Settings
    {
        public static BlueprintGuid BoonGuid;
        public static readonly string RootKey = "wotr-boat-boat-boat.settings";
        public static List<BlueprintDungeonBoon> editedBoons = new List<BlueprintDungeonBoon>();
        public static T GetSetting<T>(string key)
        {
                return ModMenu.ModMenu.GetSettingValue<T>(GetKey(key));
        }
        private static string GetKey(string partialKey)
        {
            Regex rgx = new Regex("[^a-z0-9-]");
            partialKey = rgx.Replace(partialKey.ToLower(), "");
            return $"{RootKey}.{partialKey}";
        }
    }
    [HarmonyPatch(typeof(BlueprintsCache))]
    static class BlueprintsCache_Postfix
    {
        static bool Initialized;

        [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
        static void Postfix()
        {
            if (Initialized) return;
            Initialized = true;
            
            SettingsUI.Initialize();
            Main.Log("Settings Initialized");
        }
        class SettingsUI
        {
            private static readonly string RootKey = Settings.RootKey;
            private static readonly SettingsBuilder sb = SettingsBuilder.New(RootKey, Helpers.CreateString(GetKey("title"), "BOAT BOAT BOAT"));
            private static readonly Kingmaker.Dungeon.Blueprints.BlueprintDungeonBoonReference[] dungeonBoons = BlueprintTool.Get<BlueprintDungeonRoot>("096f36d4e55b49129ddd2211b2c50513").m_Boons;
            private static readonly List<LocalizedString> boons = new List<LocalizedString>();
            public static void Initialize()
            {
                sb.AddImage(AssetLoader.LoadInternal("Settings", "boatboatboat.png",512,212),212);

                CreateSubHeader("settingssubheader");
                CreateToggle("noshametoggle");
                CreateToggle("deadisdeadtoggle");
                CreateToggle("cheevostoggle");

                CreateSubHeader("hotkeysubheader");
                CreateKeyBinding("saveandexitkeybinding", () => Cheats.RunCheat(Cheats.SaveAndExit));
                CreateKeyBinding("showmapkeybinding", () => Cheats.RunCheat(() => DungeonController.ShowMapCheat()));
                CreateKeyBinding("teleportbacktoshipkeybinding", () => Cheats.RunCheat(Cheats.TeleportBackToBoatCheat));
                CreateKeyBinding("teleportbacktoislandkeybinding", () => Cheats.RunCheat(Cheats.TeleportBackToIslandCheat));
                CreateKeyBinding("teleportbacktoportkeybinding", () => Cheats.RunCheat(Cheats.TeleportBackToPort));
                CreateKeyBinding("startoverkeybinding", () => Cheats.RunCheat(Cheats.StartOver));

                CreateSubHeader("teleportcheatssubheader");
                CreateButton("teleportbacktoship", () => Cheats.RunCheat(Cheats.TeleportBackToBoatCheat));
                CreateButton("teleportbacktoisland",() => Cheats.RunCheat(Cheats.TeleportBackToIslandCheat));
                CreateButton("teleportbacktoport", () => Cheats.RunCheat(Cheats.TeleportBackToPort));

                CreateSubHeader("expeditioncheatssubheader");
                CreateButton("finishcurrentisland",() => Cheats.RunCheat(Cheats.FinishCurrentIslandCheat));
                CreateButton("movetonextisland",() => Cheats.RunCheat(Cheats.MoveToNextIslandCheat));
                CreateButton("completeexpedition",() => Cheats.RunCheat(Cheats.CompleteExpedition));

                CreateSubHeader("misccheatssubheader");
                CreateButton("showmapbutton", () => Cheats.RunCheat(() => DungeonController.ShowMapCheat()));
/*
                foreach(var v in dungeonBoons)
                {
                    boons.Add(Helpers.CreateString(GetKey(v.Guid.ToString()), BlueprintTool.Get<BlueprintDungeonBoon>(v.deserializedGuid.ToString()).Name));
                }

                CreateDropdownButton("addBoon", SetBoonIndexAndApplyBoon,boons);
                
                CreateSubHeader("patchessubheader");
                foreach(var patch in AssetLoader.GetElements("Patch"))
                {
                    CreatePatchToggle(patch.key,patch.name,patch.description,true);
                }
*/
                ModMenu.ModMenu.AddSettings(sb);
            }
            private static void CreateSubHeader(string key)
            {
                sb.AddSubHeader(Helpers.CreateString(GetKey(key), Helpers.GetLocalizationElement("value", key)));
            }
            private static void CreateToggle(string key, bool defaultBool = false)
            {
                sb.AddToggle(Toggle.New(GetKey(key), defaultValue: defaultBool, Helpers.CreateString(GetKey(key+"-desc"), Helpers.GetLocalizationElement("description", key)))
                    .ShowVisualConnection()
                    .OnValueChanged(OnToggle)
                    .WithLongDescription(Helpers.CreateString(GetKey(key + "-long-desc"), Helpers.GetLocalizationElement("longDescription", key))));
            }
            private static void CreatePatchToggle(string key, string name, string longDescription, bool defaultBool = false)
            {
                sb.AddToggle(Toggle.New(GetKey(key), defaultValue: defaultBool, Helpers.CreateString(GetKey(key + "-desc"), name))
                    .ShowVisualConnection()
                    .OnValueChanged(OnToggle)
                    .WithLongDescription(Helpers.CreateString(GetKey(key + "-long-desc"), longDescription)));
            }

            private static void CreateButton(string key, Action action)
            {
                sb.AddButton(Button.New(Helpers.CreateString(GetKey(key + "-desc"), Helpers.GetLocalizationElement("description", key)), Helpers.CreateString(GetKey(key + "-name"), Helpers.GetLocalizationElement("name", key)), action)
                    .WithLongDescription(Helpers.CreateString(GetKey(key + "-longDesc"), Helpers.GetLocalizationElement("longDescription", key))));
            }
            private static void CreateDropdownButton(string key, Action<int> action,List<LocalizedString> list)
            {
                sb.AddDropdownButton(DropdownButton.New(GetKey(key),0,Helpers.CreateString(GetKey(key + "-desc"), Helpers.GetLocalizationElement("description", key)), Helpers.CreateString(GetKey(key + "-buttontext"), Helpers.GetLocalizationElement("buttonText", key)),action,list)
                    .WithLongDescription(Helpers.CreateString(GetKey(key + "-longDesc"), Helpers.GetLocalizationElement("longDescription", key))));
            }
            private static void CreateKeyBinding(string key, Action action, KeyboardAccess.GameModesGroup gamesModeGroup = KeyboardAccess.GameModesGroup.All, UnityEngine.KeyCode firstKey = UnityEngine.KeyCode.None, bool withctrl = false)
            {
                sb.AddKeyBinding(KeyBinding.New(GetKey(key), gamesModeGroup, Helpers.CreateString(GetKey(key + "-longDesc"), Helpers.GetLocalizationElement("longDescription", key))).SetPrimaryBinding(firstKey, withctrl), action);
            }
            private static void SetBoonIndexAndApplyBoon(int i)
            {
                var boonSelected = boons[i];
                var bf = boonSelected.Key.ToString().Replace($"{RootKey}.", "");
                var b = new BlueprintGuid(Guid.Parse(bf));
                Main.Log(b.ToString());
                Settings.BoonGuid = b;
                Main.Log(Settings.BoonGuid.ToString());
                ApplyBoon.Apply();
            }
            private static string GetKey(string partialKey)
            {
                Regex rgx = new Regex("[^a-z0-9-]");
                partialKey = rgx.Replace(partialKey.ToLower(), "");
                return $"{RootKey}.{partialKey}";
            }
            private static void OnToggle(bool value)
            {
                
            }
        }
    }
}
