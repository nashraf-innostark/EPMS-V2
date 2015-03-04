using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class CompanySocialDetail
    {
        public long CompanyId { get; set; }
        public string Twitter { get; set; }
        public string TwitterUserName { get; set; }
        public string TwitterPassword { get; set; }
        public string Facebook { get; set; }
        public string FacebookUserName { get; set; }
        public string FacebookPassword { get; set; }
        public string Youtube { get; set; }
        public string YoutubeUserName { get; set; }
        public string YoutubePassword { get; set; }
        public string LinkedIn { get; set; }
        public string LinkedInUserName { get; set; }
        public string LinkedInPassword { get; set; }
        public string GooglePlus { get; set; }
        public string GooglePlusUserName { get; set; }
        public string GooglePlusPassword { get; set; }
        public string Istegram { get; set; }
        public string IstegramUserName { get; set; }
        public string IstegramPassword { get; set; }
        public string Tumbler { get; set; }
        public string TumblerUserName { get; set; }
        public string TumblerPassword { get; set; }
        public string Flickr { get; set; }
        public string FlickrUserName { get; set; }
        public string FlickrPassword { get; set; }
        public string Pinterest { get; set; }
        public string PinterestUserName { get; set; }
        public string PinterestPassword { get; set; }
        public string Social1 { get; set; }
        public string Social1UserName { get; set; }
        public string Social1Password { get; set; }
        public string Social2 { get; set; }
        public string Social2UserName { get; set; }
        public string Social2Password { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }

        public virtual CompanyProfile CompanyProfile { get; set; }
    }
}
