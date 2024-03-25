namespace DevExProgramacaoLaura
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.JuridicalP = new System.Windows.Forms.Button();
            this.NaturalP = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // JuridicalP
            // 
            this.JuridicalP.BackColor = System.Drawing.SystemColors.ControlDark;
            this.JuridicalP.Font = new System.Drawing.Font("Candara", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JuridicalP.Location = new System.Drawing.Point(435, 366);
            this.JuridicalP.Name = "JuridicalP";
            this.JuridicalP.Size = new System.Drawing.Size(172, 68);
            this.JuridicalP.TabIndex = 11;
            this.JuridicalP.Text = "Pessoa Juridica";
            this.JuridicalP.UseVisualStyleBackColor = false;
            this.JuridicalP.Click += new System.EventHandler(this.JuridicalP_Click);
            // 
            // NaturalP
            // 
            this.NaturalP.BackColor = System.Drawing.SystemColors.ControlDark;
            this.NaturalP.Font = new System.Drawing.Font("Candara", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NaturalP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NaturalP.Location = new System.Drawing.Point(38, 366);
            this.NaturalP.Name = "NaturalP";
            this.NaturalP.Size = new System.Drawing.Size(172, 68);
            this.NaturalP.TabIndex = 10;
            this.NaturalP.Text = "Pessoa Fisica";
            this.NaturalP.UseVisualStyleBackColor = false;
            this.NaturalP.Click += new System.EventHandler(this.NaturalP_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.ErrorImage")));
            this.pictureBox2.ImageLocation = "C:\\Users\\laura.campos\\Desktop\\img";
            this.pictureBox2.Location = new System.Drawing.Point(410, 88);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(227, 228);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.ImageLocation = "C:\\Users\\laura.campos\\Desktop\\img";
            this.pictureBox1.Location = new System.Drawing.Point(12, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 228);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 513);
            this.Controls.Add(this.JuridicalP);
            this.Controls.Add(this.NaturalP);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.Button JuridicalP;
        private System.Windows.Forms.Button NaturalP;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

