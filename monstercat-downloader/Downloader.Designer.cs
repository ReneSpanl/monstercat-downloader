

namespace monstercat_downloader
{
    partial class Main_Window
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_get_releases = new System.Windows.Forms.Button();
            this.btn_start_download = new System.Windows.Forms.Button();
            this.Box_debug = new System.Windows.Forms.TextBox();
            this.lbl_rls_count = new System.Windows.Forms.Label();
            this.btn_debug = new System.Windows.Forms.Button();
            this.box_cookie = new System.Windows.Forms.TextBox();
            this.lbl_cookie = new System.Windows.Forms.Label();
            this.pb_dl = new System.Windows.Forms.ProgressBar();
            this.lbl_curtitle = new System.Windows.Forms.Label();
            this.chck_podcast = new System.Windows.Forms.CheckBox();
            this.chck_mixes = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_get_releases
            // 
            this.btn_get_releases.Location = new System.Drawing.Point(13, 12);
            this.btn_get_releases.Name = "btn_get_releases";
            this.btn_get_releases.Size = new System.Drawing.Size(75, 23);
            this.btn_get_releases.TabIndex = 0;
            this.btn_get_releases.Text = "getRealeses";
            this.btn_get_releases.UseVisualStyleBackColor = true;
            this.btn_get_releases.Click += new System.EventHandler(this.btn_get_releases_Click);
            // 
            // btn_start_download
            // 
            this.btn_start_download.Location = new System.Drawing.Point(13, 57);
            this.btn_start_download.Name = "btn_start_download";
            this.btn_start_download.Size = new System.Drawing.Size(75, 23);
            this.btn_start_download.TabIndex = 1;
            this.btn_start_download.Text = "download";
            this.btn_start_download.UseVisualStyleBackColor = true;
            this.btn_start_download.Click += new System.EventHandler(this.btn_start_download_Click);
            // 
            // Box_debug
            // 
            this.Box_debug.Location = new System.Drawing.Point(215, 218);
            this.Box_debug.Multiline = true;
            this.Box_debug.Name = "Box_debug";
            this.Box_debug.Size = new System.Drawing.Size(268, 38);
            this.Box_debug.TabIndex = 2;
            // 
            // lbl_rls_count
            // 
            this.lbl_rls_count.AutoSize = true;
            this.lbl_rls_count.Location = new System.Drawing.Point(546, 12);
            this.lbl_rls_count.Name = "lbl_rls_count";
            this.lbl_rls_count.Size = new System.Drawing.Size(91, 13);
            this.lbl_rls_count.TabIndex = 3;
            this.lbl_rls_count.Text = "available releases";
            // 
            // btn_debug
            // 
            this.btn_debug.Location = new System.Drawing.Point(591, 168);
            this.btn_debug.Name = "btn_debug";
            this.btn_debug.Size = new System.Drawing.Size(75, 23);
            this.btn_debug.TabIndex = 4;
            this.btn_debug.Text = "Stop Download";
            this.btn_debug.UseMnemonic = false;
            this.btn_debug.UseVisualStyleBackColor = true;
            this.btn_debug.Click += new System.EventHandler(this.btn_debug_Click);
            // 
            // box_cookie
            // 
            this.box_cookie.Location = new System.Drawing.Point(591, 57);
            this.box_cookie.Name = "box_cookie";
            this.box_cookie.Size = new System.Drawing.Size(100, 20);
            this.box_cookie.TabIndex = 5;
            this.box_cookie.TextChanged += new System.EventHandler(this.box_cookie_TextChanged);
            // 
            // lbl_cookie
            // 
            this.lbl_cookie.AutoSize = true;
            this.lbl_cookie.Location = new System.Drawing.Point(508, 60);
            this.lbl_cookie.Name = "lbl_cookie";
            this.lbl_cookie.Size = new System.Drawing.Size(62, 13);
            this.lbl_cookie.TabIndex = 6;
            this.lbl_cookie.Text = "connect.sid";
            // 
            // pb_dl
            // 
            this.pb_dl.ForeColor = System.Drawing.Color.SkyBlue;
            this.pb_dl.Location = new System.Drawing.Point(36, 163);
            this.pb_dl.Name = "pb_dl";
            this.pb_dl.Size = new System.Drawing.Size(521, 31);
            this.pb_dl.TabIndex = 7;
            // 
            // lbl_curtitle
            // 
            this.lbl_curtitle.AutoSize = true;
            this.lbl_curtitle.Location = new System.Drawing.Point(324, 173);
            this.lbl_curtitle.Name = "lbl_curtitle";
            this.lbl_curtitle.Size = new System.Drawing.Size(0, 13);
            this.lbl_curtitle.TabIndex = 8;
            // 
            // chck_podcast
            // 
            this.chck_podcast.AutoSize = true;
            this.chck_podcast.Location = new System.Drawing.Point(104, 61);
            this.chck_podcast.Name = "chck_podcast";
            this.chck_podcast.Size = new System.Drawing.Size(70, 17);
            this.chck_podcast.TabIndex = 9;
            this.chck_podcast.Text = "Podcasts";
            this.chck_podcast.UseVisualStyleBackColor = true;
            this.chck_podcast.CheckedChanged += new System.EventHandler(this.chck_podcast_CheckedChanged);
            // 
            // chck_mixes
            // 
            this.chck_mixes.AutoSize = true;
            this.chck_mixes.Location = new System.Drawing.Point(181, 61);
            this.chck_mixes.Name = "chck_mixes";
            this.chck_mixes.Size = new System.Drawing.Size(53, 17);
            this.chck_mixes.TabIndex = 10;
            this.chck_mixes.Text = "Mixes";
            this.chck_mixes.UseVisualStyleBackColor = true;
            this.chck_mixes.CheckedChanged += new System.EventHandler(this.chck_mixes_CheckedChanged);
            // 
            // Main_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 270);
            this.Controls.Add(this.chck_mixes);
            this.Controls.Add(this.chck_podcast);
            this.Controls.Add(this.lbl_curtitle);
            this.Controls.Add(this.pb_dl);
            this.Controls.Add(this.lbl_cookie);
            this.Controls.Add(this.box_cookie);
            this.Controls.Add(this.btn_debug);
            this.Controls.Add(this.lbl_rls_count);
            this.Controls.Add(this.Box_debug);
            this.Controls.Add(this.btn_start_download);
            this.Controls.Add(this.btn_get_releases);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main_Window";
            this.Text = "monstercat-downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_get_releases;
        private System.Windows.Forms.Button btn_start_download;
        private System.Windows.Forms.TextBox Box_debug;
        private System.Windows.Forms.Label lbl_rls_count;
        private System.Windows.Forms.Button btn_debug;
        private System.Windows.Forms.TextBox box_cookie;
        private System.Windows.Forms.Label lbl_cookie;
        private System.Windows.Forms.ProgressBar pb_dl;
        private System.Windows.Forms.Label lbl_curtitle;
        private System.Windows.Forms.CheckBox chck_podcast;
        private System.Windows.Forms.CheckBox chck_mixes;
    }
}

