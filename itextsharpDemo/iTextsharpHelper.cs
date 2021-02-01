using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp;

namespace itextsharpDemo
{
    public class iTextsharpHelper
    {
        public static void AddNewPdf()
        {
            string tempFilePath = @"C:\Users\Administrator\Desktop\ABC\temp.pdf";
            iTextSharp.text.pdf.PdfDocument document = new iTextSharp.text.pdf.PdfDocument();
            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(@"C:\Users\Administrator\Desktop\temp123.pdf");
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(tempFilePath, FileMode.OpenOrCreate));
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            pdfStamper.FormFlattening = true;
            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //BaseFont simheiBase = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont simheiBase = BaseFont.CreateFont(@"C:\Windows\Fonts\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            pdfFormFields.AddSubstitutionFont(simheiBase);
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add($"Numbering", "12369999995");
            for (int i = 1; i < 38; i++)
            {
                para.Add($"Numbering{i}", "12365");
            }
            foreach (KeyValuePair<string, string> parameter in para)
            {
                pdfStamper.AcroFields.SetField(parameter.Key, parameter.Value);
            }
            //pdfStamper.AcroFields.SetField("Names", "李朝强");
            //pdfStamper.AcroFields.SetField("chk", "yes", true);
            pdfStamper.Close();
            pdfReader.Close();


            ////获取中文字体，第三个参数表示为是否潜入字体，但只要是编码字体就都会嵌入。
            //BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            ////读取模板文件
            //PdfReader reader = new PdfReader(@"C:\Users\Administrator\Desktop\temp123.pdf");

            ////创建文件流用来保存填充模板后的文件
            //MemoryStream stream = new MemoryStream();

            //PdfStamper stamp = new PdfStamper(reader, stream);
            ////设置表单字体，在高版本有用，高版本加入这句话就不会插入字体，低版本无用
            ////stamp.AcroFields.AddSubstitutionFont(baseFont);

            //AcroFields form = stamp.AcroFields;
            ////表单文本框是否锁定
            //stamp.FormFlattening = true;
            //Dictionary<string, string> para = new Dictionary<string, string>();
            //for (int i = 0; i < 38; i++)
            //{
            //    para.Add($"{i}", "tttt");
            //}


            ////填充表单,para为表单的一个（属性-值）字典
            //foreach (KeyValuePair<string, string> parameter in para)
            //{
            //    //要输入中文就要设置域的字体;
            //    form.SetFieldProperty(parameter.Key, "textfont", baseFont, null);
            //    //为需要赋值的域设置值;
            //    form.SetField(parameter.Key, parameter.Value);
            //}


            ////最后按顺序关闭io流

            //stamp.Close();
            //reader.Close();

        }



        /// <summary>
        /// 向pdf中添加图片
        /// </summary>
        /// <param name="oldP">源pdf地址</param>
        /// <param name="imP">图片地址</param>
        /// <param name="x">x轴坐标</param>
        /// <param name="y">y轴坐标</param>
        public static void AddImg(string oldP, string newP, string imP, int x, int y)
        {
            string tempFilePath = newP;
            iTextSharp.text.pdf.PdfDocument doc = new iTextSharp.text.pdf.PdfDocument();


            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(oldP);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(tempFilePath, FileMode.OpenOrCreate));
            //获取PDF指定页面内容
            var pdfContentByte = pdfStamper.GetOverContent(1);
            //添加图片
            Image image = Image.GetInstance(@"C:\Users\Administrator\Desktop\ABC\QRCode.png");

            image.ScaleToFit(60, 60);

            image.SetAbsolutePosition(500, 700);
            pdfContentByte.AddImage(image);
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
            pdfReader.Close();


            //AcroFields pdfFormFields = pdfStamper.AcroFields;
            //pdfStamper.FormFlattening = true;

            ////BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //BaseFont simheiBase = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            // //添加图片
            // //Image image = Image.GetInstance(@"C:\Users\Administrator\Desktop\ABC\QRCode.png");
            //image.ScaleToFit(60, 60);
            //pdfStamper.Writer.Add(image);
           
            ////pdfFormFields.AddSubstitutionFont(simheiBase);
            ////pdfStamper.AcroFields.SetField("Names", "李朝强");
            ////pdfStamper.AcroFields.SetField("chk", "yes", true);
            //pdfStamper.Close();
            //pdfReader.Close();


            ///实例化一个doc 对象
            //Document doc = new Document();
            //try
            //{
            //    ///创建一个pdf 对象
            //    PdfWriter.GetInstance(doc,
            //        new FileStream(@"C:\Users\Administrator\Desktop\ABC\tempimg.pdf", FileMode.Create));
            //    //打开文件
            //    doc.Open();
            //    ///向文件中添加单个图片
            //    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(@"C:\Users\Administrator\Desktop\ABC\QRCode.png");

            //    image.ScaleToFit(60, 60);
            //    doc.Add(image);

            //    ///向文件中循环添加图片


            //}
            //catch (DocumentException dex)
            //{
            //    ////如果文件出现异常输入文件异常

            //}
            //catch (IOException ioex)
            //{
            //    ////如果文件读写出现异常输入文件异常

            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    doc.Close();
            //}
        }
    }
}
