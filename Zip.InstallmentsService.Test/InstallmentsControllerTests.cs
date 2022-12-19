using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;
using Zip.InstallmentsService.API.Controllers;
using Zip.InstallmentsService.Contracts;
using Zip.InstallmentsService.DTOs;
using Shouldly;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Zip.InstallmentsService.Test
{
    public class InstallmentsControllerTests
    {
        public Mock<IPaymentPlanFactory> mock = new();

        [Fact]
        public async Task GetInstallmentsForValidRequest()
        {
            //Arrange
            var paymentPlanMock = new PaymentPlanDto()
            {
                Id = Guid.NewGuid(),
                PurchaseAmount = 400,
                Installments = new InstallmentDto[4] { new InstallmentDto() { Amount = 100, DueDate = new DateTime(2022,12,18), Id = Guid.NewGuid() } ,
                new InstallmentDto() { Amount = 100, DueDate = new DateTime(2023,01,01), Id = Guid.NewGuid() },
                new InstallmentDto() { Amount = 100, DueDate = new DateTime(2023,01,15), Id = Guid.NewGuid() },
                new InstallmentDto() { Amount = 100, DueDate = new DateTime(2023,01,29), Id = Guid.NewGuid() }}
            };
            mock.Setup(p => p.CreatePaymentPlan(400, null)).ReturnsAsync(paymentPlanMock);                
            var installmentController = new InstallmentsController(mock.Object,null);

            //Act
            var result = await installmentController.Get(400, null);

            //Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            okResult.ShouldNotBeNull();
            var paymentPlan = okResult.Value as PaymentPlanDto;
            paymentPlanMock.PurchaseAmount.ShouldBe(paymentPlan.PurchaseAmount);
        }

        [Fact]
        public async Task GetInstallmentsForInvalidRequest()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<InstallmentsController>>();
            var installmentController = new InstallmentsController(mock.Object, mockLogger.Object);

            //Act
            var result = await installmentController.Get(0, null);

            //Assert
            result.ShouldBeOfType(typeof(BadRequestResult));
            var badRequestResult = result as BadRequestResult;
            badRequestResult.ShouldNotBeNull();
            badRequestResult.StatusCode.ShouldBe((int)HttpStatusCode.BadRequest);
        }

    }
}

