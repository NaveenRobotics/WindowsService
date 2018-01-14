using System;
using System.IO;
using System.Net;
using System.Text;

namespace MyWindowsService
{
    class HelperClass
    {
        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public void SendNotification(Message m)
        {
            string url = "https://prod-25.centralindia.logic.azure.com:443/workflows/e4c1c5b5bb894c7abf34aa4e66a967c0/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=883QeI4MLNFAC4CXwPSw_4YAaAkdz-QI_V2sDnLcRZk";
            WebRequest request = WebRequest.Create(url);

            request.Method = "POST";

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(m);
            WriteLog(postData);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();
        }

        public void WriteLog(String message)
        {
            File.AppendAllText(@"C:\tmp\ServiceLog.txt", DateTime.Now.ToString("dd/MM/yyyy h:mm tt")+" : " +message);
        }

        public string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }
    }
}
