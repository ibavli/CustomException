using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomException
{
    class Program
    {
        static void Main(string[] args)
        {
            Login("ali", "veli");
            Connect("https://www.testxxx.com");

            Not(5);
            Not(101);
            Not(null);

            BolmeIslemi("6", "3");
            BolmeIslemi("6", "0");
            BolmeIslemi("0", "5");
            BolmeIslemi("xxx", "xxx");

            Console.Read();
        }


        public static void Login(string userName, string password)
        {
            try
            {
                if (userName != "admin" && password != "123456789")
                    throw new LoginException("Oturum Açma İşlemi Başarısız");
            }
            catch (LoginException loginException)
            {
                //Save to database the error
                Console.WriteLine(loginException.Message);
            }

        }


        public static void Connect(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = null;

            try
            {
                try
                {
                    response = request.GetResponse();
                    Console.WriteLine("Siteye bağlandı");
                    Stream str = response.GetResponseStream();
                    StreamReader reader = new StreamReader(str);
                    string source = reader.ReadToEnd();
                    Console.WriteLine(source);
                }
                catch
                {
                    throw new ConnectException("Bağlantı kurulamadı");
                }


            }
            catch (ConnectException connectException)
            {
                //Save to database the error
                Console.WriteLine(connectException.Message);
            }
        }

        #region Örnek exception

        public static void Not(byte? not)
        {
            try
            {
                if (not == null)
                {
                    throw new ArgumentNullException();
                }
                byte _not = Convert.ToByte(not);

                if (_not < 45)
                {
                    Console.WriteLine("Kaldınız");
                }
                else if (_not > 45 && _not < 100)
                {
                    Console.WriteLine("Geçtiniz");
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("Değer sınır dışı");
            }
            catch (FormatException)
            {
                Console.WriteLine("Format hatası");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Değer null olamaz");
            }
        }

        public static void BolmeIslemi(string _sayi1, string _sayi2)
        {
            try
            {
                int sayi1 = Convert.ToInt32(_sayi1);
                int sayi2 = Convert.ToInt32(_sayi2);
                double sonuc = sayi1 / sayi2;
                Console.WriteLine(sonuc.ToString());
            }
            catch (FormatException ex) 
            {
                Console.WriteLine("Lütfen doğru formatta veri giriniz.");
            }
            catch (OverflowException ex) //Değişken sınırları dışı
            {
                Console.WriteLine("Değişken sınırları dışına çıktınız.");
            }
            catch (DivideByZeroException ex) //Sayıları sıfıra bölmeye çalışırsa
            {
                Console.WriteLine("Sıfıra bölünme hatası. Sayı sıfıra bölünemez.");
            }
            catch (Exception ex) //Geri kalan tüm hataları denetler
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion

        #region Login exception

        public class LoginException : Exception
        {

            public LoginException() : base()
            {

            }

            public LoginException(String message) : base(message)
            {

            }

            public LoginException(String message, Exception innerException) : base(message, innerException)
            {

            }

            protected LoginException(SerializationInfo info, StreamingContext context) : base(info, context)
            {

            }

        }

        #endregion

        #region Connect exception

        public class ConnectException : Exception
        {
            public ConnectException() : base()
            {

            }

            public ConnectException(String message) : base(message)
            {

            }

            public ConnectException(String message, Exception innerException) : base(message, innerException)
            {

            }

            protected ConnectException(SerializationInfo info, StreamingContext context) : base(info, context)
            {

            }
        }

        #endregion


    }
}
