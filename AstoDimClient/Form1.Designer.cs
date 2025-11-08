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
            mskLicenseKey = new MaskedTextBox();
            label1 = new Label();
            label3 = new Label();
            lblLicenseKey = new Label();
            btnHideKey = new Button();
            lblRemaining = new Label();
            label2 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            btnActivateLicense = new Button();
            btnInjectBot = new Button();
            SuspendLayout();
            // 
            // mskLicenseKey
            // 
            mskLicenseKey.BackColor = Color.Black;
            mskLicenseKey.BorderStyle = BorderStyle.None;
            mskLicenseKey.Font = new Font("VCR OSD Mono", 18F);
            mskLicenseKey.ForeColor = Color.FromArgb(162, 0, 0);
            mskLicenseKey.Location = new Point(486, 213);
            mskLicenseKey.Mask = ">AAAAA-AAAAA-AAAAA-AAAAA-AAAAA";
            mskLicenseKey.Name = "mskLicenseKey";
            mskLicenseKey.Size = new Size(413, 24);
            mskLicenseKey.TabIndex = 2;
            mskLicenseKey.Enter += mskLicenseKey_Enter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("VCR OSD Mono", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(162, 0, 0);
            label1.Location = new Point(484, 183);
            label1.Name = "label1";
            label1.Size = new Size(194, 19);
            label1.TabIndex = 3;
            label1.Text = "Lisans Anahtari: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("VCR OSD Mono", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.DarkRed;
            label3.Location = new Point(12, 566);
            label3.Name = "label3";
            label3.Size = new Size(125, 14);
            label3.TabIndex = 6;
            label3.Text = "Lisans Anahtari";
            // 
            // lblLicenseKey
            // 
            lblLicenseKey.AutoSize = true;
            lblLicenseKey.BackColor = Color.Transparent;
            lblLicenseKey.Font = new Font("VCR OSD Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLicenseKey.ForeColor = Color.DarkRed;
            lblLicenseKey.Location = new Point(12, 584);
            lblLicenseKey.Name = "lblLicenseKey";
            lblLicenseKey.Size = new Size(189, 14);
            lblLicenseKey.TabIndex = 7;
            lblLicenseKey.Text = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX";
            // 
            // btnHideKey
            // 
            btnHideKey.BackColor = Color.Transparent;
            btnHideKey.BackgroundImage = Properties.Resources.no_see_visible_hidde_icon_187886;
            btnHideKey.BackgroundImageLayout = ImageLayout.Stretch;
            btnHideKey.FlatAppearance.BorderSize = 0;
            btnHideKey.FlatStyle = FlatStyle.Flat;
            btnHideKey.ForeColor = Color.DarkRed;
            btnHideKey.Location = new Point(255, 572);
            btnHideKey.Name = "btnHideKey";
            btnHideKey.Size = new Size(30, 30);
            btnHideKey.TabIndex = 8;
            btnHideKey.UseVisualStyleBackColor = false;
            btnHideKey.Visible = false;
            btnHideKey.Click += btnHideKey_Click;
            // 
            // lblRemaining
            // 
            lblRemaining.Font = new Font("VCR OSD Mono", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRemaining.ForeColor = Color.FromArgb(162, 0, 0);
            lblRemaining.Location = new Point(452, 405);
            lblRemaining.Name = "lblRemaining";
            lblRemaining.Size = new Size(471, 19);
            lblRemaining.TabIndex = 14;
            lblRemaining.Text = "Lisansin kalan süresi: 30 gün";
            lblRemaining.TextAlign = ContentAlignment.MiddleCenter;
            lblRemaining.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("VCR OSD Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(162, 0, 0);
            label2.Location = new Point(486, 76);
            label2.Name = "label2";
            label2.Size = new Size(289, 42);
            label2.TabIndex = 16;
            label2.Text = "System Initialization Status: True\r\nCooler Systems: Active\r\nBypassing Security Firewall: Success";
            // 
            // timer1
            // 
            timer1.Interval = 60000;
            timer1.Tick += timer1_Tick;
            // 
            // btnActivateLicense
            // 
            btnActivateLicense.BackgroundImage = Properties.Resources.pipis;
            btnActivateLicense.BackgroundImageLayout = ImageLayout.Center;
            btnActivateLicense.FlatAppearance.BorderSize = 0;
            btnActivateLicense.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnActivateLicense.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnActivateLicense.FlatStyle = FlatStyle.Flat;
            btnActivateLicense.Font = new Font("VCR OSD Mono", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnActivateLicense.ForeColor = Color.FromArgb(192, 0, 0);
            btnActivateLicense.Location = new Point(728, 243);
            btnActivateLicense.Name = "btnActivateLicense";
            btnActivateLicense.Size = new Size(171, 50);
            btnActivateLicense.TabIndex = 17;
            btnActivateLicense.Text = "Aktiflestir";
            btnActivateLicense.UseVisualStyleBackColor = true;
            btnActivateLicense.Click += btnActivateLicense_Click;
            btnActivateLicense.MouseEnter += btnActivateLicense_MouseEnter;
            btnActivateLicense.MouseLeave += btnActivateLicense_MouseLeave;
            // 
            // btnInjectBot
            // 
            btnInjectBot.BackgroundImage = Properties.Resources.popos1;
            btnInjectBot.BackgroundImageLayout = ImageLayout.Stretch;
            btnInjectBot.FlatAppearance.BorderSize = 0;
            btnInjectBot.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnInjectBot.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnInjectBot.FlatStyle = FlatStyle.Flat;
            btnInjectBot.Font = new Font("VCR OSD Mono", 18F);
            btnInjectBot.ForeColor = Color.Red;
            btnInjectBot.Location = new Point(517, 277);
            btnInjectBot.Name = "btnInjectBot";
            btnInjectBot.Size = new Size(335, 125);
            btnInjectBot.TabIndex = 18;
            btnInjectBot.Text = "Botu Enjekte Et";
            btnInjectBot.UseVisualStyleBackColor = true;
            btnInjectBot.Visible = false;
            btnInjectBot.Click += btnInjectBot_Click_1;
            btnInjectBot.MouseEnter += btnInjectBot_MouseEnter;
            btnInjectBot.MouseLeave += btnInjectBot_MouseLeave;
            // 
            // frmClientMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.ultrakill_red_black_machine_robot_hd_wallpaper_08962df850a0cc58b08cc10e58b2040a3;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(926, 607);
            Controls.Add(btnInjectBot);
            Controls.Add(btnActivateLicense);
            Controls.Add(label2);
            Controls.Add(lblRemaining);
            Controls.Add(btnHideKey);
            Controls.Add(lblLicenseKey);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(mskLicenseKey);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmClientMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AstoDim Client";
            Load += frmClientMain_Load;
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
        private Label label2;
        private System.Windows.Forms.Timer timer1;
        private Button btnActivateLicense;
        private Button btnInjectBot;
    }
}
