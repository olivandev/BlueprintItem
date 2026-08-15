// Localized ConfigDescription/tag pattern adapted from Ice Box Studio's ModSettingMenu localization example mod.
using System.Collections.Generic;
using BepInEx.Configuration;
using Microsoft.Xna.Framework.Input;

namespace BlueprintItem.Configuration;

internal static class ConfigManager
{
    internal static ConfigEntry<Keys> OpenConstructionMenuHotkey { get; private set; }

    public static void Init(ConfigFile config)
    {
        ConfigManager.OpenConstructionMenuHotkey = config.Bind(
            "Controls",
            "OpenConstructionMenuHotkey",
            Keys.B,
            ConfigManager.Description(
                descriptionKey: "config.open_construction_menu_hotkey",
                displayNameKey: "entry.open_construction_menu_hotkey",
                entryOrder: 1,
                section: "Controls",
                sectionNameKey: "section.controls",
                sectionOrder: new int?(20),
                keybind: true));
    }

    private static ConfigDescription Description(
        string descriptionKey,
        string displayNameKey = null,
        int? entryOrder = null,
        string section = null,
        string sectionNameKey = null,
        int? sectionOrder = null,
        AcceptableValueBase acceptableValues = null,
        double? sliderStep = null,
        bool keybind = false,
        bool hidden = false)
    {
        var tags = new List<object>();
        if (!string.IsNullOrWhiteSpace(section))
        {
            tags.Add(new LocalizedSectionTag(section, sectionNameKey, sectionOrder));
        }

        tags.Add(new LocalizedEntryTag(displayNameKey, descriptionKey, entryOrder, sliderStep, keybind, hidden));
        return new ConfigDescription(Text(descriptionKey), acceptableValues, tags.ToArray());
    }

    private static string Text(string key)
    {
        return I18n.L.Text(key);
    }

    // Mod Settings Menu recognizes public properties on custom ConfigDescription tags.
    // The property names match ModSettingsSectionTag and ModSettingsEntryTag.
    private sealed class LocalizedSectionTag
    {
        private readonly string _displayNameKey;

        public LocalizedSectionTag(string section, string displayNameKey, int? order)
        {
            Section = section;
            _displayNameKey = displayNameKey;
            Order = order;
        }

        public string Section { get; }

        public string DisplayName => TextOrNull(_displayNameKey);

        public int? Order { get; }
    }

    private sealed class LocalizedEntryTag
    {
        private readonly string _displayNameKey;
        private readonly string _descriptionKey;

        public LocalizedEntryTag(string displayNameKey, string descriptionKey, int? order, double? sliderStep, bool keybind, bool hidden)
        {
            _displayNameKey = displayNameKey;
            _descriptionKey = descriptionKey;
            Order = order;
            SliderStep = sliderStep;
            Keybind = keybind ? true : null;
            Hidden = hidden ? true : null;
        }

        public string DisplayName => TextOrNull(_displayNameKey);

        public string Description => TextOrNull(_descriptionKey);

        public int? Order { get; }

        public double? SliderStep { get; }

        public bool? Keybind { get; }

        public bool? Hidden { get; }
    }

    private static string TextOrNull(string key)
    {
        return string.IsNullOrWhiteSpace(key) ? null : Text(key);
    }
}
