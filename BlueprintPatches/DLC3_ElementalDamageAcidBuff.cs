using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
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
    class DLC3_ElementalDamageAcidBuff
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                DLC3_ElementalDamageAcidBuff_Patch();

            }

            private static void DLC3_ElementalDamageAcidBuff_Patch()
            {
                var dLC3_ElementalDamageAcidBuff = BlueprintTool.Get<BlueprintBuff>("ef6c34c686854e219a465b152c542552");
                var dungeonBoon_Acid = BlueprintTool.Get<BlueprintDungeonBoon>("30d5a9af67c844eaba0a9eccd0e10c39");
                var acidBomb = BlueprintTool.Get<BlueprintAbility>("fd101fbc4aacf5d48b76a65e3aa5db6d");

                var newDescription = "All acid damage dealt by your party members is increased by 25%. \nIn addition all party members gain the ability to throw Acid Bombs like an alchemist an unlimited amount of times.";

                var acidBombInfinite = Helpers.CreateCopy(acidBomb);
                acidBombInfinite.AssetGuid = new BlueprintGuid(new Guid("6fc3300e-f4e9-4e7e-bb47-864c9f544f0f"));
                acidBombInfinite.RemoveComponents<AbilityResourceLogic>();

                Helpers.AddBlueprint(acidBombInfinite, acidBombInfinite.AssetGuid);

                dLC3_ElementalDamageAcidBuff.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[]{
                        acidBombInfinite.ToReference<BlueprintUnitFactReference>()
                    };
                });

                dLC3_ElementalDamageAcidBuff.m_Description = Helpers.CreateString(dLC3_ElementalDamageAcidBuff + ".Description", newDescription);
                dungeonBoon_Acid.m_Description = Helpers.CreateString(dungeonBoon_Acid + ".Description", newDescription);

                Main.AddBoonOnAreaLoad(dungeonBoon_Acid, false);

                var p = dungeonBoon_Acid;
                Main.Log(p.Name + " - " + p.Description);
            }
        }
    }
}
