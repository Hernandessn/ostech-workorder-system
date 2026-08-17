using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OSTech.Application.Mappings;
using OSTech.EFCore.Context;
using OSTech.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.WorkOder
{
    public class WorkOrderUnitTestController
    {
        public IUnitOfWork repository;
        public IMapper mapper;
        private static DbContextOptions<AppDbContext> dbContextOptions;

        public static string connectionString = TestConfiguration.ConnectionString;
        static WorkOrderUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .Options;
        }
        public WorkOrderUnitTestController()
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
