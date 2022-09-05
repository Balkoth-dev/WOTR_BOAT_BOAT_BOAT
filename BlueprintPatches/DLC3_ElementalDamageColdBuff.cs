using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOTR_BOAT_BOAT_BOAT.MechanicsChanges;
using WOTR_BOAT_BOAT_BOAT.Utilities;

namespace WOTR_BOAT_BOAT_BOAT.BlueprintPatches
{
    class DLC3_ElementalDamageColdBuff
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                DLC3_ElementalDamageColdBuff_Patch();

                Main.Log("DLC3_ElementalDamageColdBuff Patched");
            }

            private static void DLC3_ElementalDamageColdBuff_Patch()
            {
                var dLC3_ElementalDamageColdBuff = BlueprintTool.Get<BlueprintBuff>("59048aad5ab4455fb16d5b229a057049");
                var dungeonBoon_Cold = BlueprintTool.Get<BlueprintDungeonBoon>("7f2e836a40a94833ba75a82415712d17");
                
                var vrockAspectArea = BlueprintTool.Get<BlueprintAbilityAreaEffect>("25e6bcaf271e996468d883a9f60b41e9");
                var vrockAspectEffectBuff = BlueprintTool.Get<BlueprintBuff>("76eb2cd9b1eec0b4681c648d33c5ae3b");


                var coldAreaEffectBuff = Helpers.CreateCopy(vrockAspectEffectBuff);
                coldAreaEffectBuff.AssetGuid = new BlueprintGuid(new Guid("fe28bd42-7695-4b27-ac3d-d98083e6ff34"));

                var g = coldAreaEffectBuff.GetComponent<ContextRankConfig>();
                g.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                g.m_Buff = new BlueprintBuffReference();
                g.m_BuffRankMultiplier = 5;
                g.m_StartLevel = 0;
                g.m_StepLevel = 5;

                Helpers.AddBlueprint(coldAreaEffectBuff, coldAreaEffectBuff.AssetGuid);

                var coldArea = Helpers.CreateCopy(vrockAspectArea);
                coldArea.AssetGuid = new BlueprintGuid(new Guid("a07c9ab7-3c4d-4eb2-9fc1-d78719d2158f"));
                coldArea.Size = new Kingmaker.Utility.Feet() { m_Value = 10 };
                coldArea.GetComponent<AbilityAreaEffectBuff>().m_Buff = coldAreaEffectBuff.ToReference<BlueprintBuffReference>();

                Helpers.AddBlueprint(coldArea, coldArea.AssetGuid);

                dLC3_ElementalDamageColdBuff.AddComponent<AddAreaEffect>(c =>
                {
                    c.m_AreaEffect = coldArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });

                var newDescription = "All cold damage dealt by your party members is increased by 25%. \nIn addition all enemies within 10 feet of any party member are affected by difficult terrain and have a -1 to reflex saves, this penalty increases by 1 every 5 character levels.";

                dLC3_ElementalDamageColdBuff.m_Description = Helpers.CreateString(dLC3_ElementalDamageColdBuff + ".Description", newDescription);
                dungeonBoon_Cold.m_Description = Helpers.CreateString(dungeonBoon_Cold + ".Description", newDescription);

                Main.AddBoonOnAreaLoad(dungeonBoon_Cold, false);
            }
        }
    }
}
