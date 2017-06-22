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

        }

        /// <summary>
        /// this button will save the connection parameters into a ConnectionString object in the registry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
