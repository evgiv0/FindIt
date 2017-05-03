using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindIt.Models;

namespace FindIt.Abstract
{
    public interface INoticeRepository
    {
        IQueryable<Notice> Notices { get; }
        IQueryable<City> Cities { get; }
       // IQueryable<Image> Images { get; }
        IQueryable<Author> Authors { get; }
        IQueryable<Category> Categories { get; }
        void Add(Notice notice);
        void SaveNotice();

    }
}
