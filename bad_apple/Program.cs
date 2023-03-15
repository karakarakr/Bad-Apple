using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.Mime.MediaTypeNames.Image;
using Image = System.Drawing.Image;

namespace bad_apple
{
    internal class Program
    {
        // Хотів швидкість гіфки устаканити, але щось це не працює
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int timeBeginPeriod(int msec);
        //[System.Runtime.InteropServices.DllImport("winmm.dll")]
        //public static extern int timeEndPeriod(int msec);
        static void Main(string[] args)
        {
            Image image = Image.FromFile(@"videoplaybacktest2.gif");

            FrameDimension dimension = new FrameDimension(image.FrameDimensionsList[0]);
            int frameCount = image.GetFrameCount(dimension);
            StringBuilder sb;

            Console.WindowWidth = 221;
            Console.WindowHeight = 166;

            int left = Console.WindowLeft, top = Console.WindowTop;

            char[] chars = { ' ', '.', '-', ':', '*', '+',
                         '=', '%', '@', '#', '#' };

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"BadApple.wav");
            
            timeBeginPeriod(7);
            player.Play();

            for (int i = 0; ; i = (i + 1) % frameCount)
            {
                sb = new StringBuilder();
                image.SelectActiveFrame(dimension, i);

                for (int h = 0; h < 165; h++)
                {
                    for (int w = 0; w < 220; w++)
                    {
                        Color cl = ((Bitmap)image).GetPixel(w, h);
                        int gray = (cl.R + cl.G + cl.B) / 3;
                        int index = (gray * (chars.Length - 1)) / 255;

                        sb.Append(chars[index]);
                    }
                    sb.Append('\n');
                }

                Console.SetCursorPosition(left, top);
                Console.Write(sb.ToString());
                Thread.Sleep(11);
            }
        }
    }
}
