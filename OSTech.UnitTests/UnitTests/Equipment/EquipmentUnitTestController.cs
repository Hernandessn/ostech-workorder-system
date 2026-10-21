using Microsoft.EntityFrameworkCore;
using OSTech.EFCore.Context;
using OSTech.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Equipment
{
    public class EquipmentUnitTestController
    {
        public IUnitOfWork repository;
        private static DbContextOptions<AppDbContext> dbContextOptions;

        public static string connectionString = TestConfiguration.ConnectionString;

        static EquipmentUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }
        public EquipmentUnitTestController()
        {
            var context = new AppDbContext(dbContextOptions);

            repository = new UnitOfWork(context);
        }
    }
}
