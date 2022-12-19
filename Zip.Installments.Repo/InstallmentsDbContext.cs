using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zip.installmentsService.Repo.Contracts;
using Zip.nstallmentsService.Repo.Entities;

namespace Zip.nstallmentsService.Repo
{
    public class InstallmentsDbContext:DbContext,IInstallmentDbContext
    {
        public InstallmentsDbContext(DbContextOptions<InstallmentsDbContext> options)
            : base(options)
        {

        }
        public DbSet<PaymentPlan> PaymentPlans { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

    }
}
