using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaAPI.Application.Abstractions.Services;

namespace ProniaAPI.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int take)
        {
            return StatusCode(StatusCodes.Status200OK, await _service.GetAll(page, take));
        }
    }
}
