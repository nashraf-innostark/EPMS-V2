using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers.NotificationMapper;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Implementation.Services
{
    public class NotificationService:INotificationService
    {
        private readonly IEmployeeRequestRepository requestRepository;
        private readonly IMenuRepository menuRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly INotificationRecipientRepository notificationRecipientRepository;
        private readonly INotificationRepository notificationRepository;
        private readonly IEmployeeRepository employeeRepository;

        public NotificationService(IEmployeeRequestRepository requestRepository,IMenuRepository menuRepository, IAspNetUserRepository aspNetUserRepository, INotificationRecipientRepository notificationRecipientRepository, INotificationRepository notificationRepository, IEmployeeRepository employeeRepository)
        {
            this.requestRepository = requestRepository;
            this.menuRepository = menuRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.notificationRecipientRepository = notificationRecipientRepository;
            this.notificationRepository = notificationRepository;
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<NotificationResponse> GetAll()
        {
            return notificationRepository.GetAll().Select(x => x.CreateFromServerToClient());
        }

        public NotificationResponse FindNotification(long notificationId)
        {
            return notificationRepository.Find(notificationId).CreateFromServerToClient();
        }

        public NotificationViewModel LoadNotificationAndBaseData(long? notificationId)
        {
            NotificationViewModel notificationViewModel=new NotificationViewModel();
            IEnumerable<Employee> employees = employeeRepository.GetAll().ToList();
            if (employees.Any())
            {                
                notificationViewModel.EmployeeDDL = employees.Select(x => x.CreateForEmployeeDDL()).ToList();
            }
            if (notificationId != null && notificationId > 0)
            {
                Notification notification = notificationRepository.Find((long) notificationId);
                if(notification!=null)
                    notificationViewModel.NotificationResponse = notification.CreateFromServerToClient();
            }
            return notificationViewModel;
        }

        public NotificationViewModel LoadNotificationDetailsAndBaseData(long? notificationId,string userId)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();
            IEnumerable<Employee> employees = employeeRepository.GetAll().ToList();
            
            if (employees.Any())
            {
                notificationViewModel.EmployeeDDL = employees.Select(x => x.CreateForEmployeeDDL()).ToList();
            }
            if (notificationId != null && notificationId > 0)
            {
                Notification notification = notificationRepository.Find((long)notificationId);
                if (notification != null)
                {
                    //Mark the notification as READ
                    if (notification.SystemGenerated)
                    {
                        //Save, who viewed SystemGenerated notification
                        if (notification.NotificationRecipients.All(x => x.UserId != userId))
                        {
                            NotificationRecipient recipient = new NotificationRecipient();
                            recipient.UserId = userId;
                            recipient.NotificationId = notification.NotificationId;
                            recipient.IsRead = true;
                            notificationRecipientRepository.Add(recipient);
                            notificationRecipientRepository.SaveChanges();
                        }
                        else
                        {
                            notification.NotificationRecipients.FirstOrDefault().IsRead = true;
                            notificationRecipientRepository.Update(notification.NotificationRecipients.FirstOrDefault());
                            notificationRecipientRepository.SaveChanges();
                        }
                    }
                    //Mark as READ, who viewed Manual notification
                    else if (!notification.NotificationRecipients.FirstOrDefault().IsRead)
                    {
                        notification.NotificationRecipients.FirstOrDefault().IsRead = true;
                        notificationRecipientRepository.Update(notification.NotificationRecipients.FirstOrDefault());
                        notificationRecipientRepository.SaveChanges();
                    }
                    notificationViewModel.NotificationResponse = notification.CreateFromServerToClient();
                }
            }
            return notificationViewModel;
        }

        public bool AddUpdateNotification(NotificationResponse notificationResponse)
        {
            //Get UserId of selected employee
            if (notificationResponse.EmployeeId > 0)
                notificationResponse.UserId =
                    aspNetUserRepository.GetUserIdByEmployeeId(notificationResponse.EmployeeId);

            if (notificationResponse.NotificationId > 0)
            {
                //Update notification
                notificationResponse.NotificationId = UpdateNotification(notificationResponse);
                //Delete Notification recipient
                if(notificationRecipientRepository.DeleteRecipient(notificationResponse.NotificationId))
                    notificationRepository.SaveChanges();
            }
            else
            {
                //Save Notification
                notificationResponse.NotificationId=AddNotification(notificationResponse);
            }
            //Save Notification recipient
            if(!(string.IsNullOrEmpty(notificationResponse.UserId) && notificationResponse.EmployeeId==0))
                AddNotificationRecipient(notificationResponse.CreateRecipientFromClientToServer());
            return true;
        }

        public bool AddUpdateMeetingNotification(NotificationViewModel notificationViewModel, List<long> employeeIds)
        {
            if (notificationViewModel.NotificationResponse.NotificationId > 0)
            {
                //Update notification
                notificationViewModel.NotificationResponse.NotificationId = UpdateNotification(notificationViewModel.NotificationResponse);
                //Delete Notification recipient
                if (notificationRecipientRepository.DeleteRecipient(notificationViewModel.NotificationResponse.NotificationId))
                    notificationRepository.SaveChanges();
            }
            else
            {
                //Save Notification
                notificationViewModel.NotificationResponse.NotificationId = AddNotification(notificationViewModel.NotificationResponse);
            }
            NotificationRecipient newNotificationRecipient = new NotificationRecipient();
            foreach (var employeeId in employeeIds)
            {
                newNotificationRecipient.UserId = aspNetUserRepository.GetUserIdByEmployeeId(employeeId);
                if (string.IsNullOrEmpty(newNotificationRecipient.UserId))
                    newNotificationRecipient.UserId = null;
                newNotificationRecipient.EmployeeId = employeeId;
                newNotificationRecipient.NotificationId = notificationViewModel.NotificationResponse.NotificationId;
                AddNotificationRecipient(newNotificationRecipient);
            }
            return true;
        }

        public long AddNotification(NotificationResponse notification)
        {
            var _notification = notification.CreateFromClientToServer();
            notificationRepository.Add(_notification);
            notificationRepository.SaveChanges();
            return _notification.NotificationId;
        }
        public long UpdateNotification(NotificationResponse notification)
        {
            notificationRepository.Update(notification.CreateFromClientToServer());
            notificationRepository.SaveChanges();
            return notification.NotificationId;
        }
        public long AddNotificationRecipient(NotificationRecipient recipient)
        {
            notificationRecipientRepository.Add(recipient);
            notificationRecipientRepository.SaveChanges();
            return recipient.Id;
        }
        public long UpdateNotificationRecipient(NotificationRecipient recipient)
        {
            notificationRecipientRepository.Update(recipient);
            notificationRecipientRepository.SaveChanges();
            return recipient.Id;
        }
        public int LoadUnreadNotificationsCount(NotificationRequestParams requestParams)
        {
            return notificationRepository.GetUnreadNotificationsCount(requestParams);
        }
        public NotificationListView LoadAllNotifications(NotificationListViewRequest searchRequset)
        {
            NotificationListView notificationListView = new NotificationListView();
            var notifications = notificationRepository.GetAllNotifications(searchRequset);
            if (notifications.Notifications.Any())
            {
                notificationListView.aaData = notifications.Notifications.Select(x => x.CreateFromServerToClientList());
            }
            else
                notificationListView.aaData = Enumerable.Empty<NotificationListResponse>();

            notificationListView.iTotalDisplayRecords = notifications.TotalFiltered;
            notificationListView.iTotalRecords = notifications.TotalFiltered;
            notificationListView.sEcho = searchRequset.sEcho;
            return notificationListView;
        }
        public NotificationListView LoadAllSentNotifications(NotificationListViewRequest searchRequset)
        {
            NotificationListView notificationListView = new NotificationListView();
            var notifications = notificationRepository.GetAllSentNotifications(searchRequset);
            if (notifications.Notifications.Any())
            {
                notificationListView.aaData = notifications.Notifications.Select(x => x.CreateFromServerToClientList());

            }
            else
                notificationListView.aaData = Enumerable.Empty<NotificationListResponse>();

            notificationListView.iTotalDisplayRecords = notifications.TotalFiltered;
            notificationListView.iTotalRecords = notifications.TotalFiltered;
            notificationListView.sEcho = searchRequset.sEcho;
            return notificationListView;
        }
        public void CreateNotification(string notificationFor, long itemId, DateTime? alertDate)
        {
            if (!Utility.IsDate(alertDate)) return;

            NotificationResponse notificationResponse = new NotificationResponse();
            switch (notificationFor)
            {


                case "iqama":
                    #region Iqama Expiry Date
                    notificationResponse.EmployeeId = itemId;
                    notificationResponse.CategoryId = 3; //Employees
                    notificationResponse.SubCategoryId = 1;//Iqama
                    notificationResponse.NotificationId = notificationRepository.GetNotificationsIdByCategories(notificationResponse.CategoryId, notificationResponse.SubCategoryId, itemId);
                    notificationResponse.TitleE = ConfigurationManager.AppSettings["IqamaE"];
                    notificationResponse.TitleA = ConfigurationManager.AppSettings["IqamaA"];
                    notificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["IqamaAlertBefore"]); //Days
                    notificationResponse.ForAdmin = true;
                    notificationResponse.AlertDateType = 0; //Hijri, 1=Gregorian
                    notificationResponse.UserId = aspNetUserRepository.GetUserIdByEmployeeId(notificationResponse.EmployeeId);
                    #endregion
                    break;

                case "passport":
                    #region Passport Expiry Date
                    notificationResponse.EmployeeId = itemId;
                    notificationResponse.CategoryId = 3; //Employees
                    notificationResponse.SubCategoryId = 2;//Passport
                    notificationResponse.NotificationId = notificationRepository.GetNotificationsIdByCategories(notificationResponse.CategoryId, notificationResponse.SubCategoryId, itemId);
                    notificationResponse.TitleE = ConfigurationManager.AppSettings["PassportE"];
                    notificationResponse.TitleA = ConfigurationManager.AppSettings["PassportA"];
                    notificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["PassportAlertBefore"]); //Days
                    notificationResponse.ForAdmin = true;
                    notificationResponse.AlertDateType = 0; //Hijri, 1=Gregorian
                    notificationResponse.UserId = aspNetUserRepository.GetUserIdByEmployeeId(notificationResponse.EmployeeId);
                    #endregion
                    break;
            }

            notificationResponse.ItemId = itemId;
            notificationResponse.AlertDate = Convert.ToDateTime(alertDate).ToShortDateString();

            notificationResponse.SystemGenerated = true;
            AddUpdateNotification(notificationResponse);
        }
        public void SendEmailNotifications()
        {
            var notifications = notificationRepository.SendEmailNotifications();
            if (notifications.Any())
            {
                NotificationResponse notificationResponse = new NotificationResponse();
                long permisssionMenuId = menuRepository.GetMenuIdByPermissionKey("SystemGenerated");
                var adminEmployees = employeeRepository.GetAdminEmployees(permisssionMenuId);
                foreach (var notification in notifications)
                {
                    notificationResponse = notification.CreateFromServerToClient();
                    notificationResponse.NotificationCode = notificationResponse.CategoryId + notificationResponse.SubCategoryId.ToString();
                    if (!notification.IsEmailSent)
                    {
                        List<string> emailRecipients = new List<string>();
                        List<string> smsRecipients = new List<string>();

                        if (notification.ForAdmin != null && notification.ForAdmin == true)
                        {
                            //Send the notification to its all recipients
                            if (adminEmployees != null)
                            {
                                var adminEmployeesList = adminEmployees as IList<Employee> ?? adminEmployees.ToList();
                                foreach (var admin in adminEmployeesList)
                                {
                                    emailRecipients.Add(admin.Email);
                                    smsRecipients.Add(admin.EmployeeMobileNum);
                                }
                                notificationResponse.Email = emailRecipients.Aggregate((i, j) => i + ";" + j);
                                notificationResponse.MobileNo = smsRecipients.Aggregate((i, j) => i + ";" + j);
                                notificationResponse.TextForAdmin = true;
                                GenerateNotificationDescription(notificationResponse);

                                SendNotificationSms(notificationResponse);
                                SentNotificationEmail(notificationResponse);
                            }
                        }
                        if (notification.NotificationRecipients.Any())
                        {
                            emailRecipients = new List<string>();
                            smsRecipients = new List<string>();
                            //Send the notification to its all recipients
                            foreach (var recipient in notification.NotificationRecipients)
                            {
                                if (string.IsNullOrEmpty(recipient.UserId))
                                {
                                    if (recipient.EmployeeId != null)
                                    {
                                        var employee = employeeRepository.Find(Convert.ToInt64(recipient.EmployeeId));
                                        emailRecipients.Add(employee.Email);
                                        smsRecipients.Add(employee.EmployeeMobileNum);
                                    }
                                    
                                    if (recipient.AspNetUser.Customer != null)
                                    {
                                        emailRecipients.Add(recipient.AspNetUser.Email);
                                        smsRecipients.Add(recipient.AspNetUser.Customer.CustomerMobile);
                                    }
                                }
                                else
                                {
                                    emailRecipients.Add(recipient.AspNetUser.Email);
                                    if (recipient.AspNetUser.Customer != null)
                                    {
                                        smsRecipients.Add(recipient.AspNetUser.Customer.CustomerMobile);
                                    }
                                    if (recipient.AspNetUser.Employee != null)
                                    {
                                        smsRecipients.Add(recipient.AspNetUser.Employee.EmployeeMobileNum);
                                    }
                                }
                            }
                            notificationResponse.Email = emailRecipients.Aggregate((i, j) => i + ";" + j);
                            notificationResponse.MobileNo = smsRecipients.Aggregate((i, j) => i + ";" + j);
                            notificationResponse.TextForAdmin = false;
                            GenerateNotificationDescription(notificationResponse);

                            SendNotificationSms(notificationResponse);
                            SentNotificationEmail(notificationResponse);
                        }
                    }
                   
                    //Update notification
                    notification.IsSMSsent = true;
                    notification.IsEmailSent = true;
                    notificationRepository.Update(notification);
                }
                notificationRepository.SaveChanges();
            }
        }
        private void SendNotificationSms(NotificationResponse notificationResponse)
        {
            if (!string.IsNullOrEmpty(notificationResponse.MobileNo))
            {
                Utility.SendNotificationSms(notificationResponse.SmsText, notificationResponse.MobileNo);
            }
        }
        private void SentNotificationEmail(NotificationResponse notificationResponse)
        {
            if (!string.IsNullOrEmpty(notificationResponse.Email))
            {
                Utility.SendEmail(notificationResponse.Email,notificationResponse.TitleE + "/" + notificationResponse.TitleA, notificationResponse.EmailText);
            }
        }
        public void GenerateNotificationDescription(NotificationResponse notificationResponse)
        {
            
            string smsText = string.Empty;
            string emailText = string.Empty;

            switch (notificationResponse.NotificationCode)
            {
                case "10":
                    //CommercialRegisterExpiryDate
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "11":
                    //InsuranceCertificateExpiryDate
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "12":
                    //ChamberCertificateExpiryDate
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "13":
                    //IncomeAndZakaCertificateExpiryDate
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "14":
                    //SaudilizationCertificateExpiryDate
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "30":
                    //Employee Request
                    smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "31":
                    //Iqama
                    smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeIqamaExpirySMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeIqamaExpiryEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "32":
                    //Passport
                    smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeePassportExpirySMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeePassportExpiryEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "40":
                    //Meeting
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "51":
                    //JobApplication Admin
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "52":
                    //ProjectFinished Customer
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "53":
                    //TaskDelivery Admin
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "58":
                    //ProjectStarted Customer
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "59":
                    //ProjectDelivery Admin
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "510":
                    //TaskAssigned
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "511":
                    //FirstInsDueAtCompletion
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "512":
                    //SecondInsDueAtCompletion
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "513":
                    //ThirdInsDueAtCompletion
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
                case "514":
                    //FourthInsDueAtCompletion
                     smsText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestSMS.txt");
                    notificationResponse.SmsText = ReplaceTags(smsText, notificationResponse);
                    emailText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\NotificationsTextFiles\EmployeeRequestEmail.txt");
                    notificationResponse.EmailText = ReplaceTags(emailText, notificationResponse);
                    break;
            }
        }

        private string ReplaceTags(string fileText, NotificationResponse notificationResponse)
        {
            //fileText=fileText.Replace("", "");
            if (notificationResponse.EmployeeId > 0)
            {
                var employee = employeeRepository.Find(Convert.ToInt64(notificationResponse.EmployeeId));
                if (employee != null)
                {
                    fileText = fileText.Replace("[EmployeeNameEng]", notificationResponse.TextForAdmin?employee.EmployeeNameE:"Your");
                    fileText = fileText.Replace("[EmployeeNameAr]", notificationResponse.TextForAdmin ? employee.EmployeeNameA : "Your");
                    fileText = fileText.Replace("[IqamaExpiryDate]", DateTime.ParseExact(notificationResponse.AlertDate, "dd/MM/yyyy", new CultureInfo("en")).ToShortDateString());
                    fileText = fileText.Replace("[PassportExpiryDate]", DateTime.ParseExact(notificationResponse.AlertDate, "dd/MM/yyyy", new CultureInfo("en")).ToShortDateString());
                }
            }
            if (notificationResponse.NotificationCode == "30")//Employee Request
            {
                var request = requestRepository.Find(notificationResponse.ItemId);
               
                fileText = fileText.Replace("[RequestStatusEng]", request.RequestDetails.FirstOrDefault(x => x.IsReplied).IsApproved ? "accepted" : "declined");
                fileText = fileText.Replace("[RequestStatusAr]", request.RequestDetails.FirstOrDefault(x => x.IsReplied).IsApproved ? "قبلت" : "ورفض");

                fileText = fileText.Replace("[RequestTopic]", request.RequestTopic);
            }
            return fileText;
        }
    }
}
