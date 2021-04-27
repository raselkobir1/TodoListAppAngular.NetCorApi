using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository.Service.Interface;

namespace WebApi.Repository.Service.Implementation
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly DataContext dc;
        public UnitOfWorkService(DataContext dc)
        {
            this.dc = dc;
        }
        public ITodoRepositoryService TodoRepository => 
            new TodoRepositoryService(dc);

        public IUserRepositoryService UserRepository => 
            new UserRepositoryService(dc);


        public async Task<bool> SaveChangeAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
        
    }
}
