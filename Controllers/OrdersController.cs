using System;
using System.Collections.Generic;
using System.Linq;
using ABE_Atacadista.Models;
using ABE_Atacadista.Orders;
using Microsoft.AspNetCore.Mvc;

namespace ABE_Atacadista.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public static List<OrderResponseDTO> Orders = new List<OrderResponseDTO>();
        private static Random rnd = new Random();

        /// <summary>
        /// Get a specific order status by order id
        /// </summary>
        [HttpGet("{id}/status")]
        public ActionResult<OrderStatusResponseDTO> GetOrderStatus(int id)
        {
            var response = new OrderStatusResponseDTO();

            if (id > 0)
            {
                var order = Orders.Where(o => o.Id == id).FirstOrDefault();

                if (order != null)
                {
                    response = new OrderStatusResponseDTO
                    {
                        OrderId = order.Id,
                        OrderStatus = order.Status
                    };
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }

            return response;
        }

        /// <summary>
        /// Get the order's data
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<OrderResponseDTO> GetOrder(int id)
        {
            var response = new OrderResponseDTO();

            if (id > 0)
            {
                var order = Orders.Where(o => o.Id == id).FirstOrDefault();

                if (order != null)
                    response = order;
                else
                    return NotFound();
            }
            else
            {
                return BadRequest();
            }

            return response;
        }

        /// <summary>
        /// Place an order request to get the pricing and estimated delivery date 
        /// </summary>
        [HttpPost]
        public ActionResult<OrderResponseDTO> RequestOrder(OrderRequestDTO orderRequest)
        {
            var response = new OrderResponseDTO();

            if (orderRequest.Orders.Count > 0)
            {
                var id = Orders.Count + 1;

                var orderResponse = new OrderResponseDTO
                {
                    Id = id,
                    DeliveryDate = DateTime.Now.AddDays(rnd.Next(2, 15)),
                    Status = OrderStatus.Created,
                    OrderValue = rnd.Next(10, 50) * 1.3m,
                    Orders = orderRequest.Orders,
                    Links = new List<BaseDTOLink>(){
                        new BaseDTOLink{
                            Rel = "self",
                            Href = $"http://localhost:50000/api/v1/Orders/{id}"
                        },
                        new BaseDTOLink{
                            Rel = "status",
                            Href = $"http://localhost:50000/api/v1/Orders/{id}/status"
                        },
                        new BaseDTOLink{
                            Rel = "acceptance",
                            Href = "http://localhost:50000/api/v1/Orders"
                        }
                    }
                };

                Orders.Add(orderResponse);

                response = orderResponse;
            }
            else
            {
                return BadRequest();
            }

            return response;
        }

        /// <summary>
        /// Confirm or reject a created order
        /// </summary>
        [HttpPut]
        public ActionResult<OrderRequestDTO> RespondOrder(OrderAcceptanceResponse orderAcceptance)
        {
            var response = new OrderRequestDTO();

            var dbOrder = Orders.Where(o => o.Id == orderAcceptance.OrderId).FirstOrDefault();
            if (dbOrder != null)
            {
                if (orderAcceptance.AcceptOrder)
                {
                    dbOrder.Status = OrderStatus.Requested;
                    dbOrder.Links.AddRange(orderAcceptance.Links);
                    //chamar api lojista com status novo

                    var processor = new OrdersProcessor();
                    processor.Notify(dbOrder);
                }
                else
                {
                    dbOrder.Status = OrderStatus.Rejected;
                }

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}