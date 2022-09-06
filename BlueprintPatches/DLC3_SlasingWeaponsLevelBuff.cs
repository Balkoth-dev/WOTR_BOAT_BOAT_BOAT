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
    class DLC3_SlasingWeaponsLevelBuff
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                DLC3_SlasingWeaponsLevelBuff_Patch();

            }

            private static void DLC3_SlasingWeaponsLevelBuff_Patch()
            {
                var dLC3_SlasingWeaponsLevelBuff = BlueprintTool.Get<BlueprintBuff>("36f34c2f069540fda1d9d2d5b03f5c38");
                var dungeonBoon_Slashing = BlueprintTool.Get<BlueprintDungeonBoon>("3aeef1ddb73f4f12937c42eb046f90d3");
                var dLC3_SlashingBludgeoningLevelRankGetter = BlueprintTool.Get<BlueprintUnitProperty>("54a35f59c7a74a39b4ad214359269fb7");

                var newDescription = "All party members gain a +1 bonus for every 2 character levels (minimum +1) to damage rolls with slashing weapons.";

                dLC3_SlashingBludgeoningLevelRankGetter.EditComponent<ComplexPropertyGetter>(c => { c.Denominator = 2; });

                dLC3_SlasingWeaponsLevelBuff.m_Description = Helpers.CreateString(dLC3_SlasingWeaponsLevelBuff + ".Description", newDescription);
                dungeonBoon_Slashing.m_Description = Helpers.CreateString(dungeonBoon_Slashing + ".Description", newDescription);

                Main.AddBoonOnAreaLoad(dungeonBoon_Slashing, false);

                var p = dungeonBoon_Slashing;
                Main.Log(p.Name + " - " + p.Description);
            }
        }
    }
}
