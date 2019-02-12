namespace GiaoDienDam
{
    partial class FormSelectTenCauKien
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
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            this.clbListElementHaveParam = new System.Windows.Forms.CheckedListBox();
            this.btnOpenCommand = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtParameterToFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.clbListParam = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblLoai = new System.Windows.Forms.Label();
            this.txtS = new System.Windows.Forms.TextBox();
            this.btnS = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbListElementHaveParam
            // 
            this.clbListElementHaveParam.CheckOnClick = true;
            this.clbListElementHaveParam.FormattingEnabled = true;
            this.clbListElementHaveParam.Location = new System.Drawing.Point(253, 140);
            this.clbListElementHaveParam.Name = "clbListElementHaveParam";
            this.clbListElementHaveParam.ScrollAlwaysVisible = true;
            this.clbListElementHaveParam.Size = new System.Drawing.Size(207, 214);
            this.clbListElementHaveParam.TabIndex = 0;
            // 
            // btnOpenCommand
            // 
            this.btnOpenCommand.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnOpenCommand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.btnOpenCommand.Location = new System.Drawing.Point(37, 433);
            this.btnOpenCommand.Name = "btnOpenCommand";
            this.btnOpenCommand.Size = new System.Drawing.Size(204, 32);
            this.btnOpenCommand.TabIndex = 2;
            this.btnOpenCommand.Text = "Khai báo thép GEM";
            this.btnOpenCommand.UseVisualStyleBackColor = false;
            this.btnOpenCommand.Click += new System.EventHandler(this.btnOpenCommand_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tên cấu kiện";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Danh sách cấu kiện:";
            // 
            // txtParameterToFilter
            // 
            this.txtParameterToFilter.Location = new System.Drawing.Point(17, 41);
            this.txtParameterToFilter.Name = "txtParameterToFilter";
            this.txtParameterToFilter.Size = new System.Drawing.Size(125, 20);
            this.txtParameterToFilter.TabIndex = 5;
            this.txtParameterToFilter.Text = "TEN CAU KIEN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tên Parameter để lọc";
            // 
            // btnFilter
            // 
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFilter.Location = new System.Drawing.Point(151, 41);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 20);
            this.btnFilter.TabIndex = 7;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // clbListParam
            // 
            this.clbListParam.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.clbListParam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.clbListParam.FullRowSelect = true;
            listViewGroup2.Header = "ListViewGroup";
            listViewGroup2.Name = "listViewGroup1";
            this.clbListParam.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
            this.clbListParam.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.clbListParam.Location = new System.Drawing.Point(21, 137);
            this.clbListParam.Name = "clbListParam";
            this.clbListParam.Size = new System.Drawing.Size(205, 217);
            this.clbListParam.TabIndex = 8;
            this.clbListParam.UseCompatibleStateImageBehavior = false;
            this.clbListParam.View = System.Windows.Forms.View.Details;
            this.clbListParam.SelectedIndexChanged += new System.EventHandler(this.clbListParam_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblLoai.ForeColor = System.Drawing.Color.Red;
            this.lblLoai.Location = new System.Drawing.Point(363, 106);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(46, 18);
            this.lblLoai.TabIndex = 9;
            this.lblLoai.Text = "label4";
            // 
            // txtS
            // 
            this.txtS.Location = new System.Drawing.Point(105, 111);
            this.txtS.Name = "txtS";
            this.txtS.Size = new System.Drawing.Size(59, 20);
            this.txtS.TabIndex = 10;
            this.txtS.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnS
            // 
            this.btnS.Enabled = false;
            this.btnS.Location = new System.Drawing.Point(170, 109);
            this.btnS.Name = "btnS";
            this.btnS.Size = new System.Drawing.Size(56, 23);
            this.btnS.TabIndex = 11;
            this.btnS.Text = "Search";
            this.btnS.UseVisualStyleBackColor = true;
            this.btnS.Click += new System.EventHandler(this.btnS_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.button1.Location = new System.Drawing.Point(254, 433);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 32);
            this.button1.TabIndex = 12;
            this.button1.Text = "Khai báo thép METRO";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormSelectTenCauKien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 477);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnS);
            this.Controls.Add(this.txtS);
            this.Controls.Add(this.lblLoai);
            this.Controls.Add(this.clbListParam);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtParameterToFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenCommand);
            this.Controls.Add(this.clbListElementHaveParam);
            this.Name = "FormSelectTenCauKien";
            this.Text = "FormSelectTenCauKien";
            this.Load += new System.EventHandler(this.FormSelectTenCauKien_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbListElementHaveParam;
        private System.Windows.Forms.Button btnOpenCommand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtParameterToFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ListView clbListParam;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.TextBox txtS;
        private System.Windows.Forms.Button btnS;
        private System.Windows.Forms.Button button1;
    }
}