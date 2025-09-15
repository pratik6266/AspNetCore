using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_apis.Model;
using _02_apis.Repository.Implementation;
using _02_apis.Repository.Interface;
using _02_apis.Service.Interface;

namespace _02_apis.Service.Implementation
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsers(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> getAll()
        {
            var users = await _userRepository.GetAllUsers();
            return users;
        }

        public async Task<User?> getOneById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return user;
        }

        public Task<User> CreateUser(User newUser)
        {
            var NewUser = _userRepository.CreateUser(newUser);
            return NewUser;
        }
    }
}