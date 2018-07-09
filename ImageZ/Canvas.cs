using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace ImageZ
{
    public unsafe partial class Canvas : UserControl
    {
        private Bitmap image = null;
        private string imageFile = "";
        private double zoom = 1.0;
        private byte *imageData = null;
        private int channel = 3;

        private PixelFormat ChannelToPixelFormat(int Channel)
        {
            switch (Channel)
            {
                case 1:
                    return PixelFormat.Format8bppIndexed;
                case 3:
                    return PixelFormat.Format24bppRgb;
                case 4:
                    return PixelFormat.Format32bppArgb;
                default:
                    return PixelFormat.Undefined;
            }
        }

        private int PixelFormatToChannel(PixelFormat Format)
        {
            switch (Format)
            {
                case PixelFormat.Format8bppIndexed:
                    return 1;
                case PixelFormat.Format24bppRgb:
                    return 3;
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppRgb:
                    return 4;
                default:
                    return 0;
            }
        }

        public int Channel
        {
            get
            {
                return channel;
            }
            set
            {
                if (image != null)
                {
                    int Width = image.Width, Height = image.Height;
                    byte* SrcP, DestP;

                    if (channel != value)
                    {
                        Bitmap Temp = new Bitmap(image.Width, image.Height, ChannelToPixelFormat(value));
                        if (value == 1)
                        {
                            ColorPalette Pal = Temp.Palette;
                            for (int Y = 0; Y < Pal.Entries.Length; Y++)
                                Pal.Entries[Y] = Color.FromArgb(255, Y, Y, Y);
                            Temp.Palette = Pal;
                        }

                        BitmapData Data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, ChannelToPixelFormat(channel));
                        BitmapData DataT = Temp.LockBits(new Rectangle(0, 0, Temp.Width, Temp.Height), ImageLockMode.ReadWrite, ChannelToPixelFormat(value));

                        imageData = (byte*)DataT.Scan0;

                        for (int Y = 0; Y < Height; ++Y)
                        {
                            SrcP = (byte*)Data.Scan0 + Y * Data.Stride;
                            DestP = (byte*)DataT.Scan0 + Y * DataT.Stride;

                            if (value == 1)
                            {
                                for (int X = 0; X < Width; ++X)
                                {
                                    DestP[X] = (byte)((SrcP[0] + SrcP[1] + SrcP[1] + SrcP[2]) >> 2);
                                    SrcP += channel;
                                }
                            }
                            else if (value == 3)
                            {
                                if (channel == 1)
                                {
                                    for (int X = 0; X < Width; ++X)
                                    {
                                        DestP[0] = DestP[1] = DestP[2] = SrcP[X];
                                        DestP += 3;
                                    }
                                }
                                else
                                {
                                    for (int X = 0; X < Width; ++X)
                                    {
                                        DestP[0] = (byte)(SrcP[0] * SrcP[3] / 255);
                                        DestP[1] = (byte)(SrcP[1] * SrcP[3] / 255);
                                        DestP[2] = (byte)(SrcP[2] * SrcP[3] / 255);
                                        SrcP += 4;
                                        DestP += 3;
                                    }

                                }
                            }
                            else if (value == 4)
                            {
                                if (channel == 1)
                                {
                                    for (int X = 0; X < Width; ++X)
                                    {
                                        DestP[0] = DestP[1] = DestP[2] = SrcP[X];
                                        DestP[3] = 255;
                                        DestP += 4;
                                    }
                                }
                                else
                                {
                                    for (int X = 0; X < Width; ++X)
                                    {
                                        DestP[0] = SrcP[0];
                                        DestP[1] = SrcP[1];
                                        DestP[2] = SrcP[2];
                                        DestP[3] = 255;
                                        SrcP += 3;
                                        DestP += 4;
                                    }
                                }
                            }
                        }
                        image.UnlockBits(Data);
                        Temp.UnlockBits(DataT);
                        image.Dispose();
                        image = Temp;
                        this.Invalidate();
                    }
                } // if(image != null)
                channel = value;
            } // set
        } // public int Channel

        public Bitmap Image
        {
            get
            {
                return image;
            }
            set
            {
                if (value != null)
                {
                    int Ch = PixelFormatToChannel(value.PixelFormat);
                    if ((Ch == 1) || (Ch == 3) || (Ch == 4))
                    {
                        image = value;
                        channel = Ch;
                        BitmapData Data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
                        imageData = (byte*)Data.Scan0;
                        image.UnlockBits(Data);
                        this.Invalidate();
                    }

                }
            }
        }

        public byte* ImageData
        {
            get
            {
                return imageData;
            }

        }

        public string ImageFile
        {
            get
            {
                return imageFile;
            }
            set
            {
                imageFile = value;

                if (imageFile != "")
                {
                    Bitmap srcImage = (Bitmap)Bitmap.FromFile(imageFile, true);
                    int Ch = PixelFormatToChannel(srcImage.PixelFormat);
                    if ((Ch == 1) || (Ch == 3) || (Ch == 4))
                    {
                        image = srcImage;
                        channel = Ch;
                        BitmapData Data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
                        imageData = (byte*)Data.Scan0;
                        image.UnlockBits(Data);
                        this.Invalidate();
                    }
                    else
                    {
                        srcImage.Dispose();
                    }
                }
            }
        }

        public double Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
                this.Invalidate();
            }
        }
        public Canvas()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserMouse, true);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // 绘制图像
            if (image != null)
            {
                this.Size = new Size((int)(this.image.Width * zoom), (int)(this.image.Height * zoom));
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.DrawImage(image, new Rectangle(0, 0, this.Width, this.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }
    }
}
