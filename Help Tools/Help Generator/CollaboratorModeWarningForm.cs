using System;
using System.Windows.Forms;

namespace Help_Generator
{
    partial class CollaboratorModeWarningForm : Form
    {
        public CollaboratorModeWarningForm()
        {
            InitializeComponent();
            labelWarning.Text = "";
        }

        public void AddWarning(string warningMessage)
        {
            labelWarning.Text = ( String.IsNullOrEmpty(labelWarning.Text) ? "" : ( labelWarning.Text + "\r\n\r\n" ) ) + warningMessage;
        }

        private void CollaboratorModeWarningForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if( e.CloseReason == CloseReason.UserClosing )
                e.Cancel = true; // prevent the user from closing the the warning message
        }
    }
}
