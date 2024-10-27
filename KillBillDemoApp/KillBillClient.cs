using Microsoft.Extensions.Options;
using System.Security.Principal;
using System.Text.Json;

namespace KillBillDemoApp
{
    public class KillBillClient
    {
        private readonly HttpClient _client;
        private readonly IOptions<KillBillConfig> _config;

        public KillBillClient(HttpClient client, IOptions<KillBillConfig> config)
        {
            _client = client;
            _config = config;
            _client.BaseAddress = new Uri(_config.Value.BaseUrl);
        }

        public async Task<Account> CreateAccount(AccountRequest request)
        {
            var response = await _client.PostAsJsonAsync("/1.0/kb/accounts", request);

            // First check if the response is successful
            response.EnsureSuccessStatusCode();

            // Read the response content as string first for debugging
            var content = await response.Content.ReadAsStringAsync();

            // Then parse the JSON with error handling
            if (!string.IsNullOrEmpty(content))
            {
                return JsonSerializer.Deserialize<Account>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            throw new InvalidOperationException("Empty response received from KillBill API");
        }


        public async Task<Subscription> CreateSubscription(SubscriptionRequest request)
        {
            var response = await _client.PostAsJsonAsync("/subscriptions", request);
            return await response.Content.ReadFromJsonAsync<Subscription>();
        }
    }

}
