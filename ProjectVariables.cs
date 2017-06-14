using System;
using Microsoft.Win32;

namespace DBTableMover
{
	/// <summary>
	/// Variable that are used throughout the project, in different forms or classes
	/// </summary>
	public class ProjectVariables
	{
		private bool debug = false;
		public bool debugMode
		{
#if DEBUG
            get { return true; }
#endif
#if !DEBUG

            get{ return debug; }
#endif
        }
		public ProjectVariables()
		{
		}
		public ProjectVariables(bool debugValue)
		{
			debug = debugValue;
		}

        private string currentXMLFile;
        public string CurrentXMLFileName
        {
            get { return currentXMLFile; }
            set { currentXMLFile = value; }
        }

        public RegistryKey RegistryStorage = Registry.CurrentUser;
        public string profileLocation = @"SOFTWARE\acnicholls\Database Table Script Creator\";

    }
}
