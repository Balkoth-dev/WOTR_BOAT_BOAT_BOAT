using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Cheats;
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
            Regex rgx = new Regex("[^a-z0-9-.]");
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
            private static readonly List<LocalizedString> boonsLocalizedStringList = new List<LocalizedString>();
            private static readonly List<string> patchBoons = new List<string>()
            {
                "DungeonBoon_ArcaneArmor",
                "DungeonBoon_BarbarianHP",
                "DungeonBoon_Bludgeoning",
                "DungeonBoon_BonusDmgBows",
                "DungeonBoon_Dwarven",
                "DungeonBoon_Acid",
                "DungeonBoon_Cold",
                "DungeonBoon_Electric",
                "DungeonBoon_Fire",
                "DungeonBoon_Sonic",
                "DungeonBoon_Elven",
                "DungeonBoon_Exotic",
                "DungeonBoon_Rogues",
                "DungeonBoon_Slashing",
                "DungeonBoon_UnarmedStrikes"
            };
            private static readonly List<LocalizedString> trainingDummiesLocalizedStringList = new List<LocalizedString>()
            {
                BlueprintTool.Get<BlueprintUnit>("2c0fd840d87c3b4418ae958c9a813fe0").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("16d3e924302e06341914555b7d0c2039").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("11f98b11ab989ed4aa407f778e0262dc").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("eeb29b8b182f23e4a9a34c5520ae8571").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("00a10a2129fc7de45889a7f10e06ea92").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("b80f9014b742ea04ba27f897177bafc4").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("b3ff82b3a3e578342a5edc1880f1480c").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("dcb1e8beb63096a45a813623b9410139").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("5440e7be8185f2d4eafbb89f5925feab").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("bd08faf0a5d8a9d4d97613b95c1f1cee").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("667f0ff46d87bca498cb8f410057be03").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("ec9ff26ca80461348891cdd268a58753").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("5cc2717c960068645a1b1f78436a7b34").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("cd13c2f54d222234582aca645cde044d").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("4932555b9d789964eb41183412c8b339").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("3ab5715f9010a09408fbd307c7731c4c").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("981b08bddac11ca41b3698d3f6059629").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("04867a7006ab04140a4dd45d79d2ce1b").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("60a35995607d493c85a8118dd9da993b").LocalizedName.String,
                BlueprintTool.Get<BlueprintUnit>("d054e06bcb0242b0b3925d888a192cdd").LocalizedName.String
            };
            public static void Initialize()
            {
                sb.AddImage(AssetLoader.LoadInternal("Settings", "boatboatboat.png", 512, 212), 212);

                CreateSubHeader("settingssubheader");
                CreateToggle("noshametoggle");
                CreateToggle("deadisdeadtoggle");
                CreateToggle("cheevostoggle");

                CreateSubHeader("hotkeysubheader");
                CreateKeyBinding("saveandexitkeybinding", () => BBB_Cheats.RunCheat(BBB_Cheats.SaveAndExit));
                CreateKeyBinding("showmapkeybinding", () => BBB_Cheats.RunCheat(() => DungeonController.ShowMapCheat()));
                CreateKeyBinding("teleportbacktoshipkeybinding", () => BBB_Cheats.RunCheat(BBB_Cheats.TeleportBackToBoatCheat));
                CreateKeyBinding("teleportbacktoislandkeybinding", () => BBB_Cheats.RunCheat(BBB_Cheats.TeleportBackToIslandCheat));
                CreateKeyBinding("teleportbacktoportkeybinding", () => BBB_Cheats.RunCheat(BBB_Cheats.TeleportBackToPort));
                CreateKeyBinding("startoverkeybinding", () => BBB_Cheats.RunCheat(BBB_Cheats.StartOver));
                CreateKeyBinding("restall", () => BBB_Cheats.RunCheat(CheatsCombat.RestAll));
                CreateKeyBinding("killyou", () => BBB_Cheats.RunCheat(CheatsCombat.Kill));
                CreateKeyBinding("killall", () => BBB_Cheats.RunCheat(CheatsCombat.KillAll));

                CreateSubHeader("trainingdummiessubheader");
                
                CreateDropdown("trainingdummiesdropdown", trainingDummiesLocalizedStringList);
                CreateKeyBinding("trainingdummies", () => BBB_Cheats.RunCheat(BBB_Cheats.SummonTrainingDummy));

                CreateSubHeader("teleportcheatssubheader");
                CreateButton("teleportbacktoship", () => BBB_Cheats.RunCheat(BBB_Cheats.TeleportBackToBoatCheat));
                CreateButton("teleportbacktoisland", () => BBB_Cheats.RunCheat(BBB_Cheats.TeleportBackToIslandCheat));
                CreateButton("teleportbacktoport", () => BBB_Cheats.RunCheat(BBB_Cheats.TeleportBackToPort));

                CreateSubHeader("expeditioncheatssubheader");
                CreateButton("finishcurrentisland", () => BBB_Cheats.RunCheat(BBB_Cheats.FinishCurrentIslandCheat));
                CreateButton("movetonextisland", () => BBB_Cheats.RunCheat(BBB_Cheats.MoveToNextIslandCheat));
                CreateButton("completeexpedition", () => BBB_Cheats.RunCheat(BBB_Cheats.CompleteExpedition));

                CreateSubHeader("misccheatssubheader");
                CreateButton("showmapbutton", () => BBB_Cheats.RunCheat(() => DungeonController.ShowMapCheat()));

                foreach (var v in dungeonBoons)
                {
                    boonsLocalizedStringList.Add(Helpers.CreateString(GetKey(v.Guid.ToString()), BlueprintTool.Get<BlueprintDungeonBoon>(v.deserializedGuid.ToString()).Name));
                }

                CreateDropdownButton("addBoon", SetBoonIndexAndApplyBoon, boonsLocalizedStringList);

                CreateSubHeader("patchessubheader");
                foreach (var boonRef in patchBoons)
                {
                    CreatePatchToggle(boonRef, true);
                }

                ModMenu.ModMenu.AddSettings(sb);
            }
            private static void CreateSubHeader(string key)
            {
                sb.AddSubHeader(Helpers.CreateString(GetKey(key), Helpers.GetLocalizationElement("value", key)));
            }
            private static void CreateToggle(string key, bool defaultBool = false)
            {
                sb.AddToggle(Toggle.New(GetKey(key), defaultValue: defaultBool, Helpers.CreateString(GetKey(key + "-desc"), Helpers.GetLocalizationElement("description", key)))
                    .ShowVisualConnection()
                    .OnValueChanged(OnToggle)
                    .WithLongDescription(Helpers.CreateString(GetKey(key + "-long-desc"), Helpers.GetLocalizationElement("longDescription", key))));
            }
            private static void CreatePatchToggle(string key, bool defaultBool = false)
            {
                if (Helpers.GetLocalizationElement("assetguid", key, ".") != null)
                {
                    sb.AddToggle(Toggle.New(GetKey(key), defaultValue: defaultBool, Helpers.CreateString(GetKey(key + "-desc"), Helpers.GetLocalizationElement("assetguid", key, ".")))
                        .ShowVisualConnection()
                        .OnValueChanged(OnToggle)
                        .WithLongDescription(Helpers.CreateString(GetKey(key + "-long-desc"), Helpers.GetLocalizationElement("Description", key, "."))));
                    Main.Log(GetKey(key) + " Created");
                }
            }

            private static void CreateButton(string key, Action action)
            {
                sb.AddButton(Button.New(Helpers.CreateString(GetKey(key + "-desc"), Helpers.GetLocalizationElement("description", key)), Helpers.CreateString(GetKey(key + "-name"), Helpers.GetLocalizationElement("name", key)), action)
                    .WithLongDescription(Helpers.CreateString(GetKey(key + "-longDesc"), Helpers.GetLocalizationElement("longDescription", key))));
            }
            private static void CreateDropdown(string key, List<LocalizedString> values)
            {
                foreach(var v in values)
                {
                    Main.Log(v.Key);
                };
                Main.Log(GetKey(key + "-desc"));
                Main.Log(GetKey(key + "-longDesc"));
                sb.AddDropdownList(DropdownList.New(
                    Helpers.CreateString(GetKey(key + "-desc"), Helpers.GetLocalizationElement("description", key)),
                    0,
                    Helpers.GetLocalizationElement("description", key),
                    values
                    )
                    .WithLongDescription(Helpers.CreateString(GetKey(key + "-longDesc"), Helpers.GetLocalizationElement("longDescription", key))));
                
            }
            private static void CreateDropdownButton(string key, Action<int> action, List<LocalizedString> list)
            {
                sb.AddDropdownButton(DropdownButton.New(GetKey(key), 0, Helpers.CreateString(GetKey(key + "-desc"), Helpers.GetLocalizationElement("description", key)), Helpers.CreateString(GetKey(key + "-buttontext"), Helpers.GetLocalizationElement("buttonText", key)), action, list)
                    .WithLongDescription(Helpers.CreateString(GetKey(key + "-longDesc"), Helpers.GetLocalizationElement("longDescription", key))));
            }
            private static void CreateKeyBinding(string key, Action action, KeyboardAccess.GameModesGroup gamesModeGroup = KeyboardAccess.GameModesGroup.All, UnityEngine.KeyCode firstKey = UnityEngine.KeyCode.None, bool withctrl = false)
            {
                sb.AddKeyBinding(KeyBinding.New(GetKey(key), gamesModeGroup, Helpers.CreateString(GetKey(key + "-longDesc"), Helpers.GetLocalizationElement("longDescription", key))).SetPrimaryBinding(firstKey, withctrl), action);
            }
            private static void SetBoonIndexAndApplyBoon(int i)
            {
                var boonSelected = boonsLocalizedStringList[i];
                var bf = boonSelected.Key.ToString().Replace($"{RootKey}.", "");
                var b = new BlueprintGuid(Guid.Parse(bf));
                Main.Log(b.ToString());
                Settings.BoonGuid = b;
                Main.Log(Settings.BoonGuid.ToString());
                ApplyBoon.Apply();
            }
            private static string GetKey(string partialKey)
            {
                Regex rgx = new Regex("[^a-z0-9-.]");
                partialKey = rgx.Replace(partialKey.ToLower(), "");
                return $"{RootKey}.{partialKey}";
            }
            private static void OnToggle(bool value)
            {

            }
        }
    }
}
