using System;
using System.IO;
using System.Windows.Forms;

namespace DBTableMover
{
	/// <summary>
	/// Summary description for Licence.
	/// </summary>
	public class Licence
	{
		//private frmMain innerForm;

		//private static int Activated;

		private string outerkey;
		private byte[] KEY
		{
			get
			{
				   byte[] sysKey = new byte[36];
				   string key = "9940c1d7-ec41-455a-be84-c5eebd4d811d";
				   int i = 0;
				   foreach(char c in key.ToCharArray())
				   {
					   sysKey[i] = Convert.ToByte(c);
					   i++;
				   }
				   return sysKey;
			 }
		}
		private string innerkey = "c0cbe7cb-23de-4b3c-ac36-627cfd8eef24";
		private byte[] CODE
		{
			get
			{
				   byte[] sysCode = new byte[36];
				   int i = 0;
				   foreach(char c in innerkey.ToCharArray())
				   {
					   sysCode[i] = Convert.ToByte(c);
					   i++;
				   }
				   return sysCode;
			   }
		}

		/// <summary>
		/// this function only tests to see if the entered strings are exactly equal to themselves.
		/// only the customer's keyfile matters, the inner code it not important.
		/// Other logic could include that certain customer numbers where not allowed if the 
		/// inner key were not equal to a certain value, functions that use the new value
		/// could be included in secondary files, and calls to them could be monitored
		/// </summary>
		/// <returns>whether or not the licence key is valid.</returns>
		public bool Valid
		{
			get
			{
				bool temp = false;
				byte[] ourCode = CODE; /* Array.CreateInstance(System.Type.GetType("byte"),40);*/
				byte[] theirCode = new byte[36];  /* Array.CreateInstance(System.Type.GetType("byte"),40);*/
				try
				{
					if(outerkey.Equals(null))
					{ // call frmMain.MessageBox(string inner message, Buttons, icon, title);
						MessageBoxButtons buttonsOnForm = MessageBoxButtons.OK;
						MessageBoxIcon iconOnForm = MessageBoxIcon.Error;
						MessageBox.Show("You have not entered the KeyCode.", "Error", buttonsOnForm, iconOnForm);
					}
					else
					{
						int i = 0;
						foreach(char c in outerkey.ToCharArray())
						{
							theirCode[i] = Convert.ToByte(c);
							i++;
						}
						bool codeMatch = testByteArrays(ourCode, CODE);
						bool keyMatch = testByteArrays(theirCode, KEY);
						// two spare calls for debugging.
						if(codeMatch&keyMatch)
							temp = true;
					}
				}
				catch(Exception x)
				{
					WriteLog("Licensing Error : " + x.Message.ToString());
				}

				return temp;
			}
		}

        /// <summary>
        /// default constructor for the licence functions
        /// </summary>
        public Licence()
		{
			// have this pull the data from a registry entry or from the license file
			FileInfo licFile = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"\Licence.txt");
			if(licFile.Exists)
			{
				StreamReader reader = new StreamReader(licFile.FullName);
				outerkey = reader.ReadLine();
			}
#if DEBUG
            else
			{
			    string licTXT = "9940c1d7-ec41-455a-be84-c5eebd4d811d";
				outerkey = licTXT;
			}
#endif
        }
		private bool testByteArrays(byte[] test1, byte[] test2)
		{
			int i = test1.Length;
			int x = test2.Length;
			if(x!=i)
				return false;
			else
			{
				x = 0;
				foreach(byte b in test1)
				{
					byte testByte = test2[x];
					if(testByte!=b)
						return false;
					x++;
				}
				return true;
			}
		}

		/// <summary>
		/// this is an internal function to write debug information to a textfile 
		/// </summary>
		/// <param name="message">the message to send to the log</param>
		private void WriteLog(string message)
		{
            ProjectMethods.WriteLog("Licence", message);
		}
		
	}
}
