using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

namespace KillBillDemoApp
{
    public class AccountRequest
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public int FirstNameLength { get; set; }
        public string ExternalKey { get; set; }
        public string Email { get; set; }
        public int BillCycleDayLocal { get; set; }
        public string Currency { get; set; }
        public string ParentAccountId { get; set; }
        public bool IsPaymentDelegatedToParent { get; set; }
        public string PaymentMethodId { get; set; }
        public DateTime ReferenceTime { get; set; }
        public string TimeZone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Locale { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public bool IsMigrated { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal AccountCBA { get; set; }
        public List<AuditLog> AuditLogs { get; set; }
    }

    public class AuditLog
    {
        public string ChangeType { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }
        public string ChangedBy { get; set; }
        public string ReasonCode { get; set; }
        public string Comments { get; set; }
        public string UserToken { get; set; }
        public History History { get; set; }
    }

    public class History
    {
        public string Id { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }


    public class SubscriptionRequest
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public Guid AccountId { get; set; }
        public string PlanName { get; set; }
        public string PriceList { get; set; }
    }


    public class PaymentRequest
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Currency { get; set; }
        public string ExternalKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TimeZone { get; set; }
        public string Locale { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
    }
    public class Subscription
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string BundleId { get; set; }
        public string PlanName { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string BillingPeriod { get; set; }
        public string PriceList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string State { get; set; }
        public decimal ChargedThroughDate { get; set; }
    }
    public class Payment
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethodId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public string GatewayErrorCode { get; set; }
        public string GatewayErrorMsg { get; set; }
        public string ExternalKey { get; set; }
    }

    public class KillBillConfig
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DefaultCurrency { get; set; }
        public string DefaultPriceList { get; set; }
        public int Timeout { get; set; }
        public int RetryAttempts { get; set; }
    }

    public class KillBillAuthHandler : DelegatingHandler
    {
        private readonly KillBillConfig _config;
        private readonly ILogger<KillBillAuthHandler> _logger;

        public KillBillAuthHandler(IOptions<KillBillConfig> config, ILogger<KillBillAuthHandler> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add("X-Killbill-ApiKey", "");
            request.Headers.Add("X-Killbill-ApiSecret", "");
            request.Headers.Add("X-Killbill-Tenant", "");
            request.Headers.Add("X-Killbill-CreatedBy", "");
            request.Headers.Add("Accept", "application/json");

            // Add Basic Auth
            var authString = $"";
            var base64Auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);

            return await base.SendAsync(request, cancellationToken);
        }

    }


}