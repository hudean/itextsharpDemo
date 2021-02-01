using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace QRCode
{
    public  class ThoughtWorksHelper
    {

        /// <summary>
        /// ThoughtWorks.QRCode生成二维码
        /// </summary>
        /// <param name="idStr">写入二维码的字符串</param>
        /// <param name="strSaveDir">保存的路径</param>
        /// <param name="fileName">保存文件名称例如 QRCode.png</param>
        /// <returns></returns>
        public static void QRCodeMehed(string idStr,string strSaveDir,string fileName)
        {
            QRCodeEncoder endocder = new QRCodeEncoder();
            //二维码背景颜色
            endocder.QRCodeBackgroundColor = System.Drawing.Color.White;
            //二维码编码方式
            endocder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //每个小方格的宽度
            endocder.QRCodeScale = 10;
            //二维码版本号
            endocder.QRCodeVersion = 5;
            //纠错等级
            endocder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //var person = new { Id = 1, Name = "wolfy", Gender = 1, Age = 24 };
            //string idStr = "134165897456321";
            //将json川做成二维码
            //Bitmap bitmap = endocder.Encode(new JavaScriptSerializer().Serialize(person), System.Text.Encoding.UTF8);
            Bitmap bitmap = endocder.Encode(idStr, System.Text.Encoding.UTF8);
            //string strSaveDir = @"C:\Users\Administrator\Desktop\ABC";//Request.MapPath("/QRcode/");
            if (!Directory.Exists(strSaveDir))
            {
                Directory.CreateDirectory(strSaveDir);
            }
            // string strSavePath = Path.Combine(strSaveDir, "QRCode" + ".png");
            string strSavePath = Path.Combine(strSaveDir, fileName);
            //System.IO.FileStream fs = new System.IO.FileStream(strSavePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            //bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            if (!System.IO.File.Exists(strSavePath))
            {
                bitmap.Save(strSavePath);
            }
        }

        /// <summary>
        /// ThoughtWorks.QRCode解析二维码
        /// </summary>
        /// <returns></returns>
        public static string DeCoder(string strSaveDir, string fileName)
        {
            string result = "";
            //string strSaveDir = @"C:\Users\Administrator\Desktop\ABC";//Request.MapPath("/QRcode/");
            fileName = "QRCode.png";
            if (!Directory.Exists(strSaveDir))
            {
                Directory.CreateDirectory(strSaveDir);
            }
            string strSavePath = Path.Combine(strSaveDir, fileName );
            if (System.IO.File.Exists(strSavePath))
            {
                QRCodeDecoder decoder = new QRCodeDecoder();
                result = decoder.decode(new ThoughtWorks.QRCode.Codec.Data.QRCodeBitmapImage(new Bitmap(Image.FromFile(strSavePath))));
            }

            return result;
        }
    }
}
