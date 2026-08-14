using HarmonyLib;
using Shared.Text;

namespace BlueprintItem.Patches.Localization;

[HarmonyPatch(typeof(LocalizationManager), "SetLocalization")]
public class LocalizationManagerPatch
{
    private static void Postfix()
    {
        StringDefinitions.Strings["consumable:blueprint*item:name"] = new StringDefinition(I18n.L.Text("item.name"));
    }
}
