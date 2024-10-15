using NUnit.Framework;
using SeuManoelStore.Core;
using SeuManoelStore.Domain.Models;
using System.Collections.Generic;

namespace SeuManoelStore.Tests
{
    [TestFixture]
    public class PackingServiceTests
    {
        private PackingService _packingService;

        [SetUp]
        public void SetUp()
        {
            _packingService = new PackingService();
        }

        [Test]
        public void PackOrders_WithValidProducts_ReturnsPackedOrders()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                Orders = new List<SingleOrderRequest>
                {
                    new SingleOrderRequest
                    {
                        OrderId = 1,
                        Products = new List<OrderProduct>
                        {
                            new OrderProduct { ProductId = "Product1", Dimensions = new ProductDimensions { Height = 20, Width = 30, Length = 40 } },
                            new OrderProduct { ProductId = "Product2", Dimensions = new ProductDimensions { Height = 15, Width = 25, Length = 35 } }
                        }
                    }
                }
            };

            // Act
            var response = _packingService.PackOrders(orderRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(response.Orders, Is.Not.Empty);
                Assert.That(response.Orders.Count, Is.EqualTo(1));
                Assert.That(response.Orders[0].Boxes, Is.Not.Empty);
                Assert.That(response.Orders[0].Boxes.Count, Is.EqualTo(1));
                Assert.That(response.Orders[0].Boxes[0].BoxId, Is.EqualTo("Caixa 1"));
            });
        }

        [Test]
        public void PackOrders_WithProductsThatDoNotFit_ReturnsObservation()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                Orders = new List<SingleOrderRequest>
                {
                    new SingleOrderRequest
                    {
                        OrderId = 2,
                        Products = new List<OrderProduct>
                        {
                            new OrderProduct { ProductId = "Product3", Dimensions = new ProductDimensions { Height = 100, Width = 100, Length = 100 } }
                        }
                    }
                }
            };

            // Act
            var response = _packingService.PackOrders(orderRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(response.Orders, Is.Not.Empty);
                Assert.That(response.Orders.Count, Is.EqualTo(1));
                Assert.That(response.Orders[0].Boxes, Is.Not.Empty);
                Assert.That(response.Orders[0].Boxes[0].Observation, Contains.Substring("Produto não cabe em nenhuma caixa disponível."));
            });
        }

        [Test]
        public void PackOrders_WithMultipleProducts_ReturnsCorrectBoxes()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                Orders = new List<SingleOrderRequest>
                {
                    new SingleOrderRequest
                    {
                        OrderId = 3,
                        Products = new List<OrderProduct>
                        {
                            new OrderProduct { ProductId = "Product4", Dimensions = new ProductDimensions { Height = 20, Width = 30, Length = 40 } },
                            new OrderProduct { ProductId = "Product5", Dimensions = new ProductDimensions { Height = 20, Width = 30, Length = 40 } }
                        }
                    }
                }
            };

            // Act
            var response = _packingService.PackOrders(orderRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(response.Orders, Is.Not.Empty);
                Assert.That(response.Orders.Count, Is.EqualTo(1));
                Assert.That(response.Orders[0].Boxes, Is.Not.Empty);
                Assert.That(response.Orders[0].Boxes.Count, Is.EqualTo(1));
                Assert.That(response.Orders[0].Boxes[0].BoxId, Is.EqualTo("Caixa 1"));
            });
        }
    }
}
