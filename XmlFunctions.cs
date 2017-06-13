using System;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using System.IO;


namespace DBTableMover
{
	/// <summary>
	/// Summary description for XmlFunctions.
	/// </summary>
	public class XmlFunctions
	{
		public XmlFunctions()
		{

		}
		ProjectInfo inf = new ProjectInfo();
		DataSet xmlData = new DataSet();
		string tableName = "";
		string tableScript = "";
		string valueScript = "";

		public DataSet OpenXmlToDataSet(string newFileName)
		{
                FileInfo file = new FileInfo(newFileName);
                if (file.Extension.ToLower() == ".xml")
                {
                    FileStream XMLFILE = new FileStream(newFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    xmlData.ReadXml(XMLFILE);
                    XMLFILE.Close();
                }
                if (file.Extension.ToLower() == ".xsd")
                {
                    FileStream XMLFILE = new FileStream(newFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    xmlData.ReadXmlSchema(XMLFILE);
                    XMLFILE.Close();
                }
                return xmlData;
		}

		public string CreateTableScript(string fileName, string tableName)
		{
			this.tableScript = "";
			try
			{
				// declare variables for specific items
				string columnName = "";
				string columnType = "";
				int columnLen = 0;
				string columnNull = "";
				// start building script
				this.xmlData = this.OpenXmlToDataSet(fileName);
				this.tableName = tableName;
				// insert drop and create scripts
				tableScript = "if exists(select name from sysobjects where name='" + tableName + "' and type='U')\nBEGIN\ndrop table " + tableName + "\nEND\nGO\n\n";
				tableScript += "create table [dbo].[" + tableName + "](\n";
		
				// grab column values
				int cols = 0;
				foreach(DataColumn row in xmlData.Tables[tableName].Columns)
				{
					string columnOnly = "";
					// there's more columns past the first then add this 
					if(cols >= 1)
						columnOnly += ",\n";
					columnName = row.ColumnName.ToString();
					columnType = row.DataType.ToString();
					columnLen = Convert.ToInt32(row.MaxLength.ToString());
					if(row.AllowDBNull.ToString().ToUpper() == "TRUE")
						columnNull = "NULL";
					else
						columnNull = "NOT NULL";
					// create the script for column
					columnOnly += "[" + columnName + "] ";
					switch(columnType)
					{
						case "System.DateTime":
						{
							columnOnly += "[DateTime]";
							break;
						}
						case "System.String":
						{
							columnOnly += "[nvarchar]";

							if(row.MaxLength==-1)
								columnOnly += "(MAX)";
							else
								columnOnly += "(" + row.MaxLength + ")";
							break;
						}
						case "System.Boolean":
						{
							columnOnly += "[bit]";
							break;
						}
						case "System.Int64":
						{
							columnOnly += "[int]";
							break;
						}
						case "System.Int32":
						{
							columnOnly += "[int]";
							break;
						}
						case "System.Int16":
						{
							columnOnly += "[int]";
							break;
						}
						case "System.Decimal":
						{
							columnOnly += "[decimal]";
							break;
						}
						case "System.Double":
						{
							columnOnly += "[double]";
							break;
						}
						case "System.Single":
						{
							columnOnly += "[single]";
							break;
						}
						case "System.Byte":
						{
							columnOnly += "[byte]";
							break;
						}
						case "System.Byte[]":
						{
							columnOnly += "[byte[]]";
							break;
						}
						case "System.Guid":
						{ // don't want a value here, since the insert will auto add one.  
							// really shouldn't have this value anyway, since the keyrow should handle this.
							break;
						}

					}
					// if it's the identity set it as one
					if(xmlData.Tables[tableName].Columns[row.ColumnName].AutoIncrement)
					{
						int seed = Convert.ToInt32(xmlData.Tables[tableName].Columns[row.ColumnName].AutoIncrementSeed);
						int inc = Convert.ToInt32(xmlData.Tables[tableName].Columns[row.ColumnName].AutoIncrementStep);
						columnOnly += " IDENTITY(" + seed + "," + inc + ")";
					}
					columnOnly += " " + columnNull;
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
				tableScript += "\n) ON [PRIMARY]";
			}
			catch(Exception x)
			{
				WriteLog(x.Message);
			}
				return tableScript;
		}

		public string CreateValueScript(string fileName, string tableName)
		{

			this.valueScript = "";
			try
			{
				// grab data
				this.xmlData = this.OpenXmlToDataSet(fileName);
				this.tableName = tableName;
				///now start creating the script for the values
				// check table for data  
				int rowCount = xmlData.Tables[tableName].Rows.Count;
				if(rowCount==0)
				{
					MessageBox.Show("There is no data to script for.", "Empty Table...");
					Exception x = new Exception("Empty Table");
					throw(x);
				}
				// get table column name values
				string columnNames = "";
				DataRow columnRow = xmlData.Tables[tableName].Rows[0];
				int c = 0;
				foreach(DataColumn col in columnRow.Table.Columns)
				{
					if(col.AutoIncrement)
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
				foreach(DataRow row in xmlData.Tables[tableName].Rows)
				{
					int r = 0;  // particular row column
					valueScript += "INSERT INTO [dbo].[" + tableName + "] (" + columnNames + ") VALUES (";
					int n = 0;  // number of columns added to script
					foreach(DataColumn col in row.Table.Columns)
					{
						if(col.AutoIncrement)
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
							}
							// add one to the count of values added to valueScript
							n++;
						}
						// add one to the column count
						r++;
					}
					valueScript += ")\n\nGO\n\n";
				}
			}
			catch(Exception x)
			{
				WriteLog(x.Message);
			}
				return valueScript;
		}
		private string CheckForPrimaryKey(string tableName)
		{
			string returnString = "";
			// number of rows
			DataColumn[] keys = xmlData.Tables[tableName].PrimaryKey;
			if(keys.GetLength(0) > 0)
			{
				returnString += ",\nCONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED\n(";
				string columnNames = "";
				int count = 0;
				// now cycle through and check for "PRIMARY KEY"
				foreach(DataColumn row in keys)
				{
					//WriteLog(row["constraint_type"].ToString() + " Value : " + Convert.ToInt32(row["constraint_type"].ToString().IndexOf("RIMARY KEY",0)));
					if((count == 0 & keys.Length == 1) || (count == (keys.Length-1)))
						columnNames += "[" + row.ColumnName + "]";
					else
						columnNames += "[" + row.ColumnName + "],";
					count++;
				}
				returnString += columnNames + " ASC\n";
				returnString += ") ON [PRIMARY]";
				return returnString;
			}
			else
				return "";
		}


		/// <summary>
		/// this is an internal function to write debug information to a textfile 
		/// if the cmdline option /debug is used.
		/// </summary>
		/// <param name="message"></param>
		private void WriteLog(string message)
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
