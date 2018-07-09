namespace ImageZ
{
    partial class claraChroma
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
            this.C = new System.Windows.Forms.TrackBar();
            this.CUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.SetPoint = new System.Windows.Forms.TrackBar();
            this.SetPointUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Eps = new System.Windows.Forms.TrackBar();
            this.EpsUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Radius = new System.Windows.Forms.TrackBar();
            this.RadiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SetPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SetPointUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Eps)).BeginInit();
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
            this.ChkPreview.Location = new System.Drawing.Point(388, 117);
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
            this.btnCancel.Location = new System.Drawing.Point(385, 65);
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
            this.btnOk.Location = new System.Drawing.Point(385, 27);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.C);
            this.groupBox1.Controls.Add(this.CUpDown);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.SetPoint);
            this.groupBox1.Controls.Add(this.SetPointUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Eps);
            this.groupBox1.Controls.Add(this.EpsUpDown1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Radius);
            this.groupBox1.Controls.Add(this.RadiusUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 271);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // C
            // 
            this.C.AutoSize = false;
            this.C.Location = new System.Drawing.Point(15, 215);
            this.C.Maximum = 100;
            this.C.Name = "C";
            this.C.Size = new System.Drawing.Size(275, 31);
            this.C.SmallChange = 5;
            this.C.TabIndex = 14;
            this.C.TickStyle = System.Windows.Forms.TickStyle.None;
            this.C.Value = 50;
            this.C.Scroll += new System.EventHandler(this.C_Scroll);
            // 
            // CUpDown
            // 
            this.CUpDown.DecimalPlaces = 2;
            this.CUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.CUpDown.Location = new System.Drawing.Point(236, 194);
            this.CUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CUpDown.Name = "CUpDown";
            this.CUpDown.Size = new System.Drawing.Size(55, 21);
            this.CUpDown.TabIndex = 13;
            this.CUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.CUpDown.ValueChanged += new System.EventHandler(this.CUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "C：";
            // 
            // SetPoint
            // 
            this.SetPoint.AutoSize = false;
            this.SetPoint.LargeChange = 1;
            this.SetPoint.Location = new System.Drawing.Point(15, 161);
            this.SetPoint.Maximum = 255;
            this.SetPoint.Name = "SetPoint";
            this.SetPoint.Size = new System.Drawing.Size(275, 31);
            this.SetPoint.TabIndex = 11;
            this.SetPoint.TickStyle = System.Windows.Forms.TickStyle.None;
            this.SetPoint.Value = 130;
            this.SetPoint.Scroll += new System.EventHandler(this.SetPoint_Scroll);
            // 
            // SetPointUpDown
            // 
            this.SetPointUpDown.Location = new System.Drawing.Point(236, 140);
            this.SetPointUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.SetPointUpDown.Name = "SetPointUpDown";
            this.SetPointUpDown.Size = new System.Drawing.Size(55, 21);
            this.SetPointUpDown.TabIndex = 10;
            this.SetPointUpDown.Value = new decimal(new int[] {
            130,
            0,
            0,
            0});
            this.SetPointUpDown.ValueChanged += new System.EventHandler(this.SetPointUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "设置亮度：";
            // 
            // Eps
            // 
            this.Eps.AutoSize = false;
            this.Eps.LargeChange = 1;
            this.Eps.Location = new System.Drawing.Point(15, 109);
            this.Eps.Maximum = 1000;
            this.Eps.Minimum = 1;
            this.Eps.Name = "Eps";
            this.Eps.Size = new System.Drawing.Size(275, 25);
            this.Eps.TabIndex = 8;
            this.Eps.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Eps.Value = 120;
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
            12,
            0,
            0,
            131072});
            this.EpsUpDown1.ValueChanged += new System.EventHandler(this.EpsUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 81);
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
            this.Radius.Value = 12;
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
            12,
            0,
            0,
            0});
            this.RadiusUpDown.ValueChanged += new System.EventHandler(this.RadiusUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "半径：";
            // 
            // claraChroma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 295);
            this.Controls.Add(this.ChkPreview);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Name = "claraChroma";
            this.Text = "claraChroma";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.claraChroma_FormClosing);
            this.Load += new System.EventHandler(this.claraChroma_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SetPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SetPointUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Eps)).EndInit();
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
        private System.Windows.Forms.TrackBar C;
        private System.Windows.Forms.NumericUpDown CUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar SetPoint;
        private System.Windows.Forms.NumericUpDown SetPointUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar Eps;
        private System.Windows.Forms.NumericUpDown EpsUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar Radius;
        private System.Windows.Forms.NumericUpDown RadiusUpDown;
        private System.Windows.Forms.Label label1;
    }
}