using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OSTech.EFCore.Context;
using OSTech.WebAPI.DTOs.Mappings;
using OSTech.WebAPI.Repositories.UnitOfWork;

namespace OSTech.Tests.UnitTests.Category
{
    public class CategoryUnitTestController
    {
        public IUnitOfWork repository;
        public IMapper mapper;
        private static DbContextOptions<AppDbContext> dbContextOptions;

        public static string connectionString = "Server=localhost;Database=OSTechDatabase;User=root;Password=SENHA_REMOVIDA;";

        static CategoryUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }
        public CategoryUnitTestController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            mapper = config.CreateMapper();

            var context = new AppDbContext(dbContextOptions);

            repository = new UnitOfWork(context);
        }
    }
}
