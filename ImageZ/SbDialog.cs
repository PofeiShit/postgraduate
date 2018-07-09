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
    public unsafe partial class SbDialog : Form
    {
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        public static extern unsafe void CopyMemory(void* Dest, void* Src, int Length);


        public SbDialog()
        {
            InitializeComponent();
        }
        public SbDialog(Canvas Parent, ToolStripStatusLabel Label, string Caption)
        {
            InitializeComponent();
            this.canvas = Parent;
            this.Text = Caption;
            this.TimeUse = Label;
            this.Stride = WIDTHBYTES(canvas.Image.Width * canvas.Channel);
            Clone = (byte*)Marshal.AllocHGlobal(canvas.Image.Height * Stride);
            CopyMemory(Clone, Parent.ImageData, canvas.Image.Height * Stride);
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
        

        private void spectralB()
        {
            for (int row = 0; row < canvas.Image.Height; ++row)
            {
                int pos = row * Stride;
                for (int col = 0; col < canvas.Image.Width; ++col)
                {
                    int i = pos + canvas.Channel * col;
                    float B = Clone[i] / 255.0f, G = Clone[i + 1] / 255.0f, R = Clone[i + 2] / 255.0f;
                    float x1 = B, x2 = B * B, x3 = x2 * B;
                    float y1 = G, y2 = G * G, y3 = y2 * G;
                    canvas.ImageData[i + 2] = ClampToByte((int)(255 * (R * (float)RedLamdaUpDown.Value)));
                    canvas.ImageData[i + 1] = ClampToByte((int)(255 * (y3 * (float)Green3UpDown.Value
                        + y2 * (float)Green2UpDown.Value
                        + y1 * (float)Green1UpDown.Value + (float)GreenCUpDown.Value)));
                    canvas.ImageData[i + 0] = ClampToByte((int)(255 * (x3 * (float)Blue3UpDown.Value
                        + x2 * (float)Blue2UpDown.Value
                        + x1 * (float)Blue1UpDown.Value + (float)BlueCUpDown.Value)));
                }
            }
        }
        private void UpdateCanvas()
        {
            if (ChkPreview.Checked == true)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                spectralB();
                sw.Stop();
                TimeUse.Text = sw.ElapsedMilliseconds.ToString();
            }
            else
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
            Application.DoEvents();
            canvas.Refresh();
            Application.DoEvents();
        }
        private void SbDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void ChkPreview_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void SbDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
                canvas.Refresh();
            }
            if (Clone != null)
                Marshal.FreeHGlobal((IntPtr)Clone);
        }

        #region 滑动条
        private void RedLamdaUpDown_ValueChanged(object sender, EventArgs e)
        {
            RedLamda.Value = (int)(RedLamdaUpDown.Value * 100);
            UpdateCanvas();
        }

        private void RedLamda_Scroll(object sender, EventArgs e)
        {
            RedLamdaUpDown.Value = (decimal)((float)RedLamda.Value / 100);
        }

        private void GreenRUpDown_ValueChanged(object sender, EventArgs e)
        {
            Green3.Value = (int)(Green3UpDown.Value * 100);
            UpdateCanvas();
        }

        private void GreenR_Scroll(object sender, EventArgs e)
        {
            Green3UpDown.Value = (decimal)((float)Green3.Value / 100);
        }

        private void GreenGUpDown_ValueChanged(object sender, EventArgs e)
        {
            Green2.Value = (int)(Green2UpDown.Value * 100);
            UpdateCanvas();
        }

        private void GreenG_Scroll(object sender, EventArgs e)
        {
            Green2UpDown.Value = (decimal)((float)Green2.Value / 100);
        }

        private void GreenBUpDown_ValueChanged(object sender, EventArgs e)
        {
            Green1.Value = (int)(Green1UpDown.Value * 100);
            UpdateCanvas();
        }

        private void GreenB_Scroll(object sender, EventArgs e)
        {
            Green1UpDown.Value = (decimal)((float)Green1.Value / 100);
        }

        private void GreenCUpDown_ValueChanged(object sender, EventArgs e)
        {
            GreenC.Value = (int)(GreenCUpDown.Value * 100);
            UpdateCanvas();
        }

        private void GreenC_Scroll(object sender, EventArgs e)
        {
            GreenCUpDown.Value = (decimal)((float)GreenC.Value / 100);
        }

        private void BlueRUpDown_ValueChanged(object sender, EventArgs e)
        {
            Blue3.Value = (int)(Blue3UpDown.Value * 100);
            UpdateCanvas();
        }

        private void BlueR_Scroll(object sender, EventArgs e)
        {
            Blue3UpDown.Value = (decimal)((float)Blue3.Value / 100);
        }

        private void BlueGUpDown_ValueChanged(object sender, EventArgs e)
        {
            Blue2.Value = (int)(Blue2UpDown.Value * 100);
            UpdateCanvas();
        }

        private void BlueG_Scroll(object sender, EventArgs e)
        {
            Blue2UpDown.Value = (decimal)((float)Blue2.Value / 100);
        }

        private void BlueBUpDown_ValueChanged(object sender, EventArgs e)
        {
            Blue1.Value = (int)(Blue1UpDown.Value * 100);
            UpdateCanvas();
        }

        private void BlueB_Scroll(object sender, EventArgs e)
        {
            Blue1UpDown.Value = (decimal)((float)Blue1.Value / 100);
        }

        private void BlueCUpDown1_ValueChanged(object sender, EventArgs e)
        {
            BlueC.Value = (int)(BlueCUpDown.Value * 100);
            UpdateCanvas();
        }

        private void BlueC_Scroll(object sender, EventArgs e)
        {
            BlueCUpDown.Value = (decimal)((float)BlueC.Value / 100);
        }
        #endregion
    }
}
