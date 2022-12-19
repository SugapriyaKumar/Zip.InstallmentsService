using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zip.nstallmentsService.Repo.Entities
{
    public class PaymentPlan
    {       

        [Key]
        public Guid Id { get; set; }
        [Precision(14, 2)]
        public decimal PurchaseAmount { get; set; }
        public List<Installment> Installments { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime ModifiedTimeStamp { get; set; }    
    }
}
