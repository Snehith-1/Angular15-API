import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styleUrls: ['./page-not-found.component.scss']
})
export class PageNotFoundComponent {

  hasIntialized:any;
  appcomponent: any;
  
 constructor(private router:Router, private location: Location, private route:ActivatedRoute){
 }
   ngOnInit() {
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
