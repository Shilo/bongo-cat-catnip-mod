using HarmonyLib;

namespace CatnipMod.Utilities
{
	public static class HarmonyUtil
	{
		public static Harmony CreateAndPatch()
		{
			var id = $"com.shilo.{nameof(CatnipMod).ToLower()}";

			var harmony = new Harmony(id);
			harmony.PatchAll();

			return harmony;
		}
	}
}

