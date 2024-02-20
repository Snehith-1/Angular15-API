import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  isLoggedInStatus = false;

  constructor(private http: HttpClient) { }

  isLoggedIn(loginStatus: boolean) {
    this.isLoggedInStatus = loginStatus;
    return this.isLoggedInStatus;
  }

  apiurl = 'Login/usercodeLogin';
  otp_url = 'Login/GetOTPFlag';
  otplogin_url = 'Login/OTPlogin'

  ProceedSignin(UserCred: any) {
    //console.log(this.apiurl);
    return this.http.post(this.apiurl, UserCred);
  }

  getOTPflag() {
    return this.http.get(this.otp_url);
  }

  IsLoggedIn() {
    return localStorage.getItem('token') != null;
  }

  otplogin(email: string) {
    return this.http.post(this.otplogin_url, email);
  }

  user_profile(){
  }
}