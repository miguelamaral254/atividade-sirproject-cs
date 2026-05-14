using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirProject.Core.Entities;
using SirProject.Core.Interfaces;
using SirProject.Core.Pagination;
using SirProject.Web.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace SirProject.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index([FromQuery] PaginationParameters paginationParameters)
        {
            var paginatedUsers = await _userService.GetAllPaginatedAsync(paginationParameters.PageNumber, paginationParameters.PageSize);
            return View(paginatedUsers);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(UserCreateDTO userCreateDTO)
        {
            if (!ModelState.IsValid) return View(userCreateDTO);

            var user = new User
            {
                Username = userCreateDTO.Username,
                PasswordHash = userCreateDTO.Password,
                Role = userCreateDTO.Role.ToString()
            };

            try
            {
                await _userService.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (System.InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userCreateDTO);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var dto = new UserUpdateDTO
            {
                Id = user.Id,
                Username = user.Username,
                Role = System.Enum.Parse<SirProject.Core.Enums.UserRole>(user.Role)
            };

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, UserUpdateDTO userUpdateDTO)
        {
            if (id != userUpdateDTO.Id) return BadRequest();
            if (!ModelState.IsValid) return View(userUpdateDTO);

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
                return RedirectToAction(nameof(Index));
            }
            catch (System.InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userUpdateDTO);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
