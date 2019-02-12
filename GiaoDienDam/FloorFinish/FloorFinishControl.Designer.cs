namespace GiaoDienDam.FloorFinish
{
    partial class FloorFinishControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selected_rooms_radio = new System.Windows.Forms.RadioButton();
            this.all_rooms_radio = new System.Windows.Forms.RadioButton();
            this.listBoxFloorType = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.paramSelector = new System.Windows.Forms.ComboBox();
            this.Height_TextBox = new System.Windows.Forms.TextBox();
            this.height_param_radio = new System.Windows.Forms.RadioButton();
            this.floor_height_radio = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selected_rooms_radio);
            this.groupBox1.Controls.Add(this.all_rooms_radio);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // selected_rooms_radio
            // 
            this.selected_rooms_radio.AutoSize = true;
            this.selected_rooms_radio.Location = new System.Drawing.Point(22, 44);
            this.selected_rooms_radio.Name = "selected_rooms_radio";
            this.selected_rooms_radio.Size = new System.Drawing.Size(120, 17);
            this.selected_rooms_radio.TabIndex = 1;
            this.selected_rooms_radio.TabStop = true;
            this.selected_rooms_radio.Text = "Only selected rooms";
            this.selected_rooms_radio.UseVisualStyleBackColor = true;
            // 
            // all_rooms_radio
            // 
            this.all_rooms_radio.AutoSize = true;
            this.all_rooms_radio.Checked = true;
            this.all_rooms_radio.Location = new System.Drawing.Point(22, 20);
            this.all_rooms_radio.Name = "all_rooms_radio";
            this.all_rooms_radio.Size = new System.Drawing.Size(139, 17);
            this.all_rooms_radio.TabIndex = 0;
            this.all_rooms_radio.TabStop = true;
            this.all_rooms_radio.Text = "All rooms in current view";
            this.all_rooms_radio.UseVisualStyleBackColor = true;
            // 
            // listBoxFloorType
            // 
            this.listBoxFloorType.FormattingEnabled = true;
            this.listBoxFloorType.Location = new System.Drawing.Point(6, 19);
            this.listBoxFloorType.Name = "listBoxFloorType";
            this.listBoxFloorType.Size = new System.Drawing.Size(323, 316);
            this.listBoxFloorType.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxFloorType);
            this.groupBox2.Location = new System.Drawing.Point(12, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(335, 350);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select a floor type";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.paramSelector);
            this.groupBox3.Controls.Add(this.Height_TextBox);
            this.groupBox3.Controls.Add(this.height_param_radio);
            this.groupBox3.Controls.Add(this.floor_height_radio);
            this.groupBox3.Location = new System.Drawing.Point(12, 462);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(335, 78);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Floor height";
            // 
            // paramSelector
            // 
            this.paramSelector.Enabled = false;
            this.paramSelector.FormattingEnabled = true;
            this.paramSelector.Location = new System.Drawing.Point(113, 46);
            this.paramSelector.Name = "paramSelector";
            this.paramSelector.Size = new System.Drawing.Size(216, 21);
            this.paramSelector.TabIndex = 3;
            // 
            // Height_TextBox
            // 
            this.Height_TextBox.Location = new System.Drawing.Point(113, 19);
            this.Height_TextBox.Name = "Height_TextBox";
            this.Height_TextBox.Size = new System.Drawing.Size(216, 20);
            this.Height_TextBox.TabIndex = 2;
            this.Height_TextBox.LostFocus += new System.EventHandler(this.Height_TextBox_LostFocus);
            // 
            // height_param_radio
            // 
            this.height_param_radio.AutoSize = true;
            this.height_param_radio.Location = new System.Drawing.Point(22, 46);
            this.height_param_radio.Name = "height_param_radio";
            this.height_param_radio.Size = new System.Drawing.Size(85, 17);
            this.height_param_radio.TabIndex = 1;
            this.height_param_radio.Text = "radioButton4";
            this.height_param_radio.UseVisualStyleBackColor = true;
            this.height_param_radio.CheckedChanged += new System.EventHandler(this.height_param_radio_CheckedChanged);
            // 
            // floor_height_radio
            // 
            this.floor_height_radio.AutoSize = true;
            this.floor_height_radio.Checked = true;
            this.floor_height_radio.Location = new System.Drawing.Point(22, 22);
            this.floor_height_radio.Name = "floor_height_radio";
            this.floor_height_radio.Size = new System.Drawing.Size(85, 17);
            this.floor_height_radio.TabIndex = 0;
            this.floor_height_radio.TabStop = true;
            this.floor_height_radio.Text = "radioButton3";
            this.floor_height_radio.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 566);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(272, 566);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FloorFinishControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 592);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FloorFinishControl";
            this.Text = "FloorFinishControl";
            this.Load += new System.EventHandler(this.FloorFinishControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton selected_rooms_radio;
        private System.Windows.Forms.RadioButton all_rooms_radio;
        private System.Windows.Forms.ListBox listBoxFloorType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox paramSelector;
        private System.Windows.Forms.TextBox Height_TextBox;
        private System.Windows.Forms.RadioButton height_param_radio;
        private System.Windows.Forms.RadioButton floor_height_radio;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}