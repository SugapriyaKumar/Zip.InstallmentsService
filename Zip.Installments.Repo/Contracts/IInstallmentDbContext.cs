using Microsoft.EntityFrameworkCore;
using Zip.nstallmentsService.Repo.Entities;

namespace Zip.installmentsService.Repo.Contracts
{
    public interface IInstallmentDbContext
    {
        DbSet<PaymentPlan> PaymentPlans { get; set; }
        DbSet<Installment> Installments { get; set; }
        Task<int> SaveChangesAsync();
    }
}
