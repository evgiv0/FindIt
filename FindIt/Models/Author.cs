using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateRegistration { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public DateTime? DateBorn { get; set; }
        public int Phone { get; set; }
        public City CityID { get; set; }

        public ICollection<Notice> Notices { get; set; }
    }
}