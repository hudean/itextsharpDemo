using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itextsharpDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region  DataTable 转成pdf文件

            // Method();
            #endregion

            #region 向pdf文件添加图片

            //ITextSharpHelper2.AddImg(@"C:\Users\Administrator\Desktop\testPDF.pdf", @"C:\Users\Administrator\Desktop\tempPDF.pdf", @"C:\Users\Administrator\Desktop\QRCode.png", 300, 100);

            #endregion

            #region 向pdf模板文件添加数据

            ITextSharpHelper2.AddNewPdf(@"C:\Users\Administrator\Desktop\temp3.pdf", @"C:\Users\Administrator\Desktop\temp123.pdf");
            #endregion

            Console.ReadKey();
        }

        private static void Method()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("age", typeof(int));
            dt.Columns.Add("address", typeof(string));
            dt.Clear();
            DataRow dr = dt.NewRow();
            dr["id"] = 1; dr["name"] = "cscc"; dr["age"] = 26; dr["address"] = "江西省";
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            bool b = ITextSharpHelper2.ConvertDataTableToPDF(dt, @"C:\Users\Administrator\Desktop\testPDF.pdf", "C://WINDOWS//FONTS//SIMSUN.TTC,1", 9);
            Console.WriteLine($"{b}");
            Console.ReadKey();
        }

       
    }


    public class IsHandF : PdfPageEventHelper, IPdfPageEvent
    {
        /// <summary>
        /// 创建页面完成时发生 
        /// </summary>
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            //页眉页脚使用字体
            BaseFont bsFont = BaseFont.CreateFont("C://WINDOWS//FONTS//SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font fontheader = new iTextSharp.text.Font(bsFont, 30, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontfooter = new iTextSharp.text.Font(bsFont, 20, iTextSharp.text.Font.BOLD);
            //水印文件地址
            //string syurl = "./upload/images/sys/black.png";

            //获取文件流
            PdfContentByte cbs = writer.DirectContent;
            cbs.SetCharacterSpacing(1.3f); //设置文字显示时的字间距
            Phrase header = new Phrase("页眉", fontheader);
            Phrase footer = new Phrase(writer.PageNumber.ToString(), fontfooter);
            //页眉显示的位置 
            ColumnText.ShowTextAligned(cbs, Element.ALIGN_CENTER, header,
                       document.Right / 2, document.Top + 40, 0);
            //页脚显示的位置 
            ColumnText.ShowTextAligned(cbs, Element.ALIGN_CENTER, footer,
                       document.Right / 2, document.Bottom - 40, 0);

            //添加背景色及水印，在内容下方添加
            PdfContentByte cba = writer.DirectContentUnder;
            //背景色
            Bitmap bmp = new Bitmap(1263, 893);
            Graphics g = Graphics.FromImage(bmp);
            Color c = Color.FromArgb(0x33ff33);
            SolidBrush b = new SolidBrush(c);//这里修改颜色
            g.FillRectangle(b, 0, 0, 1263, 893);
            System.Drawing.Image ig = bmp;
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ig, new BaseColor(0xFF, 0xFF, 0xFF));
            img.SetAbsolutePosition(0, 0);
            cba.AddImage(img);

            //水印
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("");
            image.RotationDegrees = 30;//旋转角度

            PdfGState gs = new PdfGState();
            gs.FillOpacity = 0.1f;//透明度
            cba.SetGState(gs);

            int x = -1000;
            for (int j = 0; j < 15; j++)
            {
                x = x + 180;
                int a = x;
                int y = -170;
                for (int i = 0; i < 10; i++)
                {
                    a = a + 180;
                    y = y + 180;
                    image.SetAbsolutePosition(a, y);
                    cba.AddImage(image);
                }
            }
        }
    }
}
