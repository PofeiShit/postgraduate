namespace ImageZ
{
    partial class GuidedFilterDialog
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
            this.Sample = new System.Windows.Forms.TrackBar();
            this.SampleUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Eps = new System.Windows.Forms.TrackBar();
            this.EpsUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Radius = new System.Windows.Forms.TrackBar();
            this.RadiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkPreview = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Eps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpsUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Radius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Sample);
            this.groupBox1.Controls.Add(this.SampleUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Eps);
            this.groupBox1.Controls.Add(this.EpsUpDown1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Radius);
            this.groupBox1.Controls.Add(this.RadiusUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(25, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 207);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // Sample
            // 
            this.Sample.AutoSize = false;
            this.Sample.LargeChange = 1;
            this.Sample.Location = new System.Drawing.Point(24, 172);
            this.Sample.Minimum = 1;
            this.Sample.Name = "Sample";
            this.Sample.Size = new System.Drawing.Size(275, 25);
            this.Sample.TabIndex = 11;
            this.Sample.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Sample.Value = 10;
            this.Sample.Scroll += new System.EventHandler(this.Sample_Scroll);
            // 
            // SampleUpDown
            // 
            this.SampleUpDown.DecimalPlaces = 1;
            this.SampleUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.SampleUpDown.Location = new System.Drawing.Point(236, 144);
            this.SampleUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SampleUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.SampleUpDown.Name = "SampleUpDown";
            this.SampleUpDown.Size = new System.Drawing.Size(55, 21);
            this.SampleUpDown.TabIndex = 10;
            this.SampleUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SampleUpDown.ValueChanged += new System.EventHandler(this.SampleUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "采样：";
            // 
            // Eps
            // 
            this.Eps.AutoSize = false;
            this.Eps.LargeChange = 1;
            this.Eps.Location = new System.Drawing.Point(24, 109);
            this.Eps.Maximum = 1000;
            this.Eps.Minimum = 1;
            this.Eps.Name = "Eps";
            this.Eps.Size = new System.Drawing.Size(275, 25);
            this.Eps.TabIndex = 8;
            this.Eps.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Eps.Value = 10;
            this.Eps.Scroll += new System.EventHandler(this.Eps_Scroll);
            // 
            // EpsUpDown1
            // 
            this.EpsUpDown1.DecimalPlaces = 3;
            this.EpsUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.EpsUpDown1.Location = new System.Drawing.Point(236, 81);
            this.EpsUpDown1.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EpsUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.EpsUpDown1.Name = "EpsUpDown1";
            this.EpsUpDown1.Size = new System.Drawing.Size(55, 21);
            this.EpsUpDown1.TabIndex = 7;
            this.EpsUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.EpsUpDown1.ValueChanged += new System.EventHandler(this.EpsUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Eps：";
            // 
            // Radius
            // 
            this.Radius.AutoSize = false;
            this.Radius.LargeChange = 1;
            this.Radius.Location = new System.Drawing.Point(24, 47);
            this.Radius.Maximum = 32;
            this.Radius.Minimum = 1;
            this.Radius.Name = "Radius";
            this.Radius.Size = new System.Drawing.Size(275, 31);
            this.Radius.TabIndex = 4;
            this.Radius.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Radius.Value = 3;
            this.Radius.Scroll += new System.EventHandler(this.Radius_Scroll);
            // 
            // RadiusUpDown
            // 
            this.RadiusUpDown.Location = new System.Drawing.Point(236, 26);
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
            this.RadiusUpDown.Size = new System.Drawing.Size(55, 21);
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
            this.label1.Location = new System.Drawing.Point(32, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "半径：";
            // 
            // ChkPreview
            // 
            this.ChkPreview.AutoSize = true;
            this.ChkPreview.Checked = true;
            this.ChkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkPreview.Location = new System.Drawing.Point(377, 119);
            this.ChkPreview.Name = "ChkPreview";
            this.ChkPreview.Size = new System.Drawing.Size(48, 16);
            this.ChkPreview.TabIndex = 6;
            this.ChkPreview.Text = "预览";
            this.ChkPreview.UseVisualStyleBackColor = true;
            this.ChkPreview.CheckedChanged += new System.EventHandler(this.ChkPreview_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(374, 67);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(374, 29);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // GuidedFilterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 230);
            this.Controls.Add(this.ChkPreview);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Name = "GuidedFilterDialog";
            this.Text = "GuidedFilterDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GuidedFilterDialog_FormClosing);
            this.Load += new System.EventHandler(this.GuidedFilterDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Eps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpsUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Radius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar Radius;
        private System.Windows.Forms.NumericUpDown RadiusUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkPreview;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.NumericUpDown EpsUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar Eps;
        private System.Windows.Forms.TrackBar Sample;
        private System.Windows.Forms.NumericUpDown SampleUpDown;
        private System.Windows.Forms.Label label2;
    }
}