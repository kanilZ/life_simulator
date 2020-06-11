using LifeGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Life_Simulator
{

    public partial class Form1 : Form
    {
        //private Graphics graphics;
        //private int resolution;
        //private bool[,] field;
        //private int rows;
        //private int cols;
        //private int currentGeneration = 0;
        Life life = new Life();
        public Form1()
        {
            InitializeComponent();
        }

        private void StartGame()
        {
            if (timer1.Enabled)
                return;

            numResolution.Enabled = false;
            numDensity.Enabled = false;
            Text = $"Generation {life.CurrentGeneration}";

            life.SetFieldSize((int)numResolution.Value, pictureBox1.Height, pictureBox1.Width);
            life.RandomField((int)numDensity.Value);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            life.Graphics = Graphics.FromImage(pictureBox1.Image);

            timer1.Start();
        }

      
        private void NextGeneration()
        {
            life.NextGeneration();      
            pictureBox1.Refresh();
            Text = $"Generation {++life.CurrentGeneration}";
        }

       
        private void StopGame()
        {
            if (!timer1.Enabled)
                return;

            timer1.Stop();
            numResolution.Enabled = true;
            numDensity.Enabled = true;
        }
        private void numDensity_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                life.CreateCells(e.Location.X, e.Location.Y, true);

            }
            if (e.Button == MouseButtons.Right)
            {
                life.CreateCells(e.Location.X, e.Location.Y, false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = $"Generation {life.CurrentGeneration}";
        }
    }
}
