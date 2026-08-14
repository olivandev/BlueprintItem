using System.Collections.Generic;
using Shared.Data;
using HarmonyLib;
using Shared.Data.DataModels;
using Shared.Models.Items;

namespace BlueprintItem.Patches.Recipe;

[HarmonyPatch(typeof(SharedDataSetup))]
public class DataSharedDataSetupPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("SetupCarpenterRecipes")]
    private static void SetupCarpenterRecipesPostfix(List<WorkRecipeModel> CarpenterRecipes)
    {
        WorkRecipeModel blueprintRecipeModel = new WorkRecipeModel
        {
            Id = Constants.RecipeId,
            Output = new WorkOutputModel()
            {
                Type = WorkOrderType.Item,
                Amount = 1,
                CraftedId = "consumable:blueprint"
            },
            ItemIngredients = new ItemAmount[2]
            {
                new()
                {
                    ItemId = "material:linen",
                    Amount = 1
                },
                new()
                {
                    ItemId = "material:bronze_needle",
                    Amount = 1
                }
            },
            LevelRequirement = 1,
            RequiredProgress = 30f,
            Experience = 25f
        };
        CarpenterRecipes.Add(blueprintRecipeModel);
    }
}
