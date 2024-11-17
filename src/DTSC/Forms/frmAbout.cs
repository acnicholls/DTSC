using System.Diagnostics;

namespace DBTableMover
{

    /// <summary>
    /// the copyright and information form
    /// </summary>
    public class frmAbout : System.Windows.Forms.Form
    {
        private System.Windows.Forms.LinkLabel labelLink;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelDisclaimer;
        private System.Windows.Forms.Label labelAdvertise;

        private ProjectInfo info;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmAbout()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //	Insert text for about...
            info = new ProjectInfo();

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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAbout));
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelLink = new System.Windows.Forms.LinkLabel();
            this.cmdOK = new System.Windows.Forms.Button();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelDisclaimer = new System.Windows.Forms.Label();
            this.labelAdvertise = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.Location = new System.Drawing.Point(296, 176);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(175, 70);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version : 1.0.1.020\r\nManufacturer: victoriahosting.com\r\nAuthor: kod3brkr\r\n\r\nCopyr" +
                "ight: 2007";
            // 
            // labelLink
            // 
            this.labelLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.labelLink.Location = new System.Drawing.Point(8, 128);
            this.labelLink.Name = "labelLink";
            this.labelLink.Size = new System.Drawing.Size(480, 23);
            this.labelLink.TabIndex = 1;
            this.labelLink.TabStop = true;
            this.labelLink.Text = "For Change Requests Click Here";
            this.labelLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(408, 19);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelDescription
            // 
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(16, 10);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(384, 54);
            this.labelDescription.TabIndex = 3;
            this.labelDescription.Text = "DESCRIPTION: This program builds a list of folders on a target drive, alongside t" +
                "he size in bytes, kilobytes or megabytes. \r\nAn excellent tool for network admini" +
                "strators.\r\n";
            // 
            // labelDisclaimer
            // 
            this.labelDisclaimer.Location = new System.Drawing.Point(8, 160);
            this.labelDisclaimer.Name = "labelDisclaimer";
            this.labelDisclaimer.Size = new System.Drawing.Size(272, 96);
            this.labelDisclaimer.TabIndex = 4;
            this.labelDisclaimer.Text = @"WARNING: This computer program is protected by copyright law and international treaties.  Unauthorized reproduction or distribution of this program, or any part of it, may result in severe and criminal penalties, and will be prosecuted to the maximum extent possible under the law.";
            this.labelDisclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAdvertise
            // 
            this.labelAdvertise.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.labelAdvertise.Location = new System.Drawing.Point(11, 72);
            this.labelAdvertise.Name = "labelAdvertise";
            this.labelAdvertise.Size = new System.Drawing.Size(472, 48);
            this.labelAdvertise.TabIndex = 5;
            this.labelAdvertise.Text = "Produces a printed report.\r\nSchedulable, Unattended.\r\nSees network drives\r\n";
            this.labelAdvertise.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(494, 268);
            this.ControlBox = false;
            this.Controls.Add(this.labelAdvertise);
            this.Controls.Add(this.labelDisclaimer);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.labelLink);
            this.Controls.Add(this.labelVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Database Table Script Creator";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// fires up the users default browser and send them to the specified link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string address = "";
            // TODO: change this to either an email address, or a form on the new website
            address = "http://www.acnicholls.com/Support.aspx";
            address += "?Subject=Database Table Script Creator Change Request";
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = address;
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.RedirectStandardOutput = false;
            myProcess.Start();
            myProcess.Dispose();
        }

        /// <summary>
        /// this button closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// the load method of this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void frmAbout_Load(object sender, System.EventArgs e)
        {
            string message = "Version: " + info.Version.ToString() + "\n";
            message += "Manufacturer: " + info.Manufacturer.ToString() + "\n";
            message += "Author: " + info.Author.ToString() + "\n";
            message += "\n";
            message += "Copyright since 2008";
            this.labelVersion.Text = message;

            this.labelDescription.Text = info.Description.ToString();

            message = "Scripts for data as well as table structure.\n";
            message += "Scripts for any user table in your selected database.\n";
            message += "Connects to any SQL Server Database you control.";
            this.labelAdvertise.Text = message;

        }

    }
}
