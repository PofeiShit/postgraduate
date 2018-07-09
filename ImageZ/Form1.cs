using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace ImageZ
{
    public unsafe partial class Form1 : Form
    {

        private int ImgStride;
        private int WIDTHBYTES(int bytes)
        {
            return (((bytes * 8) + 31) / 32 * 4);
        }
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.statusStrip1.Hide();
        }

        #region 图像滚动
        private void MainCanvas_Resize(object sender, EventArgs e)
        {
            if (MainCanvas.Visible == false) return;

            //获取画布宽高
            int Width = MainCanvas.Width;
            int Height = MainCanvas.Height;
            //获取容器宽高
            int W = Container.ContentPanel.Width;
            int H = Container.ContentPanel.Height;

            //判断画布宽度是否超出容器宽度
            if (Width > W)
            {
                ScrollH.Visible = true;

                ScrollH.Minimum = 0;
                ScrollH.Maximum = Width;
                ScrollH.Value = (ScrollH.Maximum + ScrollH.Minimum) / 2;
            } 
            else 
            {
                ScrollH.Visible = false;
            }   
            //判断画布高度是否超出容器高度
            if (Height > H)
            {
                ScrollV.Visible = true;

                ScrollV.Minimum = 0;
                ScrollV.Maximum = Height;
                ScrollV.Value = (ScrollV.Maximum + ScrollV.Minimum) / 2;
            }
            else
            {
                ScrollV.Visible = false;
            }
            Application.DoEvents();
            MainCanvas.Left = (W - Width) / 2;
            MainCanvas.Top = (H - Height) / 2;

            //在状态栏显示图像尺寸
            SizeToolStripStatusLabel.Text = MainCanvas.Image.Width.ToString() + "x" + MainCanvas.Image.Height.ToString();
        }

        private void MainCanvas_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //当鼠标滑轮每滚动一次，滚动条滚动的距离
            int MinScrollSize = (ScrollV.Maximum - ScrollV.Minimum) / 10;
            if (ScrollV.Visible == true)
            {
                if (e.Delta < 0)
                {
                    //向下滚动
                    if (ScrollV.Value + MinScrollSize > ScrollV.Maximum)
                        ScrollV.Value = ScrollV.Maximum;
                    else
                        ScrollV.Value += MinScrollSize;
                }
                else
                {
                    //向上滚动
                    if (ScrollV.Value - MinScrollSize < ScrollV.Minimum)
                        ScrollV.Value = ScrollV.Minimum;
                    else
                        ScrollV.Value -= MinScrollSize;
                }
                MainCanvas.Top = (Container.ContentPanel.Height - MainCanvas.Height) * ScrollV.Value / MainCanvas.Height;
            }
        }

        private void ScrollV_Scroll(object sender, ScrollEventArgs e)
        {
           MainCanvas.Top = (Container.ContentPanel.Height - MainCanvas.Height) * ScrollV.Value / MainCanvas.Height;
        }

        private void ScrollH_Scroll(object sender, ScrollEventArgs e)
        {
            MainCanvas.Left = (Container.ContentPanel.Width - MainCanvas.Width) * ScrollH.Value / MainCanvas.Width;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            MainCanvas_Resize(sender, e);
        }

        int GetPosInImage(int Pos)
        {
            return (int)(Pos / MainCanvas.Zoom + 0.5);
        }
        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (MainCanvas.Image != null)
            {
                int PosX = GetPosInImage(e.X);
                int PosY = GetPosInImage(e.Y);
                if ((PosX >= 0) && (PosX < MainCanvas.Image.Width) && (PosY >= 0) && (PosY < MainCanvas.Image.Height))
                {
                    byte* Pt = MainCanvas.ImageData + PosY * ImgStride + PosY * MainCanvas.Channel;
                    if (MainCanvas.Channel == 1)
                    {
                        this.mouseToolStripStatusLabel.Text = "X:" + PosX.ToString() + ", Y:" + PosY.ToString() + " R = " + Pt[0].ToString() + ", G = " + Pt[0].ToString() + ", B = " + Pt[0].ToString();
                    }
                    else
                    {
                        this.mouseToolStripStatusLabel.Text = "X:" + PosX.ToString() + ", Y:" + PosY.ToString() + " R = " + Pt[2].ToString() + ", G = " + Pt[1].ToString() + ", B = " + Pt[0].ToString();
                    }
                }
            }
        }

        #endregion

        #region 菜单
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "All files(*.*) | *.*|Bitmap files(*.Bitmap) |*.Bmp|Jpeg files (*.jpeg)|*.jpg|Png files (*.png)|*.png" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (MainCanvas.Visible == false) MainCanvas.Visible = true;
                MainCanvas.ImageFile = openFileDialog.FileName;
                FilePathStatus.Text = openFileDialog.FileName;
                ImgStride = WIDTHBYTES(MainCanvas.Image.Width * MainCanvas.Channel);
                Application.DoEvents();
                MainCanvas_Resize(sender, e);
                this.MaximizeBox = true;
                this.MinimizeBox = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.statusStrip1.Show();
            }
        }

        private void 方框滤波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainCanvas.Image == null) return;
            BoxFilterDialog Dlg = new BoxFilterDialog(MainCanvas, TimeUse, "方框滤波");
            Dlg.ShowDialog();
        }

        private void 导向滤波ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MainCanvas.Image == null) return;
            GuidedFilterDialog Dlg = new GuidedFilterDialog(MainCanvas, TimeUse, "导向滤波");
            Dlg.ShowDialog();
        }

        private void 导向边缘保持ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainCanvas.Image == null) return;
            GuideDetailEnhanceDialog Dlg = new GuideDetailEnhanceDialog(MainCanvas, TimeUse, "边缘保持增强");
            Dlg.ShowDialog();
        }


        private void spectralAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainCanvas.Image == null) return;
            SaDialog Dlg = new SaDialog(MainCanvas, TimeUse, "Spectral A");
            Dlg.ShowDialog();
        }

        private void spectralBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainCanvas.Image == null) return;
            SbDialog Dlg = new SbDialog(MainCanvas, TimeUse, "Spectral B");
            Dlg.ShowDialog();
        }


        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainCanvas.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp";
                saveFileDialog.FilterIndex = 3;
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FilterIndex == 1)
                        MainCanvas.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                    else if (saveFileDialog.FilterIndex == 2)
                        MainCanvas.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                    else if (saveFileDialog.FilterIndex == 3)
                        MainCanvas.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                }
            }
        }


        private void 灰度化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (MainCanvas.Image == null) return;
            if (MainCanvas.Channel == 1)
            {
                MessageBox.Show("请输入彩色图像");
                return;
            }
            GrayDialog Dlg = new GrayDialog(MainCanvas, TimeUse, "灰度化");
            Dlg.ShowDialog();

        }
        private void claraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainCanvas.Image == null) return;
            if (MainCanvas.Channel == 1)
            {
                MessageBox.Show("不支持灰度图");
                return;
            }
            claraChroma Dlg = new claraChroma(MainCanvas, TimeUse, "Clara");
            Dlg.ShowDialog();
        }
        private void aINDANEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainCanvas.Image == null) return;
            if (MainCanvas.Channel == 1)
            {
                MessageBox.Show("不支持灰度图");
                return;
            }
            aindaneDialog Dlg = new aindaneDialog(MainCanvas, TimeUse, "AINDANE");
            Dlg.ShowDialog();
        }
        #region "缩放菜单"
        private void ZoomCanvas(int iZoom)
        {
            //清除掉右键菜单中所有勾选项
            this.MenuZoom50.Checked = false;
            this.MenuZoom100.Checked = false;
            this.MenuZoom200.Checked = false;
            this.MenuZoom300.Checked = false;
            //勾选用户指定的缩放比率
            // 并设置画布缩放率
            switch (iZoom)
            {
                case 50:
                    this.MenuZoom50.Checked = true;
                    this.MainCanvas.Zoom = 0.5;
                    this.ZoomFactor.Text = "50%";
                    break;
                case 100:
                    this.MenuZoom100.Checked = true;
                    this.MainCanvas.Zoom = 1.0;
                    this.ZoomFactor.Text = "100%";
                    break;
                case 200:
                    this.MenuZoom200.Checked = true;
                    this.MainCanvas.Zoom = 2.0;
                    this.ZoomFactor.Text = "200%";
                    break;
                case 300:
                    this.MenuZoom300.Checked = true;
                    this.MainCanvas.Zoom = 3.0;
                    this.ZoomFactor.Text = "300%";
                    break;
            }
        }

        #endregion

        private void MenuZoom50_Click(object sender, EventArgs e)
        {
            ZoomCanvas(50);
            Application.DoEvents();
            MainCanvas_Resize(sender, e);
        }

        private void MenuZoom100_Click(object sender, EventArgs e)
        {
            ZoomCanvas(100);
            Application.DoEvents();
            MainCanvas_Resize(sender, e);
        }

        private void MenuZoom200_Click(object sender, EventArgs e)
        {
            ZoomCanvas(200);
            Application.DoEvents();
            MainCanvas_Resize(sender, e);
        }

        private void MenuZoom300_Click(object sender, EventArgs e)
        {
            ZoomCanvas(300);
            Application.DoEvents();
            MainCanvas_Resize(sender, e);

        }
        #endregion




    }
}
