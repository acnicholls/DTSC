using System;

namespace DBTableMover
{
	/// <summary>
	/// Summary description for ProjectVariables.
	/// </summary>
	public class ProjectVariables
	{
		private bool debug;
		public bool debugMode
		{
			get{ return debug; }
		}
		public ProjectVariables()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public ProjectVariables(bool debugValue)
		{
			//
			// TODO: Add constructor logic here
			//
			debug = debugValue;
		}

        private string currentXMLFile;
        public string CurrentXMLFileName
        {
            get { return currentXMLFile; }
            set { currentXMLFile = value; }
        }
	}
}
