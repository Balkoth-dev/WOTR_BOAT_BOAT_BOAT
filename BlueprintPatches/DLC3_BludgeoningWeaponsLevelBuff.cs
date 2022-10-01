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
    class DLC3_BludgeoningWeaponsLevelBuff
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

                DLC3_BludgeoningWeaponsLevelBuff_Patch();
                Main.Log("DLC3_BludgeoningWeaponsLevelBuff_Patch");

            }

            private static void DLC3_BludgeoningWeaponsLevelBuff_Patch()
            {
                var dungeonBoon_Bludgeoning = BlueprintTool.Get<BlueprintDungeonBoon>("05815e81fa7e490584165e2ebb835134");
                if (!Settings.Settings.GetSetting<bool>("dungeonBoon_Bludgeoning"))
                {
                    return;
                }
                var dLC3_BludgeoningWeaponsLevelBuff = BlueprintTool.Get<BlueprintBuff>("ebbeb216ee414e86bdb7238e07ad88f7");
                var dLC3_SlashingBludgeoningLevelRankGetter = BlueprintTool.Get<BlueprintUnitProperty>("54a35f59c7a74a39b4ad214359269fb7");

                var newDescription = Helpers.GetLocalizationElement("Description", "DungeonBoon_Bludgeoning", ".");

                dLC3_SlashingBludgeoningLevelRankGetter.EditComponent<ComplexPropertyGetter>(c => { c.Denominator = 2; });

                dLC3_BludgeoningWeaponsLevelBuff.m_Description = Helpers.CreateString(dLC3_BludgeoningWeaponsLevelBuff + ".Description", newDescription);
                dungeonBoon_Bludgeoning.m_Description = Helpers.CreateString(dungeonBoon_Bludgeoning + ".Description", newDescription);
                

            }
        }
    }
}
