using FluentAssertions;
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
    public class PutCustomerUnitTests : IClassFixture<CustomerUnitTestController>
    {
        private readonly CustomerController _controller;

        public PutCustomerUnitTests(CustomerUnitTestController controller)
        {
            _controller = new CustomerController(NullLogger<CustomerController>.Instance, controller.repository, controller.mapper);
        }
        [Fact]
        public async Task PutCustomer_Return_OkResult()
        {
            // Arrange
            var id = 1;

            var updatedCustomer = new UpdateCustomerDTO
            {
                Name = "Novo Cliente Alterado",
                Document = "Documento do Novo Cliente Alterado",
                Email = "Email do Novo Cliente Alterado",
                Phone = "Telefone do Novo Cliente Alterado"

            };

            // Act
            var result = await _controller.Put(id, updatedCustomer);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task PutCustomer_Return_BadRequest()
        {
            var id = 0;

            var myCustomer = new UpdateCustomerDTO
            {
                Name = "Nova Categoria Alterada",
                Document = "Documento do Novo Cliente Alterado",
                Email = "Email do Novo Cliente Alterado",
                Phone = "Telefone do Novo Cliente Alterado"
            };

            var data = await _controller.Put(id, myCustomer);

            data.Result.Should().BeOfType<BadRequestResult>()
                .Which.StatusCode.Should().Be(400);
        }
    }
}
