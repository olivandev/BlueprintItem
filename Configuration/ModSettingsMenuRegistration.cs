// ModSettingsMenu registration/refresh pattern adapted from Ice Box Studio's LogisticsPlanner mod.
#nullable disable

using System;
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
    ModSettingsMenuRegistration._config = config;
    if (!ModSettingsMenuRegistration._subscribed)
    {
      ModSettingsRegistry.RefreshRequested += ModSettingsMenuRegistration.Refresh;
      ModSettingsMenuRegistration._subscribed = true;
    }
    ModSettingsMenuRegistration.RegisterCurrent(config);
  }

  private static void Refresh()
  {
    if (ModSettingsMenuRegistration._config == null)
      return;
    string currentLocale = I18n.L.CurrentLocale;
    if (ModSettingsMenuRegistration._locale == currentLocale && ModSettingsRegistry.TryGet(PluginInfo.PLUGIN_GUID, out ModSettingsRegistration _))
      return;
    ModSettingsMenuRegistration.RegisterCurrent(ModSettingsMenuRegistration._config);
  }

  private static void RegisterCurrent(ConfigFile config)
  {
    ModSettingsRegistry.Register(PluginInfo.PLUGIN_GUID, ModSettingsMenuRegistration.Text("mod.name"), config, new ModSettingsModOptions()
    {
      Icon = "blueprint",
      Version = PluginInfo.PLUGIN_VERSION,
      Author = PluginInfo.PLUGIN_AUTHOR,
      Description = ModSettingsMenuRegistration.Text("mod.description"),
      // NexusModsId = new int?(PluginInfo.NEXUS_MODS_ID),
      // UpdateManifestUrl = "PluginInfo.UPDATE_MANIFEST_URL",
      Order = new int?(-80)
    });
    ModSettingsMenuRegistration._locale = I18n.L.CurrentLocale;
  }

  private static string Text(string key) => I18n.L.Text(key);
}
