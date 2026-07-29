using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Category;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Dtos.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Customer
{
    public class PostCustomerUnitTests : IClassFixture<CustomerUnitTestController>
    {
        private readonly CustomerController _controller;

        public PostCustomerUnitTests(CustomerUnitTestController controller)
        {
            _controller = new CustomerController(NullLogger<CustomerController>.Instance, controller.repository, controller.mapper);

        }
        [Fact]
        public async Task PostCustomer_CreatedStatusCode()
        {
            // Arrange
            var newCustomerDto = new CreateCustomerDTO
            {
                Name = "Novo cliente",
                Document = "12345678901",
                Email = "email do novo cliente",
                Phone = "12345678901"
            };

            // Act
            var data = await _controller.Post(newCustomerDto);

            // Assert
            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(StatusCodes.Status201Created);
        }
        [Fact]
        public async Task PostCustomer_Return_BadRequest()
        {
            // Arrange
            CreateCustomerDTO dto = null;

            // Act
            var data = await _controller.Post(dto);

            // Assert
            data.Result.Should().BeOfType<BadRequestResult>()
                       .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
