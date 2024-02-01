namespace Raketa
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.gumbPokreni = new System.Windows.Forms.Button();
            this.gumbZatvori = new System.Windows.Forms.Button();
            this.gumbPostavke = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gumbPokreni
            // 
            this.gumbPokreni.BackColor = System.Drawing.Color.Green;
            this.gumbPokreni.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gumbPokreni.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gumbPokreni.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gumbPokreni.Location = new System.Drawing.Point(82, 127);
            this.gumbPokreni.Margin = new System.Windows.Forms.Padding(2);
            this.gumbPokreni.Name = "gumbPokreni";
            this.gumbPokreni.Size = new System.Drawing.Size(150, 41);
            this.gumbPokreni.TabIndex = 0;
            this.gumbPokreni.Text = "Pokreni igru";
            this.gumbPokreni.UseVisualStyleBackColor = false;
            this.gumbPokreni.Click += new System.EventHandler(this.gumbPokreni_Click);
            // 
            // gumbZatvori
            // 
            this.gumbZatvori.BackColor = System.Drawing.Color.Red;
            this.gumbZatvori.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gumbZatvori.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gumbZatvori.Location = new System.Drawing.Point(82, 259);
            this.gumbZatvori.Margin = new System.Windows.Forms.Padding(2);
            this.gumbZatvori.Name = "gumbZatvori";
            this.gumbZatvori.Size = new System.Drawing.Size(150, 41);
            this.gumbZatvori.TabIndex = 1;
            this.gumbZatvori.Text = "Zatvori";
            this.gumbZatvori.UseVisualStyleBackColor = false;
            this.gumbZatvori.Click += new System.EventHandler(this.gumbZatvori_Click);
            // 
            // gumbPostavke
            // 
            this.gumbPostavke.BackColor = System.Drawing.Color.LightSeaGreen;
            this.gumbPostavke.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gumbPostavke.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gumbPostavke.Location = new System.Drawing.Point(82, 195);
            this.gumbPostavke.Margin = new System.Windows.Forms.Padding(2);
            this.gumbPostavke.Name = "gumbPostavke";
            this.gumbPostavke.Size = new System.Drawing.Size(150, 41);
            this.gumbPostavke.TabIndex = 2;
            this.gumbPostavke.Text = "Postavke";
            this.gumbPostavke.UseVisualStyleBackColor = false;
            this.gumbPostavke.Click += new System.EventHandler(this.gumbPostavke_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.BackgroundImage = global::Raketa.Properties.Resources.pozadinaNovi;
            this.ClientSize = new System.Drawing.Size(316, 431);
            this.Controls.Add(this.gumbPostavke);
            this.Controls.Add(this.gumbZatvori);
            this.Controls.Add(this.gumbPokreni);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Početni izbornik";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button gumbPokreni;
        private System.Windows.Forms.Button gumbZatvori;
        private System.Windows.Forms.Button gumbPostavke;
    }
}