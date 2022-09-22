using Kingmaker;
using Kingmaker.Dungeon.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOTR_BOAT_BOAT_BOAT.Other
{
    public class ApplyBoon
    {
        public static void Apply(BlueprintDungeonBoon bd)
        {
            Game.Instance.Player.DungeonState.SelectBoon(bd);
            foreach (var p in Game.Instance.Player.PartyCharacters)
                Game.Instance.Player.DungeonState.ApplyBoon(p);
        }
    }
}
