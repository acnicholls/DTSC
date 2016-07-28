using System;
using System.Windows.Forms;
using System.IO;

namespace DBTableMover
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public class Start
	{
////		static bool debugMode = true;
////		static string error = "Error...";
////		static string confirm = "Confirm...";
////		static string processing = "Processing...";
////		static string done = "Done...";
////		static string exit = "Exiting...";
///

		private static ProjectInfo inf = new ProjectInfo();
		public Start()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			foreach(string arg in args)
			{
				if(arg.ToUpper() == "/DEBUG")
					inf.debugMode = true;
			}
			Licence lic = new Licence();
			if(lic.Valid)
			{
				try
				{
					Application.Run(new frmMain());
				}
				catch(Exception x)
				{
					WriteLog("Error Running Main Form : Is your SQL Server running ????");
					WriteLog(x.Message.ToString());
				}
			}
			else
			{
				MessageBox.Show("Bad Licence, contact your vendor.", inf.error, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				inf.error = "NEW Error titles....";
				WriteLog("Bad License Attempt");
				Application.Exit();
			}
		}
		/// <summary>
		/// this is an internal function to write debug information to a textfile 
		/// if the cmdline option /debug is used.
		/// </summary>
		/// <param name="message"></param>
		private static void WriteLog(string message)
		{
			if(inf.debugMode)
			{
				// this function writes a log of important info and is useful for debugging
				StreamWriter logFile = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\debug.txt",true);
				logFile.WriteLine(DateTime.Now + "    " + message);
				logFile.Flush();
				logFile.Close();   
			}
		}



	}
}
