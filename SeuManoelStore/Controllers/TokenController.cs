using Microsoft.AspNetCore.Mvc;
using SeuManoelStore.Core.Interface;

namespace SeuManoelStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("generate-token")]
        public ActionResult<string> GenerateToken()
        {
            var token = _tokenService.GenerateToken();
            return Ok(token);
        }
    }
}
