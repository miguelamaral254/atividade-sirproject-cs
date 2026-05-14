using SirProject.Core.Entities;
using SirProject.Web.DTOs;

namespace SirProject.Web.Mappers
{
    public static class PessoaMapper
    {
        public static Pessoa ToEntity(this PessoaDTO dto)
        {
            return new Pessoa
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone ?? string.Empty,
                BirthDate = dto.BirthDate.Kind == DateTimeKind.Unspecified 
                    ? DateTime.SpecifyKind(dto.BirthDate, DateTimeKind.Utc)
                    : dto.BirthDate.ToUniversalTime(),
                ImagePath = dto.ImagePath
            };
        }

        public static PessoaDTO ToDTO(this Pessoa entity)
        {
            return new PessoaDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                BirthDate = entity.BirthDate,
                ImagePath = entity.ImagePath
            };
        }
    }
}
