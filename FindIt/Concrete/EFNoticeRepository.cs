using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Abstract;
using FindIt.Models;

namespace FindIt.Concrete
{
    public class EFNoticeRepository : INoticeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Notice> Notices
        {
            get { return context.Notices; }
        }

        public IQueryable<Author> Authors
        {
            get { return context.Authors; }
        }

        public IQueryable<City> Cities
        {
            get { return context.Cities; }
        }

        public IQueryable<Category> Categories
        {
            get { return context.Category; }
        }

        public void Add(Notice notice)
        {
            context.Notices.Add(notice);
        }

        public void SaveNotice()
        {
            context.SaveChanges();
        }
        //public IQueryable<Image> Images
        //{
        //    get { return context.Images; }
        //}
    }
}