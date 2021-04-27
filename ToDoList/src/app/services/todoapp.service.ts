import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Todo } from '../todo';

@Injectable({
  providedIn: 'root'
})
export class TodoappService {

  baseUrl= environment.baseUrl;
  constructor(private http: HttpClient) { }
  SaveTodo(todo:Todo){
    console.log(todo)
    return this.http.post(this.baseUrl +'todoList/post',todo)
    
  }
  // TaskCompleteStatus(todo:any){
  //   return this.http.patch(this.baseUrl +'todoList/update',todo)
  // }
  TaskCompleteStatus(id:any,data:any){
    return this.http.patch(this.baseUrl +'todoList/update/'+id,data)
   
  }
  getAllTodoListDb(){
    return this.http.get(this.baseUrl +'todoList/todo');
  }

  deleteFromDb(id:any){
    return this.http.delete(this.baseUrl +'todoList/delete/'+id);
  }
}
