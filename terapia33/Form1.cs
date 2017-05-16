using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace terapia33
{
    public partial class Form1 : Form
    {
        Image file;
        int imageX;
        int imageY;
        int program = 1;
        public Form1()
        {
            InitializeComponent();

            foreach (System.Reflection.PropertyInfo prop in typeof(Color).GetProperties())
            {
                if (prop.PropertyType.FullName == "System.Drawing.Color")
                    comboBox1.Items.Add(prop.Name);
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string color = comboBox1.Text;
            this.BackColor = Color.FromName(color);
        }

        private void WybierzObrazek(object sender, EventArgs e)
        {
            OpenFileDialog image = new OpenFileDialog();
            image.Filter = "JPG (*.jpg)|*.jpg";
            if (image.ShowDialog() == DialogResult.OK)
            {
                PicBoxMovTimer.Enabled = false;
                program = 1;
                file = Image.FromFile(image.FileName);
                imageX = 0;
                imageY = this.Height / 2;
                pictureBox1.Location = new Point(imageX - pictureBox1.Width, imageY - pictureBox1.Height / 2);

                pictureBox1.Image = file;
                PicBoxMovTimer.Enabled = true;
            }
        }

        private void PicBoxMovTimer_Tick(object sender, EventArgs e)
        {
            switch (program)
            {
                case 1:
                    imageX = imageX + 4;
                    if (imageX > this.Width + pictureBox1.Width)
                        program = 2;
                    break;
                case 2:
                    imageX = imageX - 4;
                    if (imageX < 4)
                    {
                        program = 3;
                        imageX = this.Width / 2;
                        imageY = 0;
                        pictureBox1.Location = new Point(imageX - pictureBox1.Width / 2, imageY - pictureBox1.Height);
                    }
                    break;
                case 3:
                    imageY = imageY + 4;
                    if (imageY > this.Height + pictureBox1.Height)
                        program = 4;
                    break;
                case 4:
                    imageY = imageY - 4;
                    if (imageY < 4)
                    {
                        program = 5;
                        imageX = 0;
                        imageY = 0;
                        pictureBox1.Location = new Point(imageX - pictureBox1.Width, imageY - pictureBox1.Height);
                    }
                    break;
                case 5:
                    imageX = imageX + 4;
                    imageY = imageY + 4;
                    if (imageX > this.Width + pictureBox1.Width && imageY > this.Height + pictureBox1.Height)
                    {
                        program = 6;
                        imageX = this.Width;
                        imageY = 0;
                        pictureBox1.Location = new Point(imageX, imageY - pictureBox1.Height);
                    }
                    break;
                case 6:
                    imageX = imageX - 4;
                    imageY = imageY + 4;
                    if (imageX < 4)
                    {
                        program = 1;
                        imageX = 0;
                        imageY = this.Height / 2;
                        pictureBox1.Location = new Point(imageX - pictureBox1.Width, imageY - pictureBox1.Height / 2);
                    }
                    break;
                default:
                    program = 1;
                    imageX = 0;
                    imageY = this.Height / 2;
                    pictureBox1.Location = new Point(imageX - pictureBox1.Width, imageY - pictureBox1.Height / 2);
                    break;
            }

            pictureBox1.Location = new Point(imageX - pictureBox1.Width, imageY - pictureBox1.Height);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)
            {
                PicBoxMovTimer.Enabled = false;
                program = 1;
                Close();
            }
        }
    }
}
