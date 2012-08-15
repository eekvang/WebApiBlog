using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiBlog.Models;
using WebApiBlog.Repositories;

namespace WebApiBlog.UnitTest.Repositories
{
    [TestClass]
    public class BlogEntryRepositoryTest
    {
        [TestMethod]
        public void Get_TwoBlogEntriesInRepository_ReturnTheTwoEntries()
        {
            var repository = new BlogEntryRepository();

            var entries = repository.Get();

            Assert.AreEqual(2, entries.Count());
        }

        [TestMethod]
        public void Get_TwoBlogEntriesInRepository_TitleSetInEntries()
        {
            var repository = new BlogEntryRepository();

            var entries = repository.Get().ToList();

            Assert.AreEqual("First blogentry", entries[0].Title);
            Assert.AreEqual("Second blogentry", entries[1].Title);
        }

        [TestMethod]
        public void Insert_OneNewBlogEntry_NewBlogEntryAdded()
        {
            var repository = new BlogEntryRepository();

            var blogEntry = new BlogEntry{Title = "another entry"};
            repository.Insert(blogEntry);

            Assert.AreEqual(3, repository.Get().Count());
        }

        [TestMethod]
        public void Delete_DeleteBlogEntryWithId1_Only1BlogEntryLeftInRepository()
        {
            var repository = new BlogEntryRepository();

            repository.Delete(1);

            var entries = repository.Get();
            Assert.AreEqual(1, entries.Count());
            Assert.AreEqual(2, entries.ToList()[0].Id);
        }

        [TestMethod]
        public void Delete_BlogEntryNotFound_Still2EntriesInRepository()
        {
            var repository = new BlogEntryRepository();

            repository.Delete(100);

            var entries = repository.Get();
            Assert.AreEqual(2, entries.Count());
        }
    }
}
