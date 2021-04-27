import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserLoginVM, UserRegisterVM } from '../model/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl= environment.baseUrl;
  constructor(private http: HttpClient) { }
  authUser(user:UserLoginVM){
    return this.http.post(this.baseUrl +'account/login',user)
  }

  registerUser(user:UserRegisterVM){
    return this.http.post(this.baseUrl+'account/register',user);
  }
}

