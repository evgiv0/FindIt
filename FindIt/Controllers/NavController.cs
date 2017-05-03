using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindIt.Concrete;
using FindIt.Abstract;
using FindIt.Models;


namespace FindIt.Controllers
{
    public class NavController : Controller
    {
        private INoticeRepository repository;
        EFDbContext c = new EFDbContext();
        public NavController(INoticeRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu()
        {
            
            var categories = repository.Categories.Distinct().OrderBy(p => p.CategoryName);
            return PartialView(categories);
        }

    }
}
