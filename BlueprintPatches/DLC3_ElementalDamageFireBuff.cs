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
    class DLC3_ElementalDamageFireBuff
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

                DLC3_ElementalDamageFireBuff_Patch();
                Main.Log("DLC3_ElementalDamageFireBuff_Patch");

            }

            private static void DLC3_ElementalDamageFireBuff_Patch()
            {
                var fireDomainBaseAbility = BlueprintTool.Get<BlueprintAbility>("4ecdf240d81533f47a5279f5075296b9");
                var fireDomainBaseAbilityInfinite = Helpers.CreateCopy(fireDomainBaseAbility);
                fireDomainBaseAbilityInfinite.AssetGuid = new BlueprintGuid(new Guid("8229c9c4-27c4-4ebc-af95-6b5401064825"));
                fireDomainBaseAbilityInfinite.RemoveComponents<AbilityResourceLogic>();

                Helpers.AddBlueprint(fireDomainBaseAbilityInfinite, fireDomainBaseAbilityInfinite.AssetGuid);

                var dungeonBoon_Fire = BlueprintTool.Get<BlueprintDungeonBoon>("7e155f0848db47e89bb76dce6d4e0939");
                if (!Settings.Settings.GetSetting<bool>("dungeonBoon_Fire"))
                {
                    return;
                }
                var dLC3_ElementalDamageFireBuff = BlueprintTool.Get<BlueprintBuff>("a8ff427196b34707bd0e8c54bb852d32");

                var newDescription = Helpers.GetLocalizationElement("description", "dungeonBoon_Fire");

                dLC3_ElementalDamageFireBuff.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[]{
                        fireDomainBaseAbilityInfinite.ToReference<BlueprintUnitFactReference>()
                    };
                });

                dLC3_ElementalDamageFireBuff.m_Description = Helpers.CreateString(dLC3_ElementalDamageFireBuff + ".Description", newDescription);
                dungeonBoon_Fire.m_Description = Helpers.CreateString(dungeonBoon_Fire + ".Description", newDescription);
                

            }
        }
    }
}
