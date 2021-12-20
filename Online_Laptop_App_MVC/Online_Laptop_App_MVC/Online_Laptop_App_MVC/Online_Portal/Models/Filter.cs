using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Portal.Models
{
    public class Filter
    {
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public string Processor { get; set; }
        public string Color { get; set; }
    }
}