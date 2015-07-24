using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Script.Serialization;
using EPMS.Implementation;

namespace EPMS.Web
{
    public class Utility
    {
        public static string GenerateFormNumber(string form, string formNumber)
        {
            // get number 
            string num = formNumber.Substring(2);
            long number = Convert.ToInt64(num);
            // Add 1
            number++;
            string noOfZeros = "";
            switch (number.ToString().Length)
            {
                case 1:
                    noOfZeros = "0000000";
                    break;
                case 2:
                    noOfZeros = "000000";
                    break;
                case 3:
                    noOfZeros = "00000";
                    break;
                case 4:
                    noOfZeros = "0000";
                    break;
                case 5:
                    noOfZeros = "000";
                    break;
                case 6:
                    noOfZeros = "00";
                    break;
                case 7:
                    noOfZeros = "0";
                    break;
                case 8:
                    noOfZeros = "";
                    break;
            }
            return form + noOfZeros + number;
        }

        public static void SendEmailAsync(string email, string subject, string body)
        {

            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            string fromPwd = ConfigurationManager.AppSettings["FromPassword"];
            string fromDisplayName = ConfigurationManager.AppSettings["FromDisplayNameA"];
            //string cc = ConfigurationManager.AppSettings["CC"];
            //string bcc = ConfigurationManager.AppSettings["BCC"];

            //Getting the file from config, to send
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
        public bool SendSms(string smsText, string mobileNo)
        {
            string username = ConfigurationManager.AppSettings["MobileUsername"];
            string password = ConfigurationManager.AppSettings["MobilePassword"];
            string smsApiUrl = ConfigurationManager.AppSettings["SmsApiUrl"];
            string tagName = ConfigurationManager.AppSettings["TagName"];

            WebRequest smsRequest = WebRequest.Create(smsApiUrl);
            smsRequest.ContentType = "application/json";
            smsRequest.Method = "POST";

            SMS sms = new SMS();
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
}