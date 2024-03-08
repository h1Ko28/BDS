import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, NgForm, ValidationErrors, Validators } from '@angular/forms';
import { UserForRegister } from 'src/app/model/user';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  registerationForm!: FormGroup
  user!: UserForRegister;
  userSubmitted!: boolean;

  constructor(private fb: FormBuilder, private authService: AuthService, private alert: AlertifyService) { }

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

  onSubmit() {
    this.userSubmitted = true;

    if (this.registerationForm.valid) {
        // this.user = Object.assign(this.user, this.registerationForm.value);
        this.authService.RegisUser(this.dataUser()).subscribe(() =>
        {
          this.onReset();
          this.alert.success('Congrats, you are successfully registered');
        });
    }
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

  dataUser(): UserForRegister{
    return this.user = {
      userName: this.userName?.value,
      email: this.email?.value,
      password: this.password?.value,
      mobile: this.mobile?.value
    }
  }

  onReset() {
    this.userSubmitted = false;
    this.registerationForm.reset();
  }
}
