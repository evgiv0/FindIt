using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Models
{
    public class Notice
    {
        public int NoticeID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreation{ get; set; }
        public DateTime? DateEnd{ get; set; }
        public bool IsLost { get; set; }
        public bool? IsActual{ get; set; }
        //public bool? IsPremium { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        
        public int? CategoryID { get; set; }
        public Category Category { get; set; }

        //public int AuthorID { get; set; }
        //public Author Author { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        //public ICollection<Image>? Images { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue=false)]
        public string ImageMimeType { get; set; }

    }
}