using System;
using System.IO;
using System.Windows.Forms;

namespace DBTableMover
{

    /// <summary>
    /// contains methods for manipulating connectionstrings
    /// </summary>
    public class ConnectionStringFunctions
    {

        private static string currentConnectionString = string.Empty;
        /// <summary>
        /// the connectionstring currently being used
        /// </summary>
        public static string CurrentConnectionString
        {
            get
            {
                if (currentConnectionString == string.Empty)
                    currentConnectionString = RegistryFunctions.LoadFullConnectionStringFromRegistry();
                return currentConnectionString;
            }
            set { currentConnectionString = value; }
        }
        /// <summary>
        /// MySQL does not have an OLE DB type connection therefore we will need a custom form
        /// to build the connectionstring from.
        /// </summary>
        /// <returns>string</returns>
        public static bool NewMySQLConnectionString()
        {
            try
            {
                currentConnectionString = string.Empty;
                frmMySQLConnection _link = new frmMySQLConnection();
                DialogResult result = _link.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ProjectVariables.currentConType = currentConnectionType.MySQL;
                    currentConnectionString = _link.GetConnectionString;
                    RegistryFunctions.SaveConnectionStringToRegistry(currentConnectionString);
                    WriteLog("Connection String : " + currentConnectionString);
                    return true;
                }
            }
            catch (Exception f)
            {
                WriteLog(f.Message);
            }
            return false;
        }

        /// <summary>
        /// uses the MySQL Connection form to allow the user to edit the connection values
        /// </summary>
        /// <returns>true/false</returns>
        public static bool EditMySQLConnectionString()
        {
            frmMySQLConnection _link = new frmMySQLConnection(currentConnectionString);
            try
            {
                DialogResult result = _link.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ProjectVariables.currentConType = currentConnectionType.MySQL;
                    currentConnectionString = _link.GetConnectionString;
                    RegistryFunctions.SaveConnectionStringToRegistry(currentConnectionString);
                    WriteLog("Connection String : " + currentConnectionString);
                    return true;
                }
            }
            catch (Exception x)
            {
                WriteLog(x.Message);
            }
            return false;
        }
        /// <summary>
        /// gets the current MySQLConnection's database name
        /// </summary>
        public static string CurrentConnectionDatabaseName
        {
            get
            {
                string returnValue = string.Empty;
                string[] keyPairs = currentConnectionString.Split(';');
                foreach (string s in keyPairs)
                {
                    string[] values = s.Split('=');
                    if (values[0].ToLower() == "database" || values[0].ToLower() == "initial catalog")
                        returnValue = values[1];
                }
                return returnValue;
            }
        }
        /// <summary>
        /// sends the connectionstring to a file in the current folder.
        /// </summary>
        public static void WriteConnectionString()
        {

            //	save to file
            StreamWriter write = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\connectionString.txt", false);
            write.WriteLine(currentConnectionString);
            write.WriteLine();
            write.Flush();
            write.Close();
        }
        /// <summary>
        /// uses the DataLinks form to allow the user to construct a connectionstring in the proper format
        /// </summary>
        /// <returns>connection string</returns>
        public static bool NewMSSQLConnectionString()
        {
            ADODB.Connection _con = null;
            MSDASC.DataLinks _link = new MSDASC.DataLinksClass();
            _con = (ADODB.Connection)_link.PromptNew();
            if (_con == null)
                return false;
            else
            {
                ProjectVariables.currentConType = currentConnectionType.MSSQL;
                RegistryFunctions.SaveConnectionStringToRegistry(_con.ConnectionString.ToString());
                currentConnectionString = RegistryFunctions.LoadAdapterConnectionStringFromRegistry();
                WriteLog("Connection String : " + currentConnectionString);
                return true;
            }
        }

        /// <summary>
        /// uses the DataLinks form to load the current connectionstring and allows the user to modify it
        /// </summary>
        public static bool EditMSSQLConnectionString()
        {
            bool returnValue = false;
            object _con = new object();

            MSDASC.DataLinks _link = new MSDASC.DataLinksClass();
            ADODB.Connection _ado = new ADODB.Connection();
            _ado.ConnectionString = currentConnectionString;
            _con = (object)_ado;
            if (_link.PromptEdit(ref _con))
            {
                ProjectVariables.currentConType = currentConnectionType.MSSQL;
                RegistryFunctions.SaveConnectionStringToRegistry(((ADODB.Connection)_con).ConnectionString.ToString());
                currentConnectionString = RegistryFunctions.LoadAdapterConnectionStringFromRegistry();
                WriteLog("Connection String : " + currentConnectionString);
                returnValue = true;
            }
            else
            {
                returnValue = false;
            }
            return returnValue;
        }
        /// <summary>
        /// this is an internal function to write debug information to a textfile 
        /// if the cmdline option /debug is used.
        /// </summary>
        /// <param name="message"></param>
        private static void WriteLog(string message)
        {
            ProjectMethods.WriteLog("ConnectionStringFunctions", message);
        }
    }
}
