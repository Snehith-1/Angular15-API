
import { HomeService } from '../welcome/home.service';
import 'bootstrap';
import { AppService } from '../../../service.service';
import { DropDownAnimation } from '../../../animation';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { MenuService } from '../../services/welcome.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
  animations : [DropDownAnimation,
    trigger('slide_in_out', [
      state(
        'slide_in',
        style({
          width: '50px',
          'border-radius': '8px'
        })
      ),
      state(
        'slide_out',
        style({
          width: '250px',
          'border-radius': '8px',
          'text-align': 'left',     
          'animation-name': 'example',
          'animation-duration': '1s',
          'padding-left': '13px'
        })
      ),
      transition('slide_in <=> slide_out', animate(100)),
    ]),
  ],
})
export class MenuComponent implements OnInit{
  @ViewChild('toggleButton') targetButtonRef!: ElementRef;
  @ViewChild('toggleButtonMobile') targetButtonMobileRef!: ElementRef;
  token: string | null | undefined;
  
  @HostListener('window:resize', ['$event'])
  onWindowResize(event: any) {
    const screenWidth = event.target.innerWidth;
    if (screenWidth > 768) {
      this.sideBarExpanded = false;
    }
     else if(screenWidth < 768){
      this.sideBarExpanded = true;
    }
  }

  
  data :any
  responsedata: any;
  menu_list: any[] = [];
  employeename_list: any[] =[];
  companylogo_list: any[] = [];
  submenu_list: any[] = [];
  
  menuIconClickCount =0 ;
  showNav:boolean = false;
  showNavHeading:boolean = false;
  slider_state: string = 'slide_in';
  dropdown_state : string = 'slide_up';

  isFinanceSectionActive:boolean = false;
  isCommerceSectionActive:boolean = false;
  isFoundationectionActive:boolean = false;
  isCommonSectionActive:boolean = false;
  isMasterSectionActive:boolean = false;
  isSettingSectionActive:boolean = false;
  sliderIcon = "assets/icons/menu.svg";
  default_icon= "assets/icons/creditIconGreen.svg";
  isAdvanceModeSelected:boolean = true;
  sidebarButton:boolean = false;
  sidebarSubMenuButton:boolean = false;
  sideBarData:any = [];
  subMenu:any = [];
  currentUrl = '';
  headerOptionsArr:any = [
    {
      id:0,
      name:'',
      icon : ''      
    }

  ];
  selectedHeaderOption:any = {
    id:1,
    name:'Finance',
    icon : 'assets/financeImg.svg'
  };
  headerIndex:number = 1;
  mainMenu:any;
  menuName:any;
  obj = Object;
  dropdownIndex:number = 0;
  dropdownSubMenuIndex:number = 0;
  sideBarExpanded:boolean = false;
  darkModeEnnable:boolean = false;
  toggleCheckBox:boolean = false;
  applicationViewPageActiveState = false;
  mode:string = 'Light Mode';
  uiurl:any | string;
  constructor(public homeService:HomeService, public appService:AppService,public getData:MenuService,private router:Router){
    
    this.uiurl = environment.API_URL;
    // this.sideBarData = this.homeService.financeData;
    this.darkModeEnnable = localStorage.getItem('darkMode')=='true' ;
    if(localStorage.getItem('darkMode')=='true'){
      this.mode = 'Dark Mode';
    }else{
      this.mode = 'Light Mode';
    }
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.currentUrl = event.url;
        console.log(this.currentUrl);
      }
    })

  }
  /**Function for Hamburger Menu toggle button*/
  toggleSlider() {
    if(this.sideBarExpanded){
      this.slider_state = 'slide_in'
    }else{
      this.slider_state = 'slide_out'
    }/** Slider animation effect - decleared in animation array in side the component decorator */
    this.show();
  }

  /** Function for contents in the Hamburger menu show and hide when the menu toggled  */
  show(){
    if(!this.sideBarExpanded){
    setTimeout(() => {
      this.sideBarExpanded = !this.sideBarExpanded;
      this.sliderIcon = "assets/icons/slideLeft.svg";
    }, 300);
    }else{
      this.sideBarExpanded = !this.sideBarExpanded;
        this.sliderIcon = "assets/icons/menu.svg";
    }
 }

 
 /** Function for log out the Application */
 logOut(){
  localStorage.removeItem('UserName');
  localStorage.removeItem('Password');
  this.router.navigate(["page/login"])
}

/** This function used to click a one header button at a time */
// selectHeaderButton(index:number,headerName:string){
//   this.dropdownIndex = 0;
//   this.targetButtonRef.nativeElement.focus();
//   this.toggleSlider();
//   this.headerIndex = index+1;
//   if(index==0){
//     this.sideBarData = this.homeService.financeData;
//     this.isFinanceSectionActive = true;
//     this.isCommerceSectionActive = this.isFoundationectionActive = this.isCommonSectionActive = this.isMasterSectionActive = this.isSettingSectionActive = false;
//   }else if(index==1){
//     this.sideBarData = this.homeService.commerceData; 
//     this.isCommerceSectionActive = true;
//     this.isFinanceSectionActive = this.isFoundationectionActive = this.isCommonSectionActive = this.isMasterSectionActive = this.isSettingSectionActive = false;
//   }else if(index==2){
//     this.sideBarData = this.homeService.foundationData; 
//     this.isFoundationectionActive = true;
//     this.isCommerceSectionActive = this.isFinanceSectionActive = this.isCommonSectionActive = this.isMasterSectionActive = this.isSettingSectionActive = false;
//   }else if(index==3){
//     this.sideBarData = this.homeService.commonData; 
//     this.isCommonSectionActive = true;
//     this.isCommerceSectionActive = this.isFinanceSectionActive = this.isFoundationectionActive = this.isMasterSectionActive = this.isSettingSectionActive = false;
//   }else if(index==4){
//     this.sideBarData = this.homeService.masterData;
//     this.isMasterSectionActive = true;
//     this.isCommerceSectionActive = this.isFinanceSectionActive = this.isFoundationectionActive = this.isCommonSectionActive = this.isSettingSectionActive = false;
//   }else if(index==5){
//     this.sideBarData = this.homeService.settingData;
//     this.isSettingSectionActive = true;
//     this.isCommerceSectionActive = this.isFinanceSectionActive = this.isFoundationectionActive = this.isCommonSectionActive = this.isMasterSectionActive = false;
//   }
//  }

selectHeaderButton(index:number,headerName:string){
  this.dropdownIndex = 0;
  this.targetButtonRef.nativeElement.focus();
  this.toggleSlider();
  this.headerIndex = index+1;
  
  if(index==0){
    this.sideBarData = this.menu_list[index].submenu;
    this.isFinanceSectionActive = true;
    this.isCommerceSectionActive = this.isFoundationectionActive = this.isCommonSectionActive = this.isMasterSectionActive = this.isSettingSectionActive = false;
  }

  // if(index==0){
  //   this.sideBarData = this.homeService.financeData;
  //   this.isFinanceSectionActive = true;
  //   this.isCommerceSectionActive = this.isFoundationectionActive = this.isCommonSectionActive = this.isMasterSectionActive = this.isSettingSectionActive = false;
  // }
  else if(index==1){
    this.sideBarData = this.menu_list[index].submenu; 
    this.isCommerceSectionActive = true;
    this.isFinanceSectionActive = this.isFoundationectionActive = this.isCommonSectionActive = this.isMasterSectionActive = this.isSettingSectionActive = false;
  }else if(index==2){
    this.sideBarData = this.menu_list[index].submenu;
    this.isFoundationectionActive = true;
    this.isCommerceSectionActive = this.isFinanceSectionActive = this.isCommonSectionActive = this.isMasterSectionActive = this.isSettingSectionActive = false;
  }else if(index==3){
    this.sideBarData = this.menu_list[index].submenu;
    this.isCommonSectionActive = true;
    this.isCommerceSectionActive = this.isFinanceSectionActive = this.isFoundationectionActive = this.isMasterSectionActive = this.isSettingSectionActive = false;
  }else if(index==4){
    this.sideBarData = this.menu_list[index].submenu;
    this.isMasterSectionActive = true;
    this.isCommerceSectionActive = this.isFinanceSectionActive = this.isFoundationectionActive = this.isCommonSectionActive = this.isSettingSectionActive = false;
  }else if(index==5){
    this.sideBarData = this.menu_list[index].submenu;
    this.isSettingSectionActive = true;
    this.isCommerceSectionActive = this.isFinanceSectionActive = this.isFoundationectionActive = this.isCommonSectionActive = this.isMasterSectionActive = false;
  }
 }
 /** Funtion for Dark Mode toggle button */
 appModeToggle(value:any) {
  const hasClass = document.body.classList.contains('darkMode');
  if (!hasClass) {
      localStorage.setItem('darkMode', 'true');
      document.body.classList.add("darkMode"); 
      document.body.classList.remove("lightMode");       
      this.mode = 'Dark Mode'; 
  } else {
    localStorage.removeItem('darkMode');
    document.body.classList.remove("darkMode");
    document.body.classList.add("lightMode");    
    this.mode = 'Light Mode';
  }
  this.darkModeEnnable = localStorage.getItem('darkMode')=='true';  
  this.appService.darkModeEnnabled = this.darkModeEnnable;
  this.appService.setDarkMode(this.darkModeEnnable);
  // window.location.reload();
 }

 /** click action for sidebar's menu */
 sidebarClick(i:number,menuTitle:string){
  this.dropdownSubMenuIndex=0;
  if(this.dropdownIndex !== i ){
    this.dropdownIndex = i;
  }else{
    this.dropdownIndex = 0;
  }
 }

 menuIconClickEvent(i:number,menuTitle:string){  
  this.dropdownIndex = i;
  if(this.menuIconClickCount == 0){
    this.toggleSlider();
    this.sidebarClick(i,menuTitle);
    this.sidebarClick(i,menuTitle);
    this.menuIconClickCount += 1;
  }else if(!this.sideBarExpanded ){
    this.toggleSlider();
    this.sidebarClick(i,menuTitle);
    this.sidebarClick(i,menuTitle);
  }
 }
 /** click action for sidebar's sub menu */
 sidebarSubMenuClick(i:number, subMenuTitle:string){
  if(this.dropdownSubMenuIndex !== i ){
    this.dropdownSubMenuIndex = i;  
 }else{
    this.dropdownSubMenuIndex = 0;
 }

 }


/** Funtion for Header button (Dropdown) in mobile view */
mobileViewMenuDropdownFunction(selectedOption:any){
  this.targetButtonMobileRef.nativeElement.click();
  this.selectedHeaderOption = selectedOption;
  
  this.selectHeaderButton(selectedOption.id-1,selectedOption.name);
  this.sideBarExpanded = true;
}

/** trackby function for *ngFor directives */
trackByFn(index:any) {
  return index; // or item.id
}
 
 ngOnInit() { 
  document.body.classList.add("lightMode");
  this.getData.getPosts().subscribe((result,)=>{
    this.responsedata=result;
	localStorage.setItem('menu', JSON.stringify(this.responsedata.menu_list));
    this.menu_list = this.responsedata.menu_list;
    this.sideBarData = this.menu_list[0].submenu;
  });
  // this.getData.getEmployeename().subscribe((result,)=>{
  //   this.responsedata=result;
	// localStorage.setItem('menu', JSON.stringify(this.responsedata.employeename_list));
  //   this.employeename_list = this.responsedata.employeename_list;
  //   // this.sideBarData = this.menu_list[0].submenu;
  // });
  if(localStorage.getItem('darkMode')=='true'){
    this.toggleCheckBox = true;
  }
  }
  goToPage(url: string, name: string): void {
    if(name == 'Application Creation'){
      this.router.navigate(["app/applicationsummary"])
    }
  //  else if(name == 'Entity'){
  //     this.router.navigate(["app/entity"])
  //   }
    else{
      // this.router.navigate([url])
      // window.location.href = url;
      this.router.navigate([url])
    }
  }
}

