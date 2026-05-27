using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirProject.Core.Entities;
using SirProject.Core.Interfaces;
using SirProject.Core.Pagination;
using SirProject.Web.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace SirProject.Web.ControllersAPI
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersApiController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters paginationParameters)
        {
            var paginatedUsers = await _userService.GetAllPaginatedAsync(paginationParameters.PageNumber, paginationParameters.PageSize);
            return Ok(paginatedUsers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO userCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                Username = userCreateDTO.Username,
                PasswordHash = userCreateDTO.Password,
                Role = userCreateDTO.Role.ToString()
            };

            try
            {
                await _userService.CreateAsync(user);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            if (id != userUpdateDTO.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            user.Username = userUpdateDTO.Username;
            user.Role = userUpdateDTO.Role.ToString();
            if (!string.IsNullOrEmpty(userUpdateDTO.Password))
            {
                user.PasswordHash = userUpdateDTO.Password;
            }

            try
            {
                var success = await _userService.UpdateAsync(user);
                if (!success) return NotFound();
                return Ok(user);
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
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
