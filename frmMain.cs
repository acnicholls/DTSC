using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace DBTableMover
{

    // TODO: tables without primary keys do not close the query well in sql.

    /// <summary>
    /// Summary description for frmMain.
    /// </summary>
    public class frmMain : System.Windows.Forms.Form
    {
        private System.Windows.Forms.MainMenu mmMain;
        private System.Windows.Forms.MenuItem miMain;
        private System.Windows.Forms.MenuItem miExit;
        private ProjectInfo inf = new ProjectInfo();
        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem miDataConnection;
        public static string ConnectionString = "";
        private System.Windows.Forms.MenuItem miEditConnection;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnCreate;
        private System.Data.SqlClient.SqlConnection conDataConnection;
        private System.Windows.Forms.MenuItem miTables;
        private System.Windows.Forms.SaveFileDialog sFDPutFile;

        private string scriptFileName = "";
        private string tableScript = "";
        private string valueScript = "";
        private System.Windows.Forms.ComboBox cboScriptContents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miAbout;
        private System.Windows.Forms.MenuItem miXmlConnection;
        private currentConnectionType currentConType;
        private System.Windows.Forms.OpenFileDialog ofGetXML; //currentConnectionType.MSSQL currentConnectionType
        private ProjectVariables projVars;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmMain()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
            this.mmMain = new System.Windows.Forms.MainMenu();
            this.miMain = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miDataConnection = new System.Windows.Forms.MenuItem();
            this.miEditConnection = new System.Windows.Forms.MenuItem();
            this.miXmlConnection = new System.Windows.Forms.MenuItem();
            this.miTables = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.btnCreate = new System.Windows.Forms.Button();
            this.conDataConnection = new System.Data.SqlClient.SqlConnection();
            this.sFDPutFile = new System.Windows.Forms.SaveFileDialog();
            this.cboScriptContents = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ofGetXML = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // mmMain
            // 
            this.mmMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                   this.miMain,
                                                                                   this.miOptions,
                                                                                   this.miTables,
                                                                                   this.menuItem1});
            // 
            // miMain
            // 
            this.miMain.Index = 0;
            this.miMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                   this.miExit});
            this.miMain.Text = "M&ain";
            // 
            // miExit
            // 
            this.miExit.Index = 0;
            this.miExit.Text = "E&xit";
            this.miExit.Click += new System.EventHandler(this.miExit_Select);
            // 
            // miOptions
            // 
            this.miOptions.Index = 1;
            this.miOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.miDataConnection,
                                                                                      this.miEditConnection,
                                                                                      this.miXmlConnection});
            this.miOptions.Text = "&Data Connection";
            // 
            // miDataConnection
            // 
            this.miDataConnection.Index = 0;
            this.miDataConnection.Text = "&New Data Connection...";
            this.miDataConnection.Click += new System.EventHandler(this.miDataConnection_Click);
            // 
            // miEditConnection
            // 
            this.miEditConnection.Index = 1;
            this.miEditConnection.Text = "&Edit Existing...";
            this.miEditConnection.Click += new System.EventHandler(this.miEditConnection_Click);
            // 
            // miXmlConnection
            // 
            this.miXmlConnection.Index = 2;
            this.miXmlConnection.Text = "&Open Xml File...";
            this.miXmlConnection.Click += new System.EventHandler(this.miXmlConnection_Click);
            // 
            // miTables
            // 
            this.miTables.Index = 2;
            this.miTables.Text = "&Tables";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 3;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.miAbout});
            this.menuItem1.Text = "&Help";
            // 
            // miAbout
            // 
            this.miAbout.Index = 0;
            this.miAbout.Text = "&About";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.DataMember = "";
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(8, 8);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.Size = new System.Drawing.Size(712, 320);
            this.dataGrid1.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(544, 336);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(168, 23);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Create Script >>";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // sFDPutFile
            // 
            this.sFDPutFile.DefaultExt = "sql";
            this.sFDPutFile.Filter = "SQL Scripts|*.sql|All Files|*.*";
            this.sFDPutFile.Title = "Script File Location...";
            // 
            // cboScriptContents
            // 
            this.cboScriptContents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScriptContents.Items.AddRange(new object[] {
                                                                   "Table Only",
                                                                   "Values Only",
                                                                   "Table and Values",
                                                                   "Database Only",
                                                                   "Database and Values"});
            this.cboScriptContents.Location = new System.Drawing.Point(400, 336);
            this.cboScriptContents.Name = "cboScriptContents";
            this.cboScriptContents.Size = new System.Drawing.Size(128, 21);
            this.cboScriptContents.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(304, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Create Script for :";
            // 
            // ofGetXML
            // 
            this.ofGetXML.Filter = "(*.xml)|Xml Files|(*.*)|All Files";
            this.ofGetXML.Title = "Create XML Connection...";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(728, 367);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboScriptContents);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.dataGrid1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mmMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Table Script Creator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion


        private void miExit_Select(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            projVars = new ProjectVariables();
            try
            {
                try
                {
                    frmMain.ConnectionString = this.LoadAdapterConnectionStringFromRegistry();
                }
                catch
                {
                    if (ConnectionString == "")
                    {
                        MessageBox.Show("You require a connection to your data, please fill in the following form.", inf.confirm, MessageBoxButtons.OK, MessageBoxIcon.Question);

                        this.miDataConnection_Click(this, EventArgs.Empty);
                    }
                }
                if (frmMain.ConnectionString == String.Empty)
                    frmMain.ConnectionString = NewConnectionString();
                conDataConnection.ConnectionString = frmMain.ConnectionString;

                try
                {
                    this.conDataConnection.Open();
                }
                catch (Exception sqlEX)
                {
                    WriteLog("Error Loading Data......................\n");
                    WriteLog(sqlEX.Message.ToString());
                }
                GrabTables();

                this.Text = this.Text + " v" + inf.Version;
            }
            catch (Exception x)
            {
                WriteLog("Error Loading Data...\n");
                WriteLog(x.Message.ToString());
            }

        }

        /// <summary>
        /// fills the datasets for the form, calling in new records form the database etc.
        /// </summary>
        private void GrabTables()
        {
            DataSet dsTable = new DataSet();
            dsTable.Clear();
            this.miTables.MenuItems.Clear();
            SqlDataAdapter adap = new SqlDataAdapter();
            SqlCommand comm = conDataConnection.CreateCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = ("select * from sysobjects where type = 'U' and name <> 'dtproperties' order by name");
            adap.SelectCommand = comm;
            adap.Fill(dsTable);
            foreach (DataRow row in dsTable.Tables[0].Rows)
            {
                miTables.MenuItems.Add(row["name"].ToString(), new System.EventHandler(mnuTables_Click));
            }

        }

        /// <summary>
        /// This event handles the user clicking a menu item related to a database table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mnuTables_Click(object sender, System.EventArgs e)
        {
            DataSet dsTable = new DataSet();
            try
            {
                switch (currentConType)
                {
                    case currentConnectionType.MSSQL:
                        {
                            MenuItem itemClicked = (MenuItem)sender;
                            string tableName = itemClicked.Text.ToString();
                            dsTable.Tables.Clear();
                            SqlDataAdapter adap = new SqlDataAdapter();
                            SqlCommand comm = conDataConnection.CreateCommand();
                            comm.CommandType = CommandType.Text;
                            comm.CommandText = "Select * from " + tableName;
                            adap.SelectCommand = comm;
                            adap.Fill(dsTable);
                            this.dataGrid1.DataSource = dsTable.Tables[0];
                            this.dataGrid1.CaptionText = tableName;
                            dataGrid1.Invalidate();
                            break;
                        }
                    case currentConnectionType.XML:
                        {
                            // first we open a new dataset, and grab the file content
                            XmlFunctions xmlFun = new XmlFunctions();
                            dsTable = xmlFun.OpenXmlToDataSet(projVars.CurrentXMLFileName);
                            // now we get the table data that the user wants
                            MenuItem itemClicked = (MenuItem)sender;
                            string tableName = itemClicked.Text.ToString();

                            DataTable gridTable = dsTable.Tables[tableName];

                            this.dataGrid1.DataSource = gridTable;
                            this.dataGrid1.CaptionText = tableName;
                            dataGrid1.Invalidate();

                            break;
                        }
                }
            }
            catch(Exception x)
            {
                WriteLog("Error in mnuTables_Click : " + x.Message);
            }

        }


        /// <summary>
        /// this is an internal function to write debug information to a textfile 
        /// if the cmdline option /debug is used.
        /// </summary>
        /// <param name="message"></param>
        private void WriteLog(string message)
        {
            if (inf.debugMode)
            {
                // this function writes a log of important info and is useful for debugging
                StreamWriter logFile = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\debug.txt", true);
                logFile.WriteLine(DateTime.Now + "    " + message);
                logFile.Flush();
                logFile.Close();
            }
        }

        private void miDataConnection_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.ofGetXML = null;
                string provider = this.NewConnectionString();
                SaveConnectionStringToRegistry(provider);
                if (this.conDataConnection.State == ConnectionState.Open)
                {
                    this.conDataConnection.Close();
                }
                frmMain.ConnectionString = this.LoadAdapterConnectionStringFromRegistry();
                this.conDataConnection.ConnectionString = frmMain.ConnectionString.ToString();
                this.conDataConnection.Open();
                GrabTables();
            }
            catch (Exception a)
            {
                WriteLog(a.Message);
            }
            finally
            {
                this.currentConType = currentConnectionType.MSSQL;
            }
        }
        // if you want to generate a new connection string.
        private void SaveConnectionStringToRegistry(string connection)
        {
            // split connection into it's separate parts
            WriteLog("ConnectionString = " + connection);
            string[] provider2 = connection.Split(';');
            // open registry location to save to
            RegistryKey profileLocation = Registry.CurrentUser;
            string location = @"SOFTWARE\acnicholls.com\Database Table Script Creator\";
            RegistryKey storage = profileLocation.CreateSubKey(location);
            foreach (string p in provider2)
            {
                // if it's the provider string save it to registry
                // if not, compile a string for the adapter
                if (p.StartsWith("Provider="))
                    storage.SetValue("Provider", provider2[0].ToString());
                else
                    frmMain.ConnectionString += p + ";";
            }
            // now save the conenction string in registry.
            storage.SetValue("AdapterConnection", frmMain.ConnectionString);
        }

        private string LoadFullConnectionStringFromRegistry()
        {
            RegistryKey profileLocation = Registry.CurrentUser;
            string location = @"SOFTWARE\acnicholls.com\Database Table Script Creator\";
            RegistryKey storage = profileLocation.CreateSubKey(location);
            string provider = storage.GetValue("Provider").ToString();
            string connection = storage.GetValue("AdapterConnection").ToString();
            return provider + ";" + connection;
        }

        public string LoadAdapterConnectionStringFromRegistry()
        {
            RegistryKey profileLocation = Registry.CurrentUser;
            string location = @"SOFTWARE\acnicholls.com\Database Table Script Creator\";
            RegistryKey storage = profileLocation.CreateSubKey(location);
            string connection = storage.GetValue("AdapterConnection").ToString();
            return connection;
        }

        public string NewConnectionString()
        {
            object _con = null;
            MSDASC.DataLinks _link = new MSDASC.DataLinks();
            _con = _link.PromptNew();
            if (_con == null) return string.Empty;
            return ((ADODB.Connection)_con).ConnectionString;
        }

        //If you want to modify an existing connectionstring:

        public void ExistingConnection(ref string connectionstring)
        {

            object _con = new object();

            MSDASC.DataLinks _link = new MSDASC.DataLinks();
            //((ADODB.Connection)_con).ConnectionString = connectionstring;
            ADODB.Connection _ado = new ADODB.Connection();
            _ado.ConnectionString = connectionstring;
            _con = (object)_ado;
            if (_link.PromptEdit(ref _con))
                ////	{
                connectionstring = ((ADODB.Connection)_con).ConnectionString;
            ////	}
            //WriteLog("Connection String : " + connectionstring);
        }

        private void miEditConnection_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.ofGetXML = null;
                string cd = this.LoadFullConnectionStringFromRegistry();
                ExistingConnection(ref cd);
                this.SaveConnectionStringToRegistry(cd);
                this.conDataConnection.Close();
                frmMain.ConnectionString = this.LoadAdapterConnectionStringFromRegistry();
                this.conDataConnection.ConnectionString = frmMain.ConnectionString.ToString();
                this.conDataConnection.Open();
                GrabTables();
            }
            catch (Exception g)
            {
                MessageBox.Show("Cannot open connection, please retry.", inf.error);
                WriteLog("Cannot open new connection:::" + g.Message);
            }
            finally
            {
                this.currentConType = currentConnectionType.MSSQL;
            }

        }

        private void WriteConnectionString()
        {

            //	save to file
            StreamWriter write = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\connectionString.txt", false);
            write.WriteLine(frmMain.ConnectionString);
            write.WriteLine();
            write.Flush();
            write.Close();
        }

        private void btnCreate_Click(object sender, System.EventArgs e)
        {
            bool valid = true;
            if (this.cboScriptContents.SelectedIndex == -1)
            {
                valid = false;
                MessageBox.Show("Please select a script type.", "Missing Information...");
                this.cboScriptContents.Focus();
            }
            if (this.dataGrid1.CaptionText == "")
            {
                valid = false;
                MessageBox.Show("Please select a database table from the Table Menu.", "Missing Information...");
            }
            if (valid)
            {
                this.sFDPutFile.Title = "Script Location for Table " + dataGrid1.CaptionText.ToString() + "...";
                DialogResult result = this.sFDPutFile.ShowDialog(this);
                //WriteLog(result.ToString());
                if (result == DialogResult.OK)
                {
                    this.scriptFileName = this.sFDPutFile.FileName.ToString();
                    string scriptFor = this.cboScriptContents.SelectedItem.ToString();
                    WriteLog(scriptFor);
                    switch (scriptFor)
                    {
                        case "Table Only":
                            {
                                WriteScriptToFile(ScriptType.TableStructure);
                                break;
                            }
                        case "Values Only":
                            {
                                WriteScriptToFile(ScriptType.ValuesOnly);
                                break;
                            }
                        case "Table and Values":
                            {
                                WriteScriptToFile(ScriptType.TableAndValues);
                                break;
                            }
                        case "Database Only":
                            {
                                WriteScriptToFile(ScriptType.DatabaseTableStructure);
                                break;
                            }
                        case "Database and Values":
                            {
                                WriteScriptToFile(ScriptType.StructureAndValues);
                                break;
                            }
                    }
                    MessageBox.Show("Done.", "Script Complete...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CreateTableScript(string tableName)
        {
            try
            {
                switch (this.currentConType)
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
                            tableScript = xmlFun.CreateTableScript(this.ofGetXML.FileName, tableName);
                            break;
                        }
                }
            }
            catch (Exception x)
            {
                WriteLog(x.Message);
            }
        }

        private void CreateValueScript(string tableName)
        {
            try
            {
                switch (this.currentConType)
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
                            valueScript = xmlFun.CreateValueScript(this.ofGetXML.FileName, tableName);
                            break;
                        }
                }
            }
            catch (Exception x)
            {
                WriteLog(x.Message);
            }
        }

        private void WriteScriptToFile(ScriptType scriptType)
        {
            string tableName = this.dataGrid1.CaptionText.ToString();
            string scriptValue = "";
            try
            {
                switch (scriptType)
                {
                    case ScriptType.TableStructure:
                        {
                            CreateTableScript(tableName);
                            scriptValue += tableScript;
                            break;
                        }
                    case ScriptType.ValuesOnly:
                        {
                            CreateValueScript(tableName);
                            scriptValue += valueScript;
                            break;
                        }
                    case ScriptType.TableAndValues:
                        {
                            CreateTableScript(tableName);
                            CreateValueScript(tableName);
                            scriptValue += tableScript + valueScript;
                            break;
                        }
                    case ScriptType.DatabaseTableStructure:
                        {
                            foreach (MenuItem mi in this.miTables.MenuItems)
                            {
                                string TableToScript = mi.Text.ToString();
                                CreateTableScript(TableToScript);
                                scriptValue += tableScript;
                            }
                            break;
                        }
                    case ScriptType.StructureAndValues:
                        {
                            foreach (MenuItem mi in this.miTables.MenuItems)
                            {
                                string TableToScript = mi.Text.ToString();
                                CreateTableScript(TableToScript);
                                CreateValueScript(TableToScript);
                                scriptValue += tableScript + valueScript;
                            }
                            break;
                        }
                }
                // this function writes a log of important info and is useful for debugging
                StreamWriter scriptFile = new StreamWriter(scriptFileName, false);
                scriptFile.Write(scriptValue);
                scriptFile.Flush();
                scriptFile.Close();
            }
            catch (Exception x)
            {
                WriteLog(x.Message);
            }
        }

        private void miAbout_Click(object sender, System.EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog(this);
        }

        private void miXmlConnection_Click(object sender, System.EventArgs e)
        {
            DataSet dsTable = new DataSet();
            try
            {
                XmlFunctions xmlFun = new XmlFunctions();
                this.currentConType = currentConnectionType.XML;
                this.ofGetXML = new OpenFileDialog();
                DialogResult result = ofGetXML.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    string fileName = ofGetXML.FileName;
                    dsTable = xmlFun.OpenXmlToDataSet(fileName);
                    projVars.CurrentXMLFileName = fileName;
                    this.dataGrid1.DataSource = null;
                    this.miTables.MenuItems.Clear();
                    int x = 0;
                    foreach (DataTable t in dsTable.Tables)
                    {
                        this.miTables.MenuItems.Add(new MenuItem(t.TableName.ToString(), new System.EventHandler(this.mnuTables_Click)));
                        x++;
                    }
                }
            }
            catch (Exception c)
            {
                this.WriteLog("FrmMain : Create XML Connection : " + c.Message);
            }
        }
    }

}
