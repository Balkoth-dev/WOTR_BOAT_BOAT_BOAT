using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Assets.UnitLogic.Mechanics.Properties;
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
using Kingmaker.UnitLogic.Mechanics;
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
    class DLC3_UnramedAttacksBuff
    {
        [HarmonyPriority(Priority.First)]
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                DLC3_UnramedAttacksBuff_Patch();
                Main.Log("DLC3_UnramedAttacksBuff_Patch");

            }

            private static void DLC3_UnramedAttacksBuff_Patch()
            {
                var dungeonBoon_UnarmedStrikes = BlueprintTool.Get<BlueprintDungeonBoon>("5c7a5a0220e84b3fa5d78d427d10bf6b");
                if (!Settings.Settings.GetSetting<bool>(dungeonBoon_UnarmedStrikes.Name))
                {
                    return;
                }
                var dLC3_UnramedAttacksBuff = BlueprintTool.Get<BlueprintBuff>("b5dd5a68158449e9906285be5ff6bdd7");

                var newDescription = "Unarmed strikes made by your party members gain a +1 bonus on attack rolls and deal one additional die of the same type as the primary one (1d6 becomes 2d6, 2d6 becomes 3d6).\nIn addition all unarmed attacks deal damage using the damage die of the next highest category (For example d10 becomes a 2d8).";

                dLC3_UnramedAttacksBuff.GetComponent<AdditionalDiceOnAttack>().DamageType.Physical.Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Bludgeoning;
                dLC3_UnramedAttacksBuff.AddComponent<IncreaseDiceSizeOnAttack>(c => { c.CheckWeaponCategories = true; c.Categories = new WeaponCategory[1]; c.Categories = c.Categories.AppendToArray(WeaponCategory.UnarmedStrike); c.CheckWeaponSubCategories = false; c.SubCategories = new WeaponSubCategory[1]; c.SubCategories = c.SubCategories.AppendToArray(WeaponSubCategory.Disabled); c.UseContextBonus = false; c.AdditionalSize = 1; });

                dLC3_UnramedAttacksBuff.m_Description = Helpers.CreateString(dLC3_UnramedAttacksBuff + ".Description", newDescription);
                dungeonBoon_UnarmedStrikes.m_Description = Helpers.CreateString(dungeonBoon_UnarmedStrikes + ".Description", newDescription);
                               
            }
        }
    }
}
