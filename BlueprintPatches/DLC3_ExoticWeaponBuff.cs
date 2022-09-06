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
    class DLC3_ExoticWeaponBuff
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                DLC3_ExoticWeaponBuff_Patch();

            }

            private static void DLC3_ExoticWeaponBuff_Patch()
            {
                var dLC3_ExoticWeaponBuff = BlueprintTool.Get<BlueprintBuff>("bb564ac242ae4fe1ab7def92b325de3a");
                var dungeonBoon_Exotic = BlueprintTool.Get<BlueprintDungeonBoon>("89987c26492844a2a07afc2715474b1b");

                var bastardSwordProficiency = BlueprintTool.Get<BlueprintFeature>("57299a78b2256604dadf1ab9a42e2873").ToReference<BlueprintFeatureReference>();
                var doubleAxeProficiency = BlueprintTool.Get<BlueprintFeature>("0ea5cf20b69aea44793043e1926e9057").ToReference<BlueprintFeatureReference>();
                var doubleSwordProficiency = BlueprintTool.Get<BlueprintFeature>("716c2d518bb9ecd49a32bef96a3f431b").ToReference<BlueprintFeatureReference>();
                var duelingSwordProficiency = BlueprintTool.Get<BlueprintFeature>("9c37279588fd9e34e9c4cb234857492c").ToReference<BlueprintFeatureReference>();
                var dwarvenWaraxeProficiency = BlueprintTool.Get<BlueprintFeature>("bd0d7feca087d2247b12965c1467790c").ToReference<BlueprintFeatureReference>();
                var elvenCurvedBladeProficiency = BlueprintTool.Get<BlueprintFeature>("0fca9259e370cd049a1dd50bede687f7").ToReference<BlueprintFeatureReference>();
                var estocProficiency = BlueprintTool.Get<BlueprintFeature>("9dc64f0b9161a354c9471a631318e16c").ToReference<BlueprintFeatureReference>();
                var falcataProficiency = BlueprintTool.Get<BlueprintFeature>("91fe4440ac82dbf4383c872c065c6661").ToReference<BlueprintFeatureReference>();
                var fauchardProficiency = BlueprintTool.Get<BlueprintFeature>("b3f41cd91571ba54e8c3b0c5da4a7071").ToReference<BlueprintFeatureReference>();
                var hookedHammerProficiency = BlueprintTool.Get<BlueprintFeature>("38691cbdccbbf4b42928a5ea39e42db8").ToReference<BlueprintFeatureReference>();
                var kamaProficiency = BlueprintTool.Get<BlueprintFeature>("403740e8112651141a12f0d73d793dbc").ToReference<BlueprintFeatureReference>();
                var saiProficiency = BlueprintTool.Get<BlueprintFeature>("a9a692792f6668d4dbe32c9c4f023800").ToReference<BlueprintFeatureReference>();
                var slingStaffProficiency = BlueprintTool.Get<BlueprintFeature>("a0be067e11f4d8345a8b57a92e52a301").ToReference<BlueprintFeatureReference>();
                var tongiProficiency = BlueprintTool.Get<BlueprintFeature>("8a81cd5caec059147ba5fbb74043b8f3").ToReference<BlueprintFeatureReference>();
                var urgroshProficiency = BlueprintTool.Get<BlueprintFeature>("d24f7545b1aa3b34e8216f8cb3140563").ToReference<BlueprintFeatureReference>();
                var nunchakuProficiency = BlueprintTool.Get<BlueprintFeature>("097c1ceaf18f9a045b5969bad82b1fa4").ToReference<BlueprintFeatureReference>();

                var newDescription = "When wielded by your party members, all exotic weapons deal damage using the damage die of the next highest category (for example, a d10 becomes a 2d8).\nIn addition all party members are proficiant in all exotic weapons.";

                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = bastardSwordProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = doubleAxeProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = doubleSwordProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = duelingSwordProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = dwarvenWaraxeProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = elvenCurvedBladeProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = estocProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = falcataProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = fauchardProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = hookedHammerProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = kamaProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = saiProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = slingStaffProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = tongiProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = urgroshProficiency; });
                dungeonBoon_Exotic.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = nunchakuProficiency; });

                dLC3_ExoticWeaponBuff.m_Description = Helpers.CreateString(dLC3_ExoticWeaponBuff + ".Description", newDescription);
                dungeonBoon_Exotic.m_Description = Helpers.CreateString(dungeonBoon_Exotic + ".Description", newDescription);

                Main.AddBoonOnAreaLoad(dungeonBoon_Exotic, false);
                var dungeonBoon_UnarmedStrikes = BlueprintTool.Get<BlueprintDungeonBoon>("5c7a5a0220e84b3fa5d78d427d10bf6b");
            }
        }
    }
}
