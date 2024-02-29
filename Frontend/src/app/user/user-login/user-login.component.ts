import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserForLogin } from 'src/app/model/user';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  constructor(private authService: AuthService, private alert: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  onLogin(loginForm: NgForm) {
    this.authService.AuthUser(loginForm.value).subscribe(
      (response) => {
          const user = response as UserForLogin;
          if (user) {
            localStorage.setItem('token', user.Token);
            localStorage.setItem('userName', user.UserName);
            this.alert.success('Login Successful');
            this.router.navigate(['/']);
          }
      },
      (error) => {
          console.log(error);
          this.alert.error(error.error);
      }
    )
  }
}
