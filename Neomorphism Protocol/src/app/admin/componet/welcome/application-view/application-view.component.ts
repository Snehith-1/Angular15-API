import { Component, OnInit } from '@angular/core';
import 'bootstrap';
import { HomeService } from '../home.service';

@Component({
  selector: 'app-application-view',
  templateUrl: './application-view.component.html',
  styleUrls: ['./application-view.component.scss']
})
export class ApplicationViewComponent implements OnInit{

  girdViewLoop = ['1', '2', '3', '4','5','6','7','8'];
  activeCardViewLoop = ['1', '2', '3', '4','5','6','7','8'];
  activeGridView:boolean = false;
  draftGridView:boolean = false;
  activeTableView:boolean = false;
  draftTableView:boolean = true;
  onHoldGridView:boolean = false;
  onHoldTableView:boolean = false;
  rejectedGridView:boolean = false;
  rejectedTableView:boolean = false;
  tabs:any;
  applicationActiveTableHeader : any;  
  applicationActiveRenewalTableHeader:any;
  applicationActiveTable:any

  applicationTypesArr:any;
  isRenewal:boolean = false;
  isNew:boolean = true;
  applicationTypeIndex = 0;
  applicationCategoryArr:any;
  applicationCategoryIndex = 0;
  applicationDraftTableHeader:any;
  applicationDraftTable:any;
  applicationOnHoldTableHeader:any;
  applicationOnHoldRenewalTableHeader:any;
  applicationOnHoldTable :any;
  applicationRejectedTableHeader:any
  applicationRejectedRenewalTableHeader:any;
  applicationRejectedTable :any;
  numberOfRow = 100;
  togleView:boolean = false;
  tabIndex:number = 1;
  gridTableViewToggle:boolean = false;
  activeApplications:boolean = false;
  draftApplications:boolean = true;
  onHoldApplications:boolean = false;
  rejectedApplications:boolean = false;

  sortingPosistion = '';
  sortingAction:boolean = true;
  sortingColumnNumber:number = 0;
  index:number=100;
   constructor(public homeService:HomeService){
    // this.tabs = homeService.headerTabs;
    // this.applicationActiveTableHeader = homeService.applicationActiveNewTableHeader; //Asigning a value of active Application  header
    // this.applicationActiveRenewalTableHeader = homeService.applicationActiveRenewalEnhancementTableHeader;
    // this.applicationActiveTable = homeService.applicationActiveTableData;
    // this.applicationRejectedTableHeader = homeService.applicationRejectedNewTableHeader; //Asigning a value of active Application  header
    // this.applicationRejectedRenewalTableHeader = homeService.applicationRejectedRenewalEnhancementTableHeader;
    // this.applicationRejectedTable = homeService.applicationRejectedTableData
    // this.applicationOnHoldTableHeader = homeService.applicationOnHoldNewTableHeader; //Asigning a value of active Application  header
    // this.applicationOnHoldRenewalTableHeader = homeService.applicationOnHoldRenewalEnhancementTableHeader;
    // this.applicationOnHoldTable = homeService.applicationOnHoldTableData
    // this.applicationTypesArr = homeService.applicationTypesTitle;
    // this.applicationCategoryArr = homeService.applicationCategoryTitle;
    // this.applicationDraftTableHeader = homeService.applicationDraftNewTableHeaderArr;
    // this.applicationDraftTable = homeService.applicationDraftTableData;
    
   }
/** Function for Application view tabs it will change the tab as a clicked state (CSS style change) also */
   tabFunction(tab:any, i:number){
    this.tabIndex = tab.id;
    
    if(tab.tabName == 'Draft'){
      this.draftTab()
    }
    if(tab.tabName == 'Active'){
      this.activeTab()
    }
    if(tab.tabName == 'On-Hold'){
      this.onHoldTab()
    }
    if(tab.tabName == 'Rejected'){
      this.rejectedTab()
    }

   }
   draftTab(){
    this.draftApplications = true;
    this.activeApplications = 
    this.onHoldApplications =
    this.rejectedApplications = false;
   }
   activeTab(){
    this.activeApplications = true;
    this.draftApplications = 
    this.onHoldApplications =
    this.rejectedApplications = false;
   }
   onHoldTab(){   
    this.onHoldApplications = true;
    this.draftApplications = this.activeApplications = this.rejectedApplications = false;
   }
   rejectedTab(){  
    this.rejectedApplications = true;
    this.draftApplications = this.activeApplications = this.onHoldApplications = false;
   }

   /** this function toggles the grid view <=> table view */
   gridView(){
    this.gridTableViewToggle = !this.gridTableViewToggle;
   }

   /** Default select for application type */
   applicationType(i:number){
    
    this.applicationTypeIndex = i
   
    if(i==0){
      this.isNew = true;
      this.isRenewal = false;
      // this.applicationActiveTableHeader = this.homeService.applicationActiveNewTableHeader;
      // this.applicationDraftTableHeader = this.homeService.applicationDraftNewTableHeaderArr;
      // this.applicationOnHoldTableHeader = this.homeService.applicationOnHoldNewTableHeader;
      // this.applicationRejectedTableHeader = this.homeService.applicationRejectedNewTableHeader;
    }else{
      this.isNew = false;
      this.isRenewal = true;   
      // this.applicationActiveTableHeader = this.applicationActiveRenewalTableHeader;
      // this.applicationDraftTableHeader = this.homeService.applicationDraftTableHeaderArr;      
      // this.applicationOnHoldTableHeader = this.homeService.applicationOnHoldRenewalEnhancementTableHeader;
      // this.applicationRejectedTableHeader = this.homeService.applicationRejectedRenewalEnhancementTableHeader;
    }
   }

   /** Default select for application category */
   applicationCategory(i:number){
    this.applicationCategoryIndex = i;
   }
/**Category Button style in Application draft grid view */
   categoryButtonStyle(categoryName:string){
    if(categoryName == 'Enhancement'){
      return 'background-color: #EB6A0C;'
    }else if(categoryName == 'Renewal'){
      return 'background-color: #0A77DC;'
    }else{
      return 'background-color: #448EE4;'
    }
   }
/**Function for table sorting */
   sorting(n:number){
    this.sortingAction = !this.sortingAction;
    this.sortingColumnNumber = n;
    if(this.sortingAction){
      this.sortingPosistion = '-desc';
    }else{
      this.sortingPosistion = '-asc';
    }
   }
  ngOnInit() {
  }
}
