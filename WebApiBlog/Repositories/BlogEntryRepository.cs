using System.Collections.Generic;
using WebApiBlog.Models;

namespace WebApiBlog.Repositories
{
    public class BlogEntryRepository : IBlogEntryRepository
    {
        private readonly List<BlogEntry> _blogEntries;

        public BlogEntryRepository()
        {
            _blogEntries = new List<BlogEntry>
                               {
                                   new BlogEntry {Id = 1, Title = "First blogentry"},
                                   new BlogEntry {Id = 2, Title = "Second blogentry"}
                               };
        }

        public IEnumerable<BlogEntry> Get()
        {
            return _blogEntries;
        }

        public void Insert(BlogEntry blogEntry)
        {
            _blogEntries.Add(blogEntry);
        }

        public void Delete(int id)
        {
            var toBeDeleted = _blogEntries.Find(blogEntry => blogEntry.Id == id);
            _blogEntries.Remove(toBeDeleted);
        }
    }
}