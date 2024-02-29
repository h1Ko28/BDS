import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserForLogin, UserForRegister } from '../model/user';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  AuthUser(userLogin: UserForLogin){
    return this.http.post("http://localhost:5237/api/account/login/", userLogin);
  }

  RegisUser(userRegis: UserForRegister) {
    return this.http.post("http://localhost:5237/api/account/register/", userRegis)
  }
}
