using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartToiletService
{
    /// <summary>
    /// 处理数据类型转换，数制转换、编码转换相关的类
    /// </summary>    
    public sealed class ConvertHelper
    {
        #region 补足位数
        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //连接text
            temp += text;

            //返回补足0的字符串
            return temp;
        }
        #endregion

        #region 各进制数间转换
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }
        #endregion

        #region 16位字符串转换成String[]数组
        /// <summary>
        /// 转换为string数组，每两位放一个
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static string[] HexStrTobyte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            string[] returnStrings = new string[hexString.Length / 2];
            for (int i = 0; i < returnStrings.Length; i++)
                returnStrings[i] = hexString.Substring(i * 2, 2).Trim();
            return returnStrings;
        }
        #endregion

        #region byte[]转为16进制字符串
        /// <summary>
        /// byte[]转为16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        #endregion

        #region  将16进制的字符串转为byte[]
        /// <summary>
        /// 将16进制的字符串转为byte[]
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrToHexByte(string hexString)
        {

            hexString = hexString.Replace(" ", "");
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }
        #endregion


        #region 将16进制字节数组转换为10进制数
        /// <summary>
        /// 将16进制字节数组转换为10进制数
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int StringArrToTen(byte[] data) => Convert.ToInt32(ConvertBase(ByteToHexStr(data), 16, 10));
        #endregion

        #region 将10进制字节数组转换为10进制数
        /// <summary>
        /// 将10进制字节数组转换为10进制数
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int TenStringToint(byte[] data) => Convert.ToInt32(ConvertBase(ByteToHexStr(data), 10, 10));
        #endregion


        #region 将10进制字节数组转换为10进制字符串

        /// <summary>
        /// 将10进制字节数组转换为10进制数
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string TenStringToString(byte[] data) => ConvertBase(ByteToHexStr(data), 10, 10);
        #endregion


        #region 将16进制字节数组转换为有符号的10进制
        /// <summary>
        /// 将16进制转换为有符号的10进制 统一采用最高Bit位来表示符号位
        /// </summary>
        /// <param name="Bithex"></param>
        /// <returns></returns>
        public static int ConvertHexToSIntStr(byte[] Bithex)
        {
            string hexstr = ByteToHexStr(Bithex);
            string TowBit = ConvertBase(hexstr, 16, 2);
            if (Convert.ToInt32(TowBit.Substring(0, 1)) > 0)
            {
                
                string HexBit = TowBit.Substring(1, TowBit.Length - 1);
                return -Convert.ToInt32(ConvertBase(HexBit, 2, 10));
            }
            else
            {
                return Convert.ToInt32(ConvertBase(hexstr, 16, 10));
            }

        }
        #endregion

        /// <summary>
        /// 将Ascll转换为String字符串
        /// </summary>
        /// <param name="AscllString"></param>
        /// <returns></returns>

        public static string Ascii2Str(byte[] buf)
        {
            return Encoding.ASCII.GetString(buf);
        }

        /// <summary>
        /// 将string类型转换为Ascll字符串
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>

        public static byte[] Str2ASCII(string xmlStr)
        {
            return Encoding.Default.GetBytes(xmlStr);
        }
        public static string StrRe(string strcrc)
        {
            string Down = strcrc.Substring(0, 2);
            string Up = strcrc.Substring(2, 2);
            return Up + Down;
        }

    }
}
