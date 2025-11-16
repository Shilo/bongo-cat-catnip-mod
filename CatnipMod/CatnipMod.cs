using UnityEngine;

namespace CatnipMod
{
	public class CatnipMod : MonoBehaviour
	{
		private void Awake()
		{
			Debug.Log("[CatnipMod] Mod initialized.");
		}

		private void OnDestroy()
		{
			Debug.Log("[CatnipMod] Mod unloaded.");
		}
	}
}
