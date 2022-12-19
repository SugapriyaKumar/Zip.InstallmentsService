using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Zip.InstallmentsService.BusinessLogic.Mapper;
using Zip.nstallmentsService.Repo;

namespace Zip.InstallmentsService.Test
{
    public class PaymentPlanFactoryTests
    {
        private DbContextOptions<InstallmentsDbContext> GetTestInstallmentsDbContextOptions([System.Runtime.CompilerServices.CallerMemberName] string name = "")
        {
            var options = new DbContextOptionsBuilder<InstallmentsDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;

            return options;
        }

        private IMapper GetMockPapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentPlanProfile());
                cfg.AddProfile(new InstallmentProfile());
            });

            return mockMapper.CreateMapper();
        }

        [Fact]
        public async Task WhenCreatePaymentPlanWithValidOrderAmount_ShouldReturnValidPaymentPlan()
        {
            // Arrange
            var dbContextOptions = GetTestInstallmentsDbContextOptions();
            using var dbContext = new InstallmentsDbContext(dbContextOptions);
            var paymentPlanFactory = new PaymentPlanFactory(dbContext, GetMockPapper());

            // Act
            var paymentPlan = await paymentPlanFactory.CreatePaymentPlan(400, null);

            // Assert
            paymentPlan.ShouldNotBeNull();
            paymentPlan.Installments.ShouldNotBeNull();
            paymentPlan.Installments.Length.ShouldBe(4);
            paymentPlan.Installments[0].Amount.ShouldBe(100);
            paymentPlan.Installments[1].Amount.ShouldBe(paymentPlan.Installments[0].Amount);
            paymentPlan.Installments[0].DueDate.ShouldBe(DateTime.Today);

            //DbContext Assertions
            dbContext.PaymentPlans.Count().ShouldBe(1);
            dbContext.Installments.Count().ShouldBe(4);
        }

        [Fact]
        public async Task WhenCreatePaymentPlanWithInValidOrderAmount_ShouldReturnNullPaymentPlan()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory(null, null);

            // Act
            var paymentPlan = await paymentPlanFactory.CreatePaymentPlan(0, null);

            // Assert
            paymentPlan.ShouldBeNull();
        }

        [Fact]
        public async Task WhenCreatePaymentPlanWithValidOrderAmount_ShouldReturnValidDueDates()
        {
            // Arrange
            var dbContextOptions = GetTestInstallmentsDbContextOptions();
            using var dbContext = new InstallmentsDbContext(dbContextOptions);
            var paymentPlanFactory = new PaymentPlanFactory(dbContext, GetMockPapper());

            // Act
            var paymentPlan = await paymentPlanFactory.CreatePaymentPlan(400, new DateTime(2023,01,01));

            // Assert
            paymentPlan.ShouldNotBeNull();
            paymentPlan.Installments.ShouldNotBeNull();            
            paymentPlan.Installments[1].DueDate.ShouldBe(new DateTime(2023, 01, 15));
            paymentPlan.Installments[2].DueDate.ShouldBe(new DateTime(2023, 01, 29));
            paymentPlan.Installments[3].DueDate.ShouldBe(new DateTime(2023, 02, 12));

            //DbContext Assertions
            dbContext.PaymentPlans.Count().ShouldBe(1);
            dbContext.Installments.Count().ShouldBe(4);
        }
    }
}
