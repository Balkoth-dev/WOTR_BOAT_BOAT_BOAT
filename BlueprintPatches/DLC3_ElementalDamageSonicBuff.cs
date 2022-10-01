using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOTR_BOAT_BOAT_BOAT.Utilities;

namespace WOTR_BOAT_BOAT_BOAT.BlueprintPatches
{
    class DLC3_ElementalDamageSonicBuff
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

                DLC3_ElementalDamageSonicBuff_Patch();
                Main.Log("DLC3_ElementalDamageSonicBuff_Patch");

            }

            private static void DLC3_ElementalDamageSonicBuff_Patch()
            {
                var dungeonBoon_Sonic = BlueprintTool.Get<BlueprintDungeonBoon>("57dbbe89c45a48c9a19196e206064273");
                if (!Settings.Settings.GetSetting<bool>("dungeonBoon_Sonic"))
                {
                    return;
                }
                var dLC3_ElementalDamageSonicBuff = BlueprintTool.Get<BlueprintBuff>("f7e94934bcda4b16b9d6d24e0b745283");

                var newDescription = Helpers.GetLocalizationElement("description", "dungeonBoon_Sonic");
                dLC3_ElementalDamageSonicBuff.EditComponent<EnergyDamageBonus>(c => { c.Multiplier = (float)1.5; });

                dLC3_ElementalDamageSonicBuff.m_Description = Helpers.CreateString(dLC3_ElementalDamageSonicBuff + ".Description", newDescription);
                dungeonBoon_Sonic.m_Description = Helpers.CreateString(dungeonBoon_Sonic + ".Description", newDescription);
                
            }
        }
    }
}
