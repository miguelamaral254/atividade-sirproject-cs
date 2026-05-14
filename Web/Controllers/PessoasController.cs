using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SirProject.Core.Interfaces;
using SirProject.Core.Pagination;
using SirProject.Web.DTOs;
using SirProject.Web.Mappers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SirProject.Controllers
{
    [Authorize]
    public class PessoasController : Controller
    {
        private readonly IPessoaService _pessoaService;
        private readonly IWebHostEnvironment _environment;

        public PessoasController(IPessoaService pessoaService, IWebHostEnvironment environment)
        {
            _pessoaService = pessoaService;
            _environment = environment;
        }

        public async Task<IActionResult> Index([FromQuery] PaginationParameters paginationParameters)
        {
            var paginatedPessoas = await _pessoaService.GetAllPaginatedAsync(paginationParameters.PageNumber, paginationParameters.PageSize);
            var dtos = paginatedPessoas.Select(p => p.ToDTO()).ToList();
            var paginatedDtos = new PaginatedList<PessoaDTO>(dtos, paginatedPessoas.TotalCount, paginatedPessoas.PageIndex, paginatedPessoas.PageSize);
            return View(paginatedDtos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pessoa = await _pessoaService.GetByIdAsync(id);
            if (pessoa == null) return NotFound();
            return View(pessoa.ToDTO());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PessoaDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            if (dto.ImageFile != null)
            {
                dto.ImagePath = await SaveImage(dto.ImageFile);
            }

            try
            {
                await _pessoaService.CreateAsync(dto.ToEntity());
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(dto);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _pessoaService.GetByIdAsync(id);
            if (pessoa == null) return NotFound();
            return View(pessoa.ToDTO());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, PessoaDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(dto);

            if (dto.ImageFile != null)
            {
                dto.ImagePath = await SaveImage(dto.ImageFile);
            }

            try
            {
                var success = await _pessoaService.UpdateAsync(dto.ToEntity());
                if (!success) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(dto);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _pessoaService.GetByIdAsync(id);
            if (pessoa == null) return NotFound();
            return View(pessoa.ToDTO());
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pessoaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveImage(IFormFile file)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/" + fileName;
        }
    }
}
