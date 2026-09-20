using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using OSTech.Application.Mappings;
using OSTech.EFCore.Context;
using OSTech.Infrastructure.UnitOfWork;
using OSTech.WebAPI.Commands.WorkOrders;

namespace OSTech.Tests.UnitTests.WorkOder
{
    public class WorkOrderUnitTestController
    {
        public IUnitOfWork repository;
        public IMapper mapper;
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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            mapper = config.CreateMapper();

            var context = new AppDbContext(dbContextOptions);

            repository = new UnitOfWork(context);

            var services = new ServiceCollection();

            services.AddSingleton(repository);
            services.AddSingleton(context);       
            services.AddSingleton(mapper);

            services.AddLogging();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateWorkOrderCommand).Assembly));

            var provider = services.BuildServiceProvider();

            mediator = provider.GetRequiredService<IMediator>();
        }
    }
}