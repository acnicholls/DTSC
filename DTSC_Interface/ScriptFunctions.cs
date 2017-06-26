using System;

namespace DBTableMover
{
    /// <summary>
    /// contains the methods that call the correct script generators
    /// </summary>
    public class ScriptFunctions
    {

        /// <summary>
        /// creates a table creation script for the passed in table name in the currently connected database.
        /// </summary>
        /// <param name="tableName"></param>
        public static string CreateTableScript(string tableName)
        {
            string tableScript = "";
            try
            {
                switch (ProjectVariables.currentConType)
                {
                    case currentConnectionType.MSSQL:
                        {
                            SqlFunctions sqlFun = new SqlFunctions();
                            tableScript = sqlFun.CreateTableScript(tableName);
                            break;
                        }
                    case currentConnectionType.XML:
                        {
                            XmlFunctions xmlFun = new XmlFunctions();
                            tableScript = xmlFun.CreateTableScript(tableName);
                            break;
                        }
                    case currentConnectionType.MySQL:
                        {
                            MySQLFunctions mysqlFun = new MySQLFunctions();
                            tableScript = mysqlFun.CreateTableScript(tableName);
                            break;
                        }
                }
            }
            catch (Exception x)
            {
                ProjectMethods.WriteLog("CreateTableScript", x.Message);
            }
            return tableScript;
        }

        /// <summary>
        /// creates an insert script for each row in the passed in table
        /// </summary>
        /// <param name="tableName"></param>
        public static string CreateValueScript(string tableName)
        {
            string valueScript = "";
            try
            {
                switch (ProjectVariables.currentConType)
                {
                    case currentConnectionType.MSSQL:
                        {
                            SqlFunctions sqlFun = new SqlFunctions();
                            valueScript = sqlFun.CreateValueScript(tableName);
                            break;
                        }
                    case currentConnectionType.XML:
                        {
                            XmlFunctions xmlFun = new XmlFunctions();
                            valueScript = xmlFun.CreateValueScript(tableName);
                            break;
                        }
                    case currentConnectionType.MySQL:
                        {
                            MySQLFunctions mysqlFun = new MySQLFunctions();
                            valueScript = mysqlFun.CreateValueScript(tableName);
                            break;
                        }

                }
            }
            catch (Exception x)
            {
                ProjectMethods.WriteLog("CreateValueScript", x.Message);
            }
            return valueScript;
        }
    }
}
