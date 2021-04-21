using Checkout.PaymentGateway.Infrastructure.DatabaseFactory;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.IntegrationTests.Common
{
    public class Utils
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(CheckoutWebDbContext context)
        {

            context.SaveChanges();
        }
    }
}
