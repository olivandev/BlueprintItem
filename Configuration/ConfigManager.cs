// Localized ConfigDescription/tag pattern adapted from Ice Box Studio's LogisticsPlanner mod.
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
                entryNameKey: "entry.open_construction_menu_hotkey",
                entryOrder: 1,
                section: "Controls",
                sectionNameKey: "section.controls",
                sectionOrder: new int?(20),
                keybind: true));
    }

    private static ConfigDescription Description(
        string descriptionKey,
        string entryNameKey,
        int entryOrder,
        string section = null,
        string sectionNameKey = null,
        int? sectionOrder = null,
        AcceptableValueBase acceptableValues = null,
        double? sliderStep = null,
        bool keybind = false)
    {
        List<object> objectList = new List<object>();
        if (!string.IsNullOrWhiteSpace(section))
            objectList.Add((object) new ConfigManager.LocalizedSectionTag(section, sectionNameKey, sectionOrder));
        objectList.Add((object) new ConfigManager.LocalizedEntryTag(entryNameKey, descriptionKey, new int?(entryOrder), sliderStep, keybind));
        return new ConfigDescription(ConfigManager.Text(descriptionKey), acceptableValues, objectList.ToArray());
    }

    private static string Text(string key) => I18n.L.Text(key);

    private sealed class LocalizedSectionTag
    {
        private readonly string _displayNameKey;

        public LocalizedSectionTag(string section, string displayNameKey, int? order)
        {
            this.Section = section;
            this._displayNameKey = displayNameKey;
            this.Order = order;
        }

        public string Section { get; }

        public string DisplayName => ConfigManager.Text(this._displayNameKey);

        public int? Order { get; }
    }

    private sealed class LocalizedEntryTag
    {
        private readonly string _displayNameKey;
        private readonly string _descriptionKey;

        public LocalizedEntryTag(
            string displayNameKey,
            string descriptionKey,
            int? order,
            double? sliderStep,
            bool keybind)
        {
            this._displayNameKey = displayNameKey;
            this._descriptionKey = descriptionKey;
            this.Order = order;
            this.SliderStep = sliderStep;
            this.Keybind = new bool?(keybind);
        }

        public string DisplayName => ConfigManager.Text(this._displayNameKey);

        public string Description => ConfigManager.Text(this._descriptionKey);

        public int? Order { get; }

        public double? SliderStep { get; }

        public bool? Keybind { get; }
    }
}
