using CatnipMod.Debug;
using CatnipMod.Utilities;
using HarmonyLib;
using IroxGames.StoreFronts.Steam;
using UnityEngine;

namespace Doorstop
{
	internal class Entrypoint
	{
		public static void Start()
		{
			Log.SetupTimestampHandler();
			DoorstopUtil.UnsetEnvVarsIfNotSteamRunning();
			HarmonyUtil.CreateAndPatch();
		}
	}

	[HarmonyPatch(typeof(SteamManager), "Awake")]
	class SteamManagerAwakePatch
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
}
