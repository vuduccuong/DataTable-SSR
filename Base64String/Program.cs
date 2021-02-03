using System;
using System.Text;

namespace Base64String
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string strInput = Console.ReadLine();
            
            string strEncode = EncodeBase64(strInput);
            string strDecode = DecodeBase64(strEncode);
            Console.WriteLine("Chuỗi mã hoá: {0} ", strEncode);
            Console.WriteLine("Chuỗi giải mã: {0} ", strDecode);
            Console.ReadLine();
        }

        private static string DecodeBase64(string strEncode)
        {
            if (String.IsNullOrEmpty(strEncode))
            {
                return String.Empty;
            }
            var strBytes = Convert.FromBase64String(strEncode);
            return Encoding.UTF8.GetString(strBytes);
        }

        private static string EncodeBase64(string strInput)
        {
            if (String.IsNullOrEmpty(strInput))
            {
                return String.Empty;
            }
            var strBytes = Encoding.UTF8.GetBytes(strInput);
            return Convert.ToBase64String(strBytes);
        }
    }
}
