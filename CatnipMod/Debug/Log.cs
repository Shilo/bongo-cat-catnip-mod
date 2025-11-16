using CatnipMod.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace CatnipMod.Debug
{
	public static class Log
	{
		private static readonly string Prefix = $"[{nameof(CatnipMod)}]";

		public static void SetupTimestampHandler()
		{
			UnityEngine.Debug.unityLogger.logHandler = new TimestampLogHandler(UnityEngine.Debug.unityLogger.logHandler);
		}

		public static void Info(params object[] messages)
		{
			UnityEngine.Debug.Log(BuildMessage(messages));
		}

		public static void Warning(params object[] messages)
		{
			UnityEngine.Debug.LogWarning(BuildMessage(messages));
		}

		public static void Error(params object[] messages)
		{
			UnityEngine.Debug.LogError(BuildMessage(messages));
		}

		public static void Exception(System.Exception exception, params object[] messages)
		{
			var allMessages = new List<object>(messages)
			{
				messages.Length > 0 ? $": {exception}" : exception.ToString()
			};

			UnityEngine.Debug.LogError(BuildMessage(allMessages.ToArray()));
		}

		private static string BuildMessage(params object[] messages)
		{
			var callerClass = RuntimeUtil.GetCallerClassName(skipFrames: 3);
			var callerClassPrefix = callerClass != null ? $"{callerClass} | " : string.Empty;
			var message = string.Join(" ", messages.Select(m => m?.ToString() ?? "null"));

			return $"{Prefix} {callerClassPrefix}{message}";
		}
	}
}

