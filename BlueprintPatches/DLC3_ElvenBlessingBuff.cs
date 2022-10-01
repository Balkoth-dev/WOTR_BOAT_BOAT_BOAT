using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Loot;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.Dungeon.Blueprints.Boons;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOTR_BOAT_BOAT_BOAT.MechanicsChanges;
using WOTR_BOAT_BOAT_BOAT.Utilities;

namespace WOTR_BOAT_BOAT_BOAT.BlueprintPatches
{
    class DLC3_ElvenBlessingBuff
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

                DLC3_ElvenBlessingBuff_Patch();
                Main.Log("DLC3_ElvenBlessingBuff_Patch");

            }

            private static void DLC3_ElvenBlessingBuff_Patch()
            {
                var dungeonBoon_Elven = BlueprintTool.Get<BlueprintDungeonBoon>("dbb242c6be10406799fd659feb40d266");
                if (!Settings.Settings.GetSetting<bool>("dungeonBoon_Elven"))
                {
                    return;
                }
                var dLC3_ElvenBlessingBuff = BlueprintTool.Get<BlueprintBuff>("8b845d7f13c4402fb70f50e90bd407ad");

                var x = Helpers.Create<WeaponCriticalEdgeIncreaseStackable>();
                x.Value = 1;
                x.IncludeCategories = new WeaponCategory[1];
                x.IncludeCategories = x.IncludeCategories.AppendToArray(WeaponCategory.ElvenCurvedBlade);
                x.IncludeAttackTypes = new Kingmaker.RuleSystem.AttackType[1];
                x.IncludeAttackTypes = x.IncludeAttackTypes.AppendToArray(Kingmaker.RuleSystem.AttackType.Melee);

                dLC3_ElvenBlessingBuff.AddComponent<WeaponCriticalEdgeIncreaseStackable>(c => { c = x; });
                var newDescription = Helpers.GetLocalizationElement("description", "dungeonBoon_Elven");

                dLC3_ElvenBlessingBuff.m_Description = Helpers.CreateString(dLC3_ElvenBlessingBuff + ".Description", newDescription);
                dungeonBoon_Elven.m_Description = Helpers.CreateString(dungeonBoon_Elven + ".Description", newDescription);
                

            }
        }
    }
}
