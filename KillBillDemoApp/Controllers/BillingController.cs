using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KillBillDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly KillBillClient _killBillClient;

        public BillingController(KillBillClient killBillClient)
        {
            _killBillClient = killBillClient;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> CreateSubscription(SubscriptionRequest request)
        {
            // Create account first
            var account = await _killBillClient.CreateAccount(new AccountRequest
            {
                Name = request.CustomerName,
                Email = request.Email,
                Currency = "USD"
            });

            // Create subscription
            var subscription = await _killBillClient.CreateSubscription(new SubscriptionRequest
            {
                AccountId = account.Id,
                PlanName = request.PlanName,
                PriceList = "DEFAULT"
            });

            return Ok(subscription);
        }
    }
}