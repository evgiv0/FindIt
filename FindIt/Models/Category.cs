using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FindIt.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [DisplayName("Категория")]
        public string CategoryName { get; set; }
        public string CategoryNameEng { get; set; }
        public IEnumerable<Notice> Notices { get; set; }
    }
}