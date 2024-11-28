
namespace CarRental
{
    partial class addRental
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
            this.comboBoxMake = new System.Windows.Forms.ComboBox();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            this.dateTimePickerRentalDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerReturnDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxTotalAmount = new System.Windows.Forms.TextBox();
            this.buttonAddRental = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxMake
            // 
            this.comboBoxMake.FormattingEnabled = true;
            this.comboBoxMake.Location = new System.Drawing.Point(49, 47);
            this.comboBoxMake.Name = "comboBoxMake";
            this.comboBoxMake.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMake.TabIndex = 0;
            this.comboBoxMake.SelectedIndexChanged += new System.EventHandler(this.comboBoxMake_SelectedIndexChanged);
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(49, 97);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(121, 21);
            this.comboBoxModel.TabIndex = 1;
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Location = new System.Drawing.Point(49, 157);
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCustomer.TabIndex = 2;
            // 
            // dateTimePickerRentalDate
            // 
            this.dateTimePickerRentalDate.Location = new System.Drawing.Point(30, 234);
            this.dateTimePickerRentalDate.Name = "dateTimePickerRentalDate";
            this.dateTimePickerRentalDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerRentalDate.TabIndex = 3;
            // 
            // dateTimePickerReturnDate
            // 
            this.dateTimePickerReturnDate.Location = new System.Drawing.Point(30, 297);
            this.dateTimePickerReturnDate.Name = "dateTimePickerReturnDate";
            this.dateTimePickerReturnDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerReturnDate.TabIndex = 4;
            this.dateTimePickerReturnDate.ValueChanged += new System.EventHandler(this.dateTimePickerReturnDate_ValueChanged);
            // 
            // textBoxTotalAmount
            // 
            this.textBoxTotalAmount.Location = new System.Drawing.Point(49, 354);
            this.textBoxTotalAmount.Name = "textBoxTotalAmount";
            this.textBoxTotalAmount.Size = new System.Drawing.Size(100, 20);
            this.textBoxTotalAmount.TabIndex = 5;
            // 
            // buttonAddRental
            // 
            this.buttonAddRental.Location = new System.Drawing.Point(49, 408);
            this.buttonAddRental.Name = "buttonAddRental";
            this.buttonAddRental.Size = new System.Drawing.Size(75, 20);
            this.buttonAddRental.TabIndex = 6;
            this.buttonAddRental.Text = "button1";
            this.buttonAddRental.UseVisualStyleBackColor = true;
            this.buttonAddRental.Click += new System.EventHandler(this.buttonAddRental_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(49, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // addRental
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(445, 629);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonAddRental);
            this.Controls.Add(this.textBoxTotalAmount);
            this.Controls.Add(this.dateTimePickerReturnDate);
            this.Controls.Add(this.dateTimePickerRentalDate);
            this.Controls.Add(this.comboBoxCustomer);
            this.Controls.Add(this.comboBoxModel);
            this.Controls.Add(this.comboBoxMake);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "addRental";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "addRental";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMake;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
        private System.Windows.Forms.DateTimePicker dateTimePickerRentalDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerReturnDate;
        private System.Windows.Forms.TextBox textBoxTotalAmount;
        private System.Windows.Forms.Button buttonAddRental;
        private System.Windows.Forms.Button button1;
    }
}