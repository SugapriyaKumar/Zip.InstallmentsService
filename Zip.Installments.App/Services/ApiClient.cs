using Newtonsoft.Json;
using Zip.Installments.App.Services.Contracts;
using Zip.Installments.App.ViewModel;

namespace Zip.Installments.App.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient apiClient;
        public ApiClient(IHttpClientFactory factory)
        {
            apiClient = factory.CreateClient("ZipInstallmentsServiceAPI");
        }

        public async Task<PaymentPlanViewModel> GetPaymentPlan(OrdersViewModel order)
        {
            
            var response = await apiClient.GetAsync($"Installments?purchaseAmount={order.OrderAmount}&purchaseDate={order.OrderDate}");
            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PaymentPlanViewModel>(responseBody);
            }
            else
            {
                return null;
            }            
        }
    }
}
