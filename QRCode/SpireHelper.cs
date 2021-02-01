using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace QRCode
{
    public class SpireHelper
    {
        //文档地址 https://www.e-iceblue.cn/spirepdfnet/spire-pdf-for-net-program-guide-content.html


        /// <summary>
        /// Spire插件添加二维码到PDF
        /// </summary>
        /// <param name="sourcePdf">pdf文件路径</param>
        /// <param name="sourceImg">二维码图片路径</param>
        public static void AddQrCodeToPdf(string sourcePdf, string sourceImg)
        {
            //初始化PdfDocument实例,导入PDF文件  
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();

            //加载现有文档
            doc.LoadFromFile(sourcePdf);
            //添加一个空白页，目的为了删除jar包添加的水印，后面再移除这一页  (没有用)
            //PdfPageBase pb = doc.Pages.Add();
            //获取第二页
            //PdfPageBase page = doc.Pages[1];
            //获取第1页
            PdfPageBase page = doc.Pages[0];
            //加载图片到Image对象
            Image image = Image.FromFile(sourceImg);

            //调整图片大小
            int width = image.Width;
            int height = image.Height;
            float scale = 0.18f;  //缩放比例0.18f;
            Size size = new Size((int)(width * scale), (int)(height * scale));
            Bitmap scaledImage = new Bitmap(image, size);

            //加载缩放后的图片到PdfImage对象
            Spire.Pdf.Graphics.PdfImage pdfImage = Spire.Pdf.Graphics.PdfImage.FromImage(scaledImage);

            //设置图片位置
            float x = 516f;
            float y = 8f;

            //在指定位置绘入图片
            //page.Canvas.DrawImage(pdfImage, x, y);
            page.Canvas.DrawImage(pdfImage, new PointF(x,y),size);
            //移除第一个页
            //doc.Pages.Remove(pb); //去除第一页水印
            //保存文档
            doc.SaveToFile(@sourcePdf);
            doc.Close();
            //释放图片资源
            image.Dispose();
        }

        /// <summary>
        /// Spire插件读取PDF中的二维码并保存二维码图片
        /// </summary>
        /// <param name="file">pdf文件路径</param>
        public static void QrCodeToPdf(string file,string fileImg)
        {
            //加载PDF文档
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(file);

            List<Image> listImages = new List<Image>();

            for (int i = 0; i < doc.Pages.Count; i++)
            {
                // 实例化一个Spire.Pdf.PdfPageBase对象
                PdfPageBase page = doc.Pages[i];

                // 获取所有pages里面的图片
                Image[] images = page.ExtractImages(true); //page.ExtractImages();
                if (images != null && images.Length > 0)
                {
                    listImages.AddRange(images);
                }

            }

            // 将提取到的图片保存到本地路径
            if (listImages.Count > 0)
            {
                for (int i = 0; i < listImages.Count; i++)
                {
                    Image image = listImages[i];
                    //image.Save(@"C:\Users\Administrator\Desktop\ABC\image" + (i + 3).ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                    image.Save(fileImg, System.Drawing.Imaging.ImageFormat.Png);
                }

            }
        }

        /// <summary>
        /// Spire插件读取PDF中的二维码,Zxing识别二维码
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string QrCodeToPdftwo(string file)
        {
            string str = "";
            //加载PDF文档
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(file);

            List<Image> listImages = new List<Image>();

            for (int i = 0; i < doc.Pages.Count; i++)
            {
                // 实例化一个Spire.Pdf.PdfPageBase对象
                PdfPageBase page = doc.Pages[i];

                // 获取所有pages里面的图片
                Image[] images = page.ExtractImages();
                if (images != null && images.Length > 0)
                {
                    listImages.AddRange(images);
                }

            }

            if (listImages.Count > 0)
            {

                //QRCodeDecoder decoder = new QRCodeDecoder();
                //var image = listImages[0];
                //str = decoder.decode(new ThoughtWorks.QRCode.Codec.Data.QRCodeBitmapImage((Bitmap)image));

                var image = listImages[0];
                ZXing.BarcodeReader reader = new ZXing.BarcodeReader();
                reader.Options.CharacterSet = "UTF-8";
                Bitmap map = new Bitmap(image);
                Result result = reader.Decode(map);
                return result == null ? "" : result.Text;
            }

            return str;
        }
    }
}
