using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zip.Installments.App.ViewModel
{
    public class PaymentPlanViewModel
    {
        [DisplayName("Total Order Amount entered")]
        public decimal PurchaseAmount { get; set; }

        public InstallmentViewModel[] Installments { get; set; }
    }

    public class InstallmentViewModel
    {
        /// <summary>
        /// Gets or sets the date that the installment payment is due.
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")] 
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the amount of the installment.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
