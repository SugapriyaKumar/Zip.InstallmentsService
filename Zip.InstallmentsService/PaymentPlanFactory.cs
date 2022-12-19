using AutoMapper;
using System;
using System.Threading.Tasks;
using Zip.installmentsService.Repo.Contracts;
using Zip.InstallmentsService.Contracts;
using Zip.InstallmentsService.DTOs;
using Zip.InstallmentsService.Filters;
using Zip.InstallmentsService.Globals;
using Zip.nstallmentsService.Repo.Entities;

namespace Zip.InstallmentsService
{
    /// <summary>
    /// This class is responsible for building the PaymentPlan according to the Zip product definition.
    /// </summary>
    public class PaymentPlanFactory : IPaymentPlanFactory
    {
        private readonly IInstallmentDbContext installmentDbContext;
        private readonly IMapper mapper;

        public PaymentPlanFactory(IInstallmentDbContext installmentDbContext, IMapper mapper)
        {
            this.installmentDbContext = installmentDbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// Builds the PaymentPlan instance.
        /// </summary>
        /// <param name="purchaseAmount">The total amount for the purchase that the customer is making.</param>
        /// <returns>The PaymentPlan created with all properties set.</returns>
        /// <exception cref="ProcessException"></exception>
        public async Task<PaymentPlanDto> CreatePaymentPlan(decimal purchaseAmount, DateTime? purchaseDate)
        {
            PaymentPlanDto paymentPlan;
            try
            {
                if (purchaseAmount > 0)
                {
                    paymentPlan = new()
                    {
                        Id = Guid.NewGuid(),
                        PurchaseAmount = purchaseAmount,
                        //If the purchase date from input is not given, then current date is assumed as the purchase date
                        Installments = ComputeInstallments(purchaseAmount, purchaseDate ?? DateTime.Today)
                    };

                    var paymentPlanDBEntity = mapper.Map<PaymentPlan>(paymentPlan);

                    installmentDbContext.PaymentPlans.Add(paymentPlanDBEntity);
                    await installmentDbContext.SaveChangesAsync();
                }          
                else
                {
                    paymentPlan = null;
                }
            }
            catch (Exception ex)
            {
                throw new ProcessException($"Can not create payment plan. { ex.Message}. ");
            }
            return paymentPlan;
        }

        /// <summary>
        /// Computes installation amount based on number of installments, purchase amount and days interval
        /// </summary>
        /// <param name="purchaseAmount"></param>
        /// <param name="purchaseDate"></param>
        /// <returns>An array of installment with installment date and amount</returns>
        /// <exception cref="ProcessException"></exception>
        private InstallmentDto[] ComputeInstallments(decimal purchaseAmount, DateTime purchaseDate)
        {
            var installments = new InstallmentDto[GlobalConstants.NumberOfInstallments];
            try
            {
                if (purchaseAmount > 0)
                {     
                    //Purchased amount is equally divided for the given number of installments
                    var installmentAmount = purchaseAmount / GlobalConstants.NumberOfInstallments;

                    //Initially purchase date is set as the first date of due
                    var dueDate = purchaseDate;

                    //Forming the Installments array based on the number of installments
                    for (var count=0; count < GlobalConstants.NumberOfInstallments; count++)
                    {
                        installments[count] = new()
                        {
                            Id = Guid.NewGuid(),
                            Amount = Math.Round(installmentAmount, 2),
                            DueDate = dueDate
                        };
                        //Due date is reset as the last due date
                        dueDate = dueDate.AddDays(GlobalConstants.NumberOfIntervalDays);
                    }
                }
                else
                {
                    throw new ProcessException($"Purchase amount can not be zero. Try with non-zero and non-negative numbers. ");
                }
            }
            catch (Exception ex)
            {
                throw new ProcessException($"Can not compute installments. {ex.Message}. ");
            }
            return installments;
        }
    }
}
