using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository.Service.Interface;
using WebApi.Repository.ViewModels;

namespace WebApi.Controllers
{
    public class TodoListController : BaseController
    {
        private readonly DataContext _dc; 
        private readonly IUnitOfWorkService _unitOfWork;   
        private readonly IMapper _mapper;    

        public TodoListController(IUnitOfWorkService unitOfWork, IMapper mapper, DataContext dc)  
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dc = dc;
        }

        //api/TodoList/todo
        [AllowAnonymous]
        [HttpGet("todo")]
        public async Task<IActionResult> GetTodoList() 
        {
            var cities = await _unitOfWork.TodoRepository.GetTodoAsync();
            var cityVM = _mapper.Map<IEnumerable<TodoViewModel>>(cities);
            return Ok(cityVM);
        }

        //api/TodoList/post
        [AllowAnonymous]
        [HttpPost("post")]   
        public async Task<IActionResult> AddTodo(TodoViewModel todoVM)  
        {
            var todo = _mapper.Map<Todo>(todoVM);
            _unitOfWork.TodoRepository.AddTodo(todo);
            await _unitOfWork.SaveChangeAsync();
            return StatusCode(201);
        }
        [AllowAnonymous]
        [HttpPut("update1")]
        public async Task<IActionResult> UpdateTodo(TodoViewModel todoVM)  
        {
            int lastTodoId = _dc.Todos.Max(item => item.Id); //get last inserted id
            try
            {
                var todoFromDb = await _unitOfWork.TodoRepository.FindTodoById(lastTodoId);
                todoFromDb.status = "Done";
                todoFromDb.name = todoVM.name;
                if (todoFromDb == null) 
                {
                    return BadRequest("Update not Allowed");
                }
                _mapper.Map(todoVM, todoFromDb);
                await _unitOfWork.SaveChangeAsync();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpPatch("update/{id}")]  //httpPatch use for partially update
        public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<Todo> todoToPatch)
        {
            var todoFromDb = await _unitOfWork.TodoRepository.FindTodoById(id);
            //todoFromDb.status = "Done";
            todoToPatch.ApplyTo(todoFromDb, ModelState);
            await _unitOfWork.SaveChangeAsync();
            return StatusCode(200);
        }


        [AllowAnonymous]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            _unitOfWork.TodoRepository.DeleteTodo(id); 
            await _unitOfWork.SaveChangeAsync();
            return Ok(id);
        }
    }
}

