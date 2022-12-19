using System;

namespace Zip.InstallmentsService.DTOs
{
    /// <summary>
    /// Data structure which defines all the properties for a purchase installment plan.
    /// </summary>
    public class PaymentPlanDto
    {
        public Guid Id { get; set; }

        public decimal PurchaseAmount { get; set; }

        public InstallmentDto[] Installments { get; set; }
    }

}
