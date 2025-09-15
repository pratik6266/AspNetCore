using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_apis.Db;
using _02_apis.Model;

namespace _02_apis.Repository.Interface
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsers();
        public Task<User?> GetUserById(int id);
        public Task<User> CreateUser(User newUser);
    }
}