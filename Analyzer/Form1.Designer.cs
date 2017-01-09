namespace Analyzer
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
            this.bCrawlURL = new System.Windows.Forms.Button();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bCrawlURL
            // 
            this.bCrawlURL.Location = new System.Drawing.Point(881, 32);
            this.bCrawlURL.Name = "bCrawlURL";
            this.bCrawlURL.Size = new System.Drawing.Size(75, 23);
            this.bCrawlURL.TabIndex = 0;
            this.bCrawlURL.Text = "Crawl URL";
            this.bCrawlURL.UseVisualStyleBackColor = true;
            this.bCrawlURL.Click += new System.EventHandler(this.bCrawlURL_Click);
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(37, 34);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(838, 20);
            this.tbURL.TabIndex = 1;
            this.tbURL.Text = "https://lionadi.wordpress.com/feed/";
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(37, 13);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(29, 13);
            this.lblURL.TabIndex = 2;
            this.lblURL.Text = "URL";
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(40, 96);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(916, 525);
            this.tbResult.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 642);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.bCrawlURL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bCrawlURL;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox tbResult;
    }
}

