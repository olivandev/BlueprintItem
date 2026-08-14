using System.Linq;
using HarmonyLib;
using CandideServer.Data.SharedDataSetups;
using CandideServer.Database;

namespace BlueprintItem.Patches.Item;

[HarmonyPatch(typeof(SharedDataSetup), "SetupWorkRecipeGroups")]
public class SharedDataSetupsPatch
{
    private static void Postfix(SharedDataSetup __instance)
    {
        var carpenterRecipeGroup = __instance.RecipeGroups.Find(group => group.Id == "job:carpenter:1");
        if (carpenterRecipeGroup == null) return;
        carpenterRecipeGroup.RecipeIds = carpenterRecipeGroup.RecipeIds.Append(Constants.RecipeId).ToArray();
    }
}
