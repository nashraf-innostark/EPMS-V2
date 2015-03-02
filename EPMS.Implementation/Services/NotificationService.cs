using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly IMenuRepository menuRepository;
        private readonly AspNetUserService aspNetUserService;
        private readonly INotificationRecipientRepository notificationRecipientRepository;
        private readonly INotificationRepository notificationRepository;
        private readonly IEmployeeRepository employeeRepository;

        public NotificationService(IMenuRepository menuRepository,AspNetUserService aspNetUserService,INotificationRecipientRepository notificationRecipientRepository,INotificationRepository notificationRepository, IEmployeeRepository employeeRepository)
        {
            this.menuRepository = menuRepository;
            this.aspNetUserService = aspNetUserService;
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

        public bool AddUpdateNotification(NotificationViewModel notificationViewModel)
        {
            //Get UserId of selected employee
            if (notificationViewModel.NotificationResponse.EmployeeId > 0)
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserService.GetUserIdByEmployeeId(notificationViewModel.NotificationResponse.EmployeeId);

            if (notificationViewModel.NotificationResponse.NotificationId > 0)
            {
                notificationViewModel.NotificationResponse.RecLastUpdatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;
                //Update notification
                notificationViewModel.NotificationResponse.NotificationId = UpdateNotification(notificationViewModel.NotificationResponse);
                //Delete Notification recipient
                if(notificationRecipientRepository.DeleteRecipient(notificationViewModel.NotificationResponse.NotificationId))
                    notificationRepository.SaveChanges();
            }
            else
            {
                notificationViewModel.NotificationResponse.RecCreatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecCreatedDate = DateTime.Now;
                notificationViewModel.NotificationResponse.RecLastUpdatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;
                //Save Notification
                notificationViewModel.NotificationResponse.NotificationId=AddNotification(notificationViewModel.NotificationResponse);
            }
            //Save Notification recipient
            if(!(string.IsNullOrEmpty(notificationViewModel.NotificationResponse.UserId) && notificationViewModel.NotificationResponse.EmployeeId==0))
                AddNotificationRecipient(notificationViewModel.NotificationResponse.CreateRecipientFromClientToServer());
            return true;
        }

        public bool AddUpdateMeetingNotification(NotificationViewModel notificationViewModel, List<long> employeeIds)
        {
            if (notificationViewModel.NotificationResponse.NotificationId > 0)
            {
                notificationViewModel.NotificationResponse.RecLastUpdatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;
                //Update notification
                notificationViewModel.NotificationResponse.NotificationId = UpdateNotification(notificationViewModel.NotificationResponse);
                //Delete Notification recipient
                if (notificationRecipientRepository.DeleteRecipient(notificationViewModel.NotificationResponse.NotificationId))
                    notificationRepository.SaveChanges();
            }
            else
            {
                notificationViewModel.NotificationResponse.RecCreatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecCreatedDate = DateTime.Now;
                notificationViewModel.NotificationResponse.RecLastUpdatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;
                //Save Notification
                notificationViewModel.NotificationResponse.NotificationId = AddNotification(notificationViewModel.NotificationResponse);
            }
            NotificationRecipient newNotificationRecipient = new NotificationRecipient();
            foreach (var employeeId in employeeIds)
            {
                newNotificationRecipient.UserId = aspNetUserService.GetUserIdByEmployeeId(employeeId);
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
        public long AddNotificationRecipient(NotificationRecipient recipient)
        {
            notificationRecipientRepository.Add(recipient);
            notificationRecipientRepository.SaveChanges();
            return recipient.Id;
        }
        public long UpdateNotification(NotificationResponse notification)
        {
            notificationRepository.Update(notification.CreateFromClientToServer());
            notificationRepository.SaveChanges();
            return notification.NotificationId;
        }
        public long UpdateNotificationRecipient(NotificationRecipient recipient)
        {
            notificationRecipientRepository.Update(recipient);
            notificationRecipientRepository.SaveChanges();
            return recipient.Id;
        }

        public void SendEmailNotifications()
        {
            var notifications = notificationRepository.SendEmailNotifications();
            if (notifications.Any())
            {
                NotificationResponse notificationResponse = new NotificationResponse();
                long permisssionMenuId = menuRepository.GetMenuIdByPermissionKey("SystemGenerated");
                var employees = employeeRepository.GetAdminEmployees(permisssionMenuId);
                foreach (var notification in notifications)
                {
                    if (!notification.IsEmailSent)
                    {
                        if (notification.SystemGenerated)
                        {
                            //Send the notification to its all recipients
                            if (employees != null)
                            {
                                var employeesList = employees as IList<Employee> ?? employees.ToList();
                                foreach (var employee in employeesList)
                                {
                                    notificationResponse.Email = employee.Email;
                                    notificationResponse.MobileNo = employee.EmployeeMobileNum;

                                    notificationResponse.TitleE = notification.TitleE;
                                    notificationResponse.TitleA = notification.TitleA;

                                    SendNotificationSms(notificationResponse);
                                    SentNotificationEmail(notificationResponse);
                                }
                            }
                        }
                        else
                        {
                            //Send the notification to its all recipients
                            foreach (var recipient in notification.NotificationRecipients)
                            {
                                if (string.IsNullOrEmpty(recipient.UserId))
                                {
                                    if (recipient.EmployeeId != null)
                                    {
                                        var employee = employeeRepository.Find(Convert.ToInt64(recipient.EmployeeId));
                                        notificationResponse.Email = employee.Email;
                                        notificationResponse.MobileNo = employee.EmployeeMobileNum;
                                    }
                                    
                                    if (recipient.AspNetUser.Customer != null)
                                    {
                                        notificationResponse.Email = recipient.AspNetUser.Email;
                                        notificationResponse.MobileNo = recipient.AspNetUser.Customer.CustomerMobile;
                                    }
                                }
                                else
                                {
                                    notificationResponse.Email = recipient.AspNetUser.Email;
                                    if (recipient.AspNetUser.Customer != null)
                                    {
                                        notificationResponse.MobileNo = recipient.AspNetUser.Customer.CustomerMobile;
                                    }
                                    if (recipient.AspNetUser.Employee != null)
                                    {
                                        notificationResponse.MobileNo = recipient.AspNetUser.Employee.EmployeeMobileNum;
                                    }
                                }
                                
                                notificationResponse.TitleE = notification.TitleE;
                                notificationResponse.TitleA = notification.TitleA;

                                SendNotificationSms(notificationResponse);
                                SentNotificationEmail(notificationResponse);
                            }
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
        

        public int LoadUnreadNotificationsCount(NotificationRequestParams requestParams)
        {
            return notificationRepository.GetUnreadNotificationsCount(requestParams);
        }

        public NotificationListView LoadAllNotifications(NotificationListViewRequest searchRequset)
        {
            NotificationListView notificationListView=new NotificationListView();
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

        private static void SendNotificationSms(NotificationResponse notificationResponse)
        {
            if (!string.IsNullOrEmpty(notificationResponse.MobileNo))
            {
                string smsText = "Notification:\n" + notificationResponse.TitleE + "<br/>" +
                                 notificationResponse.TitleA;
                Utility.SendNotificationSms(smsText, notificationResponse.MobileNo);
            }
        }

        private static void SentNotificationEmail(NotificationResponse notificationResponse)
        {
            if (!string.IsNullOrEmpty(notificationResponse.Email))
            {
                string emailBody = "Notification:<br/>" + notificationResponse.TitleE + "<br/>" +
                                   notificationResponse.TitleA;
                Utility.SendEmail(notificationResponse.Email,
                    ConfigurationManager.AppSettings["SubjectNotification"], emailBody);
            }
        }
    }
}
