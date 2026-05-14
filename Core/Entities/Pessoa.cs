using System;

namespace SirProject.Core.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string? ImagePath { get; set; }
    }
}
