using CatnipMod.Utilities;
using UnityEngine;

namespace CatnipMod
{
	public class CatnipMod : MonoBehaviour
	{
		private void Awake()
		{
			Log.Info("Mod loaded.");
		}

		private void OnDestroy()
		{
			Log.Info("Mod unloaded.");
		}
	}
}
