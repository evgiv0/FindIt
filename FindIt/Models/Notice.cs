using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Models
{
    public class Notice
    {
        [HiddenInput(DisplayValue = false)]
        public int NoticeID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}")]
        public DateTime DateCreation{ get; set; }
        public DateTime? DateEnd{ get; set; }

        [Required(ErrorMessage = "Выберите тип объявлений")]
        [HiddenInput(DisplayValue = false)]
        [DisplayName("Тип объявления")]
        public bool IsLost { get; set; }

        [ScaffoldColumn(false)]
        public bool? IsActual{ get; set; }
        //public bool? IsPremium { get; set; }

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
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        //[HiddenInput(DisplayValue = false)]

        [Required]
        [DisplayName("Категория")]
        public int? CategoryID { get; set; }
        public Category Category { get; set; }

        //public int AuthorID { get; set; }
        //public Author Author { get; set; }
        //[HiddenInput(DisplayValue = false)]
        [DisplayName("Город")]
        public int CityId { get; set; }
        public City City { get; set; }

        //public ICollection<Image>? Images { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue=false)]
        public string ImageMimeType { get; set; }

    }
}