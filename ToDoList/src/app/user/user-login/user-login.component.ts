import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLoginVM } from 'src/app/model/User';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
  }
  onLogin(loginForm: NgForm) {
    this.authService.authUser(loginForm.value).subscribe((data:any) => {
      const user = data;
      console.log(user)
      console.log('login Token :'+user.token);
      this.router.navigate(['/user/todoList']);
      alert("Login success");
      loginForm.reset();
    })
  }
}
