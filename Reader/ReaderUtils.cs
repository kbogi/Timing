using System;
using System.Collections.Generic;
using System.Text;

namespace UHFDemo
{
    public class ReaderUtils
    {
        /// <summary>
        /// 字符数组转为16进制数组
        /// </summary>
        /// <param name="strAryHex"></param>
        /// <param name="nLen"></param>
        /// <returns></returns>
        public static byte[] StringArrayToByteArray(string[] strAryHex, int nLen)
        {
            if (strAryHex.Length < nLen)
            {
                nLen = strAryHex.Length;
            }

            byte[] btAryHex = new byte[nLen];

            try
            {
                int nIndex = 0;
                foreach (string strTemp in strAryHex)
                {
                    btAryHex[nIndex] = Convert.ToByte(strTemp, 16);
                    nIndex++;
                }
            }
            catch (System.Exception ex)
            {
            	
            }

            return btAryHex;
        }

        /// <summary>
        /// 16进制字符数组转成字符串
        /// </summary>
        /// <param name="btAryHex"></param>
        /// <param name="nIndex"></param>
        /// <param name="nLen"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] btAryHex, int nIndex, int nLen)
        {
            if (nIndex + nLen > btAryHex.Length)
            {
                nLen = btAryHex.Length - nIndex;
            }

            string strResult = string.Empty;

            for (int nloop = nIndex; nloop < nIndex + nLen; nloop++ )
            {
                string strTemp = string.Format(" {0:X2}", btAryHex[nloop]);

                strResult += strTemp;
            }

            return strResult;
        }

        /// <summary>
        /// 将字符串按照指定长度截取并转存为字符数组，空格忽略
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="nLength"></param>
        /// <returns></returns>
        public static string[] StringToStringArray(string strValue, int nLength)
        {
            string[] strAryResult = null;

            if (!string.IsNullOrEmpty(strValue))
            {
                System.Collections.ArrayList strListResult = new System.Collections.ArrayList();
                string strTemp = string.Empty;
                int nTemp = 0;

                for (int nloop = 0; nloop < strValue.Length; nloop++ )
                {
                    if (strValue[nloop] == ' ')
                    {
                        continue;
                    }
                    else
                    {
                        nTemp++;

                        //校验截取的字符是否在A~F、0~9区间，不在则直接退出
                        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^(([A-F])*(\d)*)$");
                        if (!reg.IsMatch(strValue.Substring(nloop, 1)))
                        {
                            return strAryResult;
                        }

                        strTemp += strValue.Substring(nloop, 1);

                        //判断是否到达截取长度
                        if ((nTemp == nLength) || (nloop == strValue.Length - 1 && !string.IsNullOrEmpty(strTemp)))
                        {
                            strListResult.Add(strTemp);
                            nTemp = 0;
                            strTemp = string.Empty;
                        }
                    }
                }

                if (strListResult.Count > 0)
                {
                    strAryResult = new string[strListResult.Count];
                    strListResult.CopyTo(strAryResult);
                }
            }

            return strAryResult;
        }

        public static string FormatErrorCode(byte btErrorCode)
        {
            string strErrorCode = "";
            switch (btErrorCode)
            {
                case 0x10:
                    strErrorCode = "命令已执行";
                    break;
                case 0x11:
                    strErrorCode = "命令执行失败";
                    break;
                case 0x20:
                    strErrorCode = "CPU 复位错误";
                    break;
                case 0x21:
                    strErrorCode = "打开CW 错误";
                    break;
                case 0x22:
                    strErrorCode = "天线未连接";
                    break;
                case 0x23:
                    strErrorCode = "写Flash 错误";
                    break;
                case 0x24:
                    strErrorCode = "读Flash 错误";
                    break;
                case 0x25:
                    strErrorCode = "设置发射功率错误";
                    break;
                case 0x31:
                    strErrorCode = "盘存标签错误";
                    break;
                case 0x32:
                    strErrorCode = "读标签错误";
                    break;
                case 0x33:
                    strErrorCode = "写标签错误";
                    break;
                case 0x34:
                    strErrorCode = "锁定标签错误";
                    break;
                case 0x35:
                    strErrorCode = "灭活标签错误";
                    break;
                case 0x36:
                    strErrorCode = "无可操作标签错误";
                    break;
                case 0x37:
                    strErrorCode = "成功盘存但访问失败";
                    break;
                case 0x38:
                    strErrorCode = "缓存为空";
                    break;
                case 0x40:
                    strErrorCode = "访问标签错误或访问密码错误";
                    break;
                case 0x41:
                    strErrorCode = "无效的参数";
                    break;
                case 0x42:
                    strErrorCode = "wordCnt 参数超过规定长度";
                    break;
                case 0x43:
                    strErrorCode = "MemBank 参数超出范围";
                    break;
                case 0x44:
                    strErrorCode = "Lock 数据区参数超出范围";
                    break;
                case 0x45:
                    strErrorCode = "LockType 参数超出范围";
                    break;
                case 0x46:
                    strErrorCode = "读卡器地址无效";
                    break;
                case 0x47:
                    strErrorCode = "Antenna_id 超出范围";
                    break;
                case 0x48:
                    strErrorCode = "输出功率参数超出范围";
                    break;
                case 0x49:
                    strErrorCode = "射频规范区域参数超出范围";
                    break;
                case 0x4A:
                    strErrorCode = "波特率参数超过范围";
                    break;
                case 0x4B:
                    strErrorCode = "蜂鸣器设置参数超出范围";
                    break;
                case 0x4C:
                    strErrorCode = "EPC 匹配长度越界";
                    break;
                case 0x4D:
                    strErrorCode = "EPC 匹配长度错误";
                    break;
                case 0x4E:
                    strErrorCode = "EPC 匹配参数超出范围";
                    break;
                case 0x4F:
                    strErrorCode = "频率范围设置参数错误";
                    break;
                case 0x50:
                    strErrorCode = "无法接收标签的RN16";
                    break;
                case 0x51:
                    strErrorCode = "DRM 设置参数错误";
                    break;
                case 0x52:
                    strErrorCode = "PLL 不能锁定";
                    break;
                case 0x53:
                    strErrorCode = "射频芯片无响应";
                    break;
                case 0x54:
                    strErrorCode = "输出达不到指定的输出功率";
                    break;
                case 0x55:
                    strErrorCode = "版权认证未通过";
                    break;
                case 0x56:
                    strErrorCode = "频谱规范设置错误";
                    break;
                case 0x57:
                    strErrorCode = "输出功率过低";
                    break;
                case 0xFF:
                    strErrorCode = "未知错误";
                    break;

                default:
                    break;
            }

            return strErrorCode;
        }


        #region FromHex

        /// <summary>
        /// Convert human-readable hex string to byte array;
        /// e.g., 123456 or 0x123456 -> {0x12, 0x34, 0x56};
        /// </summary>
        /// <param name="hex">Human-readable hex string to convert</param>
        /// <returns>Byte array</returns>
        public static byte[] FromHex(string hex)
        {
            int prelen = 0;

            if (hex.StartsWith("0x") || hex.StartsWith("0X"))
                prelen = 2;

            byte[] bytes = new byte[(hex.Length - prelen) / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                string bytestring = hex.Substring(prelen + (2 * i), 2);
                bytes[i] = byte.Parse(bytestring, System.Globalization.NumberStyles.HexNumber);
            }

            return bytes;
        }

        #endregion

        #region ToHex

        /// <summary>
        /// Convert byte array to human-readable hex string; e.g., {0x12, 0x34, 0x56} -> 123456
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <returns>Human-readable hex string</returns>
        public static string ToHex(byte[] bytes)
        {
            return ToHex(bytes, "0x", "");
        }

        /// <summary>
        /// Convert byte array to human-readable hex string; e.g., {0x12, 0x34, 0x56} -> 123456
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <param name="prefix">String to place before byte strings</param>
        /// <param name="separator">String to place between byte strings</param>
        /// <returns>Human-readable hex string</returns>
        public static string ToHex(byte[] bytes, string prefix, string separator)
        {
            if (null == bytes)
                return "null";

            List<string> bytestrings = new List<string>();

            foreach (byte b in bytes)
                bytestrings.Add(b.ToString("X2"));

            return prefix + String.Join(separator, bytestrings.ToArray());
        }

        /// <summary>
        /// Convert u16 array to human-readable hex string; e.g., {0x1234, 0x5678} -> 12345678
        /// </summary>
        /// <param name="words">u16 array to convert</param>
        /// <returns>Human-readable hex string</returns>
        public static string ToHex(UInt16[] words)
        {
            StringBuilder sb = new StringBuilder(4 * words.Length);

            foreach (UInt16 word in words)
                sb.Append(word.ToString("X4"));

            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// Converts word array to byte array
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <returns>The converted word array</returns>
        public static ushort[] bytesToWords(byte[] bytes)
        {
            ushort[] memWords = new ushort[bytes.Length / 2];
            for (int i = 0; i < memWords.Length; i++)
            {
                memWords[i] = bytes[2 * i];
                memWords[i] <<= 8;
                memWords[i] |= bytes[2 * i + 1];
            }
            return memWords;
        }

        /// <summary>
        /// Converts byte array to word array
        /// </summary>
        /// <param name="words">The word array</param>
        /// <returns>The converted byte array</returns>
        public static byte[] wordsToBytes(ushort[] words)
        {
            byte[] dataBytes = new byte[words.Length * 2];
            for (int i = 0; i < words.Length; i++)
            {
                dataBytes[2 * i] = (byte)((words[i] >> 8) & 0xFF);
                dataBytes[2 * i + 1] = (byte)((words[i]) & 0xFF);
            }
            return dataBytes;
        }

        #region FromU16

        /// <summary>
        /// Insert unsigned 16-bit integer into big-endian byte string
        /// </summary>
        /// <param name="bytes">Target big-endian byte string</param>
        /// <param name="offset">Location to insert into</param>
        /// <param name="value">16-bit integer to insert</param>
        /// <returns>Number of bytes inserted</returns>
        public static int FromU16(byte[] bytes, int offset, UInt16 value)
        {
            int end = offset;
            bytes[end++] = (byte)((value >> 8) & 0xFF);
            bytes[end++] = (byte)((value >> 0) & 0xFF);
            return end - offset;
        }

        #endregion

        /// <summary>
        /// Extract unsigned 16-bit integer from big-endian byte string
        /// </summary>
        /// <param name="bytes">Source big-endian byte string</param>
        /// <param name="offset">Location to extract from.  Will be updated to post-decode offset.</param>
        /// <returns>Unsigned 16-bit integer</returns>
        public static UInt16 ToU16(byte[] bytes, ref int offset)
        {
            if (null == bytes) return default(byte);
            int hi = (UInt16)(bytes[offset++]) << 8;
            int lo = (UInt16)(bytes[offset++]);
            return (UInt16)(hi | lo);
        }

        #region FromU32

        /// <summary>
        /// Insert unsigned 32-bit integer into big-endian byte string
        /// </summary>
        /// <param name="bytes">Target big-endian byte string</param>
        /// <param name="offset">Location to insert into</param>
        /// <param name="value">32-bit integer to insert</param>
        /// <returns>Number of bytes inserted</returns>
        public static int FromU32(byte[] bytes, int offset, UInt32 value)
        {
            int end = offset;
            bytes[end++] = (byte)((value >> 24) & 0xFF);
            bytes[end++] = (byte)((value >> 16) & 0xFF);
            bytes[end++] = (byte)((value >> 8) & 0xFF);
            bytes[end++] = (byte)((value >> 0) & 0xFF);
            return end - offset;
        }

        #endregion

        #region ToU32

        /// <summary>
        /// Extract unsigned 32-bit integer from big-endian byte string
        /// </summary>
        /// <param name="bytes">Source big-endian byte string</param>
        /// <param name="offset">Location to extract from</param>
        /// <returns>Unsigned 32-bit integer</returns>
        public static UInt32 ToU32(byte[] bytes, ref int offset)
        {
            return (UInt32)(0
                | ((UInt32)(bytes[offset++]) << 24)
                | ((UInt32)(bytes[offset++]) << 16)
                | ((UInt32)(bytes[offset++]) << 8)
                | ((UInt32)(bytes[offset++]) << 0)
                );
        }

        #endregion

    }
}
