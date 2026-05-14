using SirProject.Core.Entities;
using SirProject.Core.Interfaces;
using SirProject.Core.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SirProject.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<PaginatedList<User>> GetAllPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _userRepository.GetAllPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(User user)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(user.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }
            return await _userRepository.CreateAsync(user);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                return false;
            }

            var usernameConflict = await _userRepository.GetByUsernameAsync(user.Username);
            if (usernameConflict != null && usernameConflict.Id != user.Id)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
