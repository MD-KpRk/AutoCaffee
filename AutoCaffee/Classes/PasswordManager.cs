using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AutoCaffee.Classes
{
    public static class PasswordManager
    {
        static byte[] stringToByteHash(string str)
        {
            byte[] tmpHash;
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(str));
            return tmpHash;
        }

        static string bytesToStringHash(byte[] byteArr)
        {
            StringBuilder sOutput = new StringBuilder(byteArr.Length);
            for (int i = 0; i < byteArr.Length; i++) sOutput.Append(byteArr[i].ToString("X2"));
            return sOutput.ToString();
        }

        public static string stringToStringHash(string str) => bytesToStringHash(stringToByteHash(str));

    }
}
