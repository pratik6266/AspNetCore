using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_apis.Model;

namespace _02_apis.Service.Interface
{
    public interface IGetAllUsers
    {
        Task<List<User>> getAll();
        Task<User?> getOneById(int id);
        Task<User> CreateUser(User newUser);
    }
}