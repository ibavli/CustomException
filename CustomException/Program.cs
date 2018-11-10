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
            Console.Read();
        }

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
    }
}
