using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace SirProject.Web.DTOs
{
    public class PessoaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo E-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória!")]
        public DateTime BirthDate { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile? ImageFile { get; set; }
    }

    public class PessoaCreateDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo E-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória!")]
        public DateTime BirthDate { get; set; }
    }

    public class LoginDTO
    {
        [Required(ErrorMessage = "O campo Usuário é obrigatório!")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        public string Password { get; set; } = string.Empty;
    }
}
