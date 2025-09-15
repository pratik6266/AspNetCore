using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_apis.Db;
using _02_apis.Dtos;
using _02_apis.Model;
using _02_apis.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace _02_apis.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextProvider _dbContextProvider;
        public UserRepository(DbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _dbContextProvider.Users
            .Include(u => u.Comments)
            .ToListAsync();
            return users;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _dbContextProvider.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> CreateUser(User newUser)
        {
            _dbContextProvider.Users.Add(newUser);
            await _dbContextProvider.SaveChangesAsync();
            return newUser;
        }
    }
}