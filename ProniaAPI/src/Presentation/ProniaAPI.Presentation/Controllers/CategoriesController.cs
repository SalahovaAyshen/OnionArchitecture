using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Categories;
using ProniaAPI.Domain.Enums;

namespace ProniaAPI.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ITokenHandler _handler;

        public CategoriesController(ICategoryService service, ITokenHandler handler)
        {
            _service = service;
            _handler = handler;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 0, int take = 0)
        {
            return StatusCode(StatusCodes.Status200OK, await _service.GetAllAsync(page, take));
        }
        [HttpGet("{id}")]

        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> GetById(int id)
        {
            if(id<=0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK, await _service.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto createDTO)
        {

            return StatusCode(StatusCodes.Status201Created, _service.Create(createDTO));
        }
        [HttpGet("[Action]")]

        public async Task<IActionResult> Task()
        {
            return Ok(_handler.CreateRefreshToken());
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
