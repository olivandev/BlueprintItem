using Candide.GameModels;
using Candide.GameModels.Managers;
using CandideServer.EventBus;

namespace BlueprintItem;

public class BlueprintEventBusService
{
    [CandideServer.EventBus.EventBus(Constants.EventKey, true, null)]
    public static void OnReceive_PlayerAddedBlueprint(PlayerAddedBlueprintMessage msg, EventBusArgs args)
    {
        PlayerCharacterManager.SetDynamicFlag(GameState.LocalPlayer.Character.Character.EntityId, msg.FlagName, msg.NewFlagValue, 0.0f, false);
    }
}
