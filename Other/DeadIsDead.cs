using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Dungeon;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Persistence;
using Kingmaker.GameModes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WOTR_BOAT_BOAT_BOAT.Utilities;
using WOTR_BOAT_BOAT_BOAT.Settings;

namespace WOTR_BOAT_BOAT_BOAT.Methods
{
    class DeadIsDead
    {
        private static Dictionary<UnitReference, bool> party = new Dictionary<UnitReference, bool>();
        private static SaveManager sm = new SaveManager();
        private static BlueprintCampaignReference dlc3CampaignRef = new BlueprintCampaignReference() { deserializedGuid = new BlueprintGuid(new Guid("e1bde745-d6ad-47c0-bc9f-b8e479b29153")) };
        private static bool PartyDeathStateChanged()
        {
            var mc = Game.Instance.Player.MainCharacter;
            var characterIsDead = mc.Value.State.IsDead;
            if (party.ContainsKey(Game.Instance.Player.MainCharacter))
            {
                if (characterIsDead)
                {
                    if (party[mc] != characterIsDead)
                    {
                        party[mc] = characterIsDead;
                        return true;
                    }
                }
            }
            else
            {
                party.Add(mc, mc.Value.State.IsDead);
            }
            foreach (var p in Game.Instance.Player.PartyCharacters)
            {
                if (p != mc)
                {
                    characterIsDead = p.Value.State.IsFinallyDead;
                    if (party.ContainsKey(p))
                    {
                        if (characterIsDead)
                        {
                            if (party[p] != characterIsDead)
                            {
                                party[p] = characterIsDead;
                                return true;
                            }
                        }
                    }
                    else
                    {
                        party.Add(p, p.Value.State.IsFinallyDead);
                    }
                }
            }
            return false;
        }
        private static void CreateBackup(string oldsave, string backup)
        {

            if (File.Exists(oldsave))
            {
                if (File.Exists(backup))
                {
                    File.Delete(backup);
                }
                File.Copy(oldsave, backup, false);
            }
        }
        private static void DeleteSave(string save)
        {
            File.Delete(save);
        }
        private static async void ManageSaves(string save, string backup, SaveInfo ls)
        {
            Main.Log("Deleting The Backup");
            DeleteSave(backup);
            while (File.Exists(backup))
            {
                Main.Log("Backup Still Exists");
                await Task.Delay(25);
            }
            Main.Log("Creating The Backup");
            CreateBackup(save, backup);
            while (!File.Exists(backup))
            {
                Main.Log("Backup Does Not Exist");
                await Task.Delay(25);
            }
            Main.Log("Deleting The Save");
            DeleteSave(save);
            while (File.Exists(save))
            {
                Main.Log("Save Still Exists");
                await Task.Delay(25);
            }
            Main.Log("Removing The Save From List");
            sm.RemoveSaveFromList(ls);
        }
        public static void SaveGameWhenDeath()
        {
            try
            {
                if ((Game.Instance == null || Game.Instance.CurrentMode != GameModeType.Default) || !Settings.Settings.GetSetting<bool>("deadisdead"))
            {
                return;
            }
                if (DungeonController.IsDungeonCampaign)
                {
                    if (PartyDeathStateChanged())
                    {
                        sm = Game.Instance.SaveManager;
                        var ls = sm.GetLatestSave();
                        Path.GetFullPath(ls.FolderName);
                        var backup = ls.FolderName.Replace(ls.FileName, "") + "BBB_Backup.bak";
                        if (ls.Campaign.ToReference<BlueprintCampaignReference>().deserializedGuid == dlc3CampaignRef.deserializedGuid && ls.EndlessDelveSeed == DungeonController.DungeonState.Seed)
                        {
                            ManageSaves(ls.FolderName,backup,ls);
                        }
                    }
                }
            }
            catch { }
        }


    }
}
