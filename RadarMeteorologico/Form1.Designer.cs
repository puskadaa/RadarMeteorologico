namespace RadarMeteorologico
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos sendo usados.
        /// </summary>
        /// <param name="disposing">
        /// true se os recursos gerenciados devem ser descartados; caso contrário, false.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();

            this.SuspendLayout();

            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;

            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;

            this.pictureBox1.Location = new System.Drawing.Point(0, 0);

            this.pictureBox1.Name = "pictureBox1";

            this.pictureBox1.Size = new System.Drawing.Size(800, 600);

            this.pictureBox1.TabIndex = 0;

            this.pictureBox1.TabStop = false;

            this.pictureBox1.Paint +=
                new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

            this.pictureBox1.MouseClick +=
                new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);

            this.pictureBox1.Click +=
                new System.EventHandler(this.pictureBox1_Click);

            // 
            // Form1
            // 
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(8F, 16F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(800, 600);

            this.Controls.Add(this.pictureBox1);

            this.Name = "Form1";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "Radar Meteorologico";

            this.Load +=
                new System.EventHandler(this.Form1_Load);

            this.KeyDown +=
                new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);

            this.KeyUp +=
                new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
    }
}