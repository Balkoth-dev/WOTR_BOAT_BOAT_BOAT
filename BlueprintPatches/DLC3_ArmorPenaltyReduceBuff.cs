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
        [HarmonyPriority(Priority.Last)]
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                DLC3_ArmorPenaltyReduceBuff_Patch();
                Main.Log("DLC3_ArmorPenaltyReduceBuff_Patch");

            }

            private static void DLC3_ArmorPenaltyReduceBuff_Patch()
            {
                var dungeonBoon_ArcaneArmor = BlueprintTool.Get<BlueprintDungeonBoon>("e27cec242d6b4f8299126c9abe62505e");
                if(!Settings.Settings.GetSetting<bool>("dungeonBoon_ArcaneArmor"))
                {
                    return;
                }
                var dLC3_ArmorPenaltyReduceBuff = BlueprintTool.Get<BlueprintBuff>("1c8d105f94f94017a119719a5623fccd");

                var newDescription = Helpers.GetLocalizationElement("description", "dungeonBoon_ArcaneArmor");

                dLC3_ArmorPenaltyReduceBuff.m_Description = Helpers.CreateString(dLC3_ArmorPenaltyReduceBuff + ".Description", newDescription);
                dungeonBoon_ArcaneArmor.m_Description = Helpers.CreateString(dLC3_ArmorPenaltyReduceBuff + ".Description", newDescription);

                var p = dungeonBoon_ArcaneArmor;
                
            }
        }
    }
}
