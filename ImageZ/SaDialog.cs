using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace ImageZ
{
    public unsafe partial class SaDialog : Form
    {
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        public static extern unsafe void CopyMemory(void* Dest, void* Src, int Length);
        public SaDialog()
        {
            InitializeComponent();
        }
        private int WIDTHBYTES(int bytes)
        {
            return (((bytes * 8) + 31) / 32 * 4);
        }
        private byte ClampToByte(int Value)
        {
            return (byte)((Value | ((int)(255 - Value) >> 31)) & ~((int)Value >> 31));
        }
        private Canvas canvas = null;
        private byte* Clone = null;
        private ToolStripStatusLabel TimeUse;
        private int Stride;
        private const float coeffR = 0.299f;
        private const float coeffG = 0.587f;
        private const float coeffB = 0.114f;
        public SaDialog(Canvas Parent, ToolStripStatusLabel Label, string Caption)
        {
            InitializeComponent();
            this.canvas = Parent;
            this.Text = Caption;
            this.TimeUse = Label;
            this.Stride = WIDTHBYTES(canvas.Image.Width * canvas.Channel);
            Clone = (byte*)Marshal.AllocHGlobal(canvas.Image.Height * Stride);
            CopyMemory(Clone, Parent.ImageData, canvas.Image.Height * Stride);
        }

        private void spectralA()
        {
            for (int row = 0; row < canvas.Image.Height; ++row)
            {
                int pos = row * Stride;
                for (int col = 0; col < canvas.Image.Width; ++col)
                {
                    int i = pos + canvas.Channel * col;
                    byte B = Clone[i], G = Clone[i + 1], R = Clone[i + 2];
                    canvas.ImageData[i + 2] = ClampToByte((int)((B * coeffB + G * coeffG + R * coeffR) * (float)GrayLamdaUpDown.Value));
                    canvas.ImageData[i + 1] = ClampToByte((int)((B * OrangeBUpDown.Value + G * OrangeGUpDown.Value + R * OrangeRUpDown.Value) * OrangeLamdaUpDown.Value));
                    canvas.ImageData[i + 0] = ClampToByte((int)((B * BlueBUpDown.Value + G * BlueGUpDown.Value + R * BlueRUpDown.Value) * BlueLamdaUpDown.Value));
                }
            }
        }
        private void UpdataCanvas()
        {
            if (ChkPreview.Checked == true)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                spectralA();
                sw.Stop();
                TimeUse.Text = "计算用时:" + sw.ElapsedMilliseconds.ToString() + "ms";

            }
            else
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
            Application.DoEvents();
            canvas.Refresh();
            Application.DoEvents();
        }
        private void SaDialog_Load(object sender, EventArgs e)
        {
            UpdataCanvas();
        }

        private void ChkPreview_CheckedChanged(object sender, EventArgs e)
        {
            UpdataCanvas();
        }

        private void SaDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
                canvas.Refresh();
            }
            if (Clone != null)
                Marshal.FreeHGlobal((IntPtr)Clone);
        }
        #region 控制条
        private void GrayLamdaUpDown_ValueChanged(object sender, EventArgs e)
        {
            GrayLamda.Value = (int)(GrayLamdaUpDown.Value * 10);
            UpdataCanvas();
        }

        private void GrayLamda_Scroll(object sender, EventArgs e)
        {
            GrayLamdaUpDown.Value = (decimal)((float)GrayLamda.Value / 10);
        }

        private void OrangeLamda_Scroll(object sender, EventArgs e)
        {
            OrangeLamdaUpDown.Value = (decimal)((float)OrangeLamda.Value / 10);
        }

        private void OrangeLamdaUpDown1_ValueChanged(object sender, EventArgs e)
        {
            OrangeLamda.Value = (int)(OrangeLamdaUpDown.Value * 10);
            UpdataCanvas();
        }

        private void OrangeRUpDown1_ValueChanged(object sender, EventArgs e)
        {
            OrangeR.Value = (int)(OrangeRUpDown.Value * 1000);
            UpdataCanvas();
        }

        private void OrangeR_Scroll(object sender, EventArgs e)
        {
            OrangeRUpDown.Value = (decimal)((float)OrangeR.Value / 1000);
        }

        private void OrangeGUpDown_ValueChanged(object sender, EventArgs e)
        {
            OrangeG.Value = (int)(OrangeGUpDown.Value * 1000);
            UpdataCanvas();
        }

        private void OrangeG_Scroll(object sender, EventArgs e)
        {
            OrangeGUpDown.Value = (decimal)((float)OrangeG.Value / 1000);
        }

        private void OrangeBUpDown_ValueChanged(object sender, EventArgs e)
        {
            OrangeB.Value = (int)(OrangeBUpDown.Value * 1000);
            UpdataCanvas();
        }

        private void OrangeB_Scroll(object sender, EventArgs e)
        {
            OrangeBUpDown.Value = (decimal)((float)OrangeB.Value / 1000);
        }

        private void BlueLamdaUpDown_ValueChanged(object sender, EventArgs e)
        {
            BlueLamda.Value = (int)(BlueLamdaUpDown.Value * 10);
            UpdataCanvas();
        }

        private void BlueLamda_Scroll(object sender, EventArgs e)
        {
            BlueLamdaUpDown.Value = (decimal)((float)BlueLamda.Value / 10);

        }

        private void BlueRUpDown_ValueChanged(object sender, EventArgs e)
        {
            BlueR.Value = (int)(BlueRUpDown.Value * 1000);
            UpdataCanvas();
        }

        private void BlueR_Scroll(object sender, EventArgs e)
        {
            BlueRUpDown.Value = (decimal)((float)BlueR.Value / 1000);

        }

        private void BlueGUpDown_ValueChanged(object sender, EventArgs e)
        {
            BlueG.Value = (int)(BlueGUpDown.Value * 1000);
            UpdataCanvas();
        }

        private void BlueG_Scroll(object sender, EventArgs e)
        {
            BlueGUpDown.Value = (decimal)((float)BlueG.Value / 1000);
        }

        private void BlueBUpDown_ValueChanged(object sender, EventArgs e)
        {
            BlueB.Value = (int)(BlueBUpDown.Value * 1000);
            UpdataCanvas();
        }

        private void BlueB_Scroll(object sender, EventArgs e)
        {
            BlueBUpDown.Value = (decimal)((float)BlueB.Value / 1000);
        }
        #endregion
    }
}
