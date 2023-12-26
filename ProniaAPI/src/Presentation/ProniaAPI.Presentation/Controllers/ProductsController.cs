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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductUpdateDto updateDto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id, updateDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
