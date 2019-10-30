using System;
using System.Net;
using ABE_Atacadista.Controllers;
using ABE_Atacadista.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ABE_Atacadista.Tests
{
    public class OrdersTest
    {
        [Fact]
        public void GetOrderStatus()
        {
            // Arrange
            ResetOrderList();
            var controller = new OrdersController();

            // Act
            var response = controller.GetOrderStatus(1).Value as OrderStatusResponseDTO;

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void RequestOrder()
        {
            // Arrange
            var controller = new OrdersController();
            var request = new OrderRequestDTO
            {
                Orders = new System.Collections.Generic.List<Order>()
                {
                    new Order
                    {
                        Code = 2,
                        Quantity = 12,
                        Notes = "qaad ads asd"
                    }
                }
            };
            OrdersController.Orders = new System.Collections.Generic.List<OrderResponseDTO>();

            // Act
            var response = controller.RequestOrder(request).Value as OrderResponseDTO;
            var expected = OrderStatus.Created;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expected, response.Status);
        }

        [Fact]
        public void RespondOrder()
        {
            // Arrange
            var controller = new OrdersController();
            var request = new OrderAcceptanceResponse();

            // Act
            var response = controller.RespondOrder(request);

            // Assert
            Assert.NotNull(response);
        }


        private void ResetOrderList()
        {
            OrdersController.Orders = new System.Collections.Generic.List<OrderResponseDTO>()
            {
                new OrderResponseDTO
                {
                    Id = 1,
                    DeliveryDate = DateTime.Now,
                    Orders = new System.Collections.Generic.List<Order>(){
                        new Order{
                            Code = 1,
                            Quantity = 12,
                            Notes = "testes teste"
                        }
                    },
                    OrderValue = 12.56m,
                    Status = OrderStatus.Created,
                    Links = new System.Collections.Generic.List<BaseDTOLink>()
                }
            };
        }
    }
}