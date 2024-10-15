using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SeuManoelStore.Domain.Models
{
    public class OrderResponse
    {
        [JsonPropertyName("pedidos")]
        public required List<Order> Orders { get; set; }
    }

    public class Order
    {
        [JsonPropertyName("pedido_id")]
        public int OrderId { get; set; }

        [JsonPropertyName("caixas")]
        public required List<BoxResponse> Boxes { get; set; }
    }

    public class BoxResponse
    {
        [JsonPropertyName("caixa_id")]
        public string? BoxId { get; set; }

        [JsonPropertyName("produtos")]
        public required List<string> Products { get; set; }

        [JsonPropertyName("observacao")]
        public string? Observation { get; set; }
    }
}