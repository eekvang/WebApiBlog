using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiBlog.Filters;
using WebApiBlog.Models;
using WebApiBlog.Repositories;

namespace WebApiBlog.Controllers
{
    public class BlogEntryController : ApiController
    {
        private readonly IBlogEntryRepository _repository;

        public BlogEntryController(IBlogEntryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BlogEntry> Get()
        {
            return _repository.Get();
        }

        public BlogEntry Get(int id)
        {
            var blogEntry = _repository.Get().FirstOrDefault(entry => entry.Id == id);

            if (blogEntry == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return blogEntry;
        }

        [ValidationActionFilter]
        public HttpResponseMessage Post(BlogEntry blogEntry)
        {
            _repository.Insert(blogEntry);
            return Request.CreateResponse(HttpStatusCode.Created, blogEntry);
        }

        public HttpResponseMessage Delete(int id)
        {
            _repository.Delete(1);
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        public HttpResponseMessage Put(int id, BlogEntry newValue)
        {
            var blogEntry = Get(id);
            blogEntry.Title = newValue.Title;

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}
