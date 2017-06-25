using System;
using System.Reflection;

namespace DBTableMover
{
/// <summary>
/// class containing information regarding the current project
/// </summary>
	public class ProjectInfo
	{
        /// <summary>
        /// the version of the currently executing assemmbly
        /// </summary>
		public string Version
		{
			get
			{
				Assembly a = Assembly.GetExecutingAssembly();
				AssemblyName an = a.GetName();
				return Convert.ToString(an.Version);
			}
		}

        /// <summary>
        /// the language the program is written in
        /// </summary>
		public string Language
		{
			get
			{
				string lang = "C#";
				return lang;
			}
		}

        /// <summary>
        /// the author of the program
        /// </summary>
		public string Author
		{
			get
			{
				string auth = "AC Nicholls";
				return auth;
			}
		}

        /// <summary>
        /// the website of the author and/or program
        /// </summary>
		public string Website
		{
			get
			{
				string url = "http://ac.is-a-guru.com";
				return url;
			}
		}

        /// <summary>
        /// the name of the program
        /// </summary>
		public string Name
		{
			get
			{
				string name = "Database Table Script Creator";
				return name;
			}
		}

        /// <summary>
        /// the manufacturer fo the program
        /// </summary>
		public string Manufacturer
		{
			get
			{
				string fact = "acnicholls.com";
				return fact;
			}
		}

        /// <summary>
        /// the description of the program
        /// </summary>
		public string Description
		{
			get
			{
				string desc = "DESCRIPTION: This program creates a text file that contains an SQL script that will create a database table and fill it with the same data as one in a database.";
				return desc;
			}
		}

		// set these generic titles;
        /// <summary>
        /// generic error message
        /// </summary>
		public string error = "Error...";

        /// <summary>
        /// generic confirmation message
        /// </summary>
		public string confirm = "Confirm...";

        /// <summary>
        /// generic processing message
        /// </summary>
		public string processing = "Processing...";

        /// <summary>
        /// generic done message
        /// </summary>
		public string done = "Done...";

        /// <summary>
        /// generic exiting message
        /// </summary>
		public string exit = "Exiting...";
	} 
}
