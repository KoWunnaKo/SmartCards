namespace MRZ2HEX_Tool
{
    partial class Form1
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
            this.bnt_calc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_license_number = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_date_of_birth = new System.Windows.Forms.DateTimePicker();
            this.dtp_expire_date = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txb_res = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bnt_calc
            // 
            this.bnt_calc.Location = new System.Drawing.Point(106, 191);
            this.bnt_calc.Name = "bnt_calc";
            this.bnt_calc.Size = new System.Drawing.Size(75, 23);
            this.bnt_calc.TabIndex = 0;
            this.bnt_calc.Text = "Calc";
            this.bnt_calc.UseVisualStyleBackColor = true;
            this.bnt_calc.Click += new System.EventHandler(this.bnt_calc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "_license_number";
            // 
            // txb_license_number
            // 
            this.txb_license_number.Location = new System.Drawing.Point(13, 30);
            this.txb_license_number.Name = "txb_license_number";
            this.txb_license_number.Size = new System.Drawing.Size(234, 20);
            this.txb_license_number.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "_date_of_birth";
            // 
            // dtp_date_of_birth
            // 
            this.dtp_date_of_birth.Location = new System.Drawing.Point(13, 92);
            this.dtp_date_of_birth.Name = "dtp_date_of_birth";
            this.dtp_date_of_birth.Size = new System.Drawing.Size(200, 20);
            this.dtp_date_of_birth.TabIndex = 4;
            // 
            // dtp_expire_date
            // 
            this.dtp_expire_date.Location = new System.Drawing.Point(13, 145);
            this.dtp_expire_date.Name = "dtp_expire_date";
            this.dtp_expire_date.Size = new System.Drawing.Size(200, 20);
            this.dtp_expire_date.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "_expire_date";
            // 
            // txb_res
            // 
            this.txb_res.Location = new System.Drawing.Point(16, 235);
            this.txb_res.Name = "txb_res";
            this.txb_res.Size = new System.Drawing.Size(305, 20);
            this.txb_res.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 307);
            this.Controls.Add(this.txb_res);
            this.Controls.Add(this.dtp_expire_date);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtp_date_of_birth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txb_license_number);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnt_calc);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnt_calc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_license_number;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_date_of_birth;
        private System.Windows.Forms.DateTimePicker dtp_expire_date;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txb_res;
    }
}

