using System.Linq;
using ABE_Atacadista.Controllers;
using ABE_Atacadista.Models;
using RestSharp;

namespace ABE_Atacadista.Orders
{
    public class OrdersProcessor
    {



        public void Notify(OrderResponseDTO order)
        {
            var url = order.Links.Where(l => l.Rel.Equals("notification")).FirstOrDefault();

            if (url != null)
            {
                var client = new RestClient(url.Href);
                var request = new RestRequest(Method.PUT);
                request.AddJsonBody(order);

                var response = client.Execute(request);
                var content = response.Content;
            }
        }
    }
}