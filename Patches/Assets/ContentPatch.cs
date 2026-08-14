using System.IO;
using System.Reflection;
using CandideCreator.Shared;
using HarmonyLib;

namespace BlueprintItem.Patches.Assets;

[HarmonyPatch(typeof(Content), "Init")]
public class ContentPatch
{
    private static readonly MethodInfo FindContentMethod =
        AccessTools.Method(typeof(Content), "FindContent", new[] { typeof(string), typeof(string) });

    static void Postfix()
    {
        string modDir = Path.Combine("BepInEx", "plugins", PluginInfo.PLUGIN_NAME);
        FindContentMethod.Invoke(null, new object[] { modDir, "" });
    }
}
