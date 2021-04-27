import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TodoappService } from 'src/app/services/todoapp.service';
import { Todo } from 'src/app/todo';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {
  // @Output() addTodo: EventEmitter<any> = new EventEmitter();
  // title!: string;
  name = ""
  status = false;
  todos: Todo[] = [];
  editable = false;
  addButton = "Add To List";
  editableIndex = 0;
  ischecked = false;
  todoDbList:any[]=[];
  constructor(private todeService: TodoappService) { }

  ngOnInit(): void {
    this.getAlltodoList();
  }
  Add() {
    if (this.editable === true) {
      const TodosList: Todo = {
        name: this.name,
        status: this.status,
        editable: this.editable
      }
      this.todos.splice(this.editableIndex, 1, TodosList);
      this.addButton = "Add To List";
      this.name = "";
      this.editable = false;
    }
    else {
      const TodosList: Todo = {
        name: this.name,
        status: this.status,
        editable: this.editable
      }
      this.todos.splice(0, 0, TodosList);
      this.name = "";
      //-----------save to Data Base----------
      this.todeService.SaveTodo(TodosList).subscribe(res => {
        console.log(res);
        this.getAlltodoList();
      });
      //---------------------
    }
  }
  EditTask(item: any, index: any) {
    this.addButton = "Update";
    this.editable = true;
    this.name = item;
    this.editableIndex = index;
  }
  DeleteTask(i: any) {
    this.todos.splice(i, 1);
    this.addButton = "Add To List";
  }

  onCheckboxChange(item: any) { 
    alert("Task Complete");
    var data ={
      status:"complete"
    }
    this.todeService.TaskCompleteStatus(item,data).subscribe(res => {
      console.log(res)
      this.getAlltodoList();
    })
  }

  getAlltodoList(){
    this.todeService.getAllTodoListDb().subscribe((res:any)=>{
      this.todoDbList=res;
    })
  }
  DeleteFromDb(item:any){
    this.todeService.deleteFromDb(item).subscribe(res=>{
      this.getAlltodoList();
    });
  }
  GetUserById(){

  }

}
