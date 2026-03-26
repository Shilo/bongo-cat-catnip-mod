using CatnipMod.Debug;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CatnipMod.Utilities
{
	/// <summary>
	/// Utility class for window operations.
	/// </summary>
	public static class WindowUtil
	{
		[DllImport("user32.dll")]
		private static extern IntPtr GetActiveWindow();

		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetWindowText(IntPtr hWnd, string lpString);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

		/// <summary>
		/// Appends the specified text to the window title.
		/// </summary>
		/// <param name="monoBehaviour">The MonoBehaviour instance to start the coroutine on.</param>
		/// <param name="textToAppend">The text to append to the window title.</param>
		public static void AppendToWindowTitle(MonoBehaviour monoBehaviour, string textToAppend)
		{
			if (monoBehaviour == null)
				throw new ArgumentNullException(nameof(monoBehaviour));
			if (string.IsNullOrEmpty(textToAppend))
				throw new ArgumentException("Text to append cannot be null or empty.", nameof(textToAppend));

			monoBehaviour.StartCoroutine(UpdateWindowTitleCoroutine(textToAppend));
		}

		private static IEnumerator UpdateWindowTitleCoroutine(string textToAppend)
		{
			// Wait for the window to be focused before trying to update the title
			yield return new WaitUntil(() => Application.isFocused);
			yield return null;

			try
			{
				IntPtr hWnd = GetActiveWindow();
				if (hWnd != IntPtr.Zero)
				{
					// Get current window title
					var stringBuilder = new System.Text.StringBuilder(256);
					GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);
					string currentTitle = stringBuilder.ToString();

					// Append text if not already present
					if (!currentTitle.Contains(textToAppend))
					{
						string newTitle = $"{currentTitle}{textToAppend}";
						SetWindowText(hWnd, newTitle);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Warn($"Failed to update window title: {ex.Message}");
			}
		}
	}
}

