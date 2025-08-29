using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class FeedImageCollection
    {
        public List<ImageModel> Images { get; set; }
        public CountModel ItemCount { get; set; }
    }
}