import { AuthService } from './services/auth.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar/nav-bar.component';
import { UserLoginComponent } from './user/user-login/user-login.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TodoListComponent } from './user/todo-list/todo-list.component';
import { TodoappService } from './services/todoapp.service';

const appRoutes:Routes =[
  { path: 'user/login', component:UserLoginComponent},
  { path: 'user/register', component:UserRegisterComponent},
  { path: 'user/todoList', component:TodoListComponent},
  { path: '**', component:UserLoginComponent},
]

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    UserLoginComponent,
    UserRegisterComponent,
    TodoListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    RouterModule.forRoot(appRoutes),
    TabsModule.forRoot(),
  ],
  providers: [
    AuthService,
    TodoappService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
