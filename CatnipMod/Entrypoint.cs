using CatnipMod.Utilities;
using HarmonyLib;
using IroxGames.StoreFronts.Steam;
using System;
using UnityEngine;

namespace Doorstop
{
	internal class Entrypoint
	{
		public static void Start()
		{
			DoorstopUtil.UnsetEnvVarsIfNotSteamRunning();

			Debug.unityLogger.logHandler = new TimestampedLogHandler(Debug.unityLogger.logHandler);

			HarmonyUtil.CreateAndPatch();
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

		private class TimestampedLogHandler : ILogHandler
		{
			private readonly ILogHandler _defaultLogHandler;

			public TimestampedLogHandler(ILogHandler defaultLogHandler)
			{
				_defaultLogHandler = defaultLogHandler;
			}

			public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
			{
				var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				var formattedMessage = args != null && args.Length > 0
					? string.Format(format, args)
					: format;
				var timestampedFormat = $"[{timestamp}] {formattedMessage}";
				_defaultLogHandler.LogFormat(logType, context, timestampedFormat);
			}

			public void LogException(Exception exception, UnityEngine.Object context)
			{
				var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				var exceptionMessage = $"[{timestamp}] {exception}";
				_defaultLogHandler.LogFormat(LogType.Error, context, exceptionMessage);
			}
		}
	}
}
