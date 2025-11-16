using HarmonyLib;
using IroxGames.StoreFronts.Steam;
using Steamworks;
using System;
using UnityEngine;

namespace Doorstop
{
	internal class Entrypoint
	{
		public static void Start()
		{
			UnsetDoorstopEnvVarsIfNotSteamRunning();

			var harmony = new Harmony("com.shilo.catnipmod");
			harmony.PatchAll();
		}

		[HarmonyPatch(typeof(SteamManager), "Awake")]
		private static class SteamManagerAwakePatch
		{
			private static bool _catnipModCreated = false;

			static void Postfix()
			{
				if (_catnipModCreated)
					return;

				var catnipMod = new GameObject(nameof(CatnipMod.CatnipMod));
				catnipMod.AddComponent<CatnipMod.CatnipMod>();
				UnityEngine.Object.DontDestroyOnLoad(catnipMod);

				_catnipModCreated = true;
			}
		}

		/// <summary>
		///	Workaround fix for Doorstop bug:
		///   If Steam is not running on direct game launch, Doorstop environment variables never get unset.
		///   After game executable restarts via Steam, the environment variables are still set,
		///   causing Doorstop to skip initialization and Catnip mod never loads.
		///   Without this fix, users would have to restart Steam to load Catnip mod again.
		///   https://github.com/NeighTools/UnityDoorstop/issues/34
		/// </summary>
		private static void UnsetDoorstopEnvVarsIfNotSteamRunning()
		{
			if (SteamAPI.IsSteamRunning())
				return;

			Environment.SetEnvironmentVariable("DOORSTOP_DISABLE", null);
			Environment.SetEnvironmentVariable("DOORSTOP_INITIALIZED", null);
		}
	}
}
