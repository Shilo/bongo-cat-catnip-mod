using System.Reflection;

namespace CatnipMod.Utilities
{
	/// <summary>
	/// Utility class for assembly operations.
	/// </summary>
	public static class AssemblyUtil
	{
		/// <summary>
		/// Gets assembly information (product name, version, company).
		/// </summary>
		/// <param name="assembly">The assembly to get information from. If null, uses the executing assembly.</param>
		/// <returns>A tuple containing (productName, version, company).</returns>
		public static (string productName, string version, string company) GetAssemblyInfo(Assembly assembly = null)
		{
			const string UnknownValue = "Unknown";

			assembly = assembly ?? Assembly.GetExecutingAssembly();

			var productName = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? UnknownValue;
			var version = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? UnknownValue;
			var company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? UnknownValue;

			return (productName, version, company);
		}
	}
}

