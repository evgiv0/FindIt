using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public int NoticeID { get; set; }
        public Notice Notice { get; set; }
        public byte[] ImagePath { get; set; }
    }
}