using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
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

namespace WOTR_BOAT_BOAT_BOAT.BlueprintPatches
{
    class DLC3_DwarvenBlessingFeature
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

                DLC3_DwarvenBlessingFeature_Patch();
                Main.Log("DLC3_DwarvenBlessingFeature_Patch");

            }

            private static void DLC3_DwarvenBlessingFeature_Patch()
            {
                var dungeonBoon_Dwarven = BlueprintTool.Get<BlueprintDungeonBoon>("16e92f99e49143b3afde282bb8b94a7a");
                if (!Settings.Settings.GetSetting<bool>("dungeonBoon_Dwarven"))
                {
                    return;
                }
                var dLC3_DwarvenBlessingFeature = BlueprintTool.Get<BlueprintFeature>("ef9202f408454e17bb3d0ed16bf59adf");
                var dLC3_DwarvenBuff = BlueprintTool.Get<BlueprintBuff>("fb0c7bafd33042a8ac4c998a3b1a3893");
                var dwarvenWaraxe = BlueprintTool.Get<BlueprintWeaponType>("a6925f5f897801449a648d865637e5a0").ToReference<BlueprintWeaponTypeReference>();
                var dwarvenUrgrosh = BlueprintTool.Get<BlueprintWeaponType>("0ec97c08fdf87e44f8f16ba87b511743").ToReference<BlueprintWeaponTypeReference>(); ;

                var newDescription = Helpers.GetLocalizationElement("Description", "DungeonBoon_Dwarven", ".");

                var warAxeDamage = Helpers.Create<ContextActionDealDamage>(c =>
                {
                    c.DamageType = new DamageTypeDescription
                    {
                        Type = DamageType.Physical,
                        Energy = DamageEnergyType.Unholy
                    };
                    c.Duration = new ContextDurationValue()
                    {
                        m_IsExtendable = true,
                        DiceCountValue = new ContextValue(),
                        BonusValue = new ContextValue()
                    };
                    c.Value = new ContextDiceValue
                    {
                        DiceType = DiceType.D6,
                        DiceCountValue = new ContextValue()
                        {
                            Value = 1
                        },
                        BonusValue = new ContextValue
                        {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus
                        }
                    };
                });

                dLC3_DwarvenBuff.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c =>
                {
                    c.OnlyHit = true;
                    c.Action = new ActionList();
                    c.Action.Actions = new GameAction[] { warAxeDamage };
                    c.m_WeaponType = dwarvenWaraxe;
                });

                dLC3_DwarvenBuff.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c =>
                {
                    c.OnlyHit = true;
                    c.Action = new ActionList();
                    c.Action.Actions = new GameAction[] { warAxeDamage };
                    c.m_WeaponType = dwarvenUrgrosh;
                });

                dLC3_DwarvenBlessingFeature.m_Description = Helpers.CreateString(dLC3_DwarvenBlessingFeature + ".Description", newDescription);
                dLC3_DwarvenBuff.m_Description = Helpers.CreateString(dLC3_DwarvenBuff + ".Description", newDescription);
                dungeonBoon_Dwarven.m_Description = Helpers.CreateString(dungeonBoon_Dwarven + ".Description", newDescription);
                

            }
        }
    }
}
