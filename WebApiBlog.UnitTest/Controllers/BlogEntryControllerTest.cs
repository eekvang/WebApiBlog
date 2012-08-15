using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using WebApiBlog.Controllers;
using WebApiBlog.Models;
using WebApiBlog.Repositories;

namespace WebApiBlog.UnitTest.Controllers
{
    [TestClass]
    public class BlogEntryControllerTest
    {
        [TestMethod]
        public void Get_NoBlogEntriesInRepository_EmptyListReturned()
        {
            var repository = MockRepository.GenerateStub<IBlogEntryRepository>();
            repository.Stub(repo => repo.Get()).Return(new List<BlogEntry>());
            var blogEntryController = new BlogEntryController(repository);

            var entries = blogEntryController.Get();

            Assert.AreEqual(0, entries.Count());
        }

        [TestMethod]
        public void Get_OneBlogEntryInRepository_ReturnedListContainsBlogEntryFromRepository()
        {
            var repository = MockRepository.GenerateStub<IBlogEntryRepository>();
            repository.Stub(repo => repo.Get()).Return(new List<BlogEntry> {new BlogEntry()});

            var blogEntryController = new BlogEntryController(repository);

            var entries = blogEntryController.Get();

            Assert.AreEqual(1, entries.Count());
        }

        [TestMethod]
        public void Post_NewBlogEntry_BlogEntryAddedToRepository()
        {
            var mockedRepository = new FakeReposiory();

            var blogEntryController = CreateController(mockedRepository);
            var blogEntry = new BlogEntry {Title = "My new blogEntry"};

            blogEntryController.Post(blogEntry);

            Assert.AreEqual(1, mockedRepository.Get().Count());
        }

        [TestMethod]
        public void Post_NewBlogEntry_StatusCodeIsCreated()
        {
            var mockedRepository = new FakeReposiory();

            var blogEntryController = CreateController(mockedRepository);

            var blogEntry = new BlogEntry { Title = "My new blogEntry" };

            var response = blogEntryController.Post(blogEntry);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void Get_OneBlogEntryWithId1_BlogEntryReturned()
        {
            var repository = MockRepository.GenerateStub<IBlogEntryRepository>();
            repository.Stub(repo => repo.Get()).Return(new List<BlogEntry> { new BlogEntry{Id = 1} });

            var blogEntryController = new BlogEntryController(repository);

            var blogEntry = blogEntryController.Get(1);

            Assert.AreEqual(1, blogEntry.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void Get_BlogEntryWithIdNotFound_HttpResponseExceptionThrownWithStatusCodeNotFound()
        {
            var repository = MockRepository.GenerateStub<IBlogEntryRepository>();
            repository.Stub(repo => repo.Get()).Return(new List<BlogEntry> { new BlogEntry { Id = 1 } });

            var blogEntryController = CreateController(repository);

            blogEntryController.Get(2);
        }

        [TestMethod]
        public void Delete_BlogEntryWithId1_DeleteCalledOnRepository()
        {
            var mockedRepository = MockRepository.GenerateMock<IBlogEntryRepository>();

            var controller = CreateController(mockedRepository);

            controller.Delete(1);

            mockedRepository.AssertWasCalled(repository => repository.Delete(1));
        }

        [TestMethod]
        public void Delete_BlogEntryWithId1_StatusCodeIsAccepted()
        {
            var stubbedRepository = MockRepository.GenerateStub<IBlogEntryRepository>();

            var controller = CreateController(stubbedRepository);

            var response = controller.Delete(1);

            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        [TestMethod]
        public void Put_BlogEntryFound_TitleUpdated()
        {
            var blogEntry = new BlogEntry {Id = 1, Title = "MyTitle"};
            var stubbedRepository = MockRepository.GenerateStub<IBlogEntryRepository>();
            stubbedRepository.Stub(repository => repository.Get()).Return(new List<BlogEntry> {blogEntry});

            var controller = CreateController(stubbedRepository);

            var entryWithNewTitle = new BlogEntry{Title = "The new title"};
            
            controller.Put(1, entryWithNewTitle);

            Assert.AreEqual("The new title", blogEntry.Title);
        }

        [TestMethod]
        public void Put_BlogEntryFound_StatusCodeIsAccepted()
        {
            var blogEntry = new BlogEntry { Id = 1, Title = "MyTitle" };
            var stubbedRepository = MockRepository.GenerateStub<IBlogEntryRepository>();
            stubbedRepository.Stub(repository => repository.Get()).Return(new List<BlogEntry> { blogEntry });

            var controller = CreateController(stubbedRepository);

            var entryWithNewTitle = new BlogEntry { Title = "The new title" };

            var response = controller.Put(1, entryWithNewTitle);

            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        private static BlogEntryController CreateController(IBlogEntryRepository mockedRepository)
        {
            var request = new HttpRequestMessage();
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var blogEntryController = new BlogEntryController(mockedRepository) {Request = request};
            return blogEntryController;
        }
    }

    public class FakeReposiory : IBlogEntryRepository
    {
        private readonly List<BlogEntry> _entries = new List<BlogEntry>();

        public IEnumerable<BlogEntry> Get()
        {
            return _entries;
        }

        public void Insert(BlogEntry blogEntry)
        {
            _entries.Add(blogEntry);
        }

        public void Delete(int id)
        {
            
        }
    }
}
