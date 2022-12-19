using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zip.nstallmentsService.Repo.Entities
{
    public class Installment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PaymentPlanId { get; set; }
        public DateTime DueDate { get; set; }
        [Precision(14, 2)] 
        public decimal Amount { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime ModifiedTimeStamp { get; set; }

        [ForeignKey(nameof(PaymentPlanId))]
        public PaymentPlan PaymentPlan { get; set; }
    }
}
