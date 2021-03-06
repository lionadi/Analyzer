﻿namespace AnalyzerServiceApplication
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
            this.components = new System.ComponentModel.Container();
            this.bStartService = new System.Windows.Forms.Button();
            this.bStopService = new System.Windows.Forms.Button();
            this.lblServiceStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRuntime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lblItemsProcessed = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblItemsFailed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bStartService
            // 
            this.bStartService.Location = new System.Drawing.Point(12, 267);
            this.bStartService.Name = "bStartService";
            this.bStartService.Size = new System.Drawing.Size(75, 23);
            this.bStartService.TabIndex = 0;
            this.bStartService.Text = "Start";
            this.bStartService.UseVisualStyleBackColor = true;
            this.bStartService.Click += new System.EventHandler(this.bStartService_Click);
            // 
            // bStopService
            // 
            this.bStopService.Location = new System.Drawing.Point(357, 267);
            this.bStopService.Name = "bStopService";
            this.bStopService.Size = new System.Drawing.Size(75, 23);
            this.bStopService.TabIndex = 1;
            this.bStopService.Text = "Stop";
            this.bStopService.UseVisualStyleBackColor = true;
            this.bStopService.Click += new System.EventHandler(this.bStopService_Click);
            // 
            // lblServiceStatus
            // 
            this.lblServiceStatus.AutoSize = true;
            this.lblServiceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceStatus.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblServiceStatus.Location = new System.Drawing.Point(81, 18);
            this.lblServiceStatus.Name = "lblServiceStatus";
            this.lblServiceStatus.Size = new System.Drawing.Size(289, 76);
            this.lblServiceStatus.TabIndex = 2;
            this.lblServiceStatus.Text = "Stopped";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "Runtime:";
            // 
            // lblRuntime
            // 
            this.lblRuntime.AutoSize = true;
            this.lblRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuntime.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblRuntime.Location = new System.Drawing.Point(327, 182);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(43, 46);
            this.lblRuntime.TabIndex = 4;
            this.lblRuntime.Text = "0";
            this.lblRuntime.TextChanged += new System.EventHandler(this.lblRuntime_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.label2.Location = new System.Drawing.Point(20, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Items processed:";
            // 
            // lblItemsProcessed
            // 
            this.lblItemsProcessed.AutoSize = true;
            this.lblItemsProcessed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsProcessed.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblItemsProcessed.Location = new System.Drawing.Point(256, 138);
            this.lblItemsProcessed.Name = "lblItemsProcessed";
            this.lblItemsProcessed.Size = new System.Drawing.Size(14, 13);
            this.lblItemsProcessed.TabIndex = 6;
            this.lblItemsProcessed.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Location = new System.Drawing.Point(20, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Items failed:";
            // 
            // lblItemsFailed
            // 
            this.lblItemsFailed.AutoSize = true;
            this.lblItemsFailed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsFailed.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblItemsFailed.Location = new System.Drawing.Point(256, 160);
            this.lblItemsFailed.Name = "lblItemsFailed";
            this.lblItemsFailed.Size = new System.Drawing.Size(14, 13);
            this.lblItemsFailed.TabIndex = 8;
            this.lblItemsFailed.Text = "0";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 302);
            this.Controls.Add(this.lblItemsFailed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblItemsProcessed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRuntime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblServiceStatus);
            this.Controls.Add(this.bStopService);
            this.Controls.Add(this.bStartService);
            this.Name = "Main";
            this.Text = "Analyzer Web Scraper Service Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bStartService;
        private System.Windows.Forms.Button bStopService;
        private System.Windows.Forms.Label lblServiceStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRuntime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblItemsProcessed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblItemsFailed;
    }
}

