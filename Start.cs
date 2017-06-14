using System;
using System.Windows.Forms;

namespace DBTableMover
{
	/// <summary>
	/// checks commandline options, checks the user's licence file, and starts the main form
	/// </summary>
	public class Start
	{

		private static ProjectInfo inf = new ProjectInfo();
        private static ProjectVariables projVars = new ProjectVariables();
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
                if (arg.ToUpper() == "/DEBUG")
                    projVars = new ProjectVariables(true);
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
		/// used to write to the log
		/// </summary>
		/// <param name="message">message to send to the log</param>
		private static void WriteLog(string message)
		{
			if(projVars.debugMode)
			{
                ProjectMethods.WriteLog("Start", message);
			}
		}

	}
}
