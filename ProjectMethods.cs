using System;
using System.IO;

namespace DBTableMover
{
	/// <summary>
	/// methods that can be used throughout the project in different forms and classes
	/// </summary>
	public class ProjectMethods
	{
		public ProjectMethods()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        /// <summary>
        /// this is an internal function to write debug information to a textfile 
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLog(string message)
        {
            StreamWriter logFile = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\debug.txt", true);
            logFile.WriteLine(DateTime.Now + "    " + message);
            logFile.Flush();
            logFile.Close();
        }

        /// <summary>
        /// this is an internal function to write debug information to a textfile 
        /// </summary>
        /// <param name="caller">the name of the calling function</param>
        /// <param name="message">the message to write</param>
        public static void WriteLog(string caller, string message)
        {
            if (ProjectVariables.debugMode)
            {
// only write to the log, if the debug mode is on
                StreamWriter logFile = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\debug.txt", true);
                logFile.WriteLine(DateTime.Now + "    " + caller + "    " + message);
                logFile.Flush();
                logFile.Close();
            }
        }
    }
}
