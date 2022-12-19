using Zip.Installments.App.ViewModel;

namespace Zip.Installments.App.Services.Contracts
{
    public interface IApiClient
    {
        Task<PaymentPlanViewModel> GetPaymentPlan(OrdersViewModel order);
    }
}
