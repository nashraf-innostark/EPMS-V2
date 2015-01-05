using System.Collections.Generic;
using EPMS.Models.DomainModels;
using RequestDetail = EPMS.Web.Models.RequestDetail;

namespace EPMS.Web.ViewModels.Request
{
    public class RequestViewModel
    {
        public RequestViewModel()
        {
            Request = new Models.Request();
            RequestDetail = new RequestDetail();
            RequestReply = new RequestDetail();
        }
        public Models.Request Request { get; set; }
        public RequestDetail RequestDetail { get; set; }
        public RequestDetail RequestReply { get; set; }
        public string RequestDesc { get; set; }
    }
}