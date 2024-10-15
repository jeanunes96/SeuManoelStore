using SeuManoelStore.Core.Interface;
using SeuManoelStore.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SeuManoelStore.Core
{
    public class PackingService : IPackingService
    {
        private static readonly List<Box> AvailableBoxes = new List<Box>
        {
            new Box { BoxId = "Caixa 1", Dimensions = new Dimensions { Height = 30, Width = 40, Length = 80 } },
            new Box { BoxId = "Caixa 2", Dimensions = new Dimensions { Height = 80, Width = 50, Length = 40 } },
            new Box { BoxId = "Caixa 3", Dimensions = new Dimensions { Height = 50, Width = 80, Length = 60 } }
        };

        public OrderResponse PackOrders(OrderRequest orderRequests)
        {
            var orders = orderRequests.Orders.Select(orderRequest =>
                new Order
                {
                    OrderId = orderRequest.OrderId,
                    Boxes = PackProductsIntoBoxes(orderRequest.Products)
                }).ToList();

            return new OrderResponse { Orders = orders };
        }

        private List<BoxResponse> PackProductsIntoBoxes(List<OrderProduct> products)
        {
            var boxUsages = new List<BoxResponse>();
            var productsNotPacked = new List<OrderProduct>(products);

            foreach (var box in AvailableBoxes)
            {
                var packedProducts = new List<OrderProduct>();

                foreach (var product in productsNotPacked.ToList())
                {
                    if (FitsInBox(product, box))
                    {
                        packedProducts.Add(product);
                        productsNotPacked.Remove(product);
                    }
                }

                if (packedProducts.Any())
                {
                    boxUsages.Add(new BoxResponse
                    {
                        BoxId = box.BoxId,
                        Products = packedProducts.Select(p => p.ProductId).ToList(),
                        Observation = null
                    });
                }
                
                if (!productsNotPacked.Any())
                {
                    break;
                }
            }

            HandleUnpackedProducts(boxUsages, productsNotPacked);
            return boxUsages;
        }

        private bool FitsInBox(OrderProduct product, Box box)
        {
            return product.Dimensions.Height <= box.Dimensions.Height &&
                   product.Dimensions.Width <= box.Dimensions.Width &&
                   product.Dimensions.Length <= box.Dimensions.Length;
        }

        private void HandleUnpackedProducts(List<BoxResponse> boxUsages, List<OrderProduct> productsNotPacked)
        {
            if (productsNotPacked.Any())
            {
                foreach (var box in boxUsages)
                {
                    box.Observation = "Produto não cabe em nenhuma caixa disponível.";
                }

                if (!boxUsages.Any())
                {
                    boxUsages.Add(new BoxResponse
                    {
                        BoxId = null,
                        Products = productsNotPacked.Select(p => p.ProductId).ToList(),
                        Observation = "Produto não cabe em nenhuma caixa disponível."
                    });
                }
            }
        }
    }
}
