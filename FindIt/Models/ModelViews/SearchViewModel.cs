using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FindIt.Models.ModelViews
{
    public class SearchViewModel
    {
        [DisplayName("Введите строку для поиска")]
        public string SearchString { get; set; }

        [DisplayName("Категория")]
        public int CategoryID { get; set; }
        
        [DisplayName("Город")]
        public int CityID { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Cities { get; set; }

        public SearchViewModel()
        {
            Categories = new List<SelectListItem>();
            Cities = new List<SelectListItem>();
        }
    }
}