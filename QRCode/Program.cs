using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using ZXing;
using ZXing.Presentation;

namespace QRCode
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 使用ThoughtWorks 生成识别二维码

            ////生成二维码
            //ThoughtWorksHelper.QRCodeMehed("123598788", @"C:\Users\Administrator\Desktop\", "QRCode.png");

            ////解析二维码
            //string str = ThoughtWorksHelper.DeCoder(@"C:\Users\Administrator\Desktop\", "QRCode.png");
            //Console.WriteLine(str);
            //Console.ReadKey();
            #endregion


            #region 使用Zxing 生成识别二维码
            //生成二维码
            //ZxingHelper.Generate1("8585855885",@"C:\Users\Administrator\Desktop\QRCode1.png");

            ////解析二维码
            //string str = ZxingHelper.Read1(@"C:\Users\Administrator\Desktop\QRCode1.png");
            //Console.WriteLine(str);
            //Console.ReadKey();
            #endregion

            #region 使用Spire 向pdf添加二维码或读取pdf文件里的二维码

            //文档地址 https://www.e-iceblue.cn/spirepdfnet/spire-pdf-for-net-program-guide-content.html

            //向pdf添加二维码
            //SpireHelper.AddQrCodeToPdf(@"C:\Users\Administrator\Desktop\testPDF.pdf", @"C:\Users\Administrator\Desktop\QRCode1.png");
            //在pdf读取二维码，并保存二维码图片
            //SpireHelper.QrCodeToPdf(@"C:\Users\Administrator\Desktop\testPDF.pdf", @"C:\Users\Administrator\Desktop\QRCode2.png");
            //在pdf读取二维码
            string str = SpireHelper.QrCodeToPdftwo(@"C:\Users\Administrator\Desktop\testPDF.pdf");
            Console.WriteLine(str);
            Console.ReadKey();
            #endregion
        }
    }
}
