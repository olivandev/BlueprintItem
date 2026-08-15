using RomesteadLocalizationAPI.Api;

namespace BlueprintItem;

internal static class I18n
{
    public static readonly RomesteadLocalizer L = RomesteadLocalization.For(PluginInfo.PLUGIN_GUID);
}
