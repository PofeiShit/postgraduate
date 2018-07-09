namespace ImageZ
{
    partial class BoxFilterDialog
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
            this.Radius = new System.Windows.Forms.TrackBar();
            this.RadiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ChkPreview = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Radius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Radius);
            this.groupBox1.Controls.Add(this.RadiusUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // Radius
            // 
            this.Radius.AutoSize = false;
            this.Radius.LargeChange = 1;
            this.Radius.Location = new System.Drawing.Point(15, 58);
            this.Radius.Maximum = 32;
            this.Radius.Minimum = 1;
            this.Radius.Name = "Radius";
            this.Radius.Size = new System.Drawing.Size(275, 45);
            this.Radius.TabIndex = 4;
            this.Radius.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Radius.Value = 3;
            this.Radius.Scroll += new System.EventHandler(this.Radius_Scroll);
            // 
            // RadiusUpDown
            // 
            this.RadiusUpDown.Location = new System.Drawing.Point(219, 20);
            this.RadiusUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.RadiusUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RadiusUpDown.Name = "RadiusUpDown";
            this.RadiusUpDown.Size = new System.Drawing.Size(72, 21);
            this.RadiusUpDown.TabIndex = 2;
            this.RadiusUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.RadiusUpDown.ValueChanged += new System.EventHandler(this.RadiusUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "半径：";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(350, 32);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(350, 70);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChkPreview
            // 
            this.ChkPreview.AutoSize = true;
            this.ChkPreview.Checked = true;
            this.ChkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkPreview.Location = new System.Drawing.Point(353, 122);
            this.ChkPreview.Name = "ChkPreview";
            this.ChkPreview.Size = new System.Drawing.Size(48, 16);
            this.ChkPreview.TabIndex = 3;
            this.ChkPreview.Text = "预览";
            this.ChkPreview.UseVisualStyleBackColor = true;
            this.ChkPreview.CheckedChanged += new System.EventHandler(this.ChkPreview_CheckedChanged);
            // 
            // BoxFilterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 150);
            this.Controls.Add(this.ChkPreview);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BoxFilterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BoxFilter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BoxFilterDialog_FormClosing);
            this.Load += new System.EventHandler(this.BoxFilterDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Radius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar Radius;
        private System.Windows.Forms.NumericUpDown RadiusUpDown;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox ChkPreview;
    }
}