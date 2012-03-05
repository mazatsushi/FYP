using System;
using System.Windows.Forms;
using EvilDicom.Image;

namespace Evil_DICOM_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImageMatrix im = new ImageMatrix(openFileDialog.FileName);
                pictureBox.Image = im.GetImage(0);
                pictureBox.Update();
            }
        }
    }
}
