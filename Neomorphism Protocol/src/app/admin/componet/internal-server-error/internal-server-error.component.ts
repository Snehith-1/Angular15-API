import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-internal-server-error',
  templateUrl: './internal-server-error.component.html',
  styleUrls: ['./internal-server-error.component.scss']
})
export class InternalServerErrorComponent {
  constructor(private router:Router,public appcomponent: AppComponent,){
    localStorage.clear(); 
    this.appcomponent.logout();
    this.appcomponent.hideheader();
  }
  backToLoginPage(){
    this.router.navigateByUrl('/page/login');
  }
  backToDashboard(){
    this.router.navigateByUrl('/app/welcome');
  }
}
