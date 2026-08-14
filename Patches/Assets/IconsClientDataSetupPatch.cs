using System.Collections.Generic;
using Candide.Data.Icons;
using CandideCreator.Shared;
using CandideCreator.Shared.Graphics;
using HarmonyLib;

namespace BlueprintItem.Patches.Assets;

[HarmonyPatch(typeof(ClientDataSetup), "SetupGeneralIcons")]
internal static class IconsClientDataSetupPatch
{
    private static void Postfix(List<IconData> icons)
    {
        IconSheetMetaData spriteSheetMetaData = new IconSheetMetaData()
        {
            Small = Content.SpriteSheet($"{PluginInfo.PLUGIN_NAME}Content/blueprint_18", 18, 18),
            Medium = Content.SpriteSheet($"{PluginInfo.PLUGIN_NAME}Content/blueprint_24", 24, 24)
        };

        icons.Add(new IconData()
        {
            Id = Constants.IconId,
            Variations = IconSetupHelper.CreateIconVariations(spriteSheetMetaData, 0)
        });
    }
}
