import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  currentActiveUrl = '';
  applicationViewPageActive = false;
  private dataSubject = new Subject<boolean>();
  public mode$ = this.dataSubject.asObservable();

  isLoggedInStatus = false; //this boolean value indicates the login status
  darkModeEnnabled:boolean = false; ////this boolean value indicates the dark mode ennabled status

  ApplicationCreationMenu= [
    {
      "id": 1,
      "title": "General Details",
      "subMenu": [
        "General Details",
        "SPOC Details",
        "Business Activities"
      ]
    },    
    {
      "id": 2,
      "title": "Borrower Details",
      "subMenu": [
        "Institution",
        "Other Details",
        "Contact Person Details",
        "Address Details",
        "License Details",
        "FPO Coverage Area",
        "Document Upload",
        "Economic Capital",
        "Genetic Code by Business"
      ]
    },
    {
      "id": 3,
      "title": "Stakeholder Details", 
      "subMenu": [
        "Basic Details",
        "Family Details",
        "Occupation Details",
        "Contact Details",
        "Identification Proof(s)",
        "Address Details",
        "Document Upload",
        "Other Details",
        "Economic Capital",
      ]
    },
    {
      "id": 4,
      "title": "Facility & Charges",
      "subMenu": [
        "Overall Recommendations of Limit Details",
        "Add Facility / Product ",
        "Add Charges",
        "Hypothecation Details"
      ]
    },
    {
      "id": 6,
      "title": "Submission",
    }
  ];

  /* these following boolean variables are used to indicate the form sections and forms completed state */
  isGeneralInformationCompleted:boolean = false; 
  isSPoCDetailsCompleted:boolean = false; 
  isBusinessActivitiesCompleted:boolean = false; 
  isGeneralDetailsFormSubmitted:boolean = false;
  isLoginPageActive = false;
  tableData= [];
  productTableData= [];
  jsonObjectApplication = {

  }


  constructor( private http:HttpClient) {
   }

   setDarkMode(mode:boolean){
    this.dataSubject.next(mode);
   }
  
   isLoggedIn(loginStatus:boolean){
    this.isLoggedInStatus = loginStatus;
    return this.isLoggedInStatus;
   }
}
