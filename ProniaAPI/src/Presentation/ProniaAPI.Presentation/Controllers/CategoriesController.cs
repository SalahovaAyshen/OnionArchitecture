using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Categories;

namespace ProniaAPI.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 0, int take = 0)
        {
            return StatusCode(StatusCodes.Status200OK, await _service.GetAllAsync(page, take));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto createDTO)
        {

            return StatusCode(StatusCodes.Status201Created, _service.Create(createDTO));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.Update(id, name);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.SoftDeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
