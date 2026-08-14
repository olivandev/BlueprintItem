using System.IO;
using HarmonyLib;
using Microsoft.Xna.Framework.Content;

namespace BlueprintItem.Patches.Assets;

[HarmonyPatch(typeof(ContentManager), "OpenStream")]
public static class OpenStreamRedirect
{
    static readonly string PluginRoot = Path.Combine("BepInEx", "plugins", PluginInfo.PLUGIN_NAME);

    static bool Prefix(string assetName, ref Stream __result)
    {
        if (!assetName.StartsWith($"{PluginInfo.PLUGIN_NAME}Content")) return true;
        string path = Path.Combine(PluginRoot, assetName) + ".xnb";
        if (!File.Exists(path)) return true;
        __result = File.OpenRead(path);
        return false;
    }
}
