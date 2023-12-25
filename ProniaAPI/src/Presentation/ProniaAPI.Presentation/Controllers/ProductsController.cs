using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Products;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(id<=0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK, await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateDto createDto)
        {
            await _service.CreateAsync(createDto);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
