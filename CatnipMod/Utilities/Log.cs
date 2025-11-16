using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CatnipMod.Utilities
{
	public static class Log
	{
		private static readonly string Prefix = $"[{nameof(CatnipMod)}]";

		public static void Info(params object[] messages)
		{
			Debug.Log(BuildMessage(messages));
		}

		public static void Warning(params object[] messages)
		{
			Debug.LogWarning(BuildMessage(messages));
		}

		public static void Error(params object[] messages)
		{
			Debug.LogError(BuildMessage(messages));
		}

		public static void Exception(System.Exception exception, params object[] messages)
		{
			var allMessages = new List<object>(messages)
			{
				messages.Length > 0 ? $": {exception}" : exception.ToString()
			};

			Debug.LogError(BuildMessage(allMessages.ToArray()));
		}

		private static string BuildMessage(params object[] messages)
		{
			var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			var message = string.Join(" ", messages.Select(m => m?.ToString() ?? "null"));

			return $"[{timestamp}] {Prefix} {message}";
		}
	}
}

