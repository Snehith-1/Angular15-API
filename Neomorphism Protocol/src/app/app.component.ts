import { Component, OnInit, Renderer2, VERSION, ViewChild } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { UserIdleModule, UserIdleService } from "angular-user-idle";
import { merge, fromEvent } from "rxjs";
import { UikitComponent } from './shared/component/uikit/uikit.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title:any = 'Samunnati';
  showmenu = true;
  currentUrl:any;
  showFooter:boolean = true;
  loading:any;
  showMenu:boolean = true;
  darkModeEnnable:boolean = false;
  @ViewChild('toastDiv') toastDiv:any;
  constructor(public router: Router, public userIdle: UserIdleService, public renderer: Renderer2, public notify:UikitComponent){
    this.darkModeEnnable = localStorage.getItem('darkMode')=='true' ;
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        
        this.currentUrl = event.url;
        if(this.currentUrl == '/page/500' || this.currentUrl == '/page/login' || this.currentUrl == '/' || this.currentUrl == '/page/internel-server-error'){
          this.showmenu = false;        
        }else{
          this.showmenu = true;
        }

        if(this.currentUrl == '/page/page-not-found' || this.currentUrl == '/page/login' || this.currentUrl == '/page/internel-server-error'){
          this.showFooter = false;        
        }else{
          this.showFooter = true;
        }
        
      }
    });

    router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.loading = true;
      }
      else if (event instanceof NavigationEnd) {
        setTimeout(() => {
          this.loading = false;
        }, 500);
      }
    });
  }
  ngOnInit(){
    this.router.events.subscribe(
      (val)=>{
        if(val instanceof NavigationEnd){
          if(val.url == '/page/login' || val.url == '/page/500' || val.url == '/page/404'){
            this.showmenu = false;
          }
          else{
            this.showmenu = true;
          }
        }
      }
    )
  }
  uilock(){this.loading = true;}
  uiunlock(){this.loading = false;}
  hideheader(){
    this.router.events.subscribe(
      (val)=>{
        if(val instanceof NavigationEnd){
          if(val.url == '/page/login' || val.url == '/page/500' || val.url == '/page/404'){
            this.showmenu = false;
          }
          else{
            this.showmenu = true;
          }
        }
      }
    )
  }
  logout(){
    this.showmenu = false;
  }
  showToastMessage(status:any,message:any){
    this.notify.showToastMessage(status,this.toastDiv,message);
  }
}
