using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirProject.Core.Interfaces;
using SirProject.Core.Pagination;
using SirProject.Web.DTOs;
using SirProject.Web.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace SirProject.Web.ControllersAPI
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PessoasApiController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasApiController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters paginationParameters)
        {
            var paginatedPessoas = await _pessoaService.GetAllPaginatedAsync(paginationParameters.PageNumber, paginationParameters.PageSize);
            var dtos = paginatedPessoas.Select(p => p.ToDTO()).ToList();
            return Ok(new PaginatedList<PessoaDTO>(dtos, paginatedPessoas.TotalCount, paginatedPessoas.PageIndex, paginatedPessoas.PageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pessoa = await _pessoaService.GetByIdAsync(id);
            if (pessoa == null) return NotFound();
            return Ok(pessoa.ToDTO());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PessoaCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // Map PessoaCreateDTO to Entity manually or via mapper
                var entity = dto.ToEntity();
                await _pessoaService.CreateAsync(entity);
                return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] PessoaDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var success = await _pessoaService.UpdateAsync(dto.ToEntity());
                if (!success) return NotFound();
                return Ok(dto);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pessoaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
