using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public static class ImageWorker
    {
        public static Bitmap ConverImageToBitmap(Image image, int maxWidth, int maxHeight) //65*65
        {
            try
            {
                using (Bitmap originalPic = new Bitmap(image))
                {
                    int originalWidth = originalPic.Width; //165
                    int originalHeight = originalPic.Height; //85
                    float rationX = (float)maxWidth / (float)originalWidth; //0.39
                    float rationY = (float)maxHeight / (float)originalHeight;//0.76
                    float ration = Math.Min(rationX, rationY); //0.39
                    int width = (int)(originalWidth * ration); // 65
                    int height = (int)(originalHeight * ration);// 34

                    using (Bitmap outBmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                    {
                        using (Graphics oGraphics = Graphics.FromImage(outBmp))
                        {
                            oGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            oGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            oGraphics.DrawImage(originalPic, 0, 0, width, height);

                            //Font font = new Font("Arial", 20);
                            //Brush brush = new SolidBrush(Color.Brown);
                            //oGraphics.DrawString("Аслан - лев", font, brush, new Point(width - 200, height - 80));

                            //Водяний занак
                            return new Bitmap(outBmp);
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

        }

    }
}
