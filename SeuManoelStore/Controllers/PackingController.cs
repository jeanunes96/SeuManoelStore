using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeuManoelStore.Core.Interface;
using SeuManoelStore.Domain.Models;

namespace SeuManoelStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Isso exige que um token válido seja fornecido
    public class PackingController : ControllerBase
    {
        private readonly IPackingService _packingService;

        public PackingController(IPackingService packingService)
        {
            _packingService = packingService;
        }

        [HttpPost("pack-orders")]
        public ActionResult<OrderResponse> PackOrders([FromBody] OrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                return BadRequest("Nenhum pedido fornecido.");
            }

            var response = _packingService.PackOrders(orderRequest);
            return Ok(response);
        }
    }
}
