import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserRegisterVM } from 'src/app/model/User';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {
  registrationForm!: FormGroup;
  user!: UserRegisterVM;
  userSubmitted!: boolean;
  constructor(
    private fb: FormBuilder, 
    private authService: AuthService,
    ) 
    { }

  ngOnInit(): void {
    this.createRegisterationForm();
  }
createRegisterationForm(){
  this.registrationForm = this.fb.group({
    userName:[null,Validators.required],
    email:[null,[Validators.required,Validators.email]],
    password:[null, [Validators.required,Validators.minLength(5)]],
    confirmPassword:[null, [Validators.required]],
    mobile:[null, [Validators.required,Validators.maxLength(11)]],
    role:[null, [Validators.required,Validators.maxLength(11)]]
  });
}


  get userName() {
    return this.registrationForm.get('userName') as FormControl;
  }
  get email() {
    return this.registrationForm.get('email') as FormControl;
  }
  get password() {
    return this.registrationForm.get('password') as FormControl;
  }
  get confirmPassword() {
    return this.registrationForm.get('confirmPassword') as FormControl;
  }
  get mobile() {
    return this.registrationForm.get('mobile') as FormControl;
  }
  get role() {
     return this.registrationForm.get('role') as FormControl;
  }

  onSubmit() {
    this.userSubmitted = true;
    if(this.registrationForm.valid){
      this.authService.registerUser(this.userData()).subscribe(()=>{
        this.registrationForm.reset();
        this.userSubmitted = false;
      },error=>{
        console.log(error);
      })
    }else{
    }
  }

  userData():UserRegisterVM{
    return this.user ={
      userName:this.userName.value,
      email:this.email.value,
      password:this.password.value,
      mobile:this.mobile.value,
      role:this.role.value,
    }
  }
}
