using System;
using Microsoft.Win32;

namespace DBTableMover
{
    /// <summary>
    /// contains methods for using the MS Windows Registry
    /// </summary>
    public class RegistryFunctions
    {
        /// <summary>
        /// saves a connection string to the user's registry.  will be loaded automatically each time the program is run.
        /// </summary>
        /// <param name="connection">the connection string you want to load</param>
        public static void SaveConnectionStringToRegistry(string connection)
        {
            try
            {
                string connectionVar = string.Empty;
                // split connection into it's separate parts
                WriteLog("ConnectionString = " + connection);
                string[] provider2 = connection.Split(';');
                // open registry location to save to
                RegistryKey storage = ProjectVariables.RegistryStorage.CreateSubKey(ProjectVariables.profileLocation);
                switch (ProjectVariables.currentConType)
                { 
                    case currentConnectionType.MSSQL:
                    {
                        foreach (string p in provider2)
                        {
                            // if it's the provider string save it to registry
                            // if not, compile a string for the adapter
                            if (p.StartsWith("Provider="))
                                storage.SetValue("Provider", provider2[0].ToString());
                            else
                                connectionVar += p + ";";
                        }
                        break;
                    }
                case currentConnectionType.MySQL:
                    {
                        // Provider=MSDASQL
                        storage.SetValue("Provider", "Provider=MSDASQL");
                        foreach (string p in provider2)
                        {
                            connectionVar += p + ";";
                        }
                        break;
                    }
                }
                // now save the connection string in registry.
                storage.SetValue("AdapterConnection", connectionVar);
                storage.SetValue("ConnectionString", connection + ";");
                storage.SetValue("ConnectionType", ProjectVariables.currentConType.ToString());

            }
            catch(Exception x)
            {
                WriteLog(x.Message);
            }
        }

        /// <summary>
        /// loads both the provider and the connection string from the registry
        /// provider is required for the DataLinks form's first page
        /// </summary>
        /// <returns>full connectionString</returns>
        public static string LoadFullConnectionStringFromRegistry()
        {
            try
            {
                RegistryKey storage = ProjectVariables.RegistryStorage.OpenSubKey(ProjectVariables.profileLocation);
                string provider = storage.GetValue("Provider").ToString();
                string connection = storage.GetValue("AdapterConnection").ToString();
                return provider + ";" + connection;
            }
            catch(Exception x)
            {
                WriteLog(x.Message);
            }
            return string.Empty;
        }

        /// <summary>
        /// loads just the connection string from the registry.  all that's required for most connections
        /// </summary>
        /// <returns>connectionString</returns>
        public static string LoadAdapterConnectionStringFromRegistry()
        {
            try
            {
                RegistryKey storage = ProjectVariables.RegistryStorage.OpenSubKey(ProjectVariables.profileLocation);
                string connection = storage.GetValue("AdapterConnection").ToString();
                return connection;
            }
            catch(Exception x)
            {
                WriteLog(x.Message);
            }
            return string.Empty;
        }

        /// <summary>
        /// checks to see if there is a connection saved in the registry
        /// </summary>
        public static bool IsSavedConnection
        {
            get
            {
                try
                {
                    RegistryKey storage = ProjectVariables.RegistryStorage.OpenSubKey(ProjectVariables.profileLocation);
                    string provider = storage.GetValue("ConnectionType").ToString();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        /// <summary>
        /// checks the type of the saved connection
        /// </summary>
        public static currentConnectionType SavedConnectionType
        {
            get
            {
                RegistryKey storage = ProjectVariables.RegistryStorage.OpenSubKey(ProjectVariables.profileLocation);
                string provider = storage.GetValue("ConnectionType").ToString();
                switch (provider)
                {
                    case "MSSQL":
                        {
                            return currentConnectionType.MSSQL;
                        }
                    case "MySQL":
                        {
                            return currentConnectionType.MySQL;
                        }
                }
                return currentConnectionType.XML;
            }
        }

        /// <summary>
        /// this is an internal function to write debug information to a textfile 
        /// if the cmdline option /debug is used.
        /// </summary>
        /// <param name="message"></param>
        private static void WriteLog(string message)
        {
            ProjectMethods.WriteLog("RegistryFunctions", message);
        }
    }
}
