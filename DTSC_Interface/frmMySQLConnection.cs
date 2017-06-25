using System;
using System.Windows.Forms;

namespace DBTableMover
{
    /// <summary>
    /// this form is used to build a connection string for a MySQL Database
    /// </summary>
    public partial class frmMySQLConnection : Form
    {
        enum KeyName
        {
            Server,
            Database,
            UID,
            PWD
        }
        private MySql.Data.MySqlClient.MySqlConnectionStringBuilder builder = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();
        private string[] keyPairs;

        /// <summary>
        /// builds the connectionstring from the values entered in the form controls
        /// </summary>
        public string GetConnectionString
        {
            get
            {
                builder.Add(KeyName.Server.ToString(), txtServer.Text.Trim());
                builder.Add(KeyName.Database.ToString(), txtDatabase.Text.Trim());
                builder.Add(KeyName.UID.ToString(), txtUser.Text.Trim());
                builder.Add(KeyName.PWD.ToString(), txtPassword.Text.Trim());
                builder.Add("Port", 3306);
                return builder.GetConnectionString(true);
            }
        }

        /// <summary>
        /// default constructor for this form
        /// </summary>
        public frmMySQLConnection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// constructs this form with values from a given connectionstring
        /// </summary>
        /// <param name="connectionString">the connectionstring values to populate form controls with</param>
        public frmMySQLConnection(string connectionString)
        {
            // required default call to create child controls.
            InitializeComponent();
            // custom commands to manipulate form
            try
            {
                keyPairs = connectionString.Split(';');
                if (KeywordExists(KeyName.Server.ToString()))
                    this.txtServer.Text = KeywordValue(KeyName.Server.ToString());
                if (KeywordExists(KeyName.Database.ToString()))
                    this.txtDatabase.Text = KeywordValue(KeyName.Database.ToString());
                // TODO: fix the problem where the "Add" function of the connectionstringbuilder object doesn't add UID, instead it adds "user id"...same with password
                if (KeywordExists(KeyName.UID.ToString()))
                    this.txtUser.Text = KeywordValue(KeyName.UID.ToString());
                if (KeywordExists("user id"))
                    this.txtUser.Text = KeywordValue("user id");
                if (KeywordExists(KeyName.PWD.ToString()))
                    this.txtPassword.Text = KeywordValue(KeyName.PWD.ToString());
                if (KeywordExists("password"))
                    this.txtPassword.Text = KeywordValue("password");
            }
            catch(Exception x)
            {
                WriteLog(x.Message);
            }
        }

        /// <summary>
        /// checks the keyPairs in the connectionstring to see if the key exists
        /// </summary>
        /// <param name="keyword">the word we're looking for</param>
        /// <returns>true/false</returns>
        private bool KeywordExists(string keyword)
        {
            foreach(string key in keyPairs)
            {
                string[] keyValue = key.Split('=');
                string word = keyValue[0];
                if (word.ToLower() == keyword.ToLower())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// finds the keyword in keyPairs and returns the associated value
        /// </summary>
        /// <param name="keyword">the value we want's index</param>
        /// <returns>string</returns>
        private string KeywordValue(string keyword)
        {
            foreach (string key in keyPairs)
            {
                string[] keyValue = key.Split('=');
                string word = keyValue[0];
                if (word.ToLower() == keyword.ToLower())
                    return keyValue[1].ToString();
            }
            return "";
        }

        /// <summary>
        /// this button cancels the current operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// this button will save the connection parameters into a ConnectionString object in the registry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// tests the connectionstring by attempting to open a connection to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection _con = new MySql.Data.MySqlClient.MySqlConnection(this.GetConnectionString);
            try
            {
                _con.Open();
                MessageBox.Show("Connection Successful!","Test Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception x)
            {
                WriteLog(x.Message);
                WriteLog(_con.ConnectionString.ToString());
                MessageBox.Show("Connection Failed.", "Test Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _con.Close();
            }

        }
        /// <summary>
        /// this is an internal function to write debug information to a textfile 
        /// if the cmdline option /debug is used.
        /// </summary>
        /// <param name="message"></param>
        private void WriteLog(string message)
        {
            ProjectMethods.WriteLog("frmMySQLConnection", message);
        }

    }
}
