namespace ImageZ
{
    partial class GuideDetailEnhanceDialog
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
            this.ChkPreview = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Sample = new System.Windows.Forms.TrackBar();
            this.SampleUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.Lamda = new System.Windows.Forms.TrackBar();
            this.LamdaUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.EpsSample = new System.Windows.Forms.TrackBar();
            this.EpsUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Radius = new System.Windows.Forms.TrackBar();
            this.RadiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lamda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LamdaUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpsSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpsUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Radius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ChkPreview
            // 
            this.ChkPreview.AutoSize = true;
            this.ChkPreview.Checked = true;
            this.ChkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkPreview.Location = new System.Drawing.Point(369, 122);
            this.ChkPreview.Name = "ChkPreview";
            this.ChkPreview.Size = new System.Drawing.Size(48, 16);
            this.ChkPreview.TabIndex = 10;
            this.ChkPreview.Text = "预览";
            this.ChkPreview.UseVisualStyleBackColor = true;
            this.ChkPreview.CheckedChanged += new System.EventHandler(this.ChkPreview_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(366, 70);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(366, 32);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Sample);
            this.groupBox1.Controls.Add(this.SampleUpDown);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Lamda);
            this.groupBox1.Controls.Add(this.LamdaUpDown1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.EpsSample);
            this.groupBox1.Controls.Add(this.EpsUpDown1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Radius);
            this.groupBox1.Controls.Add(this.RadiusUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(17, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 248);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // Sample
            // 
            this.Sample.AutoSize = false;
            this.Sample.LargeChange = 1;
            this.Sample.Location = new System.Drawing.Point(15, 215);
            this.Sample.Minimum = 1;
            this.Sample.Name = "Sample";
            this.Sample.Size = new System.Drawing.Size(275, 25);
            this.Sample.TabIndex = 14;
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
            this.SampleUpDown.Location = new System.Drawing.Point(236, 187);
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
            this.SampleUpDown.TabIndex = 13;
            this.SampleUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SampleUpDown.ValueChanged += new System.EventHandler(this.SampleUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "采样：";
            // 
            // Lamda
            // 
            this.Lamda.AutoSize = false;
            this.Lamda.Location = new System.Drawing.Point(15, 164);
            this.Lamda.Maximum = 50;
            this.Lamda.Minimum = 10;
            this.Lamda.Name = "Lamda";
            this.Lamda.Size = new System.Drawing.Size(275, 25);
            this.Lamda.TabIndex = 11;
            this.Lamda.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Lamda.Value = 20;
            this.Lamda.Scroll += new System.EventHandler(this.Lamda_Scroll);
            // 
            // LamdaUpDown1
            // 
            this.LamdaUpDown1.DecimalPlaces = 1;
            this.LamdaUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.LamdaUpDown1.Location = new System.Drawing.Point(236, 136);
            this.LamdaUpDown1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.LamdaUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LamdaUpDown1.Name = "LamdaUpDown1";
            this.LamdaUpDown1.Size = new System.Drawing.Size(55, 21);
            this.LamdaUpDown1.TabIndex = 10;
            this.LamdaUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.LamdaUpDown1.ValueChanged += new System.EventHandler(this.LamdaUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Lamda：";
            // 
            // EpsSample
            // 
            this.EpsSample.AutoSize = false;
            this.EpsSample.LargeChange = 1;
            this.EpsSample.Location = new System.Drawing.Point(15, 109);
            this.EpsSample.Maximum = 1000;
            this.EpsSample.Minimum = 1;
            this.EpsSample.Name = "EpsSample";
            this.EpsSample.Size = new System.Drawing.Size(275, 25);
            this.EpsSample.TabIndex = 8;
            this.EpsSample.TickStyle = System.Windows.Forms.TickStyle.None;
            this.EpsSample.Value = 1;
            this.EpsSample.Scroll += new System.EventHandler(this.SubSample_Scroll);
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
            this.label3.Location = new System.Drawing.Point(24, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Eps：";
            // 
            // Radius
            // 
            this.Radius.AutoSize = false;
            this.Radius.LargeChange = 1;
            this.Radius.Location = new System.Drawing.Point(15, 47);
            this.Radius.Maximum = 32;
            this.Radius.Minimum = 1;
            this.Radius.Name = "Radius";
            this.Radius.Size = new System.Drawing.Size(275, 31);
            this.Radius.TabIndex = 4;
            this.Radius.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Radius.Value = 13;
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
            13,
            0,
            0,
            0});
            this.RadiusUpDown.ValueChanged += new System.EventHandler(this.RadiusUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "半径：";
            // 
            // GuideDetailEnhanceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 269);
            this.Controls.Add(this.ChkPreview);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Name = "GuideDetailEnhanceDialog";
            this.Text = "guideDetailEnhance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.guideDetailEnhanceDialog_FormClosing);
            this.Load += new System.EventHandler(this.guideDetailEnhanceDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lamda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LamdaUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpsSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpsUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Radius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkPreview;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar Lamda;
        private System.Windows.Forms.NumericUpDown LamdaUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar EpsSample;
        private System.Windows.Forms.NumericUpDown EpsUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar Radius;
        private System.Windows.Forms.NumericUpDown RadiusUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar Sample;
        private System.Windows.Forms.NumericUpDown SampleUpDown;
        private System.Windows.Forms.Label label4;
    }
}