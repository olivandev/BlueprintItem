using System.Collections.Generic;
using HarmonyLib;
using Shared.Combat.Spells.Parameters;
using Shared.Models.Spells;

namespace BlueprintItem.Patches.Spell;

[HarmonyPatch(typeof(Shared.Data.Spells.SharedDataSetup), nameof(Shared.Data.Spells.SharedDataSetup.SetupItemSpells))]
internal class SpellsSharedDataSetupPatch
{
    private static void Postfix(List<SpellDataModel> spells)
    {
        spells.Add(new SpellDataModel()
        {
            Id = Constants.SpellId,
            SpellTypeId = "spelltype:scripted",
            CastSoundEffect = "event:/interface/player inventory/ui_inventory_equip_general",
            SpellTypeArgs = (object) new ScriptedSpellArgs()
            {
                Script = string.Empty
            },
            CanHit = ProjectileHitFlags.NoSharedFaction
        });
    }
}
