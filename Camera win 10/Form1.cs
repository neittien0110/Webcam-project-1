using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.IO;

namespace Camera_win_10
{
    public partial class CameraWin10 : Form
    {
        public CameraWin10()
        {
            InitializeComponent();
        }
        private FilterInfoCollection videoDevice;
        private VideoCaptureDevice FinalFrame;
        private void CameraWin10_Load(object sender, EventArgs e)
        {
            videoDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);// kết nối webcam trên máy tính
            foreach (FilterInfo Device in videoDevice)
            {
                comboBox1.Items.Add(Device.Name);
            }
            comboBox1.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();
        }

        private void Start_Click(object sender, EventArgs e)// bắt đầu hiển thị hình ảnh từ webcam
        {
            FinalFrame = new VideoCaptureDevice(videoDevice[comboBox1.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += FinalFrame_NewFrame;
            FinalFrame.Start();
        }
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)//Hiển thị hình ảnh vào pictureBox1
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private void CameraWin10_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalFrame.IsRunning == true)
            {
                FinalFrame.Stop();
            }
        }

        private void Capture_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Image = (Bitmap)pictureBox1.Image.Clone();
            }
            catch
            {
                MessageBox.Show("Đã có gì đâu mà chụp -_-");
            }
        }

        private void Save_Click(object sender, EventArgs e) //save ảnh bằng SaveFileDialog
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    myStream.Close();
                }
            }
        }
    }
}
