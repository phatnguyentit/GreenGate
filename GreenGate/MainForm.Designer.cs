
namespace GreenGate
{
    partial class MainForm
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
            this.pbCam = new System.Windows.Forms.PictureBox();
            this.cbbCam = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.listboxCapture = new System.Windows.Forms.ListBox();
            this.sttStrip = new System.Windows.Forms.StatusStrip();
            this.lbProcessing2CText = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb2CCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbSeparator1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbProcessing1CText = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb1CCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.saparator2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbCurrentCheck = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRefreshCamList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCam)).BeginInit();
            this.sttStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCam
            // 
            this.pbCam.Location = new System.Drawing.Point(47, 102);
            this.pbCam.Name = "pbCam";
            this.pbCam.Size = new System.Drawing.Size(770, 443);
            this.pbCam.TabIndex = 0;
            this.pbCam.TabStop = false;
            this.pbCam.WaitOnLoad = true;
            // 
            // cbbCam
            // 
            this.cbbCam.FormattingEnabled = true;
            this.cbbCam.Location = new System.Drawing.Point(276, 33);
            this.cbbCam.Name = "cbbCam";
            this.cbbCam.Size = new System.Drawing.Size(282, 23);
            this.cbbCam.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(379, 62);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // lbCapture
            // 
            this.listboxCapture.FormattingEnabled = true;
            this.listboxCapture.ItemHeight = 15;
            this.listboxCapture.Location = new System.Drawing.Point(837, 102);
            this.listboxCapture.Name = "listboxCapture";
            this.listboxCapture.Size = new System.Drawing.Size(334, 439);
            this.listboxCapture.TabIndex = 3;
            // 
            // sttStrip
            // 
            this.sttStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbProcessing2CText,
            this.lb2CCount,
            this.lbSeparator1,
            this.lbProcessing1CText,
            this.lb1CCount,
            this.saparator2,
            this.lbCurrentCheck});
            this.sttStrip.Location = new System.Drawing.Point(0, 569);
            this.sttStrip.Name = "sttStrip";
            this.sttStrip.Size = new System.Drawing.Size(1192, 22);
            this.sttStrip.TabIndex = 4;
            this.sttStrip.Text = "statusStrip1";
            // 
            // lbProcessing2CText
            // 
            this.lbProcessing2CText.Name = "lbProcessing2CText";
            this.lbProcessing2CText.Size = new System.Drawing.Size(87, 17);
            this.lbProcessing2CText.Text = "Đã tiêm 2 mũi: ";
            // 
            // lb2CCount
            // 
            this.lb2CCount.Name = "lb2CCount";
            this.lb2CCount.Size = new System.Drawing.Size(13, 17);
            this.lb2CCount.Text = "0";
            // 
            // lbSeparator1
            // 
            this.lbSeparator1.Name = "lbSeparator1";
            this.lbSeparator1.Size = new System.Drawing.Size(16, 17);
            this.lbSeparator1.Text = " | ";
            // 
            // lbProcessing1CText
            // 
            this.lbProcessing1CText.Name = "lbProcessing1CText";
            this.lbProcessing1CText.Size = new System.Drawing.Size(87, 17);
            this.lbProcessing1CText.Text = "Đã tiêm 1 mũi: ";
            // 
            // lb1CCount
            // 
            this.lb1CCount.Name = "lb1CCount";
            this.lb1CCount.Size = new System.Drawing.Size(13, 17);
            this.lb1CCount.Text = "0";
            // 
            // saparator2
            // 
            this.saparator2.Name = "saparator2";
            this.saparator2.Size = new System.Drawing.Size(16, 17);
            this.saparator2.Text = " | ";
            // 
            // lbCurrentCheck
            // 
            this.lbCurrentCheck.Name = "lbCurrentCheck";
            this.lbCurrentCheck.Size = new System.Drawing.Size(0, 17);
            // 
            // btnRefreshCamList
            // 
            this.btnRefreshCamList.Location = new System.Drawing.Point(592, 32);
            this.btnRefreshCamList.Name = "btnRefreshCamList";
            this.btnRefreshCamList.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshCamList.TabIndex = 5;
            this.btnRefreshCamList.Text = "Refresh";
            this.btnRefreshCamList.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 591);
            this.Controls.Add(this.btnRefreshCamList);
            this.Controls.Add(this.sttStrip);
            this.Controls.Add(this.listboxCapture);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cbbCam);
            this.Controls.Add(this.pbCam);
            this.Name = "MainForm";
            this.Text = "Green Gate";
            ((System.ComponentModel.ISupportInitialize)(this.pbCam)).EndInit();
            this.sttStrip.ResumeLayout(false);
            this.sttStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCam;
        private System.Windows.Forms.ComboBox cbbCam;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox listboxCapture;
        private System.Windows.Forms.StatusStrip sttStrip;
        private System.Windows.Forms.ToolStripStatusLabel lbProcessing2CText;
        private System.Windows.Forms.ToolStripStatusLabel lb2CCount;
        private System.Windows.Forms.ToolStripStatusLabel lbProcessing1CText;
        private System.Windows.Forms.ToolStripStatusLabel lb1CCount;
        private System.Windows.Forms.ToolStripStatusLabel lbSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel saparator2;
        private System.Windows.Forms.ToolStripStatusLabel lbCurrentCheck;
        private System.Windows.Forms.Button btnRefreshCamList;
    }
}

