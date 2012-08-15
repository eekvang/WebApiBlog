using Ninject.Modules;
using WebApiBlog.Repositories;

namespace WebApiBlog.Modules
{
    public class BlogEntryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBlogEntryRepository>().To<BlogEntryRepository>();
        }
    }
}