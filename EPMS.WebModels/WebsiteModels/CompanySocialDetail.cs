using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class CompanySocialDetail
    {
        public long CompanyId { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string Twitter { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string TwitterUserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string TwitterPassword { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string Facebook { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string FacebookUserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string FacebookPassword { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string Youtube { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string YoutubeUserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string YoutubePassword { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string LinkedIn { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string LinkedInUserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string LinkedInPassword { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string GooglePlus { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string GooglePlusUserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string GooglePlusPassword { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string Istegram { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string IstegramUserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string IstegramPassword { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string Tumbler { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string TumblerUserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string TumblerPassword { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string Flickr { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string FlickrUserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string FlickrPassword { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string Pinterest { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string PinterestUserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string PinterestPassword { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string Social1 { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string Social1UserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string Social1Password { get; set; }
        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string Social2 { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string Social2UserName { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string Social2Password { get; set; }
        public string CompanyLogoPath { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }
    }
}