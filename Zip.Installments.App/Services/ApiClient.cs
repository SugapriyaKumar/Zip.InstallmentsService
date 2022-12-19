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
            if(order == null)
                return null;

            var purchaseDate = order.OrderDate.HasValue ? $"&purchaseDate={order.OrderDate.Value.ToString("MM/dd/yyyy")}" : String.Empty ;
            var response = await apiClient.GetAsync($"Installments?purchaseAmount={order.OrderAmount}{purchaseDate}");
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
