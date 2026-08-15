using BlueprintItem.Configuration;
using Candide;
using Candide.CandideUI;
using Candide.GameModels;
using Candide.GameModels.Helpers;
using Candide.GameModels.Managers;
using Candide.GameModels.Models;
using Candide.Input;
using Candide.PlayerMode;
using Candide.Sound;
using HarmonyLib;
using Shared.Entity;

namespace BlueprintItem.Patches.Menu;

[HarmonyPatch(typeof (CandideEngine))]
internal static class CandideEngineUpdatePatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(CandideEngine.Initialize))]
    private static void InitalizePostfix(CandideEngine __instance)
    {
        __instance.NetworkEventBusManager.Subscribe(typeof(BlueprintItem).Assembly);
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(CandideEngine.Update))]
    private static void UpdatePostfix(CandideEngine __instance)
    {
        if (CandideUiSystem.InputFocusableElement != null ||
            PauseModeManager.Instance.Active ||
            __instance.Terminal.Active ||
            !InputManager.Pressed(ConfigManager.OpenConstructionMenuHotkey.Value)) return;

        if ((int)LocalPlayerFlags.GetFlagValue1(Constants.Flag, Constants.FlagDefault) < 1)
        {
            SoundPlayer.PlayEventOneShot("event:/interface/cancel");
            PlayerWarningMessage.Add(I18n.L.Text("item.missing"));
            return;
        }

        if (BigModeManager.Instance.Active)
        {
            BigModeManager.Instance.Exit();
            return;
        }

        EntityWrapper player = GameState.LocalPlayer.Character.Entity;

        if (player == null) return;

        ClientTownModel closestTown = TownsManager.GetClosestTown(player.Position2);
        BigModeManager.Instance.Town = closestTown?.Model;
        BigModeManager.Instance.WorkbenchEntity = player;
        Globals.Game.SetModeManager((AbstractModeManager) BigModeManager.Instance);
        BigModeManager.Instance.EnterSubMode(SubMode.Construction);
    }
}
