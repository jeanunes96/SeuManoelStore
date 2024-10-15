using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SeuManoelStore.Domain.Models
{
    public class OrderRequest
    {
        [JsonPropertyName("pedidos")]
        public required List<SingleOrderRequest> Orders { get; set; }
    }

    public class SingleOrderRequest
    {
        [JsonPropertyName("pedido_id")]
        public int OrderId { get; set; }

        [JsonPropertyName("produtos")]
        public required List<OrderProduct> Products { get; set; }
    }

    public class OrderProduct
    {
        [JsonPropertyName("produto_id")]
        public required string ProductId { get; set; }

        [JsonPropertyName("dimensoes")]
        public required ProductDimensions Dimensions { get; set; }
    }

    public class ProductDimensions
    {
        [JsonPropertyName("altura")]
        public int Height { get; set; }

        [JsonPropertyName("largura")]
        public int Width { get; set; }

        [JsonPropertyName("comprimento")]
        public int Length { get; set; }
    }
}