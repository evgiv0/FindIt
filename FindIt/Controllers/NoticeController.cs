using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindIt.Concrete;
using FindIt.Abstract;
using System.Data.Entity;
using FindIt.Models;
using FindIt.Models.ModelViews;
using PagedList;
using AutoMapper;

namespace FindIt.Controllers
{
    public class NoticeController : Controller
    {
        private INoticeRepository repository;
        private int DefaultPageSize = 8;

        public NoticeController(INoticeRepository repo)
        {
            repository = repo;
        }

        public ActionResult List(int page = 1, string filter="")
        {
            if (Request.IsAjaxRequest())
            {
                if (filter != "")
                {
                    var cat = repository.Categories.Where(p => p.CategoryNameEng == filter).FirstOrDefault();
                    if (cat != null)
                    {
                        var result = repository.Notices.Include(p => p.Category).Where(p => p.Category.CategoryNameEng == filter)
                            .Include(p => p.City).OrderByDescending(p => p.DateCreation).ToPagedList(page, DefaultPageSize);
                        return View("ListAjax", result);
                    }
                    else
                    {
                        var result = repository.Notices.Include(p => p.City).Where(p => p.City.CityNameEng == filter)
                            .Include(p => p.Category).OrderByDescending(p => p.DateCreation).ToPagedList(page, DefaultPageSize);
                        return View("ListAjax", result);
                    }
                }
                else
                {
                    var result = repository.Notices.Include(p => p.Category)
                        .Include(p => p.City).OrderByDescending(p => p.DateCreation).ToPagedList(page, DefaultPageSize);
                    return View("ListAjax", result);
                }
            }
            else
            { 
                if (filter != "")
                {
                    var cat = repository.Categories.Where(p => p.CategoryNameEng == filter).FirstOrDefault();
                    if (cat != null)
                    {
                        var result = repository.Notices.Include(p => p.Category).Where(p => p.Category.CategoryNameEng == filter)
                            .Include(p => p.City).OrderByDescending(p => p.DateCreation).ToPagedList(page, DefaultPageSize);
                        return View(result);
                    }
                    else
                    {
                        var result = repository.Notices.Include(p => p.City).Where(p => p.City.CityNameEng == filter)
                            .Include(p => p.Category).OrderByDescending(p => p.DateCreation).ToPagedList(page, DefaultPageSize);
                        return View(result);
                    }
                }
                else
                {
                    var result = repository.Notices.Include(p => p.Category)
                        .Include(p => p.City).OrderByDescending(p => p.DateCreation).ToPagedList(page, DefaultPageSize);
                    return View(result);
                }
            }
        }

        public ActionResult Show(int idN)
        {
            var notices = repository.Notices.Where(p => p.NoticeID == idN).Include(p => p.Category).Include(p => p.City).FirstOrDefault();
            return View(notices);
        }

        [HttpGet]
        public ActionResult AddNotice()
        {
            NoticeView notice = new NoticeView();
            notice.Categories = repository.Categories.Select(p => new SelectListItem { Text = p.CategoryName, Value = p.CategoryID.ToString() });
            notice.Cities = repository.Cities.Select(p => new SelectListItem { Text = p.CityName, Value = p.CityId.ToString() });

            return View(notice);
        }

        [HttpPost]
        public ActionResult AddNotice(NoticeView notice, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {
                //if(image != null)
                //{
                //    noticeForSave.ImageData = new byte[image.ContentLength];
                //    noticeForSave.ImageMimeType = image.ContentType;
                //    image.InputStream.Read(notice.ImageData, 0, image.ContentLength);
                //}

                Mapper.Initialize(cfg => cfg.CreateMap<NoticeView, Notice>()
                .ForMember("DateCreation", opt => opt.UseValue(DateTime.Now)));
                Notice noticeForSave = Mapper.Map<NoticeView, Notice>(notice);

                if (image != null)
                {
                    noticeForSave.ImageData = new byte[image.ContentLength];
                    noticeForSave.ImageMimeType = image.ContentType;
                    image.InputStream.Read(noticeForSave.ImageData, 0, image.ContentLength);
                }

                //noticeForSave = notice.Notice;
                //noticeForSave.AuthorName = notice.AuthorName;
                //noticeForSave.CategoryID = notice.CategoryID;
                //noticeForSave.CityId = notice.CityId;
                //noticeForSave.Content = notice.Content;
                //noticeForSave.Email = notice.Email;
                //noticeForSave.Phone = notice.Phone;
                //noticeForSave.Theme = notice.Theme;
                //noticeForSave.DateCreation = DateTime.Now;
                //noticeForSave.IsLost.
                repository.Add(noticeForSave);
                repository.SaveNotice();

                return RedirectToAction("List");
            }

            notice.Categories = repository.Categories.Select(p => new SelectListItem { Text = p.CategoryName, Value = p.CategoryID.ToString() });
            notice.Cities = repository.Cities.Select(p => new SelectListItem { Text = p.CityName, Value = p.CityId.ToString() });
            return View(notice);
        }

        public ActionResult SearchNotice()
        {
            SearchViewModel searchForm = new SearchViewModel();

            var categories = repository.Categories.Select(p => new SelectListItem { Text = p.CategoryName, Value = p.CategoryID.ToString() });
            searchForm.Categories.Add(new SelectListItem { Text = "Выберите категорию" });
            foreach (var item in categories)
            {
                searchForm.Categories.Add(item);
            }

            //notice.Categories = repository.Categories.Select(p => new SelectListItem { Text = p.CategoryName, Value = p.CategoryID.ToString() });
            var cities = repository.Cities.Select(p => new SelectListItem { Text = p.CityName, Value = p.CityId.ToString() });
            searchForm.Cities.Add(new SelectListItem { Text = "Выберите город" });
            foreach (var item in cities)
            {
                searchForm.Cities.Add(item);
            }

            return View(searchForm);
        }

        [HttpPost]
        [ActionName("SearchPost")]
        public ActionResult SearchNotice(SearchViewModel searchParams)
        {
            int currentPageIndex =  1;
            var notices = repository.Notices;
            if (searchParams.CategoryID != 0)
            {
                notices = notices.Include(p => p.Category).Where(p => p.Category.CategoryID == searchParams.CategoryID);
            }
            else
            {
                notices = notices.Include(p => p.Category);
            }

            if (searchParams.CityID != 0)
            {
                notices = notices.Include(p => p.City).Where(p => p.City.CityId== searchParams.CityID);
            }
            else
            {
                notices = notices.Include(p => p.City);

            }

            if (searchParams.SearchString != null)
            {
                notices = notices.Where(p => p.Theme.Contains(searchParams.SearchString) || p.Content.Contains(searchParams.SearchString));

            }

            return View("List", notices.OrderByDescending(p => p.DateCreation).ToPagedList(currentPageIndex,DefaultPageSize));
        }

        public FileContentResult GetImage(int noticeID)
        {
            Notice notice = repository.Notices.FirstOrDefault(p => p.NoticeID == noticeID);
            if(notice != null)
            {
                return File(notice.ImageData, notice.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
