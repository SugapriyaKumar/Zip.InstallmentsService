using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zip.InstallmentsService.DTOs;

namespace Zip.InstallmentsService.Contracts
{
    public interface IPaymentPlanFactory
    {
        Task<PaymentPlanDto> CreatePaymentPlan(decimal purchaseAmount, DateTime? purchaseDate);

    }
}
