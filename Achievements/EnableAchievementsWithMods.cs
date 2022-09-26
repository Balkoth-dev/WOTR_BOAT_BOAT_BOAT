using HarmonyLib;
using Kingmaker;
using Kingmaker.Achievements;
using Kingmaker.Blueprints.Root;
using Kingmaker.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOTR_BOAT_BOAT_BOAT.Achievements
{
    [HarmonyPatch(typeof(AchievementEntity), nameof(AchievementEntity.IsDisabled), MethodType.Getter)]
    public static class AchievementEntity_IsDisabled_Patch
    {
        private static void Postfix(ref bool __result, AchievementEntity __instance)
        {
            //modLogger.Log("AchievementEntity.IsDisabled");
            if (Settings.Settings.GetSetting<bool>("cheevos"))
            {
                if (__instance.Data.OnlyMainCampaign && !Game.Instance.Player.Campaign.IsMainGameContent)
                {
                    __result = true;
                    return;
                }
                //modLogger.Log($"AchievementEntity.IsDisabled - {__result}");
                BlueprintCampaign blueprintCampaign = __instance.Data.SpecificCampaign?.Get();
                __result = !__instance.Data.OnlyMainCampaign && blueprintCampaign != null && Game.Instance.Player.Campaign != blueprintCampaign || ((UnityEngine.Object)__instance.Data.MinDifficulty != (UnityEngine.Object)null && Game.Instance.Player.MinDifficultyController.MinDifficulty.CompareTo(__instance.Data.MinDifficulty.Preset) < 0 || __instance.Data.MinCrusadeDifficulty > (KingdomDifficulty)(SettingsEntity<KingdomDifficulty>)SettingsRoot.Difficulty.KingdomDifficulty) || (__instance.Data.IronMan && !(bool)(SettingsEntity<bool>)SettingsRoot.Difficulty.OnlyOneSave);
                // || (Game.Instance.Player.ModsUser || OwlcatModificationsManager.Instance.IsAnyModActive));
                //modLogger.Log($"AchievementEntity.IsDisabled - {__result}");
            }
        }
    }
}
