﻿using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using static UnityModManagerNet.UnityModManager;
using static WOTR_BOAT_BOAT_BOAT.Settings.BlueprintsCache_Postfix;
using Newtonsoft.Json.Linq;
using BlueprintCore.Utils;

namespace WOTR_BOAT_BOAT_BOAT.Utilities
{
    class AssetLoader
    {
        public static ModEntry ModEntry;
        protected static string lang = Settings.Settings.GetSetting<string>("lang");
        public static Sprite LoadInternal(string folder, string file, int width = 64, int height = 64)
        {
            return Image2Sprite.Create($"{ModEntry.Path}Assets{Path.DirectorySeparatorChar}{folder}{Path.DirectorySeparatorChar}{file}",width,height);
        }
        // Loosely based on https://forum.unity.com/threads/generating-sprites-dynamically-from-png-or-jpeg-files-in-c.343735/
        public static class Image2Sprite
        {
            public static string icons_folder = "";
            public static Sprite Create(string filePath, int height = 64, int width = 64)
            {
                var bytes = File.ReadAllBytes(icons_folder + filePath);
                var texture = new Texture2D(height, width, TextureFormat.RGBA32, false);
                _ = texture.LoadImage(bytes);
                var sprite = Sprite.Create(texture, new Rect(0, 0, height, width), new Vector2(0, 0));
                return sprite;
            }
        }
    }
    public static class Helpers
    {
        private static Dictionary<string, LocalizedString> textToLocalizedString = new Dictionary<string, LocalizedString>();
        public static readonly Dictionary<BlueprintGuid, SimpleBlueprint> ModBlueprints = new Dictionary<BlueprintGuid, SimpleBlueprint>();

        private static readonly Dictionary<string, Guid> GuidsByName = new();

        public static T CreateCopy<T>(T original, Action<T> init = null)
        {
            var result = (T)ObjectDeepCopier.Clone(original);
            init?.Invoke(result);
            return result;
        }
        public static T Create<T>(Action<T> init = null) where T : new()
        {
            var result = new T();
            init?.Invoke(result);
            return result;
        }
        public static T GenericAction<T>(Action<T> init = null) where T : Kingmaker.ElementsSystem.GameAction, new()
        {
            var result = (T)Kingmaker.ElementsSystem.Element.CreateInstance(typeof(T));
            init?.Invoke(result);
            return result;
        }

        public static void AddBlueprint([NotNull] SimpleBlueprint blueprint, BlueprintGuid assetId)
        {
            var loadedBlueprint = ResourcesLibrary.TryGetBlueprint(assetId);
            if (loadedBlueprint == null)
            {
                ModBlueprints[assetId] = blueprint;
                ResourcesLibrary.BlueprintsCache.AddCachedBlueprint(assetId, blueprint);
                blueprint.OnEnable();
            }
            else
            {
                Main.Log($"Failed to Add: {blueprint.name}");
                Main.Log($"Asset ID: {assetId} already in use by: {loadedBlueprint.name}");
            }
        }
        public static LocalizedString CreateString(string key, string value)
        {
            // See if we used the text previously.
            // (It's common for many features to use the same localized text.
            // In that case, we reuse the old entry instead of making a new one.)
            if (textToLocalizedString.TryGetValue(value, out LocalizedString localized))
            {
                return localized;
            }
            var strings = LocalizationManager.CurrentPack?.m_Strings;
            if (strings!.TryGetValue(key, out var oldValue) && value != oldValue.Text)
            {
                Main.Log($"Info: duplicate localized string `{key}`, different text.");
            }
            var sE = new Kingmaker.Localization.LocalizationPack.StringEntry();
            sE.Text = value;
            strings[key] = sE;
            localized = new LocalizedString
            {
                m_Key = key
            };
            textToLocalizedString[value] = localized;
            return localized;
        }
        public static void SetFeatures(this BlueprintFeatureSelection selection, params BlueprintFeature[] features)
        {
            selection.m_AllFeatures = selection.m_Features = features.Select(bp => bp.ToReference<BlueprintFeatureReference>()).ToArray();
        }
        public static void AddFeatures(this BlueprintFeatureSelection selection, params BlueprintFeature[] features)
        {
            foreach (var feature in features)
            {
                var featureReference = feature.ToReference<BlueprintFeatureReference>();
                if (!selection.m_AllFeatures.Contains(featureReference))
                {
                    selection.m_AllFeatures = selection.m_AllFeatures.AppendToArray(featureReference);
                }
                if (!selection.m_Features.Contains(featureReference))
                {
                    selection.m_Features = selection.m_Features.AppendToArray(featureReference);
                }
            }
            selection.m_AllFeatures = selection.m_AllFeatures.OrderBy(feature => feature.Get().Name).ToArray();
            selection.m_Features = selection.m_Features.OrderBy(feature => feature.Get().Name).ToArray();
        }
        public static T Get<T>(string nameOrGuid) where T : SimpleBlueprint
        {
            if (!GuidsByName.TryGetValue(nameOrGuid, out Guid assetId)) { assetId = Guid.Parse(nameOrGuid); }

            SimpleBlueprint asset = ResourcesLibrary.TryGetBlueprint(new BlueprintGuid(assetId));
            if (asset is T result) { return result; }
            else
            {
                throw new InvalidOperationException(
                    $"Failed to fetch blueprint: {nameOrGuid} - {assetId}.\nIs the type correct? {typeof(T)}");
            }
        }
        public static LocalizedString GetLocalizationElement(string name, string key, string seperator = "-")
        {
            try
            {
                return LocalizationTool.GetString("WOTR-BOAT-BOAT-BOAT." + key + seperator + name);
            }
            catch(Exception ex)
            {
                Main.Log(ex.Message);
                return null;
            }
        }
    }
}
