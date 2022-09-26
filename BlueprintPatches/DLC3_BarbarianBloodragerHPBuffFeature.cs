using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOTR_BOAT_BOAT_BOAT.Utilities;

namespace WOTR_BOAT_BOAT_BOAT.Patches
{
    class DLC3_BarbarianBloodragerHPBuffFeature
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

                DLC3_BarbarianBloodragerHPBuffFeature_Patch();
                Main.Log("DLC3_BarbarianBloodragerHPBuffFeature_Patch");

            }

            private static void DLC3_BarbarianBloodragerHPBuffFeature_Patch()
            {
                var dLC3_BarbarianBloodragerHPBuffFeature = BlueprintTool.Get<BlueprintFeature>("756724a8e0a7401aa93a4135fbbdfa8e");
                var dLC3_BarbarianBloodragerHPProperty = BlueprintTool.Get<BlueprintUnitProperty>("d469ee34c5614824934c37d27aeff25d");
                var fastHealing2 = BlueprintTool.Get<BlueprintBuff>("7ada82367e07da04f9421fa8d2818945");
                var fastHealingBarbBuff = Helpers.CreateCopy(fastHealing2);
                fastHealingBarbBuff.AssetGuid = new BlueprintGuid(new Guid("c9c49c79-0177-4e99-991a-eb3c747aac9d"));
                fastHealingBarbBuff.name = "Fast Healing Barb Buff " + fastHealingBarbBuff.AssetGuid;
                fastHealingBarbBuff.m_Icon = dLC3_BarbarianBloodragerHPBuffFeature.m_Icon;
                fastHealingBarbBuff.m_Description = dLC3_BarbarianBloodragerHPBuffFeature.m_Description;
                fastHealingBarbBuff.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                //var h = Helpers.CreateCopy(dLC3_BarbarianBloodragerHPBuffFeature.GetComponent<AddContextStatBonus>().Value);
                var h = Helpers.Create<ContextValue>(c => {
                    c.ValueType = ContextValueType.CasterCustomProperty;
                    c.Value = 0;
                    c.ValueRank = Kingmaker.Enums.AbilityRankType.Default;
                    c.ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage;
                    c.Property = UnitProperty.None;
                    c.m_AbilityParameter = AbilityParameterType.Level;
                    c.m_CustomProperty = dLC3_BarbarianBloodragerHPProperty.ToReference<BlueprintUnitPropertyReference>();
                });
                fastHealingBarbBuff.EditComponent<AddEffectFastHealing>(c => { c.Heal = 0; c.Bonus = h; });

                Helpers.AddBlueprint(fastHealingBarbBuff, fastHealingBarbBuff.AssetGuid);

                var dungeonBoon_BarbarianHP = BlueprintTool.Get<BlueprintDungeonBoon>("e27cec242d6b4f8299126c9abe62505e");
                if (!Settings.Settings.GetSetting<bool>(dungeonBoon_BarbarianHP.Name))
                {
                    return;
                }
                var primalDruidArchetype = BlueprintTool.Get<BlueprintArchetype>("c1c86e2997fd4257a13ef5601b5dc6dd").ToReference<BlueprintArchetypeReference>();
                var elementalRampagerArchetype = BlueprintTool.Get<BlueprintArchetype>("f815594bd1e3454182022d375bf70fd1").ToReference<BlueprintArchetypeReference>();
                
                
                foreach (var v in dLC3_BarbarianBloodragerHPProperty.GetComponents<ClassLevelGetter>().ToArray())
                {
                    if (v.m_Archetype.ToString() == primalDruidArchetype.ToString())
                    {
                        v.m_Archetype = elementalRampagerArchetype;
                    }
                }
                var newDescription = "All barbarians, bloodragers, skalds with the battle scion archetype, and druids with the elemental rampager archetype in your party gain an additional +5 hit points and Fast Healing 1 every time they gain a level in that class.";

                dLC3_BarbarianBloodragerHPBuffFeature.m_Description = Helpers.CreateString(dLC3_BarbarianBloodragerHPBuffFeature + ".Description", newDescription);
                dungeonBoon_BarbarianHP.m_Description = Helpers.CreateString(dungeonBoon_BarbarianHP + ".Description", newDescription);

                dLC3_BarbarianBloodragerHPBuffFeature.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                    fastHealingBarbBuff.ToReference<BlueprintUnitFactReference>()
                };
                });

                //Weird issue where the description doesn't update in the selection screen. Readding it should fix the issue.
                var dungeonRoot = BlueprintTool.Get<BlueprintDungeonRoot>("096f36d4e55b49129ddd2211b2c50513");
                var dungeonBoonRef = new Kingmaker.Dungeon.Blueprints.BlueprintDungeonBoonReference() { deserializedGuid = dungeonBoon_BarbarianHP.AssetGuid };
                dungeonRoot.m_Boons = dungeonRoot.m_Boons.RemoveFromArray(dungeonBoonRef);
                dungeonRoot.m_Boons = dungeonRoot.m_Boons.AppendToArray(dungeonBoonRef);
                

            }
        }
    }
}
