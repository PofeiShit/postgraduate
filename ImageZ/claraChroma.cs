using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace ImageZ
{
    public unsafe partial class claraChroma : Form
    {
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        public static extern unsafe void CopyMemory(void* Dest, void* Src, int Length);
        [DllImport("claraChromaDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern void clara(byte* Src, byte* Dest, int Width, int Heihgt, float Eps, int Radius, int Channel, int SetPoint, float C);

        public claraChroma()
        {
            InitializeComponent();
        }
        public int WIDTHBYTES(int bytes)
        {
            return (((bytes * 8) + 31) / 32 * 4);
        }
        private Canvas canvas = null;
        private byte* Clone = null;
        private ToolStripLabel TimeUse;
        private int Stride;
        public claraChroma(Canvas Parent, ToolStripLabel Label, string caption)
        {
            InitializeComponent();
            this.canvas = Parent;
            this.Text = caption;
            this.TimeUse = Label;
            this.Stride = WIDTHBYTES(canvas.Image.Width * canvas.Channel);
            Clone = (byte*)Marshal.AllocHGlobal(canvas.Image.Height * Stride);
            CopyMemory(Clone, canvas.ImageData, canvas.Image.Height * Stride);
        }
        private void UpdateCanvas()
        {
            if (ChkPreview.Checked == true)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                clara(Clone, canvas.ImageData, canvas.Image.Width, canvas.Image.Height, (float)EpsUpDown1.Value, (int)RadiusUpDown.Value, canvas.Channel, SetPoint.Value, (float)CUpDown.Value);
                if (canvas.Channel == 4)
                {
                    for (int i = 3; i < canvas.Height * Stride; i += 4)
                        canvas.ImageData[i] = 255;
                }
                sw.Stop();
                TimeUse.Text = "计算用时" + sw.ElapsedMilliseconds.ToString() + "ms";
            }
            else
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
            Application.DoEvents();
            canvas.Refresh();
            Application.DoEvents();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
            canvas.Refresh();
        }

        private void claraChroma_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
                canvas.Refresh();
            }
            if (Clone != null)
                Marshal.FreeHGlobal((IntPtr)Clone);
        }

        private void claraChroma_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
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

        private void EpsUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Eps.Value = (int)(EpsUpDown1.Value * 1000);
            UpdateCanvas();
        }

        private void Eps_Scroll(object sender, EventArgs e)
        {
            EpsUpDown1.Value = (decimal)((float)(Eps.Value) / 1000);
        }

        private void ChkPreview_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void SetPointUpDown_ValueChanged(object sender, EventArgs e)
        {
            SetPoint.Value = (int)SetPointUpDown.Value;
            UpdateCanvas();
        }

        private void SetPoint_Scroll(object sender, EventArgs e)
        {
            SetPointUpDown.Value = SetPoint.Value;
        }

        private void C_Scroll(object sender, EventArgs e)
        {
            CUpDown.Value = (decimal)((float)(C.Value) / 100);
        }

        private void CUpDown_ValueChanged(object sender, EventArgs e)
        {
            C.Value = (int)(CUpDown.Value * 100);
            UpdateCanvas();
        }
    }
}
