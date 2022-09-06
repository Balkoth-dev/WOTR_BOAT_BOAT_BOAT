using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOTR_BOAT_BOAT_BOAT.MechanicsChanges;
using WOTR_BOAT_BOAT_BOAT.Utilities;

namespace WOTR_BOAT_BOAT_BOAT.Patches
{
    class DLC3_ArmorPenaltyReduceBuff
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                DLC3_ArmorPenaltyReduceBuff_Patch();

            }

            private static void DLC3_ArmorPenaltyReduceBuff_Patch()
            {
                var dLC3_ArmorPenaltyReduceBuff = BlueprintTool.Get<BlueprintBuff>("1c8d105f94f94017a119719a5623fccd");
                var dungeonBoon_ArcaneArmor = BlueprintTool.Get<BlueprintDungeonBoon>("e27cec242d6b4f8299126c9abe62505e");

                var newDescription = "Arcane spell failure chance for wearing armor is reduced by 20% for you and your party members. When a spell is cast it does bonus damage equal to 3% for each 1% of their arcane spell failure, before reductions, even if it's not an arcane spell.";

                dLC3_ArmorPenaltyReduceBuff.m_Description = Helpers.CreateString(dLC3_ArmorPenaltyReduceBuff + ".Description", newDescription);
                dungeonBoon_ArcaneArmor.m_Description = Helpers.CreateString(dLC3_ArmorPenaltyReduceBuff + ".Description", newDescription);

                Main.AddBoonOnAreaLoad(dungeonBoon_ArcaneArmor,false);

                var p = dungeonBoon_ArcaneArmor;
                Main.Log(p.Name + " - " + p.Description);
            }
        }
    }
}
