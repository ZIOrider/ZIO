using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CourseManager.Models.ValidatableObjects
{
    public static class EncryptExtensions
    {
        public static string MD5Encoding(this string rawPass)
        {
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(rawPass);
            byte[] hs = md5.ComputeHash(bs);
            var sb = new StringBuilder();
            foreach (byte b in hs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString().ToUpper();
        }

        private const string QueryStringKey = "@#$%!&*%";

        public static string EncryptQueryString(this string queryString)
        {
            return Encrypt(queryString, QueryStringKey);
        }

        public static string DecryptQueryString(this string queryString)
        {
            try
            {
                return Decrypt(queryString, QueryStringKey);
            }
            catch
            {
                return "";
            }
        }

        public static string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();//把字符串加到byte数组中

            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);

            des.Key = Encoding.ASCII.GetBytes(sKey);//建立加密对象的密钥和偏移值
            des.IV = Encoding.ASCII.GetBytes(sKey);//原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            MemoryStream ms = new MemoryStream();//使得输入密码必须输入英文文本
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        public static string Decrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();//把字符串加到byte数组中

            byte[] inputByteArray = new byte[pToEncrypt.Length / 2];
            for (int x = 0; x < pToEncrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToEncrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.ASCII.GetBytes(sKey);//建立加密对象的密钥和偏移值,重要,不能修改
            des.IV = Encoding.ASCII.GetBytes(sKey);//原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            MemoryStream ms = new MemoryStream();//使得输入密码必须输入英文文本
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}