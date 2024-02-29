using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string UserName, string password);

        void Register(string UserName, string password);
        Task<bool> UserAlreadyExists(string userName);
    }
}