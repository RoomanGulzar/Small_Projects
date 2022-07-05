using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDCFinal2
{
    public partial class RideDetails : Form
    {
        public RideDetails()
        {
            InitializeComponent();
        }

        private void btnAddMe_Click(object sender, EventArgs e)
        {
            if(tbAddress.Text!= string.Empty && tbIdCard.Text != string.Empty &&
                tbName.Text!= string.Empty && tbPhone.Text!= string.Empty)
            {
                LocationDetails.Name = tbName.Text;
                LocationDetails.cate = cbCategory.SelectedItem.ToString();
                MessageBox.Show("You Are Added");
                DialogResult = DialogResult.OK;

                this.Close();
                
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex > -1)
                btnAddMe.Text = $"Add me as {cbCategory.Text}";
        }

        private void RideDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
          //  if(e.CloseReason == CloseReason.UserClosing)
            // Application.Exit();
        }
    }
}
