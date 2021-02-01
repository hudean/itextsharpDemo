using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itextsharpDemo
{

    public class ITextSharpHelper2
    {
        /// <summary>
        /// 根据pdf模板写入数据生成新的pdf
        /// </summary>
        /// <param name="saveFile">保存的新pdf路径</param>
        /// <param name="sourceFile">原pdf路径</param>
        public static void AddNewPdf(string saveFile, string sourceFile)
        {
            //写入新的pdf地址
            //sourceFile = @"C:\Users\Administrator\Desktop\ABC\temp.pdf";
            //sourceFile = @"C:\Users\Administrator\Desktop\temp123.pdf";
            iTextSharp.text.pdf.PdfDocument document = new iTextSharp.text.pdf.PdfDocument();
            //读取的源pdf文件
            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(sourceFile);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(saveFile, FileMode.OpenOrCreate));
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
        }

        /// <summary>
        /// pdf文件添加图片
        /// </summary>
        /// <param name="oldPath">源PDF文件</param>
        /// <param name="newPath">新PDF文件</param>
        /// <param name="imPath">图片地址</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void AddImg(string oldPath, string newPath, string imPath, float x, float y)
        {
            iTextSharp.text.pdf.PdfDocument doc = new iTextSharp.text.pdf.PdfDocument();


            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(oldPath);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newPath, FileMode.OpenOrCreate));
            //获取PDF指定页面内容
            var pdfContentByte = pdfStamper.GetOverContent(1);
            //添加图片
            Image image = Image.GetInstance(imPath);

            //设置图片大小
            image.ScaleToFit(100, 100);
            //设置图片位置
            image.SetAbsolutePosition(x, y);
            pdfContentByte.AddImage(image);
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
            pdfReader.Close();

        }


        /// <summary>
        /// DataTable 转成Pdf文件
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="PDFFile"></param>
        /// <param name="FontPath"></param>
        /// <param name="FontSize"></param>
        /// <returns></returns>
        public static bool ConvertDataTableToPDF(DataTable Data, string PDFFile, string FontPath, float FontSize)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(PDFFile, FileMode.Create));
            document.Open();
            BaseFont baseFont =
                BaseFont.CreateFont(
                FontPath,
                BaseFont.IDENTITY_H,
                BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, FontSize);
            PdfPTable table = new PdfPTable(Data.Columns.Count);
            for (int i = 0; i < Data.Rows.Count; i++)
            {
                for (int j = 0; j < Data.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(Data.Rows[i][j].ToString(), font));
                }
            }
            document.Add(table);
            document.Close();
            writer.Close();
            return true;
        }

    }
}
