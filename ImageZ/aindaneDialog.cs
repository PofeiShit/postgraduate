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
    public unsafe partial class aindaneDialog : Form
    {
        public aindaneDialog()
        {
            InitializeComponent();
        }
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        public static extern unsafe void CopyMemory(void* Dest, void* Src, int Length);
        [DllImport("illumianceDetailDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern void illumianceDetail(byte* Src, byte* Dest, int Width, int Heihgt, int Channel, int Radius, float Eps);

        private int WIDTHBYTES(int bytes)
        {
            return (((bytes * 8) + 31) / 32 * 4);
        }
        private Canvas canvas = null;
        private byte* Clone = null;
        private ToolStripStatusLabel TimeUse;
        private int Stride;

        public aindaneDialog(Canvas Parent, ToolStripStatusLabel Label, string Caption)
        {
            InitializeComponent();
            this.canvas = Parent;
            this.Text = Caption;
            this.TimeUse = Label;
            this.Stride = WIDTHBYTES(canvas.Image.Width * canvas.Channel);
            Clone = (byte*)Marshal.AllocHGlobal(canvas.Image.Height * Stride);
            CopyMemory(Clone, Parent.ImageData, canvas.Image.Height * Stride);
        }

        private void UpdateCanvas()
        {
            if (ChkPreview.Checked == true)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                // image process algorithm
                illumianceDetail(Clone, canvas.ImageData, canvas.Image.Width, canvas.Image.Height, canvas.Channel, Radius.Value, (float)EpsUpDown1.Value);
                //if (canvas.Channel == 4)
                //{
                //    for (int i = 3; i < canvas.Height * Stride; i += 4)
                //        canvas.ImageData[i] = 255;
                //}
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



        private void Radius_Scroll(object sender, EventArgs e)
        {
            RadiusUpDown.Value = Radius.Value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
            canvas.Refresh();
        }

        private void aindaneDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void aindaneDialog_FormClosing(object sender, FormClosingEventArgs e)
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

        private void RadiusUpDown_ValueChanged(object sender, EventArgs e)
        {
            Radius.Value = (int)RadiusUpDown.Value;
            UpdateCanvas();
        }

        private void EpsUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Eps.Value = (int)(EpsUpDown1.Value * 1000);
            UpdateCanvas();
        }

        private void ChkPreview_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void Eps_Scroll(object sender, EventArgs e)
        {
            EpsUpDown1.Value = (decimal)((float)(Eps.Value) / 1000);
        }
    }
}
