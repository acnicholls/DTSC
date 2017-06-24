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

        /// <summary>
        /// provides a variable to show the program if debug mode is on
        /// </summary>
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

        /// <summary>
        /// provides the hive location of registry storage
        /// </summary>
        public static RegistryKey RegistryStorage = Registry.CurrentUser;

        /// <summary>
        /// provides the key name of registry storage
        /// </summary>
        public static string profileLocation = @"SOFTWARE\acnicholls\Database Table Script Creator\";

    }
}
