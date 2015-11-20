using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Security {
    public class SecurityHelper {
        /// <summary>
        /// DES加密使用的密钥 长度为8位
        /// </summary>
        public static string Salt {
            get {
                var salt = AppSetting.Salt;
                if (string.IsNullOrWhiteSpace(salt)) {
                    throw new ArgumentNullException("Salt", "未在配置源中设定");
                }
                salt = salt.Trim();
                if (salt.Length < 8) {
                    throw new ArgumentOutOfRangeException("Salt", "盐长度不能少于8");
                }
                return salt.Substring(0, 8);
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="decryptString">原文</param>
        /// <returns>密文</returns>        
        public static string SHA1Encrypt(string Source_String) {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes) {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }

        /// <summary>
        /// DES加密使用的密钥 长度为8位
        /// </summary>
        public static string DesKey {
            get {
                var desKey = AppSetting.DesKey;
                if (string.IsNullOrWhiteSpace(desKey)) {
                    throw new ArgumentNullException("DesKey", "未在配置源中设定");
                }
                desKey = desKey.Trim();
                if (desKey.Length < 8) {
                    throw new ArgumentOutOfRangeException("DesKey", "长度不能少于8");
                }
                return desKey.Substring(0, 8);
            }
        }

        // 验值 
        static string saltValue = AppSetting.DesKey;
        // 密码值 
        static string pwdValue = AppSetting.DesKey;
 
        /// <summary>
        /// 加密
        /// </summary>
        public static string AESEncrypt( string input ) {
            byte[ ] data = System.Text.UTF8Encoding.UTF8.GetBytes( input );
            byte[ ] salt = System.Text.UTF8Encoding.UTF8.GetBytes( saltValue );
 
            // AesManaged - 高级加密标准(AES) 对称算法的管理类 
            System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged( );
            // Rfc2898DeriveBytes - 通过使用基于 HMACSHA1 的伪随机数生成器，实现基于密码的密钥派生功能 (PBKDF2 - 一种基于密码的密钥派生函数) 
            // 通过 密码 和 salt 派生密钥 
            System.Security.Cryptography.Rfc2898DeriveBytes rfc = new System.Security.Cryptography.Rfc2898DeriveBytes( pwdValue, salt );
 
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            aes.Key = rfc.GetBytes( aes.KeySize / 8 );
            aes.IV = rfc.GetBytes( aes.BlockSize / 8 );
 
            // 用当前的 Key 属性和初始化向量 IV 创建对称加密器对象 
            System.Security.Cryptography.ICryptoTransform encryptTransform = aes.CreateEncryptor( );
            // 加密后的输出流 
            System.IO.MemoryStream encryptStream = new System.IO.MemoryStream( );
            // 将加密后的目标流（encryptStream）与加密转换（encryptTransform）相连接 
            System.Security.Cryptography.CryptoStream encryptor = new System.Security.Cryptography.CryptoStream
                ( encryptStream, encryptTransform, System.Security.Cryptography.CryptoStreamMode.Write );
 
            // 将一个字节序列写入当前 CryptoStream （完成加密的过程）
            encryptor.Write( data, 0, data.Length );
            encryptor.Close( );
            // 将加密后所得到的流转换成字节数组，再用Base64编码将其转换为字符串 
            string encryptedString = Convert.ToBase64String( encryptStream.ToArray( ) );
            return encryptedString;
        }
        /// <summary>
        /// 解密
        /// </summary>
        public static string AESDecrypt(string input) {
            byte[ ] encryptBytes = Convert.FromBase64String( input );
            byte[ ] salt = Encoding.UTF8.GetBytes( saltValue );
            System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged( );
            System.Security.Cryptography.Rfc2898DeriveBytes rfc = new System.Security.Cryptography.Rfc2898DeriveBytes( pwdValue, salt );
 
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            aes.Key = rfc.GetBytes( aes.KeySize / 8 );
            aes.IV = rfc.GetBytes( aes.BlockSize / 8 );
 
            // 用当前的 Key 属性和初始化向量 IV 创建对称解密器对象 
            System.Security.Cryptography.ICryptoTransform decryptTransform = aes.CreateDecryptor( );
            // 解密后的输出流 
            System.IO.MemoryStream decryptStream = new System.IO.MemoryStream( );
 
            // 将解密后的目标流（decryptStream）与解密转换（decryptTransform）相连接 
            System.Security.Cryptography.CryptoStream decryptor = new System.Security.Cryptography.CryptoStream(
                decryptStream, decryptTransform, System.Security.Cryptography.CryptoStreamMode.Write );
            // 将一个字节序列写入当前 CryptoStream （完成解密的过程） 
            decryptor.Write( encryptBytes, 0, encryptBytes.Length );
            decryptor.Close( );
 
            // 将解密后所得到的流转换为字符串 
            byte[ ] decryptBytes = decryptStream.ToArray( );
            string decryptedString = UTF8Encoding.UTF8.GetString( decryptBytes, 0, decryptBytes.Length );
            return decryptedString;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="decryptString">原文</param>
        /// <returns>密文</returns>
        public static string MD5Encrypt(string encryptString) {
            byte[] result = Encoding.Default.GetBytes(encryptString);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString">原文</param>
        /// <returns>密文</returns>
        public static string DesEncrypt(string encryptString) {
            byte[] keyBytes = Encoding.UTF8.GetBytes(SecurityHelper.DesKey);
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密数据库连接串
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <returns>原文</returns>
        public static string DesDecryptConnection(string decryptString) {
            byte[] keyBytes = Encoding.UTF8.GetBytes(SecurityHelper.DesKey);
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <returns>原文</returns>
        public static string DesDecrypt(string decryptString) {
            byte[] keyBytes = Encoding.UTF8.GetBytes(SecurityHelper.DesKey);
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        /// <summary>
        /// DES加密 - 使用指定的8位长度的密钥进行加密
        /// </summary>
        /// <param name="encryptString">原文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static string DesEncrypt(string encryptString, string key) {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密 - 使用指定的8位长度的密钥进行解密
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>原文</returns>
        public static string DesDecrypt(string decryptString, string key) {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(string source) {
            return EncodeBase64("utf-8", source);
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(string result) {
            return DecodeBase64("utf-8", result);
        }

        /// <summary>
        /// 将字符串使用base64算法加密
        /// </summary>
        /// <param name="code_type">编码类型（编码名称）
        /// * 代码页 名称
        /// * 1200 "UTF-16LE"、"utf-16"、"ucs-2"、"unicode"或"ISO-10646-UCS-2"
        /// * 1201 "UTF-16BE"或"unicodeFFFE"
        /// * 1252 "windows-1252"
        /// * 65000 "utf-7"、"csUnicode11UTF7"、"unicode-1-1-utf-7"、"unicode-2-0-utf-7"、"x-unicode-1-1-utf-7"或"x-unicode-2-0-utf-7"
        /// * 65001 "utf-8"、"unicode-1-1-utf-8"、"unicode-2-0-utf-8"、"x-unicode-1-1-utf-8"或"x-unicode-2-0-utf-8"
        /// * 20127 "us-ascii"、"us"、"ascii"、"ANSI_X3.4-1968"、"ANSI_X3.4-1986"、"cp367"、"csASCII"、"IBM367"、"iso-ir-6"、"ISO646-US"或"ISO_646.irv:1991"
        /// * 54936 "GB18030"
        /// </param>
        /// <param name="code">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(string code_type, string code) {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);  //将一组字符编码为一个字节序列.
            try {
                encode = Convert.ToBase64String(bytes);  //将8位无符号整数数组的子集转换为其等效的,以64为基的数字编码的字符串形式.
            }
            catch {
                encode = code;
            }
            return encode;
        }

        /// <summary>
        /// 将字符串使用base64算法解密
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code">已用base64算法加密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(string code_type, string code) {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);  //将2进制编码转换为8位无符号整数数组.
            try {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);  //将指定字节数组中的一个字节序列解码为一个字符串。
            }
            catch {
                decode = code;
            }
            return decode;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncryToString<T>(T source) {
            string returnStr = string.Empty;
            //returnStr = HttpUtility.UrlEncode(DesEncrypt(source.ToString()));
            returnStr = DesEncrypt(source.ToString());
            return returnStr;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DecryFromString<T>(T source) {
            string returnStr = string.Empty;
            //returnStr = DesDecrypt(HttpUtility.UrlDecode(source.ToString()));
            returnStr = DesDecrypt(source.ToString());
            return returnStr;
        }
    }
}