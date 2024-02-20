import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, Renderer2 } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { UikitComponent } from 'src/app/shared/component/uikit/uikit.component';
import { environment } from 'src/environments/environment';
import { ApplicationSummaryService } from '../../services/application-summary.service';
import { ApplicationService } from '../../services/application.service';

@Component({
  selector: 'app-application-summary',
  templateUrl: './application-summary.component.html',
  styleUrls: ['./application-summary.component.scss']
})
export class ApplicationSummaryComponent {
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
  searchValue:any = '';
  sortingPosistion = '';
  sortingAction:boolean = true;
  sortingColumnNumber:number = 0;
  index:number=100;
  responsedata: any;
  draftCount: any;
  rejectedCount: any;
  holdCount: any;
  activebusinessCount: any;
  activecreditCount: any;
  activeccCount: any;

  approved_status:any;
  activeCount: any;
  dataTableFullyLoaded:boolean = true;
   activeDataTableFullyLoaded:boolean = false;
   draftDataTableFullyLoaded:boolean = true;
   onHoldDataTableFullyLoaded:boolean = false;
   rejectedDataTableFullyLoaded:boolean = false;

   isNewDataTableFullyLoaded:boolean = false;
   isRenewalDataTableFullyLoaded:boolean = false;
   isEnhancementDataTableFullyLoaded:boolean = false;

   isBusinessDataTableFullyLoaded:boolean = false;
   isCreditDataTableFullyLoaded:boolean = false;
   isCCDataTableFullyLoaded:boolean = false;
   isSearched:boolean = false;
  text = "Fully Loaded";
  approch = "Data Table";
  activeTabName = "Draft";
  param1 = "New";
  param2 = "null";
  searchText: any;

  draftNewApplicationTable: any;
      draftRenewalApplicationTable: any;
      activeNewApplicationTable: any;
      activeRenewalApplicationTable: any;
      onHoldNewApplicationTable: any;
      onHoldRenewalApplicationTable: any;
      rejectedNewApplicationTable: any;
      rejectedRenewalApplicationTable: any;
  applicationCategoryTitle: any;
  rejectedbusinessCount: any;
  rejectedcreditCount: any;
  rejectedccCount: any;
  holdbusinessCount: any;
  holdcreditCount: any;
  holdCCCount: any;
  ApplicationcategoryCount: any;
  busineess_count: any;
  credit_count: any;
  cc_count: any;


   constructor(public homeService:ApplicationSummaryService,private post:ApplicationService,public renderer:Renderer2,private elementRef: ElementRef, public http:HttpClient,private route:Router,public cmnFunctionService:UikitComponent,private Actroute: ActivatedRoute,private router:Router){
    this.tabs = homeService.headerTabs;
    this.applicationActiveTableHeader = homeService.applicationActiveNewTableHeader; //Asigning a value of active Application  header
    this.applicationActiveRenewalTableHeader = homeService.applicationActiveRenewalEnhancementTableHeader;
    //this.applicationActiveTable = homeService.applicationActiveTableData;
    this.applicationRejectedTableHeader = homeService.applicationRejectedRenewalEnhancementTableHeader; //Asigning a value of active Application  header
    this.applicationRejectedRenewalTableHeader = homeService.applicationRejectedNewTableHeader;
    //this.applicationRejectedTable = homeService.applicationRejectedTableData
    this.applicationOnHoldTableHeader = homeService.applicationOnHoldRenewalEnhancementTableHeader; //Asigning a value of active Application  header
    this.applicationOnHoldRenewalTableHeader = homeService.applicationOnHoldNewTableHeader;
    //this.applicationOnHoldTable = homeService.applicationOnHoldTableData
    this.applicationTypesArr = homeService.applicationTypesTitle;
    this.applicationCategoryArr = homeService.applicationCategoryTitle;
    this.applicationDraftTableHeader = homeService.applicationDraftNewTableHeaderArr;
    //this.applicationDraftTable = homeService.applicationDraftTableData;
    
   }
/** Function for Application view tabs it will change the tab as a clicked state (CSS style change) also */
   tabFunction(tab:any, i:number,searchText:any){
    
    this.tabIndex = tab.id;
    this.activeTabName = tab.tabName;
    this.param2 = 'Business';
   this.searchText = searchText;
    this.applicationCategory(0)
    if(tab.tabName == 'Draft'){
      this.param2 = 'null';
      console.log(searchText);
      this.draftTab();
     
    }
    if(tab.tabName == 'Active'){
      this.activeTab();
      this.applicationCategoryArr[0].count = this.busineess_count;
      this.applicationCategoryArr[1].count = this.credit_count;
      this.applicationCategoryArr[2].count = this.cc_count;
  
     
    }
    if(tab.tabName == 'On-Hold'){
      this.onHoldTab();
      this.applicationCategoryArr[0].count = this.busineess_count;
      this.applicationCategoryArr[1].count = this.credit_count;
      this.applicationCategoryArr[2].count = this.cc_count;
    }
    if(tab.tabName == 'Rejected'){
      this.onrejectedTab();
      this.applicationCategoryArr[0].count = this.busineess_count;
      this.applicationCategoryArr[1].count = this.credit_count;
      this.applicationCategoryArr[2].count = this.cc_count;
    }

   }

  
   searchFunction(searchText:string,activeTabName:any){
    
      this.searchText= searchText;
    console.log(this.searchText);
    if(activeTabName == 'Draft'){
      this.param2 = 'null';
      console.log(searchText);
      this.draftTab();
     
    }
    if(activeTabName == 'Active'){
      this.activeTab();
     
    }
    if(activeTabName == 'On-Hold'){
      this.onHoldTab();
   
      
    }
    if(activeTabName == 'Rejected'){
      this.onrejectedTab();
     
     
    }

  }

  // if(this.activeTabName == 'Active'){
  //   this.post.getapprovedapplicationbusiness(this.searchText).subscribe((result) => {
  //   this.responsedata = result;
  //   console.log(result);
  //   this.applicationActiveTable = this.responsedata.applicationadd_list;
  //   }); 
  // }
  // if(this.activeTabName == 'Hold'){
  //   this.post.getapplicationhold(this.searchText).subscribe((result)=>{
  //   this.responsedata=result; 
  //   this.applicationOnHoldTable= this.responsedata.applicationadd_list;
  //   });
  // }
  // if(this.activeTabName == 'Rejected'){
  //   this.post.getapplicationrejected(this.searchText).subscribe((result)=>{
  //   this.responsedata=result;
  //   this.applicationRejectedTable= this.responsedata.applicationadd_list;

  //    });
  //   }
  //   }
  draftTab(){
    
    // if(this.searchValue == '' ){
      this.post.getapplication(this.searchText).subscribe((result) => {
        this.responsedata = result; 
        this.applicationDraftTable = this.responsedata.applicationadd_list;
      });
      this.draftDataTableFullyLoaded = true;
      this.dataTableFullyLoaded=true;
   

   if(this.draftDataTableFullyLoaded){
      this.dataTableFullyLoaded=true;
      this.text = "Fully Loaded"
      this.approch = "Data Table"
   }
 this.draftApplications = true;
 this.activeApplications = this.onHoldApplications = this.rejectedApplications = false;
   }
   draftRenewelTab(){
    // if(this.searchValue == '' ){
      this.post.getrenewalapplication(this.searchText).subscribe((result) => {
        this.responsedata = result; 
        this.applicationDraftTable = this.responsedata.applicationadd_list;
      });
      this.draftDataTableFullyLoaded = true;
      this.dataTableFullyLoaded=true;
   

   if(this.draftDataTableFullyLoaded){
      this.dataTableFullyLoaded=true;
      this.text = "Fully Loaded"
      this.approch = "Data Table"
   }
 this.draftApplications = true;
 this.activeApplications = this.onHoldApplications = this.rejectedApplications = false;
   }
   draftEnhancementTab(){
    // if(this.searchValue == '' ){
      this.post.getenhancementapplication(this.searchText).subscribe((result) => {
        this.responsedata = result; 
        this.applicationDraftTable = this.responsedata.applicationadd_list;
      });
      this.draftDataTableFullyLoaded = true;
      this.dataTableFullyLoaded=true;
   

   if(this.draftDataTableFullyLoaded){
      this.dataTableFullyLoaded=true;
      this.text = "Fully Loaded"
      this.approch = "Data Table"
   }
 this.draftApplications = true;
 this.activeApplications = this.onHoldApplications = this.rejectedApplications = false;
   }
   activeTab(){
    
    if(this.param1 == 'New' && this.param2 == 'Business'){
      this.post.getapprovedapplicationbusiness(this.searchText).subscribe((result) => {
        this.responsedata = result;
        this.applicationActiveTable = this.responsedata.applicationadd_list;
      });

      this.post.applicationcount().subscribe((result,)=>{
        this.responsedata=result; 
        this.applicationCategoryArr[0].count = this.responsedata.activebusiness_count;
        this.applicationCategoryArr[1].count = this.responsedata.activecredit_count;
        this.applicationCategoryArr[2].count = this.responsedata.activecc_count;
    });
    }
    if(this.param1 == 'Renewal' && this.param2 == 'Business'){
      this.post.getapprovedrenewalapplication(this.searchText).subscribe((result) => {
        this.responsedata = result; 
        
        this.applicationActiveTable = this.responsedata.applicationadd_list;
      });
    }
      if(this.param1 == 'Enhancement' && this.param2 == 'Business'){
        
        this.post.getapprovedenhancementapplication(this.searchText).subscribe((result) => {
          this.responsedata = result; 
          
          this.applicationActiveTable = this.responsedata.applicationadd_list;
        });
   }
   
    if(this.param1 == 'New' && this.param2 == 'Credit'){
    this.post.getapprovedapplicationcredit(this.searchText).subscribe((result) => {
      this.responsedata = result; 
      this.applicationActiveTable = this.responsedata.applicationadd_list;
    });

    this.post.applicationcount().subscribe((result,)=>{
      
      this.responsedata=result; 
      this.ApplicationcategoryCount= this.responsedata.activecredit_count;
  });
    this.activeDataTableFullyLoaded = true;
    this.dataTableFullyLoaded=true;
  }
  if(this.param1 == 'Renewal' && this.param2 == 'Credit'){
    this.post.getapprovedcreditrenewalapplication(this.searchText).subscribe((result) => {
      this.responsedata = result; 
      this.applicationActiveTable = this.responsedata.applicationadd_list;
    });
    }

    if(this.param1 == 'Enhancement' && this.param2 == 'Credit'){
      this.post.getapprovecreditdenhancementapplication(this.searchText).subscribe((result) => {
        this.responsedata = result; 
        this.applicationActiveTable = this.responsedata.applicationadd_list;
      });
    }
  if(this.param1 == 'New' && this.param2 == 'CC'){
  this.post.getapprovedapplicationcc(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationActiveTable = this.responsedata.applicationadd_list;
  });

  this.post.applicationcount().subscribe((result,)=>{
    
    this.responsedata=result; 
    this.ApplicationcategoryCount= this.responsedata.activecc_count;
});
}

if(this.param1 == 'Renewal' && this.param2 == 'CC'){
  this.post.getapprovedCCrenewalapplication(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationActiveTable = this.responsedata.applicationadd_list;
  });
}

if(this.param1 == 'Enhancement' && this.param2 == 'CC'){
  this.post.getapprovedCCenhancementapplication(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationActiveTable = this.responsedata.applicationadd_list;
  });
}
this.activeApplications = true;
       this.draftApplications = this.onHoldApplications = this.rejectedApplications = false;
 
   }
   onHoldTab(){   
    if(this.param1 == 'New' && this.param2 == 'Business'){
      this.post.getapplicationbusinesshold(this.searchText).subscribe((result) => {
        this.responsedata = result; 
        this.applicationOnHoldTable = this.responsedata.applicationadd_list;
      });
      this.post.applicationcount().subscribe((result,)=>{
        this.responsedata=result; 
        this.applicationCategoryArr[0].count = this.responsedata.holdbusiness_count;
        this.applicationCategoryArr[1].count = this.responsedata.holdcredit_count;
        this.applicationCategoryArr[2].count = this.responsedata.holdcc_count;
    });
   }
   if(this.param1 == 'Renewal' && this.param2 == 'Business'){
    this.post.getapplicationbusinessholdRENEWAL(this.searchText).subscribe((result) => {
      this.responsedata = result; 
      this.applicationOnHoldTable = this.responsedata.applicationadd_list;
    });
 }
 if(this.param1 == 'Enhancement' && this.param2 == 'Business'){
  this.post.getapplicationbusinessholdENHANCEMENT(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationOnHoldTable = this.responsedata.applicationadd_list;
  });
}
if(this.param1 == 'New' && this.param2 == 'Credit'){
    this.post.getapplicationcredithold(this.searchText).subscribe((result) => {
      this.responsedata = result; 
      this.applicationOnHoldTable = this.responsedata.applicationadd_list;
    });
 }
 if(this.param1 == 'Renewal' && this.param2 == 'Credit'){
  this.post.getapplicationcreditholdRENEWAL(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationOnHoldTable = this.responsedata.applicationadd_list;
  });
}
if(this.param1 == 'Enhancement' && this.param2 == 'Credit'){
  this.post.getapplicationcreditholdENHANCEMENT(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationOnHoldTable = this.responsedata.applicationadd_list;
  });
}
if(this.param1 == 'New' && this.param2 == 'CC'){
  this.post.getapplicationcchold(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationOnHoldTable = this.responsedata.applicationadd_list;
  });
   }
   if(this.param1 == 'Renewal' && this.param2 == 'CC'){
    this.post.getapplicationccholdRENEWAL(this.searchText).subscribe((result) => {
      this.responsedata = result; 
      this.applicationOnHoldTable = this.responsedata.applicationadd_list;
    });
     }
     if(this.param1 == 'Enhancement' && this.param2 == 'CC'){
      this.post.getapplicationccholdENHANCEMENT(this.searchText).subscribe((result) => {
        this.responsedata = result; 
        this.applicationOnHoldTable = this.responsedata.applicationadd_list;
      });
       }
   this.onHoldApplications = true;
       this.draftApplications = this.activeApplications = this.rejectedApplications = false;
      
  }
   onrejectedTab(){  
    if(this.param1 == 'New' && this.param2 == 'Business'){
      this.post.getapplicationbusinessrejected(this.searchText).subscribe((result) => {
        this.responsedata = result; 
        this.applicationRejectedTable = this.responsedata.applicationadd_list;
      });
      this.post.applicationcount().subscribe((result,)=>{
        this.responsedata=result; 
        this.applicationCategoryArr[0].count = this.responsedata.rejectedbusiness_count;
        this.applicationCategoryArr[1].count = this.responsedata.rejectedcredit_count;
        this.applicationCategoryArr[2].count = this.responsedata.rejectedcc_count;
    });
   }
   if(this.param1 == 'Renewal' && this.param2 == 'Business'){
    this.post.getapplicationbusinessrejectedRENEWAL(this.searchText).subscribe((result) => {
      this.responsedata = result; 
      this.applicationRejectedTable = this.responsedata.applicationadd_list;
    });
 }
 if(this.param1 == 'Enhancement' && this.param2 == 'Business'){
  this.post.getapplicationbusinessrejectedENHANCEMENT(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationRejectedTable = this.responsedata.applicationadd_list;
  });
}
debugger
if(this.param1 == 'New' && this.param2 == 'Credit'){
    this.post.getapplicationcreditrejected(this.searchText).subscribe((result) => {
      this.responsedata = result; 
      this.applicationRejectedTable = this.responsedata.applicationadd_list;
    });

 }
 if(this.param1 == 'Renewal' && this.param2 == 'Credit'){
  this.post.getapplicationcreditrejectedRENEWAL(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationRejectedTable = this.responsedata.applicationadd_list;
  });

}
if(this.param1 == 'Enhancement' && this.param2 == 'Credit'){
  this.post.getapplicationcreditrejectedENHANCEMENT(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationRejectedTable = this.responsedata.applicationadd_list;
  });

}
if(this.param1 == 'New' && this.param2 == 'CC'){
  this.post.getapplicationccrejected(this.searchText).subscribe((result) => {
    this.responsedata = result; 
    this.applicationRejectedTable = this.responsedata.applicationadd_list;
  });
   }
   if(this.param1 == 'Renewal' && this.param2 == 'CC'){
    this.post.getapplicationccrejectedRENEWAL(this.searchText).subscribe((result) => {
      this.responsedata = result; 
      this.applicationRejectedTable = this.responsedata.applicationadd_list;
    });
     }
     if(this.param1 == 'Enhancement' && this.param2 == 'CC'){
      this.post.getapplicationccrejectedENHANCEMENT(this.searchText).subscribe((result) => {
        this.responsedata = result; 
        this.applicationRejectedTable = this.responsedata.applicationadd_list;
      });
       }
   this.rejectedApplications = true;
   this.draftApplications = this.activeApplications = this.onHoldApplications = false;
   }

   /** this function toggles the grid view <=> table view */
   gridView(){
    this.gridTableViewToggle = !this.gridTableViewToggle;
   }

   /** Default select for application type */
   applicationType(i:number){
    this.activeTabName;
  //  this.param2 = 'null';
   console.log(this.activeTabName);
    this.param1 = this.applicationTypesArr[i].type;
    this.applicationTypeIndex = i

    if(i==0){
      this.isNew = true;
      this.isRenewal = false;
      this.applicationActiveTableHeader = this.homeService.applicationActiveNewTableHeader;
      this.applicationDraftTableHeader = this.homeService.applicationDraftNewTableHeaderArr;
      this.applicationOnHoldTableHeader = this.homeService.applicationOnHoldRenewalEnhancementTableHeader;
      this.applicationRejectedTableHeader = this.homeService.applicationRejectedRenewalEnhancementTableHeader;
    }else{
      this.isNew = false;
      this.isRenewal = true; 
      this.applicationActiveTableHeader = this.applicationActiveRenewalTableHeader;
      this.applicationDraftTableHeader = this.homeService.applicationDraftTableHeaderArr;      
      this.applicationOnHoldRenewalTableHeader = this.homeService.applicationOnHoldNewTableHeader;
      this.applicationRejectedRenewalTableHeader = this.homeService.applicationRejectedNewTableHeader;
    }
    
     
      if(i==0){
        if (this.param2=='null' && this.activeTabName=="Draft"){
          this.draftTab();

      }

      else if(this.param1 == 'New' && this.activeTabName =='Active' && this.param2 == 'Business'){
        this.activeTab();

    }
    else if(this.param1 == 'New' && this.activeTabName =='Active' && this.param2 == 'Credit'){
          this.activeTab();
    
      }
      else if(this.param1 == 'New' && this.activeTabName =='Active' && this.param2 == 'CC'){
        this.activeTab();
   
    }
    else if(this.param1 == 'New' && this.activeTabName =='On-Hold' && this.param2 == 'Business'){
      this.onHoldTab();

  }
  else if(this.param1 == 'New' && this.activeTabName =='On-Hold' && this.param2 == 'Credit'){
    this.onHoldTab();
 
}
else if(this.param1 == 'New' && this.activeTabName =='On-Hold' && this.param2 == 'CC'){
  this.onHoldTab();

}
else if(this.param1 == 'New' && this.activeTabName =='Rejected' && this.param2 == 'Business'){
  this.onrejectedTab();

}
else if(this.param1 == 'New' && this.activeTabName =='Rejected' && this.param2 == 'Credit'){
  this.onrejectedTab();

}
else if(this.param1 == 'New' && this.activeTabName =='Rejected' && this.param2 == 'CC'){
  this.onrejectedTab();

}
      }

      if(i==1){
        
        if (this.param1 == 'Renewal' && this.param2=='null' && this.activeTabName=='Draft'){
          this.draftRenewelTab();
   
      }
      else if(this.param1 == 'Renewal' && this.activeTabName =='Active' && this.param2 == 'Business'){
        this.activeTab();
    
    }
    else if(this.param1 == 'Renewal' && this.activeTabName =='Active' && this.param2 == 'Credit'){
          this.activeTab();
     
      }
      else if(this.param1 == 'Renewal' && this.activeTabName =='Active' && this.param2 == 'CC'){
        this.activeTab();

    }
    else if(this.param1 == 'Renewal' && this.activeTabName =='On-Hold' && this.param2 == 'Business'){
      this.onHoldTab();
 
  }
  else if(this.param1 == 'Renewal' && this.activeTabName =='On-Hold' && this.param2 == 'Credit'){
    this.onHoldTab();

}
else if(this.param1 == 'Renewal' && this.activeTabName =='On-Hold' && this.param2 == 'CC'){
  this.onHoldTab();

}
else if(this.param1 == 'Renewal' && this.activeTabName =='Rejected' && this.param2 == 'Business'){
  this.onrejectedTab();

}
else if(this.param1 == 'Renewal' && this.activeTabName =='Rejected' && this.param2 == 'Credit'){
  this.onrejectedTab();

}
else if(this.param1 == 'Renewal' && this.activeTabName =='Rejected' && this.param2 == 'CC'){
  this.onrejectedTab();

}
    } 
    if(i==2){
      if ((this.param2=='null' && this.activeTabName=="Draft")){
        this.draftEnhancementTab();
    
    }
    else if(this.param1 == 'Enhancement' && this.activeTabName =='Active' && this.param2 == 'Business'){
      this.activeTab();
    
  }
  else if(this.param1 == 'Enhancement' && this.activeTabName =='Active' && this.param2 == 'Credit'){
      this.activeTab();

    }
    else if(this.param1 == 'Enhancement' && this.activeTabName =='Active' && this.param2 == 'CC'){
      this.activeTab();

    }
    else if(this.param1 == 'Enhancement' && this.activeTabName =='On-Hold' && this.param2 == 'Business'){
      this.onHoldTab();

    }
    else if(this.param1 == 'Enhancement' && this.activeTabName =='On-Hold' && this.param2 == 'Credit'){
      this.onHoldTab();

    }
    else if(this.param1 == 'Enhancement' && this.activeTabName =='On-Hold' && this.param2 == 'CC'){
      this.onHoldTab();
   
    }
    else if(this.param1 == 'Enhancement' && this.activeTabName =='Rejected' && this.param2 == 'Business'){
      this.onrejectedTab();
    }
    else if(this.param1 == 'Enhancement' && this.activeTabName =='Rejected' && this.param2 == 'Credit'){
      this.onrejectedTab();
    }
    else if(this.param1 == 'Enhancement' && this.activeTabName =='Rejected' && this.param2 == 'CC'){
      this.onrejectedTab();
    }
  }
   }

   /** Default select for application category */
   /** Default select for application category */
   applicationCategory(i:number){
    this.activeTabName;
    console.log(this.activeTabName);
    this.param2 = this.applicationCategoryArr[i].type
    console.log(this.param2);
    this.applicationCategoryIndex = i;

    // this.applicationType(0);
 
    if(this.activeTabName == 'Active'){
      this.activeTab();
      this.param2;
     
    }
    if(this.activeTabName == 'On-Hold'){
      this.onHoldTab();
      this.param2;
      
    }
    if(this.activeTabName == 'Rejected'){
      this.onrejectedTab();
      this.param2;
   }
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
    trackByFn(index:number) {
       return index;
     }
     tableLength(number:number){
      this.numberOfRow = number
      this.draftNewApplicationTable.page.len(number).draw();
      this.draftRenewalApplicationTable.page.len(number).draw();
      this.activeNewApplicationTable.page.len(number).draw();
      this.activeRenewalApplicationTable.page.len(number).draw();
      this.onHoldNewApplicationTable.page.len(number).draw();
      this.onHoldRenewalApplicationTable.page.len(number).draw();
      this.rejectedNewApplicationTable.page.len(number).draw();
      this.rejectedRenewalApplicationTable.page.len(number).draw();
     }
     goToNextPage(){
      const myLink = this.elementRef.nativeElement.querySelector('#DataTables_Table_0_next');
      this.renderer.listen(myLink, 'click', () => {
        console.log('The link was clicked!');
      });
     }
     goToPreviousePage(){
     }
     
   ngOnInit() { 
    this.post.getapplication(this.searchText).subscribe((result) => {
      this.responsedata = result; 
      this.applicationDraftTable = this.responsedata.applicationadd_list;
    });
    
    // this.post.getapprovedapplicationbusiness(this.searchText).subscribe((result) => {
    //   this.responsedata = result;
    //   console.log(result);
    //   this.applicationActiveTable = this.responsedata.applicationadd_list;
    // }); 

    // this.post.getapplicationhold(this.searchText).subscribe((result,)=>{
    //   this.responsedata=result; 
    //   this.applicationOnHoldTable= this.responsedata.applicationadd_list;
    // });

    // this.post.getapplicationrejected(this.searchText).subscribe((result,)=>{
    //   this.responsedata=result;
    //   this.applicationRejectedTable= this.responsedata.applicationadd_list;
  
    // });
    this.post.applicationcount().subscribe((result,)=>{
      
      this.responsedata=result; 
      this.draftCount= this.responsedata.newapplication_count;
      this.rejectedCount= this.responsedata.rejectedtab_count;
      this.rejectedbusinessCount= this.responsedata.rejectedbusiness_count;
      this.rejectedcreditCount= this.responsedata.rejectedcredit_count;
      this.rejectedccCount= this.responsedata.rejectedcc_count;
      this.holdCount= this.responsedata.holdtab_count;
      this.holdbusinessCount= this.responsedata.holdbusiness_count;
      this.holdcreditCount= this.responsedata.holdcredit_count;
      this.holdCCCount= this.responsedata.holdcc_count;
      this.activeCount= this.responsedata.activetab_count; 
      this.activebusinessCount= this.responsedata.activebusiness_count;
      this.activecreditCount= this.responsedata.activecredit_count;
      this.activeccCount= this.responsedata.activecc_count;


      this.tabs =[
        {
          id:1,
          tabName:'Draft',
          active : false,
          count : this.draftCount
        },
        {
          id:2,
          tabName:'Active',
          active : false,
          count : this.activeCount
        },
        {
          id:3,
          tabName:'On-Hold',
          active : false,
          count : this.holdCount
        },
        {
          id:4,
          tabName:'Rejected',
          active : false,
          count :this.rejectedCount
        }
      ]
      this.applicationCategoryArr = [
        {
          id:1,
          type : 'Business',
          count:'',
        },
        {
          id:2,
          type : 'Credit',
          count:'',
        },
        {
          id:3,
          type : 'CC',
          count:'',
        }
      ]
    
  });
}  
  view(application_gid:string){
    window.location.href = environment.API_URL+'MstApplicationCreationView?application_gid='+application_gid+'&lstab=applicationcreation';
  
  }

  edit(val:any){ 
const application_gid = (val.application_gid); 
    const encryptedParameter = this.cmnFunctionService.encryptURL('lsapplication_gid=' + application_gid);
    const url = 'app/application-edit/generalinfoedit?hash=' + encryptedParameter;
    
   
    this.router.navigateByUrl(url);
    
 }
  AssessedScore(application_gid:string){
    window.location.href = environment.API_URL+'MstGradingToolAdd?application_gid='+application_gid;
  
  }
  VisitUpdates(application_gid:string){
    window.location.href = environment.API_URL+'MstVisitReportAdd?application_gid='+application_gid;
  
  }
 
}
