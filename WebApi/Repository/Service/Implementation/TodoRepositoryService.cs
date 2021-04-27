using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository.Service.Interface;

namespace WebApi.Repository.Service.Implementation
{
    public class TodoRepositoryService : ITodoRepositoryService
    {
        private readonly DataContext dc;

        public TodoRepositoryService(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddTodo(Todo todo)
        {
            dc.Todos.AddAsync(todo);
        }

        public void DeleteTodo(int todoId)
        {
            var todo = dc.Todos.Find(todoId);
            dc.Todos.Remove(todo);
        }

        public async Task<Todo> FindTodoById(int id) 
        {
            var todo = dc.Todos.FindAsync(id);
            //var city = dc.cities.Where(d=>d.Id==id).FirstOrDefaultAsync();
            //As No Tracking
            return await todo;
        }

        public async Task<IEnumerable<Todo>> GetTodoAsync()
        {
            return await dc.Todos.ToListAsync();
        }
    }
}
