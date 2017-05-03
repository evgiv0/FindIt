using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityNameEng { get; set; }

        public IEnumerable<Notice> Notices { get; set; }
    }
}