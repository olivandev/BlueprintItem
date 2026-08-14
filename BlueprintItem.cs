using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using BlueprintItem.Configuration;
using HarmonyLib;

namespace BlueprintItem;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency(RomesteadLocalizationAPI.PluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
public class BlueprintItem : BasePlugin
{
    private Harmony _harmony;
    public static BlueprintItem Instance { get; private set; }
    internal static ManualLogSource Logger { get; private set; }
    
    public override void Load()
    {
        Instance = this;
        Logger = Log;

        try
        {
            I18n.L.RegisterJson(Path.Combine(Paths.PluginPath, PluginInfo.PLUGIN_NAME, "Localization.json"));

            ConfigManager.Init(this.Config);
            ModSettingsMenuRegistration.Register(this.Config);

            _harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
        catch (Exception ex)
        {
            Logger = Log;
            Logger.LogError($"{PluginInfo.PLUGIN_NAME} initialization error: {ex.Message}\n{ex.StackTrace}");
        }
    }
}
