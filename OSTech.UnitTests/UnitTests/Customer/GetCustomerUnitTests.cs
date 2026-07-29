using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Customer
{
    public class GetCustomerUnitTests : IClassFixture<CustomerUnitTestController>
    {
        private readonly CustomerController _controller;

        public GetCustomerUnitTests(CustomerUnitTestController controller)
        {
            _controller = new CustomerController(NullLogger<CustomerController>.Instance, controller.repository, controller.mapper);
        }

        [Fact]
        public async Task GetCustomerById_OkResult()
        {
            // Arrange
            var id = 2;

            // Act
            var data = await _controller.Get(id);

            // Assert
            data.Result.Should().BeOfType<OkObjectResult>() // verifica se o resultado é do tipo OkObjectResult
                    .Which.StatusCode.Should().Be(200); // verifica se o código de status do OkObjectResult é 200.
        }

        [Fact]
        public async Task GetCustomerById_Returns_NotFound()
        {
            var id = 999;

            var data = await _controller.Get(id);

            data.Result.Should().BeOfType<NotFoundObjectResult>()
                .Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetCustomer_Returns_ListOfCustomerDTO()
        {
            // Act
            var data = await _controller.Get();

            // Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeAssignableTo<IEnumerable<CustomerDTO>>();
        }
    }
}
