using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBTableMover
{
    /// <summary>
    /// this form is used to build a connection string for a MySQL Database
    /// </summary>
    public partial class frmMySQLConnection : Form
    {

        private string server = "";
        private string database = "";
        private string user = "";
        private string password = "";
        private MySql.Data.MySqlClient.MySqlConnectionStringBuilder builder = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();
        
        /// <summary>
        /// the result of the user's interaction with the form
        /// </summary>
        public DialogResult dialogResult = new DialogResult();

        /// <summary>
        /// builds the connectionstring from the values entered in the form controls
        /// </summary>
        public string GetConnectionString
        {
            get
            {
                builder.Add("Server", txtServer.Text.Trim());
                builder.Add("Database", txtDatabase.Text.Trim());
                builder.Add("UserID", txtUser.Text.Trim());
                builder.Add("Password", txtPassword.Text.Trim());
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
        /// this button cancels the current operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// this button will save the connection parameters into a ConnectionString object in the registry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.OK;
        }
    }
}
