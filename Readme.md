# AstroneerFullscreenFix

A tiny Windows tool that fixes Astroneer's bugged fullscreen mode by editing the game's config file for you.

## The Problem

Astroneer's fullscreen mode can break in a way that's only fixable by editing `GameUserSettings.ini` by hand. That means unhiding system folders, navigating deep into `AppData`, and carefully changing three settings without touching anything else. This tool does it in one double-click.

## What It Does

It locates your `GameUserSettings.ini` automatically and sets these three values to `0`:

```
FullscreenMode=0
LastConfirmedFullscreenMode=0
PreferredFullscreenMode=0
```

Everything else in the file is left untouched. A backup (`GameUserSettings.ini.bak`) is created before any changes, so the operation is always reversible.

## Usage

1. **Close Astroneer.** The game rewrites this file when it closes, so the fix won't stick if it's running.
2. Download the latest `.exe` from the [Releases](../../releases) page.
3. Double-click it. A console window shows exactly what was changed.
4. Launch Astroneer and fullscreen should now behave.

If you see `GameUserSettings.ini not found`, launch Astroneer at least once first so the game generates the file, then run the tool again.

## Building From Source

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
dotnet build -c Release
```

To produce a standalone single-file `.exe` that runs without .NET installed:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

The output `.exe` will be in `bin/Release/net10.0/win-x64/publish/`.

## Safety

This tool only reads and writes the three settings listed above. It makes a backup first and never deletes anything. The full source is in this repo — review it before running if you'd like.

## Disclaimer

This is an unofficial fan-made tool. Not affiliated with, endorsed by, or associated with System Era Softworks or Astroneer. Use at your own risk.

## License

MIT
