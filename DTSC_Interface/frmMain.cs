using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DBTableMover
{

    // TODO: tables without primary keys do not close the query well in sql.

    /// <summary>
    /// Main form of the program.
    /// </summary>
    public class frmMain : System.Windows.Forms.Form
    {
        private System.Windows.Forms.MainMenu mmMain;
        private System.Windows.Forms.MenuItem miMain;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem miDataConnection;
        private System.Windows.Forms.MenuItem miEditConnection;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnCreate;
        private System.Data.SqlClient.SqlConnection conDataConnection;
        private System.Windows.Forms.MenuItem miTables;
        private System.Windows.Forms.SaveFileDialog sFDPutFile;
        private System.Windows.Forms.ComboBox cboScriptContents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miAbout;
        private System.Windows.Forms.MenuItem miXmlConnection;
        private System.Windows.Forms.OpenFileDialog ofGetXML;
        private MySql.Data.MySqlClient.MySqlConnection conMySQLConnection;
        private MenuItem miMySQLConnection;
        private MenuItem miEditMySQLConnection;
        private System.ComponentModel.IContainer components;

        private ProjectInfo inf = new ProjectInfo();
        private string scriptFileName = "";
        private string tableScript = "";
        private MenuItem miDCSep1;
        private MenuItem miDCSep2;
        private string valueScript = "";

        /// <summary>
        /// main constructor
        /// </summary>
        public frmMain()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this.conMySQLConnection = new MySqlConnection();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mmMain = new System.Windows.Forms.MainMenu(this.components);
            this.miMain = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miDataConnection = new System.Windows.Forms.MenuItem();
            this.miEditConnection = new System.Windows.Forms.MenuItem();
            this.miMySQLConnection = new System.Windows.Forms.MenuItem();
            this.miEditMySQLConnection = new System.Windows.Forms.MenuItem();
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
            this.miDCSep1 = new System.Windows.Forms.MenuItem();
            this.miDCSep2 = new System.Windows.Forms.MenuItem();
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
            this.miDCSep1,
            this.miMySQLConnection,
            this.miEditMySQLConnection,
            this.miDCSep2,
            this.miXmlConnection});
            this.miOptions.Text = "&Data Connection";
            // 
            // miDataConnection
            // 
            this.miDataConnection.Index = 0;
            this.miDataConnection.Text = "&New MSSQL Connection...";
            this.miDataConnection.Click += new System.EventHandler(this.miMSSQLConnection_Click);
            // 
            // miEditConnection
            // 
            this.miEditConnection.Index = 1;
            this.miEditConnection.Text = "&Edit Existing MSSQL Connection...";
            this.miEditConnection.Click += new System.EventHandler(this.miEditMSSQLConnection_Click);
            // 
            // miMySQLConnection
            // 
            this.miMySQLConnection.Index = 3;
            this.miMySQLConnection.Text = "New &MySQL Connection...";
            this.miMySQLConnection.Click += new System.EventHandler(this.miMySQLConnection_Click);
            // 
            // miEditMySQLConnection
            // 
            this.miEditMySQLConnection.Index = 4;
            this.miEditMySQLConnection.Text = "Edit E&xisting MySQL Connection...";
            this.miEditMySQLConnection.Click += new System.EventHandler(this.miEditMySQLConnection_Click);
            // 
            // miXmlConnection
            // 
            this.miXmlConnection.Index = 6;
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
            // conDataConnection
            // 
            this.conDataConnection.FireInfoMessageEventOnUserErrors = false;
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
            "All Tables Only",
            "All Tables and Values"});
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
            // miDCSep1
            // 
            this.miDCSep1.Index = 2;
            this.miDCSep1.Text = "-";
            // 
            // miDCSep2
            // 
            this.miDCSep2.Index = 5;
            this.miDCSep2.Text = "-";
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

        /// <summary>
        /// exits the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miExit_Select(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// this form's Load method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, System.EventArgs e)
        {
            this.Text = this.Text + " v" + inf.Version;
            if(RegistryFunctions.IsSavedConnection)
            {
                ProjectVariables.currentConType = RegistryFunctions.SavedConnectionType;
                switch(ProjectVariables.currentConType)
                {
                    case currentConnectionType.MSSQL:
                        {
                            conDataConnection.ConnectionString = ConnectionStringFunctions.CurrentConnectionString;
                            GrabMSSQLTables();
                            break;
                        }
                    case currentConnectionType.MySQL:
                        {
                            conMySQLConnection.ConnectionString = ConnectionStringFunctions.CurrentConnectionString;
                            GrabMySQLTables();
                            break;
                        }
                }
                MessageBox.Show("Saved Connection Loaded.\r\n\r\n  Choose a table or script type to continue.", inf.confirm, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("To begin, click 'Data Connection' to connect the application to a database or file.", inf.confirm, MessageBoxButtons.OK, MessageBoxIcon.Question);

        }

        /// <summary>
        /// fills the datasets for the form, calling in new records form the database etc.
        /// </summary>
        private void GrabMSSQLTables()
        {
            try
            {
                DataSet dsTable = new DataSet();
                dsTable.Clear();
                this.miTables.MenuItems.Clear();
                SqlDataAdapter adap = new SqlDataAdapter();
                SqlCommand comm = conDataConnection.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = ("select * from sysobjects where type = 'U' and name <> 'dtproperties' and name <> 'sysdiagrams' order by name");
                adap.SelectCommand = comm;
                conDataConnection.Open();
                adap.Fill(dsTable);
                foreach (DataRow row in dsTable.Tables[0].Rows)
                {
                    miTables.MenuItems.Add(row["name"].ToString(), new System.EventHandler(mnuTables_Click));
                }
            }
            catch(Exception x)
            {
                WriteLog(x.Message);
            }
            finally
            {
                conDataConnection.Close();
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
            switch (ProjectVariables.currentConType)
            {
                case currentConnectionType.MSSQL:
                    {
                        try
                        {
                            MenuItem itemClicked = (MenuItem)sender;
                            string tableName = itemClicked.Text.ToString();
                            dsTable.Tables.Clear();
                            SqlDataAdapter adap = new SqlDataAdapter();
                            SqlCommand comm = conDataConnection.CreateCommand();
                            comm.CommandType = CommandType.Text;
                            comm.CommandText = "Select * from " + tableName;
                            adap.SelectCommand = comm;
                            conDataConnection.Open();
                            adap.Fill(dsTable);
                            this.dataGrid1.DataSource = dsTable.Tables[0];
                            this.dataGrid1.CaptionText = tableName;
                            dataGrid1.Invalidate();
                        }
                        catch (Exception x)
                        {
                            WriteLog(x.Message);
                        }
                        finally
                        {
                            conDataConnection.Close();
                        }
                        break;
                    }
                case currentConnectionType.XML:
                    {
                        // first we open a new dataset, and grab the file content
                        XmlFunctions xmlFun = new XmlFunctions();
                        dsTable = xmlFun.OpenXmlToDataSet(ofGetXML.FileName);
                        // now we get the table data that the user wants
                        MenuItem itemClicked = (MenuItem)sender;
                        string tableName = itemClicked.Text.ToString();

                        DataTable gridTable = dsTable.Tables[tableName];

                        this.dataGrid1.DataSource = gridTable;
                        this.dataGrid1.CaptionText = tableName;
                        dataGrid1.Invalidate();

                        break;
                    }
                case currentConnectionType.MySQL:
                    {
                        try
                        {
                            // grab all data from the selected table
                            MenuItem itemClicked = (MenuItem)sender;
                            string tableName = itemClicked.Text.ToString();
                            dsTable.Tables.Clear();
                            MySqlDataAdapter adap = new MySqlDataAdapter();
                            MySqlCommand comm = conMySQLConnection.CreateCommand();
                            comm.CommandType = CommandType.Text;
                            comm.CommandText = "Select * from " + tableName;
                            adap.SelectCommand = comm;
                            conMySQLConnection.Open();
                            adap.Fill(dsTable);
                            this.dataGrid1.DataSource = dsTable.Tables[0];
                            this.dataGrid1.CaptionText = tableName;
                            dataGrid1.Invalidate();
                        }
                        catch (Exception x)
                        {
                            WriteLog(x.Message);
                        }
                        finally
                        {
                            conMySQLConnection.Close();
                        }
                        break;
                    }
            } // end switch

        }

        /// <summary>
        /// this is an internal function to write debug information to a textfile 
        /// if the cmdline option /debug is used.
        /// </summary>
        /// <param name="message"></param>
        private void WriteLog(string message)
        {
            ProjectMethods.WriteLog("frmMain", message);
        }

        /// <summary>
        /// brings up a DataLinks form to allow the user to create a new connection.  (overwrites any current connectionstring)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miMSSQLConnection_Click(object sender, System.EventArgs e)
        {
            try
            {
                ClearValues();
                if (ConnectionStringFunctions.NewMSSQLConnectionString())
                {
                    this.conDataConnection.ConnectionString = ConnectionStringFunctions.CurrentConnectionString;
                    GrabMSSQLTables();
                    MessageBox.Show("Select a table from the menu above \r\n and/or a script type from the drop down to continue.", "Connected...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception a)
            {
                WriteLog(a.Message);
            }
        }

        /// <summary>
        /// clears all data related values when a DataConnection menu item is clicked.
        /// </summary>
        private void ClearValues()
        {
            this.ofGetXML = null;
            this.dataGrid1.DataSource = null;
            dataGrid1.Invalidate();
        }

        /// <summary>
        /// loads the current connectionstring and allows the user to modify it using the DataLinks form.
        /// then loads the table list for the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEditMSSQLConnection_Click(object sender, System.EventArgs e)
        {
            try
            {
                ClearValues();
                if (ConnectionStringFunctions.EditMSSQLConnectionString())
                {
                    this.conDataConnection.ConnectionString = ConnectionStringFunctions.CurrentConnectionString;
                    GrabMSSQLTables();
                    MessageBox.Show("Select a table from the menu above \r\n and/or a script type from the drop down to continue.", "Connected...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception g)
            {
                MessageBox.Show("Cannot open connection, please retry.\r\n\r\nError Message: " + g.Message, inf.error);
                WriteLog("Cannot open new connection:::" + g.Message);
            }
        }

        /// <summary>
        /// creates the selected script types for the selected data or database objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, System.EventArgs e)
        {
            bool valid = true;
            string scriptFor = this.cboScriptContents.SelectedItem.ToString();
            if (this.cboScriptContents.SelectedIndex == -1)
            {
                valid = false;
                MessageBox.Show("Please select a script type.", "Missing Information...");
                this.cboScriptContents.Focus();
            }
            if (this.dataGrid1.CaptionText == "")
            {
                switch (scriptFor)
                {
                    case "Table Only":
                        {
                            valid = false;
                            MessageBox.Show("Please select a database table from the Table Menu.", "Missing Information...");
                            break;
                        }
                    case "Values Only":
                        {
                            valid = false;
                            MessageBox.Show("Please select a database table from the Table Menu.", "Missing Information...");
                            break;
                        }

                    case "Table and Values":
                        {
                            valid = false;
                            MessageBox.Show("Please select a database table from the Table Menu.", "Missing Information...");
                            break;
                        }
                }
            }
            if (valid)
            {
                this.sFDPutFile.Title = "Script Location for Table " + dataGrid1.CaptionText.ToString() + "...";
                DialogResult result = this.sFDPutFile.ShowDialog(this);
                //WriteLog(result.ToString());
                if (result == DialogResult.OK)
                {
                    this.scriptFileName = this.sFDPutFile.FileName.ToString();
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
                        case "All Tables Only":
                            {
                                WriteScriptToFile(ScriptType.DatabaseTableStructure);
                                break;
                            }
                        case "All Tables and Values":
                            {
                                WriteScriptToFile(ScriptType.StructureAndValues);
                                break;
                            }
                    }
                    MessageBox.Show("Done.", "Script Complete...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// creates a table creation script for the passed in table name in the currently connected database.
        /// </summary>
        /// <param name="tableName"></param>
        private void CreateTableScript(string tableName)
        {
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

        /// <summary>
        /// creates an insert script for each row in the passed in table
        /// </summary>
        /// <param name="tableName"></param>
        private void CreateValueScript(string tableName)
        {
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

        /// <summary>
        /// initiates the generation of the selected scripts and outputs the resultant string to a file
        /// </summary>
        /// <param name="scriptType">the user selected Script Type</param>
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

        /// <summary>
        /// brings up the about form to show the user who wrote this program and copyright information regarding it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAbout_Click(object sender, System.EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog(this);
        }

        /// <summary>
        /// connects the program to an XML file to use for script generation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miXmlConnection_Click(object sender, System.EventArgs e)
        {
            ClearValues();
            DataSet dsTable = new DataSet();
            try
            {
                XmlFunctions xmlFun = new XmlFunctions();
                this.ofGetXML = new OpenFileDialog();
                DialogResult result = ofGetXML.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    string fileName = ofGetXML.FileName;
                    dsTable = xmlFun.OpenXmlToDataSet(fileName);
                    this.dataGrid1.DataSource = null;
                    this.miTables.MenuItems.Clear();
                    int x = 0;
                    foreach (DataTable t in dsTable.Tables)
                    {
                        this.miTables.MenuItems.Add(new MenuItem(t.TableName.ToString(), new System.EventHandler(this.mnuTables_Click)));
                        x++;
                    }
                    MessageBox.Show("Select a table from the menu above \r\n and/or a script type from the drop down to continue.", "Connected...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception c)
            {
                this.WriteLog("FrmMain : Create XML Connection : " + c.Message);
            }
            finally
            {
                ProjectVariables.currentConType = currentConnectionType.XML;
            }
        }

        /// <summary>
        /// edits the current connectionstring for MySQL Databases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEditMySQLConnection_Click(object sender, EventArgs e)
        {
            try
            {
                ClearValues();
                if (ConnectionStringFunctions.EditMySQLConnectionString())
                {
                    conMySQLConnection.ConnectionString = ConnectionStringFunctions.CurrentConnectionString;
                    GrabMySQLTables();
                    MessageBox.Show("Select a table from the menu above \r\n and/or a script type from the drop down to continue.", "Connected...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception g)
            {
                MessageBox.Show("Cannot open connection, please retry.\r\n\r\nError Message: " + g.Message, inf.error);
                WriteLog("Cannot open new connection:::" + g.Message);
            }
        }

        /// <summary>
        /// connects the main form to a MySQL Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miMySQLConnection_Click(object sender, EventArgs e)
        {
            try
            {
                ClearValues();
                if (ConnectionStringFunctions.NewMySQLConnectionString())
                {
                    conMySQLConnection.ConnectionString = ConnectionStringFunctions.CurrentConnectionString;
                    GrabMySQLTables();
                    MessageBox.Show("Select a table from the menu above \r\n and/or a script type from the drop down to continue.", "Connected...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception b)
            {
                WriteLog(b.Message);
            }
        }

        /// <summary>
        /// grabs a list of tables from a MySQL database
        /// </summary>
        private void GrabMySQLTables()
        {
            DataSet dsTable = new DataSet();
            try
            {
                dsTable.Clear();
                this.miTables.MenuItems.Clear();
                MySqlDataAdapter adap = new MySqlDataAdapter();
                MySqlCommand comm = conMySQLConnection.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = ("SHOW TABLES");
                adap.SelectCommand = comm;
                conMySQLConnection.Open();
                adap.Fill(dsTable);
                foreach (DataRow row in dsTable.Tables[0].Rows)
                {
                    miTables.MenuItems.Add(row["tables_in_" + ConnectionStringFunctions.CurrentConnectionDatabaseName].ToString(), new System.EventHandler(mnuTables_Click));
                }
            }
            catch (Exception b)
            {
                WriteLog(b.Message);
            }
            finally
            {
                dsTable.WriteXml(Environment.CurrentDirectory + @"/MySQLTables.xml");
                conMySQLConnection.Close();
            }
        }

    }
}
