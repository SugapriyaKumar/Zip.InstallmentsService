using Microsoft.AspNetCore.Mvc;
using Zip.InstallmentsService.Contracts;
using Zip.InstallmentsService.Filters;

namespace Zip.InstallmentsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallmentsController : ControllerBase
    {
        private readonly ILogger<InstallmentsController> _logger;
        private readonly IPaymentPlanFactory _paymentPlanFactory;

        public InstallmentsController(IPaymentPlanFactory paymentPlanFactory, ILogger<InstallmentsController> logger)
        {
           _paymentPlanFactory = paymentPlanFactory;
            _logger = logger;
        }

        [HttpGet(Name = "GetInstallments")]
        [ProcessExceptionFilter]
        //Aim is to get the installment date and installment amount for the purchase
        public async Task<ActionResult> Get(decimal purchaseAmount, DateTime? purchaseDate)
        {
            if(purchaseAmount<=0)
            {
                _logger.LogError("Purchased amount should be greater than zero to compute installments");
                _logger.LogWarning("a warning");
                return BadRequest();
            }            
            return Ok(await _paymentPlanFactory.CreatePaymentPlan(purchaseAmount, purchaseDate));
        }
    }
}
