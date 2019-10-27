using System.Collections.Generic;
using ABE_Atacadista.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABE_Atacadista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// Get a specific order status by order id
        /// </summary>
        [HttpGet("{id}/status")]
        public OrderStatusResponseDTO GetOrderStatus(int id)
        {
            var response = new OrderStatusResponseDTO();


            return response;
        }

        /// <summary>
        /// Get the order's data
        /// </summary>
        [HttpGet("{id}")]
        public OrderRequestDTO GetOrder(int id)
        {
            var response = new OrderRequestDTO();


            return response;
        }

        /// <summary>
        /// Place an order request to get the pricing and estimated delivery date 
        /// </summary>
        [HttpPost]
        public ActionResult<OrderRequestDTO> RequestOrder(OrderRequestDTO orderRequest)
        {
            var response = new OrderRequestDTO();


            return response;
        }

        /// <summary>
        /// Confirm or reject a created order
        /// </summary>
        [HttpPut]
        public ActionResult<OrderRequestDTO> RespondOrder(OrderResponseDTO orderResponse)
        {
            var response = new OrderRequestDTO();


            return response;
        }
    }
}