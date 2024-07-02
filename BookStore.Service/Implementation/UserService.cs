using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string getUserEmail(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            string email = loggedInUser?.Email;

            return email;
        }
    }
}
