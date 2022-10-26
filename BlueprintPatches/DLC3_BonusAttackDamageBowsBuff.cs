using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Dungeon.Blueprints;
using Kingmaker.Dungeon.Blueprints.Boons;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Actions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOTR_BOAT_BOAT_BOAT.Utilities;

namespace WOTR_BOAT_BOAT_BOAT.BlueprintPatches
{
    class DLC3_BonusAttackDamageBowsBuff
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

                DLC3_BonusAttackDamageBowsBuff_Patch();
                Main.Log("DLC3_BonusAttackDamageBowsBuff_Patch");

            }

            private static void DLC3_BonusAttackDamageBowsBuff_Patch()
            {
                var dungeonBoon_BonusDmgBows = BlueprintTool.Get<BlueprintDungeonBoon>("088003e6159b40a09a05233603ac5d15");
                if (!Settings.Settings.GetSetting<bool>("dungeonBoon_BonusDmgBows"))
                {
                    Main.Log("Check");
                    return;
                }
                var dLC3_BonusAttackDamageBowsBuff = BlueprintTool.Get<BlueprintBuff>("c541ad0952ba428a9bd26e4a1fa93020");

                var pointBlankShot = BlueprintTool.Get<BlueprintFeature>("0da0c194d6e1d43419eb8d990b28e0ab").ToReference<BlueprintFeatureReference>();
                var preciseShot = BlueprintTool.Get<BlueprintFeature>("8f3d1e6b4be006f4d896081f2f889665").ToReference<BlueprintFeatureReference>();
                var rapidShotFeature = BlueprintTool.Get<BlueprintFeature>("9c928dc570bb9e54a9649b3ebfe47a41").ToReference<BlueprintFeatureReference>();

                dungeonBoon_BonusDmgBows.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_MainCharacterOnly = false; c.m_Feature = pointBlankShot; });
                dungeonBoon_BonusDmgBows.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_MainCharacterOnly = false; c.m_Feature = preciseShot; });
                dungeonBoon_BonusDmgBows.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_MainCharacterOnly = false; c.m_Feature = rapidShotFeature; });

                var newDescription = Helpers.GetLocalizationElement("Description", "DungeonBoon_BonusDmgBows", ".");

                dLC3_BonusAttackDamageBowsBuff.m_Description = Helpers.CreateString(dLC3_BonusAttackDamageBowsBuff + ".Description", newDescription);
                dungeonBoon_BonusDmgBows.m_Description = Helpers.CreateString(dungeonBoon_BonusDmgBows + ".Description", newDescription);

            }
        }
    }
}
