using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DBTableMover
{
    /// <summary>
    /// Summary description for MySQLFunctions.
    /// </summary>
    public class MySQLFunctions
    {
        // For this database type we will get the required information from the following query
        // 
        // 
        // select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'help_keyword';
        //
        // the following column list will be returned
        // 
        // TABLE_CATALOG                - unused - usually null
        // TABLE_SCHEMA                 - unused - database name
        // TABLE_NAME                   - unused - ID row, columns can be filtered per table
        // COLUMN_NAME                  - name of the column
        // ORDINAL_POSITION             - unused - position in the list of columns
        // COLUMN_DEFAULT               - default value of the column
        // IS_NULLABLE                  - is the column allowed to contain null values?
        // DATA_TYPE                    - type of the data in the column
        // CHARACTER_MAXIMUM_LENGTH     - max characters allowed in column
        // CHARACTER_OCTET_LENGTH       - unused - max characters * 3?
        // NUMERIC_PRECISION            - total number of digits allowed 
        // NUMERIC_SCALE                - number of digits allowed after the decimal
        // CHARACTER_SET_NAME           - type of characters in the column
        // COLLATION_NAME               - collation used in the column
        // COLUMN_TYPE                  - unused - mysql data type of the column 
        // COLUMN_KEY                   - is the column a key of any kind?  (PRI, UNI)
        // EXTRA                        - extra information regarding the column (auto_increment)
        // PRIVILEGES                   - privileges allowed on this column - unused
        // COLUMN_COMMENT               - comments - unused

        private string tableScript;
        private string valueScript;
        private DataSet dsTable = new DataSet();

        private MySql.Data.MySqlClient.MySqlConnection conMySQLConnection = new MySqlConnection(ConnectionStringFunctions.CurrentConnectionString);

        /// <summary>
        /// figures out which type of script to create and calls the appropriate method
        /// </summary>
        /// <param name="tableName">name of the table to create the script for</param>
        /// <param name="outputType">type of database to create the script for</param>
        /// <returns>string</returns>
        public string CreateTableScript(string tableName, ScriptOutputType outputType)
        {
            switch(outputType)
            {
                case ScriptOutputType.MsSQL:
                    {
                        return CreateMSSQLTableScript(tableName);
                    }
                case ScriptOutputType.MySQL:
                    {
                        return CreateMySQLTableScript(tableName);
                    }
                default:
                    {
                        return string.Empty;
                    }
            }
        }

        private string CreateMySQLTableScript(string tableName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// creates the actual script for the passed in table
        /// </summary>
        /// <param name="tableName">table to create a script for</param>
        /// <returns>string</returns>
        private string CreateMSSQLTableScript(string tableName)
        {

            this.tableScript = "";
            try
            {
                // declare variables for specific items
                string columnName = "";
                int seed = 0;
                int inc = 0;
                string columnType = "";
                long columnLen = 0;
                string columnNull = "";
                string columnColl = "";
                int columnPrec = 0;
                int columnScale = 0;

                dsTable.Tables.Clear();
                MySqlDataAdapter adap = new MySqlDataAdapter();
                MySqlCommand comm = conMySQLConnection.CreateCommand();
                comm.CommandType = CommandType.Text;
               // comm.CommandText = "SHOW CREATE TABLE " + tableName;
                comm.CommandText = "select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tableName + "'";
                adap.SelectCommand = comm;
                conMySQLConnection.Open();
                adap.Fill(dsTable);
                conMySQLConnection.Close();
                dsTable.WriteXml(Environment.CurrentDirectory + @"\MySQL_TableDef.xml");
                // insert drop and create scripts
                tableScript = "if exists(select name from sysobjects where name='" + tableName + "' and type='U')\r\nBEGIN\r\ndrop table " + tableName + "\r\nEND\r\nGO\r\n\r\n";
                tableScript += "create table [dbo].[" + tableName + "]\r(\n";
                // find identity column
                string[] keyName = CheckForIdentity();
                if(keyName != new string[0])
                {
                    seed = 1;
                    inc = 1;
                }
                // grab column names
                int cols = 0;
                foreach(DataRow row in dsTable.Tables[0].Rows)
                {
                    string columnOnly = "";
                    if (cols >= 1)
                        columnOnly += ",\n";
                    columnName = row["COLUMN_NAME"].ToString();
                    columnType = row["DATA_TYPE"].ToString();
                    if (row["CHARACTER_MAXIMUM_LENGTH"].ToString() != "")
                        columnLen = Convert.ToInt64(row["CHARACTER_MAXIMUM_LENGTH"].ToString());
                    else
                        columnLen = 0;
                    if (row["NUMERIC_PRECISION"].ToString() != "")
                        columnPrec = Convert.ToInt32(row["NUMERIC_PRECISION"].ToString());
                    else
                        columnPrec = 0;
                    if (row["NUMERIC_SCALE"].ToString() != "")
                        columnScale = Convert.ToInt32(row["NUMERIC_SCALE"].ToString());
                    else
                        columnScale = 0;
                    if (row["IS_NULLABLE"].ToString().ToUpper() == "YES")
                        columnNull = "NULL";
                    else
                        columnNull = "NOT NULL";
                    if (row["COLLATION_NAME"].ToString() == "")
                        columnColl = "";
                    else
                        columnColl = row["COLLATION_NAME"].ToString();
                    // start the column's script
                    columnOnly += "[" + columnName + "] ";
                    // now get the column's official type
                    switch (columnType.ToLower())
                    {
                        case "char":
                            {
                                columnOnly += "[char]";
                                // now set the max characters allowed
                                if (columnLen > 8000)
                                    columnOnly += "(MAX)";
                                else
                                    columnOnly += "(" + columnLen + ")";
                                break;

                            }
                        case "varchar":
                        case "tinytext":
                        case "mediumtext":
                        case "longtext":
                        case "text":
                        case "json":
                            {
                                columnOnly += "[nvarchar]";
                                // now set the max characters allowed
                                if (columnLen > 4000)
                                    columnOnly += "(MAX)";
                                else
                                    columnOnly += "(" + columnLen + ")";
                                break;
                            }
                        case "binary":
                        case "varbinary":
                        case "blob":
                        case "tinyblob":
                        case "mediumblob":
                        case "longblob":
                            {
                                columnOnly += "[varbinary]";
                                // now set the max characters allowed
                                if (columnLen == -1)
                                    columnOnly += "(MAX)";
                                else
                                    columnOnly += "(" + columnLen + ")";
                                break;
                            }
                        case "datetime":
                            {
                                columnOnly += "[datetime2]";
                                break;
                            }
                        case "timestamp":
                            {
                                columnOnly += "[smalldatetime]";
                                break;
                            }
                        case "date":
                            {
                                columnOnly += "[date]";
                                break;
                            }
                        case "time":
                            {
                                columnOnly += "[time]";
                                break;
                            }
                        case "bit":
                            {
                                columnOnly += "[bit]";
                                break;
                            }

                        case "decimal":
                            {
                                columnOnly += "[decimal]";
                                // now set the precision and scale
                                if (columnPrec == 0 & columnScale == 0)
                                    break;
                                else if (columnPrec > 0 & columnScale == 0)
                                    columnOnly += "(" + columnPrec.ToString() + ")";
                                else if (columnPrec > 0 & columnScale > 0)
                                    columnOnly += "(" + columnPrec.ToString() + "," + columnScale.ToString() + ")";
                                break;
                            }

                        case "double":
                        case "float":
                        case "real":
                            {
                                columnOnly += "[float]";
                                // now set the precision and scale
                                if (columnPrec == 0 & columnScale == 0)
                                    break;
                                else if (columnPrec > 0 & columnScale == 0)
                                    columnOnly += "(" + columnPrec.ToString() + ")";
                                else if (columnPrec > 0 & columnScale > 0)
                                    columnOnly += "(" + columnPrec.ToString() + "," + columnScale.ToString() + ")";
                                break;
                            }
                        case "tinyint":
                            {
                                columnOnly += "[tinyint]";
                                // now set the precision and scale
                                if (columnPrec == 0 & columnScale == 0)
                                    break;
                                else if (columnPrec > 0 & columnScale == 0)
                                    columnOnly += "(" + columnPrec.ToString() + ")";
                                else if (columnPrec > 0 & columnScale > 0)
                                    columnOnly += "(" + columnPrec.ToString() + "," + columnScale.ToString() + ")";
                                break;
                            }
                        case "smallint":
                        case "year":
                            {
                                columnOnly += "[smallint]";
                                // now set the precision and scale
                                if (columnPrec == 0 & columnScale == 0)
                                    break;
                                else if (columnPrec > 0 & columnScale == 0)
                                    columnOnly += "(" + columnPrec.ToString() + ")";
                                else if (columnPrec > 0 & columnScale > 0)
                                    columnOnly += "(" + columnPrec.ToString() + "," + columnScale.ToString() + ")";
                                break;
                            }
                        case "int":
                        case "mediumint":
                            {
                                columnOnly += "[int]";
                                // now set the precision and scale
                                if (columnPrec == 0 & columnScale == 0)
                                    break;
                                else if (columnPrec > 0 & columnScale == 0)
                                    columnOnly += "(" + columnPrec.ToString() + ")";
                                else if (columnPrec > 0 & columnScale > 0)
                                    columnOnly += "(" + columnPrec.ToString() + "," + columnScale.ToString() + ")";
                                break;
                            }
                        case "bigint":
                            {
                                columnOnly += "[bigint]";
                                // now set the precision and scale
                                if (columnPrec == 0 & columnScale == 0)
                                    break;
                                else if (columnPrec > 0 & columnScale == 0)
                                    columnOnly += "(" + columnPrec.ToString() + ")";
                                else if (columnPrec > 0 & columnScale > 0)
                                    columnOnly += "(" + columnPrec.ToString() + "," + columnScale.ToString() + ")";
                                break;
                            }
                        case "enum":
                            {
                                // MSSQL does not have an "enum" data type, instead we use a CHECK constraint
                                // step 1 - find all the enumerations
                                string colType = row["COLUMN_TYPE"].ToString();
                                // remove the part "enum(" from the start
                                colType = colType.Substring(5);
                                // remove ")" from the end
                                colType = colType.Substring(0, colType.Length - 1);
                                // remove any quotes
                                colType = colType.Replace("'","");
                                // split the remainder into individual variables
                                string[] enums = colType.Split(',');
                                // step 2 - find the data type of the enumerations
                                bool columnHasCharData = false;
                                WriteLog("Enum Column");
                                foreach (string s in enums)
                                {
                                    WriteLog("testing : " + s);
                                    if (!IsNumeric(s))
                                        columnHasCharData = true;
                                }
                                if (columnHasCharData)
                                    columnOnly += "[char]";
                                else
                                    columnOnly += "[int]";
                                // step 3 - find the longest one
                                int maxLength = 0;
                                foreach(string s in enums)
                                {
                                    if (s.Length > maxLength)
                                        maxLength = s.Length;
                                }
                                columnOnly += "(" + maxLength.ToString() + ")";
                                // step 4 - create the column script
                                columnOnly += " " + columnNull + " CHECK (" + columnName + " IN (";
                                int count = 0;
                                foreach(string s in enums)
                                {
                                    if (count < enums.Length - 1)
                                        columnOnly += "'" + s + "',";
                                    else
                                        columnOnly += "'" + s + "'";
                                    count++;
                                }
                                columnOnly += "))";
                                break;
                            }
                   }
                    if (columnType != "enum")
                    {
                        // build the identity part of the script
                        if (CheckForKeyRow(keyName, columnName))
                            columnOnly += " IDENTITY(" + seed + "," + inc + ")";
                        // add the collation and NULLness of the column
                        if (columnColl == "")
                            columnOnly += " " + columnNull;
                        else
                            columnOnly += " COLLATE " + columnColl + " " + columnNull;
                    }
                    // add one to the number of columns
                    cols++;
                    // add column text to script
                    tableScript += columnOnly;
                }
                // now add the primary key if there is one
                string PrimaryKeyClause = CheckForPrimaryKey(tableName);
                if (PrimaryKeyClause.Length > 0)
                    tableScript += PrimaryKeyClause;
                // now close the script for the table
                tableScript += ") ON [PRIMARY]\r\nGO\r\n";

            }
            catch (Exception x)
            {
                WriteLog(x.Message);
            }
            finally
            {
                if(conMySQLConnection.State == ConnectionState.Open)
                    conMySQLConnection.Close();
            }
            return tableScript;
        }

        /// <summary>
        /// checks to see if the string passed can be converted to an Integer
        /// </summary>
        /// <param name="stringToCheck">string to convert to integer</param>
        /// <returns>true/false</returns>
        private bool IsNumeric(string stringToCheck)
        {
            bool returnValue = false;
            try
            {
                Convert.ToInt32(stringToCheck);
                returnValue = true;
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }

        /// <summary>
        /// figures out which type of script to create and calls the appropriate method
        /// </summary>
        /// <param name="tableName">name of the table to create the script for</param>
        /// <param name="outputType">type of database to create the script for</param>
        /// <returns>string</returns>
        public string CreateValueScript(string tableName, ScriptOutputType outputType)
        {
            switch (outputType)
            {
                case ScriptOutputType.MsSQL:
                    {
                        return CreateMSSQLValueScript(tableName);
                    }
                case ScriptOutputType.MySQL:
                    {
                        return CreateMySQLValueScript(tableName);
                    }
                default:
                    {
                        return string.Empty;
                    }
            }
        }

        private string CreateMySQLValueScript(string tableName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// creates insert scripts for each row in the passed in table
        /// </summary>
        /// <param name="tableName">name of the table to create scripts for</param>
        /// <returns>string</returns>
        private string CreateMSSQLValueScript(string tableName)
        {
            valueScript = string.Empty;
            try
            {
                MySql.Data.MySqlClient.MySqlDataAdapter adap = new MySqlDataAdapter();
                MySqlCommand comm = conMySQLConnection.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = "select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tableName + "'";
                adap.SelectCommand = comm;
                conMySQLConnection.Open();
                adap.Fill(dsTable);
                conMySQLConnection.Close();
                // get ID columns
                string[] keyrows = CheckForIdentity();
                // create value row scripts
                this.dsTable.Tables.Clear();
                comm = conMySQLConnection.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = "select * from " + tableName;
                adap.SelectCommand = comm;
                conMySQLConnection.Open();
                adap.Fill(dsTable);
                conMySQLConnection.Close();
                // check the table for data
                int rowCount = dsTable.Tables[0].Rows.Count;
                if (rowCount == 0)
                {
                    MessageBox.Show("There is no data to script for.", "Empty Table...");
                    Exception x = new Exception("Empty Table");
                    throw (x);
                }
                string columnNames = string.Empty;
                DataRow columnRow = dsTable.Tables[0].Rows[0];
                int c = 0;
                foreach (DataColumn col in columnRow.Table.Columns)
                {
                    if (CheckForKeyRow(keyrows, col.ColumnName.ToString()))
                        continue;
                    else
                    {
                        c++;
                        if (c == 1)
                            columnNames += col.ColumnName.ToString();
                        else
                            columnNames += "," + col.ColumnName.ToString();
                    }
                }
                // now grab the values and build the SQL
                foreach (DataRow row in dsTable.Tables[0].Rows)
                {
                    int r = 0;  // column in the DataRow
                    valueScript += "INSERT INTO [dbo].[" + tableName + "] (" + columnNames + ") VALUES (";
                    int n = 0;  // number of columns added to script
                    foreach (DataColumn col in row.Table.Columns)
                    {
                        if (CheckForKeyRow(keyrows, col.ColumnName.ToString()))
                        {
                            r++;
                            continue;
                        }
                        else
                        {
                            if (n > 0)
                                valueScript += ",";
                            string colType = col.DataType.ToString();
                            WriteLog("Col Name : " + col.ColumnName.ToString());
                            WriteLog("colType :" + colType);
                            WriteLog("ToString : " + col.ToString());
                            WriteLog(" \n");
                            if (Convert.IsDBNull(row[r]))
                                valueScript += "NULL";
                            else
                            {
                                // depending on dataType, set up the text that will insert the value correctly
                                // current problem datatypes
                                // date - detected as System.DateTime
                                // mediumint - detected as System.Int32
                                // decimal - detected as System.Decimal
                                // numeric only ENUM - detected as System.String
                                // blob  - detected as System.Byte[]
                                // medium text - detected as System.String
                                // year - detected as System.Int32
                                // json - detected as System.String
                                #region datatype DataFixes
                            switch(colType)
                                {
                                    case "System.DateTime":
                                        {
                                            DateTime dbdate = Convert.ToDateTime(row[r]);
                                            valueScript += "'" + dbdate.ToString("yyyyMMdd HH:mm:ss") + "'";
                                            break;
                                        }
                                    case "System.Int64":
                                        {
                                            valueScript += Convert.ToInt64(row[r]);
                                            break;
                                        }
                                    case "System.Int32":
                                        {
                                            valueScript += Convert.ToInt32(row[r]);
                                            break;
                                        }
                                    case "System.Int16":
                                        {
                                            valueScript += Convert.ToInt16(row[r]);
                                            break;
                                        }
                                    case "System.Decimal":
                                        {
                                            valueScript += Convert.ToDecimal(row[r]);
                                            break;
                                        }
                                    case "System.Double":
                                        {
                                            valueScript += Convert.ToDouble(row[r]);
                                            break;
                                        }
                                    case "System.Single":
                                        {
                                            valueScript += Convert.ToSingle(row[r]);
                                            break;
                                        }
                                    case "System.Byte":
                                        {
                                            valueScript += "'" + Convert.ToByte(row[r]) + "'";
                                            break;
                                        }
                                    case "System.Byte[]":
                                        {
                                            valueScript += "0x";
                                            string stringValue = Convert.ToString(row[r]);
                                            foreach (char b in stringValue)
                                            {
                                                valueScript += Convert.ToByte(b);
                                            }
                                            break;
                                        }
                                    case "System.String":
                                    case "System.Boolean":
                                    case "System.Object":
                                        {
                                            string tempValue = row[r].ToString();
                                            tempValue = tempValue.Replace("'", "''");
                                            valueScript += "'" + tempValue.Trim() + "'";
                                            break;
                                        }
                                    case "System.UInt64":
                                        {
                                            valueScript += Convert.ToUInt64(row[r]);
                                            break;
                                        }
                                    case "System.SByte":
                                        {
                                            valueScript += Convert.ToInt16(row[r]);
                                            break;
                                        }
                                }
                                #endregion
                            }
                            n++;
                        }
                        r++;
                    }
                    valueScript += ")\r\n\r\nGO\r\n\r\n";
                }
            }
            catch (Exception x)
            {
                WriteLog(x.Message);
            }
            return valueScript;
        }

        /// <summary>
        /// checks for an identity column
        /// </summary>
        /// <returns>name(s) of identity column(s)</returns>
		private string[] CheckForIdentity()
        {
            try
            {
                DataRow[] test = dsTable.Tables[0].Select("EXTRA='auto_increment'");
                if (test == null)
                    return null;
                else
                {
                    string[] keyrows = new string[test.Length];
                    int count = 0;
                    foreach(DataRow row in test)
                    {
                        keyrows[count] = row["COLUMN_NAME"].ToString();
                        count++;
                    }
                    return keyrows;
                }

            }
            catch(Exception x)
            {
                WriteLog(x.Message);
            }
            return null;
        }

        /// <summary>
        /// checks for primary key column and concatenates a creation string for a Primary Key on an SQL Table
        /// </summary>
        /// <param name="tableName">name of the table to check for a primary key</param>
        /// <returns>string</returns>
		private string CheckForPrimaryKey(string tableName)
        {
            string returnString = "";
            // number of rows
            int rowCount = dsTable.Tables[0].Select("COLUMN_KEY='PRI'").Length;
            if (rowCount > 0)
            {
                returnString += ",\r\nCONSTRAINT [" + tableName + "_PrimaryKey] PRIMARY KEY CLUSTERED\r\n(";
                string columnNames = "";
                int count = 0;
                foreach (DataRow row in dsTable.Tables[0].Select("COLUMN_KEY='PRI'"))
                {
                    // check to see that there's a comma between each column name
                    if (count == rowCount - 1)
                        columnNames += "[" + row["COLUMN_NAME"].ToString() + "]";
                    else
                        columnNames += "[" + row["COLUMN_NAME"].ToString() + "],";
                    count++;
                }
                returnString += columnNames + " ASC\r\n";
                returnString += ") ON [PRIMARY]\r\n";
                // send the primary key string back
                return returnString;
            }
            else
                return "";
        }

        /// <summary>
        /// checks that the passed in columnName does not exist in keys, so it is not an indentifier.  
        /// Helps when the ID column (PrimaryKey) is a generated column
        /// Doesn't help when the PrimaryKey is multi column....
        /// </summary>
        /// <param name="keys">a list of the Primary Key column names</param>
        /// <param name="columnName">name of the current column being scripted for</param>
        /// <returns></returns>
		private bool CheckForKeyRow(string[] keys, string columnName)
        {
            if (keys != null)
            {
                foreach (string f in keys)
                {
                    if (f.ToString().ToLower() == columnName.ToString().ToLower())
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// this is an internal function to write debug information to a textfile 
        /// if the cmdline option /debug is used.
        /// </summary>
        /// <param name="message"></param>
        private void WriteLog(string message)
        {
            ProjectMethods.WriteLog("MySQLFunctions", message);
        }


    }
}
