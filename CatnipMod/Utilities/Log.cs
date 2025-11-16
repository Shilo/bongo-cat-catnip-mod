using UnityEngine;

namespace CatnipMod.Utilities
{
	public static class Log
	{
		private static string Prefix = $"[{nameof(CatnipMod)}]";

		public static void Info(string message)
		{
			Debug.Log($"{Prefix} {message}");
		}

		public static void Warning(string message)
		{
			Debug.LogWarning($"{Prefix} {message}");
		}

		public static void Error(string message)
		{
			Debug.LogError($"{Prefix} {message}");
		}

		public static void Exception(System.Exception exception, string message = null)
		{
			var logMessage = message != null ? $"{message}: {exception}" : exception.ToString();
			Debug.LogError($"{Prefix} {logMessage}");
		}
	}
}

