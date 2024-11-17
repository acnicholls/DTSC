using System;
using Microsoft.Win32;

namespace DBTableMover
{
	/// <summary>
	/// Variable that are used throughout the project, in different forms or classes
	/// </summary>
	public class ProjectVariables
	{
		private static bool debug = false;
		public static bool debugMode
		{
#if DEBUG
            get { return true; }
#endif
#if !DEBUG

            get{ return debug; }
#endif
            set { debug = value; }
        }
        public static RegistryKey RegistryStorage = Registry.CurrentUser;
        public static string profileLocation = @"SOFTWARE\acnicholls\Database Table Script Creator\";

    }
}
