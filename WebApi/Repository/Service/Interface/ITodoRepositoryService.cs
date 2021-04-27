using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.Service.Interface
{
    public interface ITodoRepositoryService
    {
        Task<IEnumerable<Todo>> GetTodoAsync(); 
        void AddTodo(Todo todo);
        void DeleteTodo(int TodoId);  
        Task<Todo> FindTodoById(int id); 
    }
}
