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
		public ProjectVariables(bool debug)
		{
			//
			// TODO: Add constructor logic here
			//
			debug = true;
		}
	}
}
