using System.Collections.Generic;
using WebApiBlog.Models;

namespace WebApiBlog.Repositories
{
    public interface IBlogEntryRepository
    {
        IEnumerable<BlogEntry> Get();
        void Insert(BlogEntry blogEntry);
        void Delete(int id);
    }
}
