using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XPhoto;

// https://blog.naver.com/nonezerok/221694309266


namespace FormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mat src = new Mat("C:\\Users\\김성빈\\source\\repos\\Bincher\\GUI-Programing\\FormApp\\bin\\Debug\\Lenna.png", ImreadModes.Color);
            /*
            OpenCvSharp.Window srcWindow = new OpenCvSharp.Window("scr", scr);
            Cv2.WaitKey(0); //어떤 키라도 눌릴 때까지 대기
            Cv2.DestroyWindow("src"); //src 이름을 가진 윈도우 파괴
            srcWindow.Dispose(); //srcWindow 객체 제거
            */
            src = src.Blur(new OpenCvSharp.Size(7, 7));
            Mat dst = new Mat();
            Cv2.Resize(src, dst, new OpenCvSharp.Size(256, 256));

            //1. 프로젝트의 속성 페이지를 연다
            //2. 빌드 속성 페이지를 클릭한다.
            //3. 안전하지 않은 코드 허용 확인란을 선택한다.
            unsafe 
            {

                //IntPtr pImg = scr.Data;
                //byte* buff = (byte*)pImg.ToPointer();
                byte* buff = (byte*)src.DataPointer;

                for (int i = 0, idx = 0; i<dst.Cols * dst.Rows; i++, idx +=3)
                {
                    int gray = 0;
                    gray = (int)buff[idx + 0] + (int)buff[idx + 1] + (int)buff[idx + 2];
                    gray = gray / 3;
                    buff[idx + 0] = (byte)gray;
                    buff[idx + 1] = (byte)gray;
                    buff[idx + 2] = (byte)gray;
                }
            }

            Bitmap bmp;
            //bmp = src.ToBitmap();
            bmp = dst.ToBitmap();
            //pictureBox1.Image = bmp;
            IntPtr hWnd = pictureBox1.Handle;
            Graphics g = Graphics.FromHwnd(hWnd);
            g.DrawImage(bmp, 0, 0);
            

            //g.Dispose();
            src.Dispose();
            dst.Dispose();
        }
    }
}
