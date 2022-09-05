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
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                DLC3_DwarvenBlessingFeature_Patch();

                Main.Log("DLC3_ArmorPenaltyReduceBuff Patched");
            }

            private static void DLC3_DwarvenBlessingFeature_Patch()
            {
                var dLC3_DwarvenBlessingFeature = BlueprintTool.Get<BlueprintFeature>("ef9202f408454e17bb3d0ed16bf59adf");
                var dLC3_DwarvenBuff = BlueprintTool.Get<BlueprintBuff>("fb0c7bafd33042a8ac4c998a3b1a3893");
                var dungeonBoon_Dwarven = BlueprintTool.Get<BlueprintDungeonBoon>("16e92f99e49143b3afde282bb8b94a7a");
                var dwarvenWaraxe = BlueprintTool.Get<BlueprintWeaponType>("a6925f5f897801449a648d865637e5a0").ToReference<BlueprintWeaponTypeReference>();

                var newDescription = "Each dwarf in your party gains a +1 bonus to Constitution ability score and +3 to their base move speed for each dwarf in your party, including themselves. \nIn addition, whenever a dwarf hits with a Dwarven War Axe, they heal 1d4 per Constitution modifier.";

                var cure = Helpers.Create<ContextActionHealTarget>(c => {
                    c.Value = Helpers.Create<ContextDiceValue>(d =>
                    {
                        d.DiceType = Kingmaker.RuleSystem.DiceType.D4;
                        d.DiceCountValue = Helpers.Create<ContextValue>(e => { e.Value = 1; e.ValueType = ContextValueType.Simple; e.ValueRank = Kingmaker.Enums.AbilityRankType.Default; e.Property = UnitProperty.StatBonusConstitution; });
                        d.BonusValue = new ContextValue();
                    });
                });

                dLC3_DwarvenBuff.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c =>
                {
                    c.OnlyHit = true;
                    c.Action = new ActionList();
                    c.Action.Actions = new GameAction[] { cure };
                    c.m_WeaponType = dwarvenWaraxe;
                });

                var x = Helpers.CreateCopy(dLC3_DwarvenBuff.GetComponent<AddContextStatBonus>());
                x.Stat = StatType.Speed;
                x.Multiplier = 3;

                dLC3_DwarvenBuff.AddComponent<AddContextStatBonus>(c => { c = x; });

                dLC3_DwarvenBlessingFeature.m_Description = Helpers.CreateString(dLC3_DwarvenBlessingFeature + ".Description", newDescription);
                dLC3_DwarvenBuff.m_Description = Helpers.CreateString(dLC3_DwarvenBuff + ".Description", newDescription);
                dungeonBoon_Dwarven.m_Description = Helpers.CreateString(dungeonBoon_Dwarven + ".Description", newDescription);

                Main.AddBoonOnAreaLoad(dungeonBoon_Dwarven, false);

            }
        }
    }
}
