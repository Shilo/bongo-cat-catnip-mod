using CatnipMod.Debug;
using System.Reflection;
using UnityEngine;

namespace CatnipMod
{
	public class CatnipMod : MonoBehaviour
	{
		private void Awake()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var versionAttr = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
			var companyAttr = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();

			var version = versionAttr?.Version ?? "Unknown";
			var company = companyAttr?.Company ?? "Unknown";

			Log.Info($"Mod v{version} by {company} loaded.");
		}

		private void OnDestroy()
		{
			Log.Info("Mod unloaded.");
		}
	}
}
