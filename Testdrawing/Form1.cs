using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Subpixelprogramming;

namespace Testdrawing
{
    public partial class Form1 : Form
    {
        List<Pixel> pixels = new List<Pixel>();
        List<IUpdate> updates = new List<IUpdate>();
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            Form1_Paint(this, e);
        }

        private void Form1_Paint(Object sender, System.Windows.Forms.PaintEventArgs pe)
        {
            
            Graphics g = pe.Graphics;
            Bitmap bmp = new Bitmap(WidthRender, HeightRender);
            float[,] depthbuffer = new float[WidthRender, HeightRender];

            foreach (Pixel p in pixels)
            {
                if (p.pos.x > 0f && p.pos.x < WidthRender && p.pos.y > 0 && p.pos.y < HeightRender)
                {
                    if (-p.pos.z < -depthbuffer[(int)p.pos.x, (int)p.pos.y] && p.pos.z > Depth)
                    {
                        depthbuffer[(int)p.pos.x, (int)p.pos.y] = p.pos.z;
                        bmp.SetPixel((int)p.pos.x, (int)p.pos.y, p.color);
                    }
                }
            }
            pictureBox1.Image = bmp;
            foreach (IUpdate pu in updates)
                pu.Update();
        }

        const int WidthRender = 399;
        const int HeightRender = 399;
        float Depth = 0f;
        public void UpdateRender()
        {
            
            
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            
            Random rnd = new Random();
            for (int i = 0; i < 1; i++)
            {
                Pixel p = new Pixel(new Position(rnd.Next(WidthRender * 1000) / 1000f,
                                                    rnd.Next(HeightRender * 1000) / 1000f,
                                                    rnd.Next(1000000) / 1000f));
                p.color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                pixels.Add(p);
            }
            for (int i = 0; i < 10000; i++)
            {
                Pixel p = new MovingPixel(new Position(rnd.Next(WidthRender * 1000) / 1000f,
                                                    rnd.Next(HeightRender * 1000) / 1000f,
                                                    rnd.Next(1000000) / 1000f),
                                    new Position((rnd.Next(100) - 50) / 100f,
                                                (rnd.Next(100) - 50) / 100f,
                                                (rnd.Next(100) - 50) / 100f));
                p.color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));

                pixels.Add(p);
                updates.Add(p as IUpdate);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Depth += 50;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Depth -= 50;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
