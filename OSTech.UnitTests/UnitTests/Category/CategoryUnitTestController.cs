using Microsoft.EntityFrameworkCore;
using OSTech.EFCore.Context;
using OSTech.Infrastructure.UnitOfWork;

namespace OSTech.Tests.UnitTests.Category
{
    public class CategoryUnitTestController
    {
        public IUnitOfWork repository;
        private static DbContextOptions<AppDbContext> dbContextOptions;

        public static string connectionString = TestConfiguration.ConnectionString;

        static CategoryUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }
        public CategoryUnitTestController()
        {
            var context = new AppDbContext(dbContextOptions);

            repository = new UnitOfWork(context);
        }
    }
}
