namespace SetupTool
{
    partial class frmClientMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientMain));
            mskLicenseKey = new MaskedTextBox();
            label1 = new Label();
            label3 = new Label();
            lblLicenseKey = new Label();
            btnHideKey = new Button();
            lblRemaining = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            btnActivateLicense = new Button();
            btnInjectBot = new Button();
            lblVersion = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // mskLicenseKey
            // 
            mskLicenseKey.BackColor = Color.Black;
            mskLicenseKey.BorderStyle = BorderStyle.None;
            mskLicenseKey.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 162);
            mskLicenseKey.ForeColor = Color.FromArgb(162, 0, 0);
            mskLicenseKey.Location = new Point(35, 205);
            mskLicenseKey.Mask = ">AAAAA-AAAAA-AAAAA-AAAAA-AAAAA";
            mskLicenseKey.Name = "mskLicenseKey";
            mskLicenseKey.Size = new Size(413, 32);
            mskLicenseKey.TabIndex = 2;
            mskLicenseKey.Enter += mskLicenseKey_Enter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(162, 0, 0);
            label1.Location = new Point(32, 175);
            label1.Name = "label1";
            label1.Size = new Size(152, 27);
            label1.TabIndex = 3;
            label1.Text = "Lisans Anahtari: ";
            label1.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.DarkRed;
            label3.Location = new Point(12, 416);
            label3.Name = "label3";
            label3.Size = new Size(97, 20);
            label3.TabIndex = 6;
            label3.Text = "Lisans Anahtari";
            label3.UseCompatibleTextRendering = true;
            // 
            // lblLicenseKey
            // 
            lblLicenseKey.AutoSize = true;
            lblLicenseKey.BackColor = Color.Transparent;
            lblLicenseKey.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblLicenseKey.ForeColor = Color.DarkRed;
            lblLicenseKey.Location = new Point(12, 434);
            lblLicenseKey.Name = "lblLicenseKey";
            lblLicenseKey.Size = new Size(227, 19);
            lblLicenseKey.TabIndex = 7;
            lblLicenseKey.Text = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX";
            lblLicenseKey.UseCompatibleTextRendering = true;
            // 
            // btnHideKey
            // 
            btnHideKey.BackColor = Color.Transparent;
            btnHideKey.BackgroundImage = Properties.Resources.no_see_visible_hidde_icon_187886;
            btnHideKey.BackgroundImageLayout = ImageLayout.Stretch;
            btnHideKey.FlatAppearance.BorderSize = 0;
            btnHideKey.FlatStyle = FlatStyle.Flat;
            btnHideKey.ForeColor = Color.DarkRed;
            btnHideKey.Location = new Point(255, 422);
            btnHideKey.Name = "btnHideKey";
            btnHideKey.Size = new Size(30, 30);
            btnHideKey.TabIndex = 8;
            btnHideKey.UseVisualStyleBackColor = false;
            btnHideKey.Visible = false;
            btnHideKey.Click += btnHideKey_Click;
            // 
            // lblRemaining
            // 
            lblRemaining.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblRemaining.ForeColor = Color.FromArgb(162, 0, 0);
            lblRemaining.Location = new Point(0, 323);
            lblRemaining.Name = "lblRemaining";
            lblRemaining.Size = new Size(485, 19);
            lblRemaining.TabIndex = 14;
            lblRemaining.Text = "Lisansin kalan süresi: 30 gün";
            lblRemaining.TextAlign = ContentAlignment.MiddleCenter;
            lblRemaining.UseCompatibleTextRendering = true;
            lblRemaining.Visible = false;
            // 
            // timer1
            // 
            timer1.Interval = 1800000;
            timer1.Tick += timer1_Tick;
            // 
            // btnActivateLicense
            // 
            btnActivateLicense.BackgroundImage = Properties.Resources.button2;
            btnActivateLicense.BackgroundImageLayout = ImageLayout.Center;
            btnActivateLicense.FlatAppearance.BorderSize = 0;
            btnActivateLicense.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnActivateLicense.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnActivateLicense.FlatStyle = FlatStyle.Flat;
            btnActivateLicense.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnActivateLicense.ForeColor = Color.FromArgb(192, 0, 0);
            btnActivateLicense.Location = new Point(277, 235);
            btnActivateLicense.Name = "btnActivateLicense";
            btnActivateLicense.Size = new Size(171, 50);
            btnActivateLicense.TabIndex = 17;
            btnActivateLicense.Text = "Aktiflestir";
            btnActivateLicense.UseCompatibleTextRendering = true;
            btnActivateLicense.UseVisualStyleBackColor = true;
            btnActivateLicense.Click += btnActivateLicense_Click;
            btnActivateLicense.MouseEnter += btnActivateLicense_MouseEnter;
            btnActivateLicense.MouseLeave += btnActivateLicense_MouseLeave;
            // 
            // btnInjectBot
            // 
            btnInjectBot.BackgroundImage = Properties.Resources.button1;
            btnInjectBot.BackgroundImageLayout = ImageLayout.Stretch;
            btnInjectBot.FlatAppearance.BorderSize = 0;
            btnInjectBot.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnInjectBot.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnInjectBot.FlatStyle = FlatStyle.Flat;
            btnInjectBot.Font = new Font("Microsoft Sans Serif", 18F);
            btnInjectBot.ForeColor = Color.Red;
            btnInjectBot.Location = new Point(75, 195);
            btnInjectBot.Name = "btnInjectBot";
            btnInjectBot.Size = new Size(335, 125);
            btnInjectBot.TabIndex = 18;
            btnInjectBot.Text = "Botu Enjekte Et";
            btnInjectBot.UseCompatibleTextRendering = true;
            btnInjectBot.UseVisualStyleBackColor = true;
            btnInjectBot.Visible = false;
            btnInjectBot.Click += btnInjectBot_Click_1;
            btnInjectBot.MouseEnter += btnInjectBot_MouseEnter;
            btnInjectBot.MouseLeave += btnInjectBot_MouseLeave;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Microsoft Sans Serif", 12F);
            lblVersion.ForeColor = Color.DarkRed;
            lblVersion.Location = new Point(355, 430);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(94, 24);
            lblVersion.TabIndex = 19;
            lblVersion.Text = "v0.1 [BETA]";
            lblVersion.UseCompatibleTextRendering = true;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.astodim_logo;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(12, 34);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(460, 83);
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // frmClientMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(484, 461);
            Controls.Add(pictureBox1);
            Controls.Add(lblVersion);
            Controls.Add(btnInjectBot);
            Controls.Add(btnActivateLicense);
            Controls.Add(lblRemaining);
            Controls.Add(btnHideKey);
            Controls.Add(lblLicenseKey);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(mskLicenseKey);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmClientMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AstoDim Client";
            Load += frmClientMain_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MaskedTextBox mskLicenseKey;
        private Label label1;
        private Label label3;
        private Label lblLicenseKey;
        private Button btnHideKey;
        private Label lblRemaining;
        private System.Windows.Forms.Timer timer1;
        private Button btnActivateLicense;
        private Button btnInjectBot;
        private Label lblVersion;
        private PictureBox pictureBox1;
    }
}
