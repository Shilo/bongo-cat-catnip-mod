//using HarmonyLib;

using Steamworks;
using System;

namespace Doorstop
{
	internal class Entrypoint
	{
		public static void Start()
		{
			UnsetDoorstopEnvVarsIfNotSteamRunning();

			//var harmony = new Harmony("com.shilo.catnipmod");
			//harmony.PatchAll();
		}

		// Workaround fix for Doorstop bug:
		//   If Steam is not running on direct game launch, Doorstop environment variables never get unset.
		//   After game executable restarts via Steam, the environment variables are still set,
		//   causing Doorstop to skip initialization and Catnip mod never loads.
		//   Without this fix, users would have to restart Steam to load Catnip mod again.
		//   https://github.com/NeighTools/UnityDoorstop/issues/34
		private static void UnsetDoorstopEnvVarsIfNotSteamRunning()
		{
			if (SteamAPI.IsSteamRunning())
				return;

			Environment.SetEnvironmentVariable("DOORSTOP_DISABLE", null);
			Environment.SetEnvironmentVariable("DOORSTOP_INITIALIZED", null);
		}
	}
}
