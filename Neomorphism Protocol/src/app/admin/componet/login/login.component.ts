import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormArray, FormControl, FormBuilder, Validators } from '@angular/forms';
import { UserIdleService } from 'angular-user-idle';
import { LoginService } from '../../services/login.service';
import { CookieService } from 'ngx-cookie-service';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginUsersData: any = [];
  userNameInvalid: boolean = false;
  passwordInvalid: boolean = false;
  formSubmitted: boolean = false;
  isLogginWithSSO: boolean = false;
  isLoggedIn: boolean = false;
  button1Active: boolean = true;
  button2Active: boolean = false;
  darkMode: boolean = true;
  year: any;
  responsedata: any;
  otpresponse: any;
  status: boolean | undefined;
  token: any;
  showmenu: boolean = false;

  constructor(public router: Router, private service: LoginService, private fb: FormBuilder, private userIdle: UserIdleService, private cookieService: CookieService,public appcomponent: AppComponent,private route:Router) {
    //store a username and password data in empty array from service
    //this.loginUsersData = service.loginDetails;
    //this.isLoggedIn = service.isLoggedInStatus

    //getting current year
    this.year = new Date().getFullYear();
    localStorage.clear();
    this.appcomponent.logout();
    this.route.navigate(['page/login']);

  }
  
  // Login With User Code button function
  loginWithUserCodeFunction(loginData: any) {
    this.formSubmitted = false;
    this.service.ProceedSignin(loginData.value).subscribe((result) => {
      if (result != null) {
        this.responsedata = result;
        if (this.responsedata.user_gid == null || this.responsedata.user_gid == "") {
          //this.router.navigate(['page/login'])
          this.formSubmitted = true;
        }
        else if (this.responsedata.user_gid != null || this.responsedata.user_gid != "") {
          this.isLoggedIn = this.responsedata.status;
          this.cookieService.set('token', '"' + this.responsedata.token + '"', undefined, '/v1');
          localStorage.setItem('token', this.responsedata.token);
          localStorage.setItem('user_gid', this.responsedata.user_gid);
          this.router.navigate(['app/dashboard'])
        }
      }
    },(error) =>{
      if(error.status===401)
        this.router.navigate(['pages/401'])
      else if(error.status===404)
        this.router.navigate(['pages/404'])
    });
  }

  //Login with SSO function
  loginWithSSOfunction() {
    console.log(this.otpresponse);
    if (this.otpresponse == 'N') {
      this.isLogginWithSSO = true;
      var url = 'https://login.microsoftonline.com/655a0e0e-4a74-4a0c-86d8-370a992e90a6/oauth2/v2.0/authorize?client_id=6bbdd5f5-2e59-463c-bb96-f41c4149450e&response_type=code&redirect_uri=http://localhost/v2/response.html&response_mode=query&scope=https://graph.microsoft.com/User.Read&state=1345';
    }
    else if (this.otpresponse == 'Y'){
      url = 'http://localhost/v2/#/page/otplogin';
    }
    else{
      url = 'https://login.microsoftonline.com/655a0e0e-4a74-4a0c-86d8-370a992e90a6/oauth2/v2.0/authorize?client_id=6bbdd5f5-2e59-463c-bb96-f41c4149450e&response_type=code&redirect_uri=http://localhost/v2/response.html&response_mode=query&scope=https://graph.microsoft.com/User.Read&state=1345';
    }
    console.log(url);
    window.location.href = url;
  }

  // Login with user code function
  loginWithUserCode() {
    this.button2Active = true;
    this.button1Active = false;
  }

  // Login with SSO function
  loginWithSSO() {
    this.button1Active = true
    this.button2Active = false;
  }

  ngOnInit(): void {
    this.button2Active = true;
    localStorage['showmenu'] = false;
    localStorage.clear();
    // this.service.getOTPflag().subscribe(result => {
    //   if (result != null) {
    //     this.responsedata = result;
    //   }
    //   this.otpresponse = this.responsedata.otp_flag;
    //   console.log(this.otpresponse);
    // })
    this.status = this.service.IsLoggedIn();
  }
}
