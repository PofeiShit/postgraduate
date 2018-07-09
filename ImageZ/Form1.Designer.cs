namespace ImageZ
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Container = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ZoomFactor = new System.Windows.Forms.ToolStripDropDownButton();
            this.zoomContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuZoom300 = new System.Windows.Forms.ToolStripMenuItem();
            this.FilePathStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimeUse = new System.Windows.Forms.ToolStripStatusLabel();
            this.SizeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mouseToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ScrollV = new System.Windows.Forms.VScrollBar();
            this.ScrollH = new System.Windows.Forms.HScrollBar();
            this.MainCanvas = new ImageZ.Canvas();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像滤波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.方框滤波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导向滤波ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.图像增强ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.细节增强ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反锐化掩模ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导向边缘保持ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.双边边缘保持ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.递归边缘保持ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.对比度增强ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectralAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectralBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.亮度增强ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.claraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像转换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.灰度化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aINDANEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Container.BottomToolStripPanel.SuspendLayout();
            this.Container.ContentPanel.SuspendLayout();
            this.Container.TopToolStripPanel.SuspendLayout();
            this.Container.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.zoomContextMenuStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Container
            // 
            // 
            // Container.BottomToolStripPanel
            // 
            this.Container.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // Container.ContentPanel
            // 
            this.Container.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Container.ContentPanel.Controls.Add(this.ScrollV);
            this.Container.ContentPanel.Controls.Add(this.ScrollH);
            this.Container.ContentPanel.Controls.Add(this.MainCanvas);
            this.Container.ContentPanel.Size = new System.Drawing.Size(695, 330);
            this.Container.ContentPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MainCanvas_MouseWheel);
            this.Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Container.Location = new System.Drawing.Point(0, 0);
            this.Container.Name = "Container";
            this.Container.Size = new System.Drawing.Size(695, 381);
            this.Container.TabIndex = 0;
            this.Container.Text = "toolStripContainer1";
            // 
            // Container.TopToolStripPanel
            // 
            this.Container.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomFactor,
            this.FilePathStatus,
            this.TimeUse,
            this.SizeToolStripStatusLabel,
            this.mouseToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(695, 26);
            this.statusStrip1.TabIndex = 0;
            // 
            // ZoomFactor
            // 
            this.ZoomFactor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ZoomFactor.AutoSize = false;
            this.ZoomFactor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ZoomFactor.DropDown = this.zoomContextMenuStrip;
            this.ZoomFactor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomFactor.Name = "ZoomFactor";
            this.ZoomFactor.Size = new System.Drawing.Size(53, 24);
            this.ZoomFactor.Text = "100%";
            // 
            // zoomContextMenuStrip
            // 
            this.zoomContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuZoom50,
            this.MenuZoom100,
            this.MenuZoom200,
            this.MenuZoom300});
            this.zoomContextMenuStrip.Name = "zoomContextMenuStrip1";
            this.zoomContextMenuStrip.OwnerItem = this.ZoomFactor;
            this.zoomContextMenuStrip.Size = new System.Drawing.Size(109, 92);
            // 
            // MenuZoom50
            // 
            this.MenuZoom50.Name = "MenuZoom50";
            this.MenuZoom50.Size = new System.Drawing.Size(108, 22);
            this.MenuZoom50.Text = "50%";
            this.MenuZoom50.Click += new System.EventHandler(this.MenuZoom50_Click);
            // 
            // MenuZoom100
            // 
            this.MenuZoom100.Checked = true;
            this.MenuZoom100.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuZoom100.Name = "MenuZoom100";
            this.MenuZoom100.Size = new System.Drawing.Size(108, 22);
            this.MenuZoom100.Text = "100%";
            this.MenuZoom100.Click += new System.EventHandler(this.MenuZoom100_Click);
            // 
            // MenuZoom200
            // 
            this.MenuZoom200.Name = "MenuZoom200";
            this.MenuZoom200.Size = new System.Drawing.Size(108, 22);
            this.MenuZoom200.Text = "200%";
            this.MenuZoom200.Click += new System.EventHandler(this.MenuZoom200_Click);
            // 
            // MenuZoom300
            // 
            this.MenuZoom300.Name = "MenuZoom300";
            this.MenuZoom300.Size = new System.Drawing.Size(108, 22);
            this.MenuZoom300.Text = "300%";
            this.MenuZoom300.Click += new System.EventHandler(this.MenuZoom300_Click);
            // 
            // FilePathStatus
            // 
            this.FilePathStatus.AutoSize = false;
            this.FilePathStatus.Name = "FilePathStatus";
            this.FilePathStatus.Size = new System.Drawing.Size(400, 21);
            this.FilePathStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TimeUse
            // 
            this.TimeUse.AutoSize = false;
            this.TimeUse.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.TimeUse.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.TimeUse.Name = "TimeUse";
            this.TimeUse.Size = new System.Drawing.Size(102, 21);
            this.TimeUse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SizeToolStripStatusLabel
            // 
            this.SizeToolStripStatusLabel.AutoSize = false;
            this.SizeToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.SizeToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.SizeToolStripStatusLabel.Name = "SizeToolStripStatusLabel";
            this.SizeToolStripStatusLabel.Size = new System.Drawing.Size(74, 21);
            this.SizeToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mouseToolStripStatusLabel
            // 
            this.mouseToolStripStatusLabel.AutoSize = false;
            this.mouseToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.mouseToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.mouseToolStripStatusLabel.Name = "mouseToolStripStatusLabel";
            this.mouseToolStripStatusLabel.Size = new System.Drawing.Size(254, 21);
            this.mouseToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ScrollV
            // 
            this.ScrollV.Dock = System.Windows.Forms.DockStyle.Right;
            this.ScrollV.Location = new System.Drawing.Point(677, 0);
            this.ScrollV.Name = "ScrollV";
            this.ScrollV.Size = new System.Drawing.Size(16, 312);
            this.ScrollV.TabIndex = 2;
            this.ScrollV.Visible = false;
            this.ScrollV.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollV_Scroll);
            // 
            // ScrollH
            // 
            this.ScrollH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ScrollH.Location = new System.Drawing.Point(0, 312);
            this.ScrollH.Name = "ScrollH";
            this.ScrollH.Size = new System.Drawing.Size(693, 16);
            this.ScrollH.TabIndex = 1;
            this.ScrollH.Visible = false;
            this.ScrollH.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollH_Scroll);
            // 
            // MainCanvas
            // 
            this.MainCanvas.BackColor = System.Drawing.SystemColors.Control;
            this.MainCanvas.Channel = 3;
            this.MainCanvas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainCanvas.Image = null;
            this.MainCanvas.ImageFile = "";
            this.MainCanvas.Location = new System.Drawing.Point(171, 14);
            this.MainCanvas.Name = "MainCanvas";
            this.MainCanvas.Size = new System.Drawing.Size(277, 213);
            this.MainCanvas.TabIndex = 0;
            this.MainCanvas.Zoom = 1D;
            this.MainCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainCanvas_MouseMove);
            this.MainCanvas.Resize += new System.EventHandler(this.MainCanvas_Resize);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.图像滤波ToolStripMenuItem,
            this.图像增强ToolStripMenuItem,
            this.图像转换ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(695, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 图像滤波ToolStripMenuItem
            // 
            this.图像滤波ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.方框滤波ToolStripMenuItem,
            this.导向滤波ToolStripMenuItem1});
            this.图像滤波ToolStripMenuItem.Name = "图像滤波ToolStripMenuItem";
            this.图像滤波ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图像滤波ToolStripMenuItem.Text = "图像滤波";
            // 
            // 方框滤波ToolStripMenuItem
            // 
            this.方框滤波ToolStripMenuItem.Name = "方框滤波ToolStripMenuItem";
            this.方框滤波ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.方框滤波ToolStripMenuItem.Text = "方框滤波";
            this.方框滤波ToolStripMenuItem.Click += new System.EventHandler(this.方框滤波ToolStripMenuItem_Click);
            // 
            // 导向滤波ToolStripMenuItem1
            // 
            this.导向滤波ToolStripMenuItem1.Name = "导向滤波ToolStripMenuItem1";
            this.导向滤波ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.导向滤波ToolStripMenuItem1.Text = "导向滤波";
            this.导向滤波ToolStripMenuItem1.Click += new System.EventHandler(this.导向滤波ToolStripMenuItem1_Click);
            // 
            // 图像增强ToolStripMenuItem
            // 
            this.图像增强ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.细节增强ToolStripMenuItem,
            this.对比度增强ToolStripMenuItem,
            this.亮度增强ToolStripMenuItem});
            this.图像增强ToolStripMenuItem.Name = "图像增强ToolStripMenuItem";
            this.图像增强ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图像增强ToolStripMenuItem.Text = "图像增强";
            // 
            // 细节增强ToolStripMenuItem
            // 
            this.细节增强ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.反锐化掩模ToolStripMenuItem});
            this.细节增强ToolStripMenuItem.Name = "细节增强ToolStripMenuItem";
            this.细节增强ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.细节增强ToolStripMenuItem.Text = "细节增强";
            // 
            // 反锐化掩模ToolStripMenuItem
            // 
            this.反锐化掩模ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导向边缘保持ToolStripMenuItem,
            this.双边边缘保持ToolStripMenuItem,
            this.递归边缘保持ToolStripMenuItem});
            this.反锐化掩模ToolStripMenuItem.Name = "反锐化掩模ToolStripMenuItem";
            this.反锐化掩模ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.反锐化掩模ToolStripMenuItem.Text = "反锐化掩模";
            // 
            // 导向边缘保持ToolStripMenuItem
            // 
            this.导向边缘保持ToolStripMenuItem.Name = "导向边缘保持ToolStripMenuItem";
            this.导向边缘保持ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.导向边缘保持ToolStripMenuItem.Text = "导向边缘保持";
            this.导向边缘保持ToolStripMenuItem.Click += new System.EventHandler(this.导向边缘保持ToolStripMenuItem_Click);
            // 
            // 双边边缘保持ToolStripMenuItem
            // 
            this.双边边缘保持ToolStripMenuItem.Name = "双边边缘保持ToolStripMenuItem";
            this.双边边缘保持ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.双边边缘保持ToolStripMenuItem.Text = "双边边缘保持";
            // 
            // 递归边缘保持ToolStripMenuItem
            // 
            this.递归边缘保持ToolStripMenuItem.Name = "递归边缘保持ToolStripMenuItem";
            this.递归边缘保持ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.递归边缘保持ToolStripMenuItem.Text = "递归边缘保持";
            // 
            // 对比度增强ToolStripMenuItem
            // 
            this.对比度增强ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spectralAToolStripMenuItem,
            this.spectralBToolStripMenuItem});
            this.对比度增强ToolStripMenuItem.Name = "对比度增强ToolStripMenuItem";
            this.对比度增强ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.对比度增强ToolStripMenuItem.Text = "对比度增强";
            // 
            // spectralAToolStripMenuItem
            // 
            this.spectralAToolStripMenuItem.Name = "spectralAToolStripMenuItem";
            this.spectralAToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.spectralAToolStripMenuItem.Text = "spectral A";
            this.spectralAToolStripMenuItem.Click += new System.EventHandler(this.spectralAToolStripMenuItem_Click);
            // 
            // spectralBToolStripMenuItem
            // 
            this.spectralBToolStripMenuItem.Name = "spectralBToolStripMenuItem";
            this.spectralBToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.spectralBToolStripMenuItem.Text = "spectral B";
            this.spectralBToolStripMenuItem.Click += new System.EventHandler(this.spectralBToolStripMenuItem_Click);
            // 
            // 亮度增强ToolStripMenuItem
            // 
            this.亮度增强ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.claraToolStripMenuItem,
            this.aINDANEToolStripMenuItem});
            this.亮度增强ToolStripMenuItem.Name = "亮度增强ToolStripMenuItem";
            this.亮度增强ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.亮度增强ToolStripMenuItem.Text = "亮度增强";
            // 
            // claraToolStripMenuItem
            // 
            this.claraToolStripMenuItem.Name = "claraToolStripMenuItem";
            this.claraToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.claraToolStripMenuItem.Text = "clara";
            this.claraToolStripMenuItem.Click += new System.EventHandler(this.claraToolStripMenuItem_Click);
            // 
            // 图像转换ToolStripMenuItem
            // 
            this.图像转换ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.灰度化ToolStripMenuItem});
            this.图像转换ToolStripMenuItem.Name = "图像转换ToolStripMenuItem";
            this.图像转换ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图像转换ToolStripMenuItem.Text = "图像转换";
            // 
            // 灰度化ToolStripMenuItem
            // 
            this.灰度化ToolStripMenuItem.Name = "灰度化ToolStripMenuItem";
            this.灰度化ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.灰度化ToolStripMenuItem.Text = "灰度化";
            this.灰度化ToolStripMenuItem.Click += new System.EventHandler(this.灰度化ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // aINDANEToolStripMenuItem
            // 
            this.aINDANEToolStripMenuItem.Name = "aINDANEToolStripMenuItem";
            this.aINDANEToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aINDANEToolStripMenuItem.Text = "AINDANE";
            this.aINDANEToolStripMenuItem.Click += new System.EventHandler(this.aINDANEToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 381);
            this.Controls.Add(this.Container);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ImageZ";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.Container.BottomToolStripPanel.ResumeLayout(false);
            this.Container.BottomToolStripPanel.PerformLayout();
            this.Container.ContentPanel.ResumeLayout(false);
            this.Container.TopToolStripPanel.ResumeLayout(false);
            this.Container.TopToolStripPanel.PerformLayout();
            this.Container.ResumeLayout(false);
            this.Container.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.zoomContextMenuStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer Container;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private Canvas MainCanvas;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.VScrollBar ScrollV;
        private System.Windows.Forms.HScrollBar ScrollH;
        private System.Windows.Forms.ToolStripStatusLabel FilePathStatus;
        private System.Windows.Forms.ToolStripStatusLabel TimeUse;
        private System.Windows.Forms.ToolStripStatusLabel SizeToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel mouseToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem 图像滤波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 方框滤波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton ZoomFactor;
        private System.Windows.Forms.ToolStripMenuItem 图像增强ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 细节增强ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反锐化掩模ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 对比度增强ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导向边缘保持ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 双边边缘保持ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 递归边缘保持ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectralAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectralBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导向滤波ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 亮度增强ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem claraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip zoomContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuZoom50;
        private System.Windows.Forms.ToolStripMenuItem MenuZoom100;
        private System.Windows.Forms.ToolStripMenuItem MenuZoom200;
        private System.Windows.Forms.ToolStripMenuItem MenuZoom300;
        private System.Windows.Forms.ToolStripMenuItem 图像转换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 灰度化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aINDANEToolStripMenuItem;
    }
}

