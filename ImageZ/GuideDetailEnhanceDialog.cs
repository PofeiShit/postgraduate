﻿using System;
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
    public unsafe partial class GuideDetailEnhanceDialog : Form
    {
        public enum Way{
            FILTER,
            ENHANCE,
        };
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        public static extern unsafe void CopyMemory(void* Dest, void* Src, int Length);
        [DllImport("guidedFilterDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern void guidedFilter(byte* Src, byte* Dest, int Width, int Heihgt, int Channel, int Radius, float Eps, double scale, float lamda = 0, Way w = Way.ENHANCE);
        [DllImport("detailEnhance.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern void detailEnhance(byte* Src, byte* Dest, int Width, int Height, int Channel, int Radius, float Eps, float Lamda);
        public GuideDetailEnhanceDialog()
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

        public GuideDetailEnhanceDialog(Canvas Parent, ToolStripStatusLabel Label, string Caption)
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
                //guidedFilter(Clone, canvas.ImageData, canvas.Image.Width, canvas.Image.Height, canvas.Channel, Radius.Value, (float)EpsUpDown1.Value, (double)SampleUpDown.Value, (float)LamdaUpDown1.Value);
                detailEnhance(Clone, canvas.ImageData, canvas.Image.Width, canvas.Image.Height, canvas.Channel, Radius.Value, (float)EpsUpDown1.Value, (float)LamdaUpDown1.Value);
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

        private void Radius_Scroll(object sender, EventArgs e)
        {
            RadiusUpDown.Value = Radius.Value;
            UpdateCanvas();
        }

        private void RadiusUpDown_ValueChanged(object sender, EventArgs e)
        {
            Radius.Value = (int)RadiusUpDown.Value;
        }

        private void EpsUpDown1_ValueChanged(object sender, EventArgs e)
        {
            EpsSample.Value = (int)(EpsUpDown1.Value * 1000);
            UpdateCanvas();
        }

        private void SubSample_Scroll(object sender, EventArgs e)
        {
            EpsUpDown1.Value = (decimal)((float)(EpsSample.Value) / 1000);
        }

        private void Lamda_Scroll(object sender, EventArgs e)
        {
            LamdaUpDown1.Value = (decimal)((float)(Lamda.Value) / 10);
        }

        private void LamdaUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Lamda.Value = (int)(LamdaUpDown1.Value * 10);
            UpdateCanvas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
            canvas.Refresh();
        }

        private void guideDetailEnhanceDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void guideDetailEnhanceDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                CopyMemory(canvas.ImageData, Clone, canvas.Image.Height * Stride);
                canvas.Refresh();
            }
            if (Clone != null)
                Marshal.FreeHGlobal((IntPtr)Clone);
        }

        private void ChkPreview_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void SampleUpDown_ValueChanged(object sender, EventArgs e)
        {
            Sample.Value = (int)(SampleUpDown.Value * 10);
            UpdateCanvas();
        }

        private void Sample_Scroll(object sender, EventArgs e)
        {
            SampleUpDown.Value = (decimal)((float)(Sample.Value) / 10);
        }


    }
}
