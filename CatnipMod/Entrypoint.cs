using System.IO;

namespace Doorstop
{
	internal class Entrypoint
	{
		public static void Start()
		{
			File.WriteAllText("catnip.log", "Hello world!");
		}
	}
}
