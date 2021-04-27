using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.Service.Interface
{
    public interface IUserRepositoryService
    {
        Task<User> Authenticate(string userName, string password);  
        void Register(string userName, string password,string role);  
        Task<bool> UserAlreadyExists(string userName);
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User> FindUserById(int id);


    }
}
