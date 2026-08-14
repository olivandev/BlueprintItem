# Blueprint Item

A Romestead BepInEx mod that adds a consumable **Blueprint** item. Using it unlocks the ability to open the Construction menu without needing a physical Workbench - just press a hotkey (default `B`) from anywhere.

## Features

- **Blueprint item** - a consumable, craftable at a Carpenter's bench (1 Linen + 1 Bronze Needle, level 1).
- **Hotkey menu access** - once used, press the configured key to open the Construction menu without a Workbench.
- **Rebindable hotkey** - configurable in BepInEx's config, or through the in-game settings screen.
- **Localized** - text is served through RomesteadLocalizationAPI.
- **Multiplayer-safe** - the unlock is tracked server-side and synced to clients correctly, including for non-host players.

## Requirements

- [BepinEx 6 For Romestead](https://www.nexusmods.com/romestead/mods/1)
- [RomesteadLocalizationAPI](https://www.nexusmods.com/romestead/mods/53) (hard dependency)
- [ModSettingsMenu](https://www.nexusmods.com/romestead/mods/8) (hard dependency)

## Installation

1. Install BepInEx, RomesteadLocalizationAPI, and ModSettingsMenu first.
2. Extract this mod into the game root - the archive already includes the `BepInEx/plugins` folder structure.

## Configuration

| Setting | Default | Description |
|---|---|---|
| `Controls.OpenConstructionMenuHotkey` | `B` | Key used to open the Construction menu once you've used a Blueprint. |

Rebind it directly in `BepInEx/config/olivandev.Romestead.BlueprintItem.cfg`, or through ModSettingsMenu's in-game settings screen.

## How to Get One

Craft a Blueprint at a Carpenter's bench (requires 1 Linen and 1 Bronze Needle), then use it from your inventory. Once consumed, press your configured hotkey at any time to open the Construction menu — no Workbench required.

## Building from Source

Every plugin needs these BepInEx files as compile-time references:

- `BepInEx/core/BepInEx.Core.dll`
- `BepInEx/core/BepInEx.NET.Common.dll`
- `BepInEx/core/0Harmony.dll`

And these game files:

- `Romestead.dll`
- `CandideServer.dll`
- `Shared.dll`
- `CandideCreator.Shared.dll`
- `MonoGame.Framework.dll`

Plus these mod dependencies, installed in `BepInEx/plugins`:

- `RomesteadLocalizationAPI.dll`
- `ModSettingsMenu.dll`

None of the above are redistributed with this mod — reference them only, don't bundle them.

## Release Packaging

Distribute only this mod's own files:

```text
BepInEx/
  plugins/
    BlueprintItem/
      BlueprintItem.dll
      Localization.json
      BlueprintItemContent/
        blueprint_18.xnb
        blueprint_24.xnb
README.md
```

Do not package the game's own assemblies, BepInEx core, Steam DLLs, save files, or game assets.

## Credits

- **Ice Box Studio** — this mod depends on their [RomesteadLocalizationAPI](https://www.nexusmods.com/romestead/mods/53) and [ModSettingsMenu](https://www.nexusmods.com/romestead/mods/8), and its settings-menu localization pattern was adapted from their LogisticsPlanner mod.

## Notes

Romestead's game assemblies may produce `CA2252` and `CA1416` analyzer warnings when referenced directly. This project suppresses those warnings since it's intentionally compiled against this Windows game.
