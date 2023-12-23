import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { User } from 'src/app/model/user';
import { AlertifyService } from 'src/app/services/alertify.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {
  
  registerationForm!: FormGroup
  user!: User;
  userSubmitted!: boolean;

  constructor(private fb: FormBuilder, private userService: UserService, private alert: AlertifyService) { }

  ngOnInit() {
    this.createRegisterationForm();
  }
  
  createRegisterationForm() {
    this.registerationForm =  this.fb.group({
        userName: [null, Validators.required],
        email: [null, [Validators.required, Validators.email]],
        password: [null, [Validators.required, Validators.minLength(8)]],
        confirmPassword: [null, Validators.required],
        mobile: [null, [Validators.required, Validators.maxLength(10)]]
    }, {validators: this.passwordMatchingValidator});
  }

  passwordMatchingValidator(fc: AbstractControl): ValidationErrors | null {
    return fc.get('password')?.value === fc.get('confirmPassword')?.value ? null :
    { notmatched: true }
  };

  get userName(){
    return this.registerationForm.get('userName')
  }
  get email(){
    return this.registerationForm.get('email')
  }
  get password(){
    return this.registerationForm.get('password')
  }
  get confirmPassword(){
    return this.registerationForm.get('confirmPassword')
  }
  get mobile(){
    return this.registerationForm.get('mobile')
  }

  onSubmit(){
    this.userSubmitted = true;
    if(this.registerationForm.valid){
      this.userService.addUser(this.dataUser());
      this.registerationForm.reset();
      this.userSubmitted = false;
      this.alert.success('Register Successful!');
    } else {
      this.alert.error('Something went wrong!');
    }
  }

  dataUser(): User{
    return this.user = {
      userName: this.userName?.value,
      email: this.email?.value,
      password: this.password?.value,
      mobile: this.mobile?.value
    }
  }
}
