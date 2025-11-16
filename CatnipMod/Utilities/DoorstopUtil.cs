using Steamworks;
using System;

namespace CatnipMod.Utilities
{
	public static class DoorstopUtil
	{
		/// <summary>
		/// Workaround fix for Doorstop bug:
		/// If Steam is not running on direct game launch, Doorstop environment variables never get unset.
		/// After game executable restarts via Steam, the environment variables are still set,
		/// causing Doorstop to skip initialization and Catnip mod never loads.
		/// Without this fix, users would have to restart Steam to load Catnip mod again.
		/// </summary>
		/// <remarks>
		/// See: https://github.com/NeighTools/UnityDoorstop/issues/34
		/// </remarks>
		public static void UnsetEnvVarsIfNotSteamRunning()
		{
			if (SteamAPI.IsSteamRunning())
				return;

			Environment.SetEnvironmentVariable("DOORSTOP_DISABLE", null);
			Environment.SetEnvironmentVariable("DOORSTOP_INITIALIZED", null);
		}
	}
}

