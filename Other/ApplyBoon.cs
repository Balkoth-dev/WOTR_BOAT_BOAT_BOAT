using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.GameModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOTR_BOAT_BOAT_BOAT.Other
{
    public class ApplyBoon
    {
        public static async void Apply()
        {
            if(Game.Instance.Player == null)
            {
                return;
            }
            var currentBoonGuid = Settings.Settings.BoonGuid;
            while (Game.Instance.CurrentMode != GameModeType.Default)
            {
                if (currentBoonGuid != Settings.Settings.BoonGuid)
                { return; }
                await Task.Delay(25);
            }
            var bd = BlueprintTool.Get<BlueprintDungeonBoon>(Settings.Settings.BoonGuid.ToString());
            Main.Log("Game State Normal, attempting to apply boon: " + bd.Name);
            Game.Instance.Player.DungeonState.SelectBoon(bd);
            var currentStageIndexBest = Game.Instance.Player.DungeonState.Statistic.StageIndexBest;
            foreach (var p in Game.Instance.Player.AllCharacters)
            {
                Game.Instance.Player.DungeonState.Statistic.StageIndexBest = 999;
                Game.Instance.Player.DungeonState.ApplyBoon(p);
                Game.Instance.Player.DungeonState.Statistic.StageIndexBest = currentStageIndexBest;
            }
        }
    }
}
