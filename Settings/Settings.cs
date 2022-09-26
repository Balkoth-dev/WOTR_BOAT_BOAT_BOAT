using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Dungeon;
using Kingmaker.Dungeon.Blueprints;
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
            Regex rgx = new Regex("[^a-z0-9-]");
            var KeyName = rgx.Replace(key.ToLower(), "");
            return ModMenu.ModMenu.GetSettingValue<T>(GetKey(KeyName));
        }
        private static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}";
        }
    }
    [HarmonyPatch(typeof(BlueprintsCache))]
    [HarmonyPriority(Priority.Last)]
    static class BlueprintsCache_Postfix
    {
        static bool Initialized;

        [HarmonyPriority(Priority.Last)]
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
            public static void Initialize()
            {
                var sb = SettingsBuilder.New(RootKey, Helpers.CreateString(GetKey("title"), "BOAT BOAT BOAT"));

                sb.AddImage(AssetLoader.LoadInternal("Settings", "boatboatboat.png",512,212),212);
                sb.AddSubHeader(Helpers.CreateString(GetKey("settingsSubheader"), "Settings"));
                sb.AddToggle(Toggle.New(GetKey("noshame"), defaultValue: false, Helpers.CreateString(GetKey("noshametoggle-desc"), "No Shame")).ShowVisualConnection().OnValueChanged(OnToggle).WithLongDescription(Helpers.CreateString(GetKey("noshametoggle-long-desc"), "Toggling this option on will cause your number of attempts to always be displayed as zero.")));
                sb.AddToggle(Toggle.New(GetKey("deadisdead"), defaultValue: false, Helpers.CreateString(GetKey("deadisdeadtoggle-desc"), "Dead Is Dead")).ShowVisualConnection().OnValueChanged(OnToggle).WithLongDescription(Helpers.CreateString(GetKey("deadisdeadtoggle-long-desc"), "When a party member dies, your save is copied to a backup file called \"BBB_Backup.bak\" and then deleted.")));
                sb.AddToggle(Toggle.New(GetKey("cheevos"), defaultValue: false, Helpers.CreateString(GetKey("cheevostoggle-desc"), "Cheevos")).ShowVisualConnection().OnValueChanged(OnToggle).WithLongDescription(Helpers.CreateString(GetKey("cheevostoggle-long-desc"), "RESTART REQUIRED!\nTaken from ToyBox, allows achievements to be used with a modded game. If you use ToyBox you don't need this.")));

                sb.AddSubHeader(Helpers.CreateString(GetKey("teleportcheatssubheader"), "Teleport Cheats"));
                sb.AddButton(CreateButton("BOAT BOAT BOAT", "Teleport back to ship.", () => Cheats.RunCheat(Cheats.TeleportBackToBoatCheat), "Teleports you back to the ship."));
                sb.AddButton(CreateButton("Explorer Time", "Teleport back to your current island.", () => Cheats.RunCheat(Cheats.TeleportBackToIslandCheat), "Teleports you to your current island."));
                sb.AddButton(CreateButton("Little Bit of Chicken Fried", "Teleport Back To Port", () => Cheats.RunCheat(Cheats.TeleportBackToPort), "Teleports you back to port."));
                
                sb.AddSubHeader(Helpers.CreateString(GetKey("expeditioncheatssubheader"), "Expedition Cheats"));
                sb.AddButton(CreateButton("We're moving on", "Finish your current island.", () => Cheats.RunCheat(Cheats.FinishCurrentIslandCheat), "Your current island will be considered complete and you will be teleport back to the ship."));
                sb.AddButton(CreateButton("Magical Boat Warp", "Move to Next Island", () => Cheats.RunCheat(Cheats.MoveToNextIslandCheat), "Completes the current island and moves you to the next in the chain."));
                sb.AddButton(CreateButton("I want new islands!", "Complete Expedition", () => Cheats.RunCheat(Cheats.CompleteExpedition), "Completes the current expedition and teleports you back to port."));
                
                sb.AddSubHeader(Helpers.CreateString(GetKey("misccheatssubheader"), "Misc Cheats"));
                sb.AddButton(CreateButton("I'M THE MAP!", "Show Map", () => Cheats.RunCheat(() => DungeonController.ShowMapCheat()), "Shows the map no matter where you currently are."));

                sb.AddSubHeader(Helpers.CreateString(GetKey("changetailwindsubheaders"), "Change Tailwind (Restart Required)"));
                foreach(var x in Settings.editedBoons)
                {
                    sb.AddToggle(CreateToggle(x.Name,"",x.m_Description,true));
                }
                ModMenu.ModMenu.AddSettings(sb);

                sb.AddSubHeader(Helpers.CreateString(GetKey("settailwindsubheader"), "Set Tailwind"));
                var boonsRefs = BlueprintTool.Get<BlueprintDungeonRoot>("096f36d4e55b49129ddd2211b2c50513").m_Boons;
                foreach(var v in boonsRefs)
                {
                    CreateTailwindButton(sb, v.deserializedGuid);
                }

            }
            private static Toggle CreateToggle(string toggleName, string toggleDescription, string longDescription, bool defaultBool = false)
            {
                Regex rgx = new Regex("[^a-z0-9-]");
                var toggleKeyName = rgx.Replace(toggleName.ToLower(), "");
                return Toggle.New(GetKey(toggleKeyName), defaultValue: defaultBool, Helpers.CreateString(GetKey(toggleKeyName+"-desc"), toggleName)).ShowVisualConnection().OnValueChanged(OnToggle).WithLongDescription(Helpers.CreateString(GetKey(toggleKeyName + "-long-desc"), longDescription));
            }

            private static Button CreateButton(string buttonName, string buttonDescription, Action action, string longDescription)
            {
                Regex rgx = new Regex("[^a-z0-9-]");
                var buttonKeyName = rgx.Replace(buttonName.ToLower(), "");
                return Button.New(Helpers.CreateString(GetKey(buttonKeyName + "-desc"), buttonDescription), Helpers.CreateString(GetKey(buttonKeyName + "-name"), buttonName), action).WithLongDescription(Helpers.CreateString(GetKey(buttonKeyName + "-longDesc"),longDescription));
            }
            private static void CreateTailwindButton(SettingsBuilder sb,BlueprintGuid bpGuid)
            {
                var bd = BlueprintTool.Get<BlueprintDungeonBoon>(bpGuid.ToString());
                sb.AddButton(CreateButton(bd.Name, "Apply " + bd.Name + " Tailwind To Party.", () => SetBoonIndexAndApplyBoon(bpGuid), bd.m_Description));
            }
            private static void SetBoonIndexAndApplyBoon(BlueprintGuid b)
            {
                Settings.BoonGuid = b;
                ApplyBoon.Apply();
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
