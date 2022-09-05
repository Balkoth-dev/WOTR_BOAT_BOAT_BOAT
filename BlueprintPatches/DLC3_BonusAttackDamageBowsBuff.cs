using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
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
        static bool Initialized;

        static void Postfix()
        {
            if (Initialized) return;
            Initialized = true;

            DLC3_ArmorPenaltyReduceBuff_Patch();

            Main.Log("DLC3_ArmorPenaltyReduceBuff Patched");
        }

        private static void DLC3_ArmorPenaltyReduceBuff_Patch()
        {
            var dLC3_BonusAttackDamageBowsBuff = BlueprintTool.Get<BlueprintBuff>("c541ad0952ba428a9bd26e4a1fa93020");
            var dungeonBoon_BonusDmgBows = BlueprintTool.Get<BlueprintDungeonBoon>("088003e6159b40a09a05233603ac5d15");

            var pointBlankShot = BlueprintTool.Get<BlueprintFeature>("0da0c194d6e1d43419eb8d990b28e0ab").ToReference<BlueprintFeatureReference>();
            var preciseShot = BlueprintTool.Get<BlueprintFeature>("8f3d1e6b4be006f4d896081f2f889665").ToReference<BlueprintFeatureReference>();
            var rapidShotFeature = BlueprintTool.Get<BlueprintFeature>("9c928dc570bb9e54a9649b3ebfe47a41").ToReference<BlueprintFeatureReference>();

            dungeonBoon_BonusDmgBows.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = pointBlankShot; });
            dungeonBoon_BonusDmgBows.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = preciseShot; });
            dungeonBoon_BonusDmgBows.AddComponent<BoonLogicFeature>(c => { c.Step = 0; c.Start = 0; c.m_Feature = rapidShotFeature; });

            var newDescription = "All ranged attacks made by your party members when using a bow gain a +1 bonus on attack and damage rolls. This bonus increases by 1 for every other bow-wielding party member attacking the same target, up to a maximum of +3. \n" +
                                 "In addition, all party members obtain the Point Blank Shot, Precise Shot, and Rapid Shot feats.";

            dLC3_BonusAttackDamageBowsBuff.m_Description = Helpers.CreateString(dLC3_BonusAttackDamageBowsBuff + ".Description", newDescription);
            dungeonBoon_BonusDmgBows.m_Description = Helpers.CreateString(dungeonBoon_BonusDmgBows + ".Description", newDescription);

            Main.AddBoonOnAreaLoad(dungeonBoon_BonusDmgBows, false);

        }
    }
}
