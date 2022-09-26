using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOTR_BOAT_BOAT_BOAT.MechanicsChanges;
using WOTR_BOAT_BOAT_BOAT.Utilities;

namespace WOTR_BOAT_BOAT_BOAT.Patches
{
    class DLC3_RoguesRangersGainDRBuff
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

                DLC3_RoguesRangersGainDRBuff_Patch();
                Main.Log("DLC3_RoguesRangersGainDRBuff_Patch");

            }

            private static void DLC3_RoguesRangersGainDRBuff_Patch()
            {
                var dungeonBoon_Rogues = BlueprintTool.Get<BlueprintDungeonBoon>("2e28b57ab98b47ddb92c17df62725863");
                if (!Settings.Settings.GetSetting<bool>(dungeonBoon_Rogues.Name))
                {
                    return;
                }
                var dLC3_RoguesRangersGainDRBuff = BlueprintTool.Get<BlueprintBuff>("e9f7b6b787ea4fdaa4bfc928513f8929");
                var dLC3_RoguesRangersGainDRBuffLevelGetter = BlueprintTool.Get<BlueprintUnitProperty>("2aa73fe91377456b8734ba94a506d541");

                var newDescription = "All rogues, rangers, alchemists with the vivisectionist archetype, magi with the armored battlemage archetype, and slayers with the arcane enforcer archetype gain DR N/- equal to their level in this class.\nIn addition, every time they are hit with an attack they heal equal to their level in this class.";

                var s = Helpers.Create<ContextActionHealTarget>(c =>
                {
                    c.Value = new ContextDiceValue()
                    {
                        DiceType = Kingmaker.RuleSystem.DiceType.Zero,
                        DiceCountValue = new ContextValue() { ValueType = ContextValueType.Simple, Value = 0, ValueRank = Kingmaker.Enums.AbilityRankType.Default, ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage, Property = UnitProperty.None, m_AbilityParameter = AbilityParameterType.Level },
                        BonusValue = new ContextValue()
                        {
                            ValueType = ContextValueType.CasterCustomProperty,
                            Value = 1,
                            ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                            Property = UnitProperty.Level,
                            m_CustomProperty = dLC3_RoguesRangersGainDRBuffLevelGetter.ToReference<BlueprintUnitPropertyReference>(),
                            m_AbilityParameter = AbilityParameterType.Level
                        }
                    };
                });
                dLC3_RoguesRangersGainDRBuff.AddComponent<AddTargetAttackWithWeaponTrigger>(c => { c.OnlyHit = true; c.ActionsOnAttacker = new ActionList(); c.ActionOnSelf = new ActionList(); c.ActionOnSelf.Actions = new GameAction[] { s }; });

                dLC3_RoguesRangersGainDRBuff.m_Description = Helpers.CreateString(dLC3_RoguesRangersGainDRBuff + ".Description", newDescription);
                dungeonBoon_Rogues.m_Description = Helpers.CreateString(dungeonBoon_Rogues + ".Description", newDescription);
                


            }
        }
    }
}
