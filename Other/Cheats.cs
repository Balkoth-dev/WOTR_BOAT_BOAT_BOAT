using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Cheats;
using Kingmaker.Controllers.Units;
using Kingmaker.Dungeon;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.EntitySystem.Persistence;
using Kingmaker.GameModes;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOTR_BOAT_BOAT_BOAT.Other
{
    public static class BBB_Cheats
    {
        public static async void RunCheat(Action action)
        {
            if (Game.Instance.Player == null)
            {
                return;
            }
            while (Game.Instance.CurrentMode != GameModeType.Default)
            {
                await Task.Delay(25);
            }
            action.Invoke();
        }
        public static void TeleportBackToBoatCheat()
        {
            if (Game.Instance.CurrentlyLoadedArea != BlueprintDungeonRoot.Instance.Ship)
                Game.Instance.LoadArea(BlueprintDungeonRoot.Instance.Ship.DefaultPreset.EnterPoint, AutoSaveMode.None);
        }
        public static void TeleportBackToIslandCheat()
        {
            DungeonIslandState islandCurrent = Game.Instance.Player.DungeonState.MapState.IslandCurrent;
            if (islandCurrent.Blueprint.Area == null)
                return;
            Game.Instance.LoadArea(islandCurrent.Blueprint.Area.DefaultPreset.EnterPoint, AutoSaveMode.None);
        }
        public static void MoveToNextIslandCheat()
        {
            DungeonController.MoveToNextIslandCheat(1);
            DungeonIslandState islandCurrent = Game.Instance.Player.DungeonState.MapState.IslandCurrent; 
            if (islandCurrent.Blueprint.Area == null)
                return;
            Game.Instance.LoadArea(islandCurrent.Blueprint.Area.DefaultPreset.EnterPoint, AutoSaveMode.None);
        }
        public static void FinishCurrentIslandCheat()
        {
            DungeonController.FinishCurrentIslandCheat();
            if (Game.Instance.CurrentlyLoadedArea != BlueprintDungeonRoot.Instance.Ship)
                Game.Instance.LoadArea(BlueprintDungeonRoot.Instance.Ship.DefaultPreset.EnterPoint, AutoSaveMode.None);
        }
        public static void CompleteExpedition()
        {
            DungeonController.MoveToNextExpeditionCheat();
            if (Game.Instance.CurrentlyLoadedArea != BlueprintDungeonRoot.Instance.Port)
                Game.Instance.LoadArea(BlueprintDungeonRoot.Instance.Port.DefaultPreset.EnterPoint, AutoSaveMode.None);
        }
        public static void TeleportBackToPort()
        {
            if (Game.Instance.CurrentlyLoadedArea != BlueprintDungeonRoot.Instance.Port)
                Game.Instance.LoadArea(BlueprintDungeonRoot.Instance.Port.DefaultPreset.EnterPoint, AutoSaveMode.None);
        }
        public static void StartOver()
        {
            UnitLifeController.SetLifeState(Game.Instance.Player.MainCharacter, UnitLifeState.Dead);
        }
        public static void SaveAndExit()
        {
            Game.Instance.SaveGame(Game.Instance.SaveManager.GetNextAutoslot(), null);
            Game.Instance.ResetToMainMenu();
        }
        public static void SummonTrainingDummy()
        {
            var trainingDummyBlueprint = BlueprintTool.Get<BlueprintUnit>(Settings.Settings.GetSetting<string>("trainingdummiesdropdown"));
            
            CheatsCombat.SpawnEnemyUnderCursor(trainingDummyBlueprint.name);
        }
    }
}
