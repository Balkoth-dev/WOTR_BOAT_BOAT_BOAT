using HarmonyLib;
using System;
using System.IO;
using UnityModManagerNet;
using WOTR_BOAT_BOAT_BOAT.Utilities;
using ModKit;
using Kingmaker;
using Kingmaker.PubSubSystem;
using WOTR_BOAT_BOAT_BOAT.MechanicsChanges;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.Blueprints.Root;
using System.Linq;
using BlueprintCore.Utils;
using UnityEngine;
using Kingmaker.Controllers.Units;
using Kingmaker.UnitLogic;
using Kingmaker.GameModes;
using Kingmaker.Dungeon;
using System.Collections.Generic;
using Kingmaker.EntitySystem.Entities;
using WOTR_BOAT_BOAT_BOAT.Methods;

namespace WOTR_BOAT_BOAT_BOAT
{
    public class Main
    {
        public class Settings : UnityModManager.ModSettings
        {
            public bool toggleAllowAchievementsDuringModdedGame = false;
            public bool toggleNoShame = false;
            public bool toggleDeadIsDead = false;
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
            /*settings = Settings.Load<Settings>(modEntry);
            var settingsFile = Path.Combine(modEntry.Path, "Settings.bak");
            var copyFile = Path.Combine(modEntry.Path, "Settings.xml");
            if (File.Exists(settingsFile) && !File.Exists(copyFile))
            {
                File.Copy(settingsFile, copyFile, false);
            }
            settings = Settings.Load<Settings>(modEntry);*/
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
            modEntry.OnUpdate = OnUpdate;
            harmony.PatchAll();
            EventBus.Subscribe(new InjectStuffOnLoad());
            InjectStuffOnLoad.Injections.Add(() => { NoShame(); });
            return true;
        }

        private static void OnGUI(UnityModManager.ModEntry obj)
        {
            var dungeonRoot = BlueprintTool.Get<BlueprintDungeonRoot>("096f36d4e55b49129ddd2211b2c50513");
            UI.AutoWidth(); UI.Div(0, 15);
            using (UI.VerticalScope())
            {
                /*
                UI.Toggle("No Shame Mode".bold(), ref settings.toggleNoShame);
                if (settings.toggleNoShame)
                {
                    UI.Label("Your attempts number will always be set to zero. Toggling this off will cause the game to count your attempts normally.".green().size(10));
                }
                else
                {
                    UI.Label("Toggling this option on will cause your number of attempts to always be displayed as zero.".red().size(10));
                }
                UI.Toggle("Dead Is Dead".bold(), ref settings.toggleDeadIsDead);
                if (settings.toggleDeadIsDead)
                {
                    UI.Label("When a party member dies, your save is copied to a backup file called \"BBB_Backup.bak\" and then deleted.".green().size(10));
                }
                else
                {
                    UI.Label("When toggled on, when a party member dies, your save is copied to a backup file and then deleted.".red().size(10));
                }
                UI.Toggle("Allow Achivements With Modded Game".bold(), ref settings.toggleAllowAchievementsDuringModdedGame);
                if (settings.toggleAllowAchievementsDuringModdedGame)
                {
                    UI.Label("Taken from ToyBox, allows achievements to be used with a modded game. If you use ToyBox you don't need this.".green().size(10));
                }
                else
                {
                    UI.Label("Taken from ToyBox, allows achievements to be used with a modded game. If you use ToyBox you don't need this.".red().size(10));
                }
                UI.Label("CHEAT BOON:".grey().bold().size(20));
                foreach (var v in dungeonRoot.m_Boons)
                {
                    var x = (BlueprintDungeonBoon)v.GetBlueprint();
                    if (GUILayout.Button(x.Name, GUILayout.Width(250)))
                    {
                        ApplyBoon(x);
                    }
                }
                */
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
        public static void AddBoonOnAreaLoad(BlueprintDungeonBoon dungeonBoon, bool inject)
        {
#if DEBUG
            if (inject)
            {
                var dungeonRoot = BlueprintTool.Get<BlueprintDungeonRoot>("096f36d4e55b49129ddd2211b2c50513");
                dungeonRoot.m_Boons = new BlueprintDungeonBoonReference[0];
                for (int i = 0; i < 3; i++)
                {
                    var dungeonBoonRef = new BlueprintDungeonBoonReference() { deserializedGuid = dungeonBoon.AssetGuid };
                    dungeonRoot.m_Boons = dungeonRoot.m_Boons.AppendToArray(dungeonBoonRef);
                }
                InjectStuffOnLoad.Injections.Add(() => { SetBoon(); });
            }
#endif            
        }
        public static void SetBoon()
        {
            Game.Instance.Player.DungeonState.StageIndex = 999;
            Game.Instance.Player.DungeonState.Statistic.StageIndexBest = 998;
        }

        public static void NoShame()
        {
            if (settings.toggleNoShame)
            {
                Game.Instance.Player.DungeonState.Statistic.DelveNumber = 0;
            }
        }

        public static void ApplyBoon(BlueprintDungeonBoon bd)
        {
            Game.Instance.Player.DungeonState.SelectBoon(bd);
            foreach (var p in Game.Instance.Player.PartyCharacters)
                Game.Instance.Player.DungeonState.ApplyBoon(p);
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
        static void OnUpdate(UnityModManager.ModEntry modEntry, float delta)
        {
#if DEBUG
            if (Input.GetKeyDown(KeyCode.C) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                UnitLifeController.SetLifeState(Game.Instance.Player.MainCharacter, UnitLifeState.Dead);
            }
#endif
            if (Input.GetKeyDown(KeyCode.S) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            {
                Game.Instance.SaveGame(Game.Instance.SaveManager.GetNextAutoslot(), null);
                Game.Instance.ResetToMainMenu();
            }
            DeadIsDead.SaveGameWhenDeath();
        }

    }
}
