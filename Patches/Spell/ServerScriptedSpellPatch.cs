using System;
using System.Diagnostics.CodeAnalysis;
using CandideCreator.Shared.Helpers;
using CandideServer.Models.Player;
using CandideServer.Models.Spells;
using CandideServer.Server;
using CandideServer.ServerServices;
using HarmonyLib;
using Shared.Combat.Spells;
using Shared.Combat.Spells.Parameters;
using Shared.Helpers;
using Shared.Models.Player;

namespace BlueprintItem.Patches.Spell;

[HarmonyPatch(typeof(ServerScriptedSpell))]
internal class ServerScriptedSpellPatch
{
    private delegate bool TryGetPlayerDelegate(Guid? id, [MaybeNullWhen(false)] out ServerPlayerCharacter playerCharacter);

    private static readonly TryGetPlayerDelegate TryGetPlayer =
        AccessTools.MethodDelegate<TryGetPlayerDelegate>(
            AccessTools.Method(typeof(ServerScriptedSpell), "TryGetPlayer",
                new[] { typeof(Guid?), typeof(ServerPlayerCharacter).MakeByRefType() }));
    
    [HarmonyPrefix]
    [HarmonyPatch( "CanCast")]
    private static bool CanCastPrefix(ref CanCastSpellResult __result, ServerSpellCastingContext context)
    {
        if (context.CastingEntity == null || !(context.Context.SpellTypeArgs is ScriptedSpellArgs spellTypeArgs))
            return true;
        
        if (!Equals(spellTypeArgs.Script, Constants.ScriptName)) return true;
        __result = !TryGetPlayer.Invoke(context.Context.Target, out var playerCharacter) ? CanCastSpellResult.FailSilent : ScriptedSpellPatch.CanCast_AddBlueprint(playerCharacter.Character);
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch("DoCast")]
    private static bool DoCastPrefix(ServerSpellCastingContext context)
    {
        ScriptedSpellArgs spellTypeArgs = (ScriptedSpellArgs) context.Context.SpellTypeArgs;
        
        if (!Equals(spellTypeArgs.Script, Constants.ScriptName)) return true;
        
        if (!TryGetPlayer(context.Context.Target, out var playerCharacter)) return true;
        
        if (!PlayerServerService.JoinedPlayers.TryGetValue(playerCharacter.PlayerId, out var joinedPlayer))
        {
            ServerWarningHelper.Warning("AddBlueprint: Failed to find the connected player.");
        }
        else
        {
            int flagValue = (int) PlayerFlagsHelper.GetFlagValue1(playerCharacter.Character, Constants.Flag, Constants.FlagDefault);
            if (flagValue > 0)
            {
                ServerWarningHelper.Warning($"AddBlueprint: Flag value {flagValue} is higher than the maximum {1}");
            }
            else
            {
                playerCharacter.Character.Flags[Constants.Flag] = new CharacterFlag()
                {
                    IsStatic = false,
                    Value1 = (float) flagValue
                };

                object msg = new PlayerAddedBlueprintMessage()
                {
                    FlagName = Constants.Flag,
                    NewFlagValue = 1
                };
                
                BaseServer.Instance.SendHostMessage(Constants.EventKey, msg, joinedPlayer.Peer);
            }
        }
        return false;
    }
}
