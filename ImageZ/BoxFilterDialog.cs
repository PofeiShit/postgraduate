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
    public unsafe partial class BoxFilterDialog : Form
    {
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        public static extern unsafe void CopyMemory(void* Dest, void* Src, int Length);
        [DllImport("boxblurCpp.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern void BoxBlurSSE(byte* Src, byte* Dest, int Width, int Heihgt, int Radius, int Channel);

        public BoxFilterDialog()
        {
            InitializeComponent();
        }
        private int WIDTHBYTES(int bytes)
        {
            return (((bytes * 8) + 31) / 32 * 4);
        }
        private Canvas canvas = null;
        private byte* Clone = null;
        private ToolStripStatusLabel TimeUse;
        private int Stride;
        public BoxFilterDialog(Canvas Parent, ToolStripStatusLabel Label, string Caption)
        {
            InitializeComponent();
            this.canvas = Parent;
            this.Text = Caption;
            this.TimeUse = Label;
            this.Stride = WIDTHBYTES(canvas.Image.Width * canvas.Channel);
            Clone = (byte*)Marshal.AllocHGlobal(canvas.Image.Height * Stride);
            CopyMemory(Clone, Parent.ImageData, canvas.Image.Height * Stride);
        }
        // 8bit 24bit 32bit 宽度是否为4字节倍数
        private void UpdateCanvas()
        {
            if (ChkPreview.Checked == true)
            {
                Stopwatch Sw = new Stopwatch();
                Sw.Start();
                //image process algorithm
                BoxBlurSSE(Clone, canvas.ImageData, canvas.Image.Width, canvas.Image.Height, Radius.Value, canvas.Channel);
                if (canvas.Channel == 4)
                {
                    for (int i = 3; i < canvas.Height * Stride; i += 4)
                        canvas.ImageData[i] = 255;
                }
                Sw.Stop();
                TimeUse.Text = "计算用时" + Sw.ElapsedMilliseconds.ToString() + "ms";
            }
            else
            {
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
            }
            Application.DoEvents();
            canvas.Refresh();
            Application.DoEvents();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
            canvas.Refresh();
        }

        private void BoxFilterDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void BoxFilterDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
                canvas.Refresh();
            }
            if (Clone != null)
                Marshal.FreeHGlobal((IntPtr)Clone);
        }

        private void RadiusUpDown_ValueChanged(object sender, EventArgs e)
        {
            Radius.Value = (int)RadiusUpDown.Value;
            UpdateCanvas();
        }

        private void Radius_Scroll(object sender, EventArgs e)
        {
            RadiusUpDown.Value = Radius.Value;
        }

        private void ChkPreview_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }
    }
}
