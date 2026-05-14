using SirProject.Core.Entities;
using SirProject.Core.Interfaces;
using SirProject.Core.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SirProject.Core.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _repository;

        public PessoaService(IPessoaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Pessoa?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Pessoa pessoa)
        {
            var existing = await _repository.GetByEmailAsync(pessoa.Email);
            if (existing != null)
            {
                throw new InvalidOperationException("Email already exists");
            }
            return await _repository.CreateAsync(pessoa);
        }

        public async Task<bool> UpdateAsync(Pessoa pessoa)
        {
            var existing = await _repository.GetByIdAsync(pessoa.Id);
            if (existing == null) return false;

            var emailConflict = await _repository.GetByEmailAsync(pessoa.Email);
            if (emailConflict != null && emailConflict.Id != pessoa.Id)
            {
                throw new InvalidOperationException("Email already exists");
            }

            return await _repository.UpdateAsync(pessoa);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<PaginatedList<Pessoa>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetAllPaginatedAsync(pageNumber, pageSize);
        }
    }
}
