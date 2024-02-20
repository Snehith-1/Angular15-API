import { Component, HostListener, Input, OnInit } from '@angular/core';
import { NavigationEnd, Route, Router } from '@angular/router';
import { AppService } from 'src/app/service.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit{
  year:any;
  currentUrl:any;
  isDarkMode:any;
  @Input() childData: any;
  constructor(private router:Router, private service:AppService){
    
    this.service.mode$.subscribe(data => {
      this.isDarkMode = data;
    });
    this.year = new Date().getFullYear(); /** store a current year in a variable */
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.currentUrl = window.location.pathname;
      }
    });

  }
  ngOnInit(): void {
  }
}
