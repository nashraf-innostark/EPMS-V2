using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Script.Serialization;

namespace EPMS.Implementation
{
    public class Utility
    {
        public static void SendEmail(string email, string subject, string body)
        {

            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            string fromPwd = ConfigurationManager.AppSettings["FromPassword"];
            string fromDisplayName = ConfigurationManager.AppSettings["FromDisplayNameA"];
            //string cc = ConfigurationManager.AppSettings["CC"];
            //string bcc = ConfigurationManager.AppSettings["BCC"];

            MailMessage oEmail = new MailMessage
            {
                From = new MailAddress(fromAddress, fromDisplayName),
                Subject = subject,
                IsBodyHtml = true,
                Body = body,
                Priority = MailPriority.High
            };
            oEmail.To.Add(email);
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            string enableSsl = ConfigurationManager.AppSettings["EnableSSL"];
            SmtpClient client = new SmtpClient(smtpServer, Convert.ToInt32(smtpPort))
            {
                EnableSsl = enableSsl == "1",
                Credentials = new NetworkCredential(fromAddress, fromPwd)
            };

            client.Send(oEmail);
        }
        public static bool SendNotificationSms(string smsText, string mobileNo)
        {
            string username = ConfigurationManager.AppSettings["MobileUsername"];
            string password = ConfigurationManager.AppSettings["MobilePassword"];
            string smsApiUrl = ConfigurationManager.AppSettings["SmsApiUrl"];
            string tagName = ConfigurationManager.AppSettings["TagName"];

            WebRequest smsRequest = WebRequest.Create(smsApiUrl);
            smsRequest.ContentType = "application/json";
            smsRequest.Method = "POST";

            SMS sms=new SMS();
            sms.Username = username;
            sms.Password = password;
            sms.Tagname = tagName;
            sms.RecepientNumber = mobileNo;
            sms.VariableList = "";
            sms.ReplacementList = "";
            sms.Message = smsText;
            sms.SendDateTime = "0";
            sms.EnableDR = false;

            using (var streamWriter = new StreamWriter(smsRequest.GetRequestStream()))
            {
                var json = new JavaScriptSerializer().Serialize(sms);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse smsRequestResponse = smsRequest.GetResponse();
            Stream smsDataStream = smsRequestResponse.GetResponseStream();
            StreamReader smsReader = new StreamReader(smsDataStream);
            string smsResponse = smsReader.ReadToEnd();

            if (smsResponse.ToLower().Contains("success"))
            {
                return true;
            }
            return false;
        }
        //public static bool SendNotificationSms(string smsText, string mobileNo)
        //{
        //    string username = ConfigurationManager.AppSettings["MobileUsername"];
        //    string password = ConfigurationManager.AppSettings["MobilePassword"];
        //    string senderId = ConfigurationManager.AppSettings["SenderID"];

        //    WebRequest smsRequest =
        //        WebRequest.Create("http://www.jawalbsms.ws/api.php/sendsms?user=" + username + "&pass=" +
        //                          password +
        //                          "&to=" + mobileNo + "&message=" + smsText +
        //                          "&sender=" + senderId);
        //    WebResponse smsRequestResponse = smsRequest.GetResponse();
        //    Stream smsDataStream = smsRequestResponse.GetResponseStream();
        //    StreamReader smsReader = new StreamReader(smsDataStream);
        //    string smsResponse = smsReader.ReadToEnd();

        //    if (smsResponse.ToLower().Contains("success"))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        public static bool IsDate(Object obj)
        {
            
            try
            {
                if (obj != null)
                {
                    string strDate = obj.ToString();
                    DateTime dt = DateTime.Parse(strDate);
                    if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                        return true;
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }

    public class SMS
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Tagname { get; set; }
        public string RecepientNumber { get; set; }
        public string VariableList { get; set; }
        public string ReplacementList { get; set; }
        public string Message { get; set; }
        public string SendDateTime { get; set; }
        public bool EnableDR { get; set; }

    }
}
