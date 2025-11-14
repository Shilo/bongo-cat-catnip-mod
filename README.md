# [Bongo Cat Catnip Mod](https://github.com/Shilo/bongo-cat-catnip-mod/)

[![Bongo Cat Logo](CatnipMod/Assets/BongoCat/CatnipMod/logo.png)](https://github.com/Shilo/bongo-cat-catnip-mod/)

Catnip-infused mod that brings out [Bongo Cat](https://store.steampowered.com/app/3419430/Bongo_Cat/)'s zoomies and purrs. Includes quality of life features and fixes. Free, safe, and no cheats.

## Features

TODO

## Table of Contents

- [Features](#features)
- [Installation](#installation)
  - [Verification](#verification)
  - [Uninstallation](#uninstallation)
- [Developer Setup](#developer-setup)
  - [Prerequisites](#prerequisites)
  - [Initial Setup](#initial-setup)
  - [Building](#building)
  - [Project Structure](#project-structure)
  - [Development Workflow](#development-workflow)
  - [Key Files](#key-files)
  - [Notes](#notes)
- [Third-Party Libraries](#third-party-libraries)
  - [UnityDoorstop](#unitydoorstop-winhttpdll)

## Installation

1. Go to the [Releases](https://github.com/Shilo/bongo-cat-catnip-mod/releases) page
2. Download the latest `CatnipMod.zip` file
3. Extract the contents of the zip file

**Automatic Installation (Recommended)**

- Double-click `install.bat` in the extracted folder
- The installer will automatically detect your Bongo Cat installation path
- If Bongo Cat is running, it will be automatically closed to ensure a clean installation
- Only mod files (`winhttp.dll`, `doorstop_config.ini`, and `CatnipMod\` folder) will be copied to the game directory
- The game directory will open in Windows Explorer after successful installation

**Manual Installation**

- Copy all files and folders from the `BongoCat` folder to your Bongo Cat installation directory:
  - **Default location:** `C:\Program Files (x86)\Steam\steamapps\common\BongoCat\`
  - The files to copy include:
    - `winhttp.dll` (in the root)
    - `doorstop_config.ini` (in the root)
    - `CatnipMod\` folder (containing `CatnipMod.dll` and assets)

Your game directory structure should look like this:
```
BongoCat/
├── BongoCat.exe
├── winhttp.dll          ← Mod file
├── doorstop_config.ini  ← Mod file
├── CatnipMod/           ← Mod folder
│   ├── CatnipMod.dll
│   └── ... (mod assets)
└── ... (other game files)
```

4. Launch Bongo Cat through Steam as normal

### Verification

TODO

### Uninstallation

**Automatic Uninstallation**

- Double-click `uninstall.bat` in the extracted mod folder
- The uninstaller will automatically detect your Bongo Cat installation path
- If Bongo Cat is running, it will be automatically closed to ensure a clean uninstallation
- Only mod files (`winhttp.dll`, `doorstop_config.ini`, and `CatnipMod\` folder) will be deleted from the game directory
- The game directory will open in Windows Explorer after successful uninstallation

**Manual Uninstallation**

- Delete the mod files from your Bongo Cat installation directory:
  - **Default location:** `C:\Program Files (x86)\Steam\steamapps\common\BongoCat\`
  - The files to delete include:
    - `winhttp.dll` (in the root)
    - `doorstop_config.ini` (in the root)
    - `CatnipMod\` folder (containing `CatnipMod.dll` and assets)

---

## Developer Setup

### Prerequisites

- **Visual Studio 2019 or later** (or any IDE that supports .NET Framework 4.0+ projects)
  - Download: [Visual Studio 2026 Community](https://visualstudio.microsoft.com/downloads/) (free)
  - Or [Build Tools for Visual Studio 2026](https://aka.ms/vs/stable/vs_BuildTools.exe) (minimal installation, no IDE)
- **Bongo Cat** installed via Steam
- **.NET Framework 4.0** or later

### Initial Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Shilo/bongo-cat-catnip-mod.git
   cd bongo-cat-catnip-mod
   ```

2. **Configure build paths:**
   
   Copy `Directory.Local.props.example` to `Directory.Local.props`:
   ```bash
   copy Directory.Local.props.example Directory.Local.props
   ```
   
   Edit `Directory.Local.props` and change `<SteamPath>` if your Steam installation is in a different location:
   ```xml
   <Project>
     <PropertyGroup>
       <SteamPath>C:\Program Files (x86)\Steam</SteamPath>
       ...
     </PropertyGroup>
   </Project>
   ```
   
   > **Note:** If the `<SteamPath>` doesn't update after editing, try unloading and reloading the project from the `Solution Explorer` pane in Visual Studio.

3. **Open the solution:**
   - Open `CatnipMod.slnx` in Visual Studio

### Building

The project has three build configurations:

- **Debug** - Builds with debug symbols, launches via Steam
- **Debug (No Steam)** - Builds with debug symbols, launches `BongoCat.exe` directly
- **Release** - Optimized build without debug symbols, launches via Steam

**Quick Build (No Visual Studio Required):**

For users who want to compile the project without opening Visual Studio:

1. Double-click `build.bat` in the project root directory
2. The script will automatically find MSBuild and build the Release configuration
3. The compiled files will be in `CatnipMod\bin\Release\`
4. A zip file will be created at `CatnipMod\bin\CatnipMod.zip`
5. The output directory (`CatnipMod\bin`) will automatically open in Windows Explorer after a successful build

> **Note:** You still need Visual Studio or Visual Studio Build Tools installed for MSBuild to be available. The script will automatically detect MSBuild from common installation paths, including Visual Studio 2017, 2019, 2022, 2025, and 2026 (version 18).
> 
> **Don't have Visual Studio?** See [Prerequisites](#prerequisites) for download links.

**Building in Visual Studio:**

1. Select your desired configuration from the dropdown
2. Build the solution (F6 or Build → Build Solution)

**Build outputs:**
- Debug builds: `CatnipMod\bin\Debug\`
- Release builds: `CatnipMod\bin\Release\`
- Zip files are automatically created in `CatnipMod\bin\` after building
  - Debug builds create `CatnipMod-Debug.zip`
  - Release builds create `CatnipMod.zip`

**Automatic deployment:**
- After building, `BongoCat.exe` is automatically closed if running
- Files are automatically copied to your Bongo Cat installation directory
- The game is automatically launched (via Steam for Debug/Release, or directly for "Debug (No Steam)")
- The mod is ready to test immediately after building

### Project Structure

```
CatnipMod/
├── CatnipMod.cs          # Main mod class
├── Entrypoint.cs         # UnityDoorstop entry point (runs when game starts)
├── Assets/
│   ├── BongoCat/         # Mod files organized for release
│   │   ├── CatnipMod/
│   │   │   └── ...              # Mod assets
│   │   ├── doorstop_config.ini  # UnityDoorstop configuration
│   │   └── winhttp.dll          # UnityDoorstop loader
│   ├── install.bat       # Installation script (copied to release)
│   └── uninstall.bat     # Uninstallation script (copied to release)
└── Properties/
    └── AssemblyInfo.cs   # Assembly metadata
```

### Development Workflow

1. **Make your changes** in `CatnipMod.cs` or `Entrypoint.cs`
2. **Build the project** (files are automatically copied to the game directory)
3. **Launch the game** to test your changes

### Key Files

- **`Entrypoint.cs`** - This is where your mod code runs when the game starts. The `Start()` method is called by UnityDoorstop before the game's main code executes.
- **`CatnipMod.cs`** - Main mod class where you can add your mod functionality.
- **`doorstop_config.ini`** - Configures UnityDoorstop to load your mod DLL.
- **`install.bat`** - Automated installation script that:
  - Automatically detects Bongo Cat installation path from Windows registry
  - Closes Bongo Cat if running
  - Copies mod files and verifies installation
  - Opens game directory after completion
- **`uninstall.bat`** - Automated uninstallation script that:
  - Automatically detects Bongo Cat installation path from Windows registry
  - Closes Bongo Cat if running
  - Removes mod files and verifies deletion
  - Opens game directory after completion

### Notes

- The mod uses **UnityDoorstop** to inject into the Unity runtime
- Target framework is **.NET Framework 4.0** (matches Unity's runtime)
- Unity assemblies are referenced from `BongoCat_Data\Managed\`
- The build system automatically creates zip files for distribution
- Release builds do not generate PDB files (debug symbols)
- Installation scripts automatically detect Steam installation paths from registry (HKCU, then HKLM, then common locations)

---

## Third-Party Libraries

### [UnityDoorstop](https://github.com/NeighTools/UnityDoorstop) (`winhttp.dll`)

A library that enables modding Unity games by injecting and executing managed .NET assemblies early in the Unity runtime, before the game's main code runs. This allows mods to hook into game systems and modify behavior.

Licensed under the GNU Lesser General Public License v2.1. See [THIRD_PARTY_LICENSES.md](THIRD_PARTY_LICENSES.md) for the full license text.
