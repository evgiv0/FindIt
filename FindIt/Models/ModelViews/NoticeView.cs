using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Models.ModelViews
{
    public class NoticeView
    {
        [HiddenInput(DisplayValue =false)]
        public int NoticeID { get; set; }

        [Required(ErrorMessage ="Выберите тип объявлений")]
        [HiddenInput(DisplayValue = false)]
        [DisplayName("Тип объявления")]
        public bool IsLost { get; set; }

        [Required]
        [DisplayName("Категория")]
        public int? CategoryID { get; set; }

        [Required]
        [DisplayName("Тема")]
        public string Theme { get; set; }


        [Required(ErrorMessage = "Поле Содержание не может быть пустым")]
        [DisplayName("Содержание")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Укажите ваше имя")]
        [DisplayName("Ваше имя")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Поле Email не может быть пустым")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Ваш email")]
        public string Email { get; set; }
        

        [DisplayName("Ваш телефон")]
        public int Phone { get; set; }
        
        [DisplayName("Город")]
        public int CityID { get; set; }

        //public int AuthorID { get; set; }
        //public Author Author { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }



        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Cities{ get; set; }


    }
}