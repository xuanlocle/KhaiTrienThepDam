namespace GiaoDienDam.Openfile
{
    partial class OpenFileControl
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
            this.lboxResult = new System.Windows.Forms.ListBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbFolderFrom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tbFolderTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbOverride = new System.Windows.Forms.CheckBox();
            this.rtbFileNames = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lboxResult
            // 
            this.lboxResult.FormattingEnabled = true;
            this.lboxResult.Location = new System.Drawing.Point(11, 291);
            this.lboxResult.Name = "lboxResult";
            this.lboxResult.Size = new System.Drawing.Size(373, 147);
            this.lboxResult.TabIndex = 8;
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(83, 19);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(262, 20);
            this.tbServer.TabIndex = 1;
            this.tbServer.Text = "10.200.203.12";
            // 
            // tbFolderFrom
            // 
            this.tbFolderFrom.Location = new System.Drawing.Point(83, 45);
            this.tbFolderFrom.Name = "tbFolderFrom";
            this.tbFolderFrom.Size = new System.Drawing.Size(262, 20);
            this.tbFolderFrom.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Folder (from)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Filename";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.tbFolderTo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbOverride);
            this.groupBox1.Controls.Add(this.rtbFileNames);
            this.groupBox1.Controls.Add(this.tbServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbFolderFrom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 244);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Initialize";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(348, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(18, 22);
            this.button2.TabIndex = 3;
            this.button2.Text = "V";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbFolderTo
            // 
            this.tbFolderTo.Location = new System.Drawing.Point(83, 72);
            this.tbFolderTo.Name = "tbFolderTo";
            this.tbFolderTo.Size = new System.Drawing.Size(262, 20);
            this.tbFolderTo.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Folder (to)";
            // 
            // cbOverride
            // 
            this.cbOverride.AutoSize = true;
            this.cbOverride.Checked = true;
            this.cbOverride.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOverride.Location = new System.Drawing.Point(23, 214);
            this.cbOverride.Name = "cbOverride";
            this.cbOverride.Size = new System.Drawing.Size(109, 17);
            this.cbOverride.TabIndex = 6;
            this.cbOverride.Text = "Override if exists?";
            this.cbOverride.UseVisualStyleBackColor = true;
            // 
            // rtbFileNames
            // 
            this.rtbFileNames.Location = new System.Drawing.Point(83, 122);
            this.rtbFileNames.Name = "rtbFileNames";
            this.rtbFileNames.Size = new System.Drawing.Size(262, 86);
            this.rtbFileNames.TabIndex = 5;
            this.rtbFileNames.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(144, 262);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Copy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OpenFileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lboxResult);
            this.Name = "OpenFileControl";
            this.Text = "Copy File From Revit Server";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lboxResult;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.TextBox tbFolderFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbFileNames;
        private System.Windows.Forms.CheckBox cbOverride;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbFolderTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
    }
}