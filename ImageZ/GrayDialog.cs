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
    public unsafe partial class GrayDialog : Form
    {
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        public static extern unsafe void CopyMemory(void* Dest, void* Src, int Length);

        private int WIDTHBYTES(int bytes)
        {
            return (((bytes * 8) + 31) / 32 * 4);
        }
        private Canvas canvas = null;
        private byte* Clone = null;
        private ToolStripStatusLabel TimeUse;
        private int Stride;

        const float YCbCrYRF = 0.299F;              // RGB转YCbCr的系数(浮点类型）
        const float YCbCrYGF = 0.587F;
        const float YCbCrYBF = 0.114F;
        const float YCbCrCbRF = -0.168736F;
        const float YCbCrCbGF = -0.331264F;
        const float YCbCrCbBF = 0.500000F;
        const float YCbCrCrRF = 0.500000F;
        const float YCbCrCrGF = -0.418688F;
        const float YCbCrCrBF = -0.081312F;
        const int Shift = 20;
        const int HalfShiftValue = 1 << (Shift - 1);

        const int YCbCrYRI = (int)(YCbCrYRF * (1 << Shift) + 0.5);         // RGB转YCbCr的系数(整数类型）
        const int YCbCrYGI = (int)(YCbCrYGF * (1 << Shift) + 0.5);
        const int YCbCrYBI = (int)(YCbCrYBF * (1 << Shift) + 0.5);
        const int YCbCrCbRI = (int)(YCbCrCbRF * (1 << Shift) + 0.5);
        const int YCbCrCbGI = (int)(YCbCrCbGF * (1 << Shift) + 0.5);
        const int YCbCrCbBI = (int)(YCbCrCbBF * (1 << Shift) + 0.5);
        const int YCbCrCrRI = (int)(YCbCrCrRF * (1 << Shift) + 0.5);
        const int YCbCrCrGI = (int)(YCbCrCrGF * (1 << Shift) + 0.5);
        const int YCbCrCrBI = (int)(YCbCrCrBF * (1 << Shift) + 0.5);
        public GrayDialog()
        {
            InitializeComponent();
        }


        public GrayDialog(Canvas Parent, ToolStripStatusLabel Label, string Caption)
        {
            InitializeComponent();
            this.canvas = Parent;
            this.Text = Caption;
            this.TimeUse = Label;
            this.Stride = WIDTHBYTES(canvas.Image.Width * canvas.Channel);
            Clone = (byte*)Marshal.AllocHGlobal(canvas.Image.Height * Stride);
            CopyMemory(Clone, Parent.ImageData, canvas.Image.Height * Stride);
        }

        public void rgb2Y(byte* Src, byte* Dest, int Width, int Height, int Channel)
        {
            int Red, Blue, Green;
            for (int i = 0; i < Height; ++i)
            {
                int Pos = i * Stride;
                for (int j = 0; j < Width; ++j)
                {
                    Red   = Src[Pos + Channel * j + 2];
                    Green = Src[Pos + Channel * j + 1];
                    Blue  = Src[Pos + Channel * j + 0];
                    byte b = (byte)((YCbCrYRI * Red + YCbCrYGI * Green + YCbCrYBI * Blue + HalfShiftValue) >> Shift);;
                    Dest[Pos + Channel * j + 0] = b;
                    Dest[Pos + Channel * j + 1] = b;
                    Dest[Pos + Channel * j + 2] = b;
                }
            }
        }
        private void UpdateCanvas()
        {
            if (ChkPreview.Checked == true)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                // image process algorithm       
                rgb2Y(Clone, canvas.ImageData, canvas.Width, canvas.Height, canvas.Channel);
                if (canvas.Channel == 4)
                {
                    for (int i = 3; i < canvas.Height * Stride; i += 4)
                        canvas.ImageData[i] = 255;
                }
                sw.Stop();
                TimeUse.Text = "计算用时:" + sw.ElapsedMilliseconds.ToString() + "ms";
            }
            else
            {
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
            }
            Application.DoEvents();
            canvas.Refresh();
            Application.DoEvents();
        }

        private void GrayDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void GrayDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
                canvas.Refresh();
            }
            if (Clone != null)
            {
                Marshal.FreeHGlobal((IntPtr)Clone);
            }
        }

        private void ChkPreview_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

    }
}
