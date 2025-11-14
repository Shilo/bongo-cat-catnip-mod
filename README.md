# Bongo Cat Catnip Mod

Catnip-infused mod that brings out [Bongo Cat](https://store.steampowered.com/app/3419430/Bongo_Cat/)'s zoomies and purrs. Includes quality of life features and fixes. Free, safe, and no cheats.

## Installation

1. Go to the [Releases](https://github.com/Shilo/bongo-cat-catnip-mod/releases) page
2. Download the latest `CatnipMod.zip` file
3. Extract the contents of the zip file

**Option A: Automatic Installation (Recommended)**
- Double-click `install.bat` in the extracted folder
- If your Bongo Cat installation is in a non-default location, edit `install.bat` and change the `GAME_DIR` path before running

**Option B: Manual Installation**
- Copy all files and folders to your Bongo Cat installation directory:
  - **Default location:** `C:\Program Files (x86)\Steam\steamapps\common\BongoCat\`
  - The extracted files should include:
    - `winhttp.dll` (in the root)
    - `doorstop_config.ini` (in the root)
    - `CatnipMod\` folder (containing `CatnipMod.dll` and assets)

Your directory structure should look like this:
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

After installation, when you launch the game, a file called `catnip.log` should be created in the game directory. This confirms the mod is working.

### Uninstallation

**Option A: Automatic Uninstallation**
- Double-click `uninstall.bat` in the extracted mod folder
- If your Bongo Cat installation is in a non-default location, edit `uninstall.bat` and change the `GAME_DIR` path before running

**Option B: Manual Uninstallation**
- Delete the following from the game directory:
  - `winhttp.dll`
  - `doorstop_config.ini`
  - `CatnipMod\` folder

---

## Developer Setup

### Prerequisites

- **Visual Studio 2019 or later** (or any IDE that supports .NET Framework 4.0+ projects)
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
- **Release** - Optimized build, launches via Steam

**To build:**
1. Select your desired configuration from the dropdown
2. Build the solution (F6 or Build → Build Solution)

**Build outputs:**
- Debug builds: `CatnipMod\bin\Debug\CatnipMod\`
- Release builds: `CatnipMod\bin\Release\CatnipMod\`
- Zip files are automatically created in `CatnipMod\bin\` after building

**Automatic deployment:**
- After building, files are automatically copied to your Bongo Cat installation directory
- The mod is ready to test immediately after building

### Project Structure

```
CatnipMod/
├── CatnipMod.cs          # Main mod class
├── Entrypoint.cs         # UnityDoorstop entry point (runs when game starts)
├── Assets/
│   ├── CatnipMod/
│   │   └── ...              # Mod assets
│   ├── doorstop_config.ini  # UnityDoorstop configuration
│   └── winhttp.dll          # UnityDoorstop loader
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

### Notes

- The mod uses **UnityDoorstop** to inject into the Unity runtime
- Target framework is **.NET Framework 4.0** (matches Unity's runtime)
- Unity assemblies are referenced from `BongoCat_Data\Managed\`
- The build system automatically creates zip files for distribution

---

## Third-Party Libraries

### [UnityDoorstop](https://github.com/NeighTools/UnityDoorstop) (`winhttp.dll`)

A library that enables modding Unity games by injecting and executing managed .NET assemblies early in the Unity runtime, before the game's main code runs. This allows mods to hook into game systems and modify behavior.

Licensed under the GNU Lesser General Public License v2.1. See [THIRD_PARTY_LICENSES.md](THIRD_PARTY_LICENSES.md) for the full license text.
