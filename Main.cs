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
using WOTR_BOAT_BOAT_BOAT.Settings;

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
            if (WOTR_BOAT_BOAT_BOAT.Settings.Settings.GetSetting<bool>("noshame"))
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
            //settings.Save(modEntry);
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
