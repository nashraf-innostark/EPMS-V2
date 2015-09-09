using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.RequestModels
{
    public class NewsAndArticleSearchRequest : GetPagedListRequest
    {
        public long Id { get; set; }
        public string From { get; set; }
    }
}
