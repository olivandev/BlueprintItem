// Localized ConfigDescription/tag pattern adapted from Ice Box Studio's ModSettingMenu localization example mod.
#nullable disable

using BepInEx.Configuration;
using ModSettingsMenu.Api;

namespace BlueprintItem.Configuration;

internal static class ModSettingsMenuRegistration
{
    private static ConfigFile _config;
    private static string _locale;
    private static bool _subscribed;

    public static void Register(ConfigFile config)
    {
        _config = config;
        if (!_subscribed)
        {
            ModSettingsRegistry.RefreshRequested += Refresh;
            _subscribed = true;
        }

        RegisterCurrent(config);
    }

    private static void Refresh()
    {
        if (_config == null)
        {
            return;
        }

        var locale = I18n.L.CurrentLocale;
        if (_locale == locale && ModSettingsRegistry.TryGet(PluginInfo.PLUGIN_GUID, out _))
        {
            return;
        }

        RegisterCurrent(_config);
    }

    private static void RegisterCurrent(ConfigFile config)
    {
        ModSettingsRegistry.Register(PluginInfo.PLUGIN_GUID, Text("mod.name"), config, new ModSettingsModOptions
        {
            Icon = "blueprint",
            Version = PluginInfo.PLUGIN_VERSION,
            Author = PluginInfo.PLUGIN_AUTHOR,
            Description = Text("mod.description"),
            NexusModsId = PluginInfo.NEXUS_MODS_ID,
            UpdateManifestUrl = PluginInfo.UPDATE_MANIFEST_URL,
            Order = -89
        });

        _locale = I18n.L.CurrentLocale;
    }

    private static string Text(string key)
    {
        return I18n.L.Text(key);
    }
}
