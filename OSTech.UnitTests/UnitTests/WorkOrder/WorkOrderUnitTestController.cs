using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using OSTech.EFCore.Context;
using OSTech.Infrastructure.UnitOfWork;
using OSTech.WebAPI.Commands.WorkOrders;

namespace OSTech.Tests.UnitTests.WorkOder
{
    public class WorkOrderUnitTestController
    {
        public IUnitOfWork repository;
        public IMediator mediator;
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
            var context = new AppDbContext(dbContextOptions);

            repository = new UnitOfWork(context);

            var services = new ServiceCollection();

            services.AddSingleton(repository);
            services.AddSingleton(context);       

            services.AddLogging();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateWorkOrderCommand).Assembly));

            var provider = services.BuildServiceProvider();

            mediator = provider.GetRequiredService<IMediator>();
        }
    }
}