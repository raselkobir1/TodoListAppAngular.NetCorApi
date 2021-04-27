using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository.Service.Interface
{
    public interface IUnitOfWorkService
    {
        ITodoRepositoryService TodoRepository { get; } 
        IUserRepositoryService UserRepository { get; } 
        Task<bool> SaveChangeAsync();
    }
}


