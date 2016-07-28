using System;
using System.Reflection;

namespace DBTableMover
{
/// <summary>
/// Summary description for ProjectInfo.
/// </summary>
	public class ProjectInfo
	{
		public string Version
		{
			get
			{
				Assembly a = Assembly.GetExecutingAssembly();
				AssemblyName an = a.GetName();
				return Convert.ToString(an.Version);
			}
		}
		public string Language
		{
			get
			{
				string lang = "C#";
				return lang;
			}
		}
		public string Author
		{
			get
			{
				string auth = "AC Nicholls";
				return auth;
			}
		}
		public string Website
		{
			get
			{
				string url = "http://www.acnicholls.com";
				return url;
			}
		}
		public string Name
		{
			get
			{
				string name = "Database Table Script Creator";
				return name;
			}
		}
		public string Manufacturer
		{
			get
			{
				string fact = "acnicholls.com";
				return fact;
			}
		}

		public string Description
		{
			get
			{
				string desc = "DESCRIPTION: This program creates a text file that contains an SQL script that will create a database table and fill it with the same data as one in a database.";
				return desc;
			}
		}

		// set this for debug mode
		public bool debugMode = true;
		// set these generic titles;
		public string error = "Error...";
		public string confirm = "Confirm...";
		public string processing = "Processing...";
		public string done = "Done...";
		public string exit = "Exiting...";
		// set other global variables.
	} 
}
