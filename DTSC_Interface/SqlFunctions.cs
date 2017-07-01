using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace DBTableMover
{
	/// <summary>
	/// Summary description for SqlFunctions.
	/// </summary>
	public class SqlFunctions
	{
        // for this database type we will get the required information from the following query
        //
        // exec sp_help tableName
        //
        // several tables of data are returned, each with parts of the information required to create the 
        // table creation script.

		private string tableScript;
		private string valueScript;
		private DataSet dsTable = new DataSet();

        private SqlConnection conDataConnection = new SqlConnection(ConnectionStringFunctions.CurrentConnectionString);

        /// <summary>
        /// main constructor
        /// </summary>
		public SqlFunctions()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        /// <summary>
        /// figures out which type of script to create and passes control to the proper sub function
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
				int columnLen = 0;
				string columnNull = "";
				string columnColl = "";
				// start building script
				dsTable.Tables.Clear();
				SqlDataAdapter adap = new SqlDataAdapter();
				SqlCommand comm = conDataConnection.CreateCommand();
				comm.CommandType = CommandType.Text;
				comm.CommandText = "exec sp_help " + tableName;
				adap.SelectCommand = comm;
                try
                {
                    conDataConnection.Open();
                    adap.Fill(dsTable);
                }
                catch (Exception x)
                {
                    WriteLog(x.Message);
                }
                finally
                {
                    conDataConnection.Close();
                }
				// insert drop and create scripts
				tableScript = "if exists(select name from sysobjects where name='" + tableName + "' and type='U')\r\nBEGIN\r\ndrop table " + tableName + "\r\nEND\r\nGO\r\n\r\n";
				tableScript += "create table [dbo].[" + tableName + "]\r(\n";
				// grab primary key column
				
				string[] keyName = CheckForIdentity();
				if(keyName!=null)
				{
					seed = Convert.ToInt32(dsTable.Tables[2].Rows[0]["Seed"].ToString());
					inc = Convert.ToInt32(dsTable.Tables[2].Rows[0]["Increment"].ToString());
				}
				// grab column values
				int cols = 0;
				foreach(DataRow row in dsTable.Tables[1].Rows)
				{
					string columnOnly = "";
					// there's more columns past the first then add this 
					if(cols >= 1)
						columnOnly += ",\n";
					columnName = row["Column_name"].ToString();
					columnType = row["Type"].ToString();
					columnLen = Convert.ToInt32(row["Length"].ToString());
					if(row["Nullable"].ToString().ToUpper() == "YES")
						columnNull = "NULL";
					else
						columnNull = "NOT NULL";
					if(row["Collation"].ToString() == "NULL")
						columnColl = "";
					else
						columnColl = row["Collation"].ToString();
					// create the script for column
					columnOnly += "[" + columnName + "] ";
					columnOnly += "[" + columnType + "]";
					switch(columnType)
					{
						case "varchar":
						case "nchar":
						case "nvarchar":
						case "char":
						case "binary":
						case "varbinary":
						{
							if(columnLen==-1)
								columnOnly += "(MAX)";
							else
								columnOnly += "(" + columnLen + ")";
							break;
						}
						case "text":
						case "ntext":
						case "datetime":
						case "int":
						case "double":
						case "single":
						case "float":
						case "money":
						case "smallint":
						case "tinyint":
						case "uniqueidentifier":
						case "bit":
						case "xml":
						case "bigint":
						case "image":
						case "numeric":
						case "real":
						case "smalldatetime":
						case "smallmoney":
						case "timestamp":
						{
							break;
						}
					}
					// if it's the identity set it as one
					if(CheckForKeyRow(keyName,columnName))
						columnOnly += " IDENTITY(" + seed + "," + inc + ")";
					if (columnColl == "")
						columnOnly += " " + columnNull;
					else 
						columnOnly += " COLLATE " + columnColl + " " + columnNull;
					// add one to the number of columns
					cols++;
					// add column text to script
					tableScript += columnOnly;
				}
				// now add the primary key if there is one
				string PrimaryKeyClause = CheckForPrimaryKey(tableName);
				if(PrimaryKeyClause.Length > 0)
					tableScript += PrimaryKeyClause;
				// now close the script for the table
				tableScript += ") ON [PRIMARY]\r\nGO\r\n";

			}
			catch(Exception x)
			{
				WriteLog(x.Message);
			}

				return tableScript;
		}

        /// <summary>
        /// figures out which type of script to create and passes control to the proper sub function
        /// </summary>
        /// <param name="tableName">name of the table to create the script for</param>
        /// <param name="outputType">type of database to create the script for</param>
        /// <returns></returns>
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

			this.valueScript = "";
			try
			{
				this.dsTable.Tables.Clear();
				SqlDataAdapter adap = new SqlDataAdapter();
				SqlCommand comm = conDataConnection.CreateCommand();
				comm.CommandType = CommandType.Text;
				comm.CommandText = "exec sp_help " + tableName;
				adap.SelectCommand = comm;
				adap.Fill(dsTable);
				// grab table identities column names
				string[] keyrows = CheckForIdentity();
				// now start creating the script for the values
				this.dsTable.Tables.Clear();
				comm = conDataConnection.CreateCommand();
				comm.CommandType = CommandType.Text;
				comm.CommandText = "select * from " + tableName;
				adap.SelectCommand = comm;
                conDataConnection.Open();
				adap.Fill(dsTable);
                conDataConnection.Close();
				// check table for data  
				int rowCount = dsTable.Tables[0].Rows.Count;
				if(rowCount==0)
				{
					MessageBox.Show("There is no data to script for.", "Empty Table...");
					Exception x = new Exception("Empty Table");
					throw(x);
				}
				// get table column name values
				string columnNames = "";
				DataRow columnRow = dsTable.Tables[0].Rows[0];
				int c = 0;
				foreach(DataColumn col in columnRow.Table.Columns)
				{
					if(CheckForKeyRow(keyrows, col.ColumnName.ToString()))
						continue;
					else
					{
						c++;
						if(c==1)
							columnNames += col.ColumnName.ToString();
						else
							columnNames += "," + col.ColumnName.ToString();
					}
				}
				// now grab values and build SQL
				foreach(DataRow row in dsTable.Tables[0].Rows)
				{
					int r = 0;  // particular row column
					valueScript += "INSERT INTO [dbo].[" + tableName + "] (" + columnNames + ") VALUES (";
					int n = 0;  // number of columns added to script
					foreach(DataColumn col in row.Table.Columns)
					{
						if(CheckForKeyRow(keyrows, col.ColumnName.ToString()))
						{
							r++;
							continue;
						}
						else
						{
							if(n>0)
								valueScript += ",";
							string colType = col.DataType.ToString();
							//							WriteLog("colType :" + colType);
							//							WriteLog("ToString : " + col.ToString());
							//							WriteLog(" \n");
							if(Convert.IsDBNull(row[r]))
								valueScript += "NULL";
							else
							{
								#region dataType DataFixes
								switch(colType)
								{
									case "System.DateTime":
									{
										DateTime dbdate = Convert.ToDateTime(row[r]); 
										valueScript += "'" + dbdate.ToString("yyyyMMdd HH:mm:ss") + "'";
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
										foreach(char b in stringValue)
										{
											valueScript += Convert.ToByte(b);
										}
										break;
									}
									case "System.Guid":
									{ // don't want a value here, since the insert will auto add one.  
										// really shouldn't have this value anyway, since the keyrow should handle this.
										break;
									}
								}
								#endregion
							}
							// add one to the count of values added to valueScript
							n++;
						}
						// add one to the column count
						r++;
					}
					valueScript += ")\r\n\r\nGO\r\n\r\n";
				}
			}
			catch(Exception x)
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
				string test  = dsTable.Tables[2].Rows[0][0].ToString();
				if(test=="No identity column defined.")
					return null;
				int rowCount = dsTable.Tables[2].Rows.Count;
				if(rowCount > 0 )
				{
					string[] keyrows = new string[dsTable.Tables[2].Rows.Count];
					int x = 0;
					foreach(DataRow row in dsTable.Tables[2].Rows)
					{
						// define the identity columns
						keyrows[x] = row["Identity"].ToString();
						x++;
					}
					return keyrows;
				}
			}
			catch(Exception x)
			{
				WriteLog("CheckForIdentity  :" + x.Message);
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
			int rowCount = dsTable.Tables[6].Rows.Count;
			if(rowCount > 0)
			{
				// now cycle through and check for "PRIMARY KEY"
				foreach(DataRow row in dsTable.Tables[6].Rows)
				{
					//WriteLog(row["constraint_type"].ToString() + " Value : " + Convert.ToInt32(row["constraint_type"].ToString().IndexOf("RIMARY KEY",0)));
					if(row["constraint_type"].ToString().IndexOf("RIMARY KEY",0) == 1)
					{
						//grab all values and create primary key string
						returnString += ",\r\nCONSTRAINT [" + row[1].ToString() + "] PRIMARY KEY CLUSTERED\r\n(";
						// check to see if more than one field is in key
						string[] columns = row[6].ToString().Split(',');
						if(columns.Length>1)
						{
							string columnNames = "";
							int count = 1;
							foreach(string s in columns)
							{
								if(count == columns.Length)
									columnNames += "[" + s + "]";
								else
									columnNames += "[" + s +"],";
								count++;
							}
							returnString += columnNames + " ASC\r\n";
						}
						else
							returnString += "[" + row[6].ToString() + "] ASC\r\n";
						returnString += ") ON [PRIMARY]\r\n";
					}
				}
				return returnString;
			}
			else
				return "";
		}

        // TODO: find a solution for multi column Primary Keys...
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
			if(keys!=null)
			{
				foreach(string f in keys)
				{
					if(f.ToString().ToLower() == columnName.ToString().ToLower())
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
                ProjectMethods.WriteLog("SqlFunctions", message);
		}

	}
}
