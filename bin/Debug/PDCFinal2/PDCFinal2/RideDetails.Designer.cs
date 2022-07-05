
namespace PDCFinal2
{
    partial class RideDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbIdCard = new System.Windows.Forms.TextBox();
            this.btnAddMe = new System.Windows.Forms.Button();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(91, 13);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(227, 20);
            this.tbName.TabIndex = 0;
            this.tbName.Text = "Your Name Here";
            // 
            // tbPhone
            // 
            this.tbPhone.Location = new System.Drawing.Point(91, 39);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(227, 20);
            this.tbPhone.TabIndex = 0;
            this.tbPhone.Text = "Contact Number";
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(91, 90);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(227, 20);
            this.tbAddress.TabIndex = 0;
            this.tbAddress.Text = "Address";
            // 
            // tbIdCard
            // 
            this.tbIdCard.Location = new System.Drawing.Point(91, 126);
            this.tbIdCard.Name = "tbIdCard";
            this.tbIdCard.Size = new System.Drawing.Size(227, 20);
            this.tbIdCard.TabIndex = 0;
            this.tbIdCard.Text = "ID Card Number";
            // 
            // btnAddMe
            // 
            this.btnAddMe.Location = new System.Drawing.Point(91, 166);
            this.btnAddMe.Name = "btnAddMe";
            this.btnAddMe.Size = new System.Drawing.Size(227, 23);
            this.btnAddMe.TabIndex = 1;
            this.btnAddMe.Text = "Add me As";
            this.btnAddMe.UseVisualStyleBackColor = true;
            this.btnAddMe.Click += new System.EventHandler(this.btnAddMe_Click);
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Items.AddRange(new object[] {
            "customer",
            "Rider"});
            this.cbCategory.Location = new System.Drawing.Point(91, 65);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(227, 21);
            this.cbCategory.TabIndex = 18;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // RideDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 218);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.btnAddMe);
            this.Controls.Add(this.tbIdCard);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.tbPhone);
            this.Controls.Add(this.tbName);
            this.Name = "RideDetails";
            this.Text = "RideDetails";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RideDetails_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.TextBox tbIdCard;
        private System.Windows.Forms.Button btnAddMe;
        private System.Windows.Forms.ComboBox cbCategory;
    }
}