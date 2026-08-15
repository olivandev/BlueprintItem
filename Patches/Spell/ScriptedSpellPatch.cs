using System;
using System.Text;
using Candide.GameModels.Models.Spells;
using Candide.Multiplayer.Models;
using HarmonyLib;
using Shared.Combat.Spells;
using Shared.Combat.Spells.Casting;
using Shared.Combat.Spells.Parameters;
using Shared.Helpers;
using Shared.Models.Player;

#nullable enable
namespace BlueprintItem.Patches.Spell;

[HarmonyPatch(typeof(ScriptedSpell))]
internal class ScriptedSpellPatch
{
    public static CanCastSpellResult CanCast_AddBlueprint(PlayerCharacterModel playerCharacter)
    {
        return (double) PlayerFlagsHelper.GetFlagValue1(playerCharacter, Constants.Flag, Constants.FlagDefault) > 0.0f ? CanCastSpellResult.FailSilent : CanCastSpellResult.CanCast;
    }

    private delegate bool TryGetPlayerDelegate(Guid? id, out ClientPlayerCharacter playerCharacter);

    private static readonly TryGetPlayerDelegate TryGetPlayer =
        AccessTools.MethodDelegate<TryGetPlayerDelegate>(
            AccessTools.Method(typeof(ScriptedSpell), "TryGetPlayer",
                new[] { typeof(Guid?), typeof(ClientPlayerCharacter).MakeByRefType() }));

    [HarmonyPrefix]
    [HarmonyPatch("CanCast")]
    private static bool CanCastPrefix(ref CanCastSpellResult __result, SpellCastingContext context)
    {
        if (context.CastingEntity == null || !(context.SpellTypeArgs is ScriptedSpellArgs spellTypeArgs)) return true;

        if (!Equals(spellTypeArgs.Script, Constants.ScriptName)) return true;

        __result = !TryGetPlayer.Invoke(context.Target, out var playerCharacter)
            ? CanCastSpellResult.FailSilent
            : CanCast_AddBlueprint(playerCharacter.Character);
        return false;
    }

    [HarmonyPostfix]
    [HarmonyPatch("GetToolTipText")]
    private static void GetToolTipTextPrefix(StringBuilder sb, object? spellArgs)
    {
        StringBuilder stringBuilder = sb;
        stringBuilder.AppendLine(I18n.L.Text("item.missing"));
    }
}
