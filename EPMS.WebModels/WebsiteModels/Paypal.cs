﻿namespace EPMS.WebModels.WebsiteModels
{
    public class Paypal
    {
        public string cmd { get; set; }
        public string upload { get; set; }
        public string business { get; set; }
        public string no_shipping { get; set; }
        public string return_url { get; set; }
        public string cancel_return { get; set; }
        public string notify_url { get; set; }
        public string currency_code { get; set; }
    }
}