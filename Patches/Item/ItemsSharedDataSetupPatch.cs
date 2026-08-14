using System.Collections.Generic;
using HarmonyLib;
using Shared.Combat.Spells.Parameters;
using Shared.Models.Items;
using Shared.Text;

namespace BlueprintItem.Patches.Item;

[HarmonyPatch(typeof(Shared.Data.Items.SharedDataSetup), "SetupConsumableItems")]
internal class ItemsSharedDataSetupPatch
{
    private static void Postfix(List<ItemData> items)
    {
        items.Add(new ItemData()
        {
            Id = "consumable:blueprint",
            Name = (StringId) "Blueprint",
            Icon = Constants.IconId,
            MaxStackSize = 1,
            Tier = new int?(2),
            Flags = ItemFlag.NoFlags,
            Usable = new UsableItem()
            {
                Type = UsableItem.UsableType.Special,
                UsesMax = 1,
                Cooldown = 0.0f,
                SpellId = Constants.SpellId,
                SpellTypeArgs = (object) new ScriptedSpellArgs()
                {
                    Script = Constants.ScriptName
                }
            }
        });
    }
}
