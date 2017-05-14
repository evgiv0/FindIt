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

namespace FindIt.Controllers
{
    public class NoticeController : Controller
    {
        private INoticeRepository repository;
        EFDbContext c = new EFDbContext();
        private int DefaultPageSize = 8;

        public NoticeController(INoticeRepository repo)
        {
            repository = repo;
        }

        public ActionResult List(int? page, string filter = "")
        {
            if (Request.IsAjaxRequest())
            {
                int currentPageIndex = page.HasValue ? page.Value : 1;
                if (filter != "")
                {
                    var cat = repository.Categories.Where(p => p.CategoryNameEng == filter).FirstOrDefault();
                    if (cat != null)
                    {
                        var result = repository.Notices.Include(p => p.Category).Where(p => p.Category.CategoryNameEng == filter)
                            .Include(p => p.City).OrderByDescending(p => p.DateCreation).ToPagedList(currentPageIndex, DefaultPageSize);
                        return View("ListAjax", result);
                    }
                    else
                    {
                        var result = repository.Notices.Include(p => p.City).Where(p => p.City.CityNameEng == filter)
                            .Include(p => p.Category).OrderByDescending(p => p.DateCreation).ToPagedList(currentPageIndex, DefaultPageSize);
                        return View("ListAjax", result);
                    }
                }
                else
                {
                    var result = repository.Notices.Include(p => p.Category)
                        .Include(p => p.City).OrderByDescending(p => p.DateCreation).ToPagedList(currentPageIndex, DefaultPageSize);
                    return View("ListAjax", result);
                }
            }
            else
            {
                int currentPageIndex = page.HasValue ? page.Value : 1;
                if (filter != "")
                {
                    var cat = repository.Categories.Where(p => p.CategoryNameEng == filter).FirstOrDefault();
                    if (cat != null)
                    {
                        var result = repository.Notices.Include(p => p.Category).Where(p => p.Category.CategoryNameEng == filter)
                            .Include(p => p.City).OrderByDescending(p => p.DateCreation).ToPagedList(currentPageIndex, DefaultPageSize);
                        return View(result);
                    }
                    else
                    {
                        var result = repository.Notices.Include(p => p.City).Where(p => p.City.CityNameEng == filter)
                            .Include(p => p.Category).OrderByDescending(p => p.DateCreation).ToPagedList(currentPageIndex, DefaultPageSize);
                        return View(result);
                    }
                }
                else
                {
                    var result = repository.Notices.Include(p => p.Category)
                        .Include(p => p.City).OrderByDescending(p => p.DateCreation).ToPagedList(currentPageIndex, DefaultPageSize);
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

            Notice noticeForSave = new Notice();
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    noticeForSave.ImageData = new byte[image.ContentLength];
                    noticeForSave.ImageMimeType = image.ContentType;
                    image.InputStream.Read(noticeForSave.ImageData, 0, image.ContentLength);
                }

                noticeForSave.AuthorName = notice.AuthorName;
                noticeForSave.CategoryID = notice.CategoryID;
                noticeForSave.CityId = notice.CityID;
                noticeForSave.Content = notice.Content;
                noticeForSave.Email = notice.Email;
                noticeForSave.Phone = notice.Phone;
                noticeForSave.Theme = notice.Theme;
                noticeForSave.DateCreation = DateTime.Now;
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
            SearchViewModel searchForm = new SearchViewModel()
            {
                Categories = repository.Categories.Select(p => new SelectListItem { Text = p.CategoryName, Value = p.CategoryID.ToString() }),
                Cities = repository.Cities.Select(p => new SelectListItem { Text = p.CityName, Value = p.CityId.ToString() })
            };

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

            if (searchParams.CityID != 0)
            {
                notices = notices.Include(p => p.City).Where(p => p.City.CityId== searchParams.CityID);
            }

            if(searchParams.SearchString != null)
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
