import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  headerTabs: any;
  draft_url = 'MstNgApplicationAdd/GetApplicationNewSummary'
  active_url = 'MstNgApplicationAdd/GetApplicationApprovedSummary'
  hold_url = 'MstNgApplicationAdd/GetApplicationHoldSummary'
  rejected_url='MstNgApplicationAdd/GetApplicationRejectedSummary'
  activebuisness_url = 'MstNgApplicationAdd/GetApplicationApprovedSummary'
  activecredit_url = 'MstNgApplicationAdd/GetApplicationApprovedSummary'
  activecc_url = 'MstNgApplicationAdd/GetApplicationApprovedSummary'
  holdbuisness_url = 'MstNgApplicationAdd/GetApplicationHoldSummary'
  holdcredit_url = 'MstNgApplicationAdd/GetApplicationHoldSummary'
  holdcc_url = 'MstNgApplicationAdd/GetApplicationHoldSummary'
  rejectedbuisness_url='MstNgApplicationAdd/GetApplicationRejectedSummary'
  rejectedcredit_url='MstNgApplicationAdd/GetApplicationRejectedSummary'
  rejectedcc_url='MstNgApplicationAdd/GetApplicationRejectedSummary'
  applicationcounts='MstNgApplicationAdd/ApplicationCount'
  exportexcel = 'IdasMstDocList/ExportDocument'
  azuredownload = 'azurestorage/DownloadDocument'  
  params: any;
  
  
  
    constructor(private http:HttpClient) { }
  
  
    getapplication(searchText:any){
      
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        renewal_flag:"N",
        enhancement_flag: "N"
            
      }
      return this.http.post(this.draft_url, this.params)
     
    } 
    getrenewalapplication(searchText:any){
      
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        renewal_flag:"Y",
        enhancement_flag: "N"
            
      }
      return this.http.post(this.draft_url, this.params)
     
    } 
    getenhancementapplication(searchText:any){
      
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        renewal_flag:"N",
        enhancement_flag: "Y"
            
      }
      return this.http.post(this.draft_url, this.params)
     
    } 
    getapprovedapplicationbusiness(searchText:string){
      var token: any  = localStorage.getItem("token");
     
      this.params ={
        searchText: searchText,
        approval_status: "Submitted to Approval",
        renewal_flag:"N",
        enhancement_flag: "N"

      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.activebuisness_url, paramValue)
     
    } 
    getapprovedapplicationcredit(searchText:string){
    
      var token: any  = localStorage.getItem("token");
     debugger
      this.params ={
        searchText: searchText,
        approval_status:"'Submitted to Credit Approval'|'Submitted to Underwriting'|'Sent Back to Credit'",
        renewal_flag:"N",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.activecredit_url, paramValue)
     
    } 
    getapprovedapplicationcc(searchText:string){
    
      var token: any  = localStorage.getItem("token");
     
      this.params ={
        searchText: searchText,
        approval_status:"'Submitted to CC'|'CC Approved'",
        renewal_flag:"N",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.activecc_url, paramValue)
     
    }
    
    getappovednewapplication(searchText:any){
      
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        approval_status: "Submitted to Approval",
        renewal_flag:"N",
        enhancement_flag: "N"
            
      }
      return this.http.post(this.active_url, this.params)
     
    } 
    getapprovedrenewalapplication(searchText:any){
      debugger
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        approval_status: "Submitted to Approval",
        renewal_flag:"Y",
        enhancement_flag: "N"
            
      }
      return this.http.post(this.active_url, this.params)
     
    } 
    getapprovedenhancementapplication(searchText:any){
      debugger
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        approval_status: "Submitted to Approval",
        renewal_flag:"N",
        enhancement_flag: "Y"
            
      }
      return this.http.post(this.active_url, this.params)
     
    } 

    getapprovedcreditrenewalapplication(searchText:any){
      
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        approval_status:"'Submitted to Credit Approval'|'Submitted to Underwriting'|'Sent Back to Credit'",
        renewal_flag:"Y",
        enhancement_flag: "N"
            
      }
      return this.http.post(this.active_url, this.params)
     
    }
    getapprovecreditdenhancementapplication(searchText:any){
      
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        approval_status:"'Submitted to Credit Approval'|'Submitted to Underwriting'|'Sent Back to Credit'",
        renewal_flag:"N",
        enhancement_flag: "Y"
            
      }
      return this.http.post(this.active_url, this.params)
     
    }
    getapprovedCCenhancementapplication(searchText:any){
      
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        approval_status:"'Submitted to CC'|'CC Approved'",
        renewal_flag:"N",
        enhancement_flag: "Y"
            
      }
      return this.http.post(this.active_url, this.params)
     
    }
    getapprovedCCrenewalapplication(searchText:any){
      var token: any  = localStorage.getItem("token");
      this.params ={
       
        searchText: searchText,
        approval_status:"'Submitted to CC'|'CC Approved'",
        renewal_flag:"Y",
        enhancement_flag: "N"
            
      }
      return this.http.post(this.active_url, this.params)
     
    }
  
    getapplicationbusinesshold(searchText:any){
    
      var token: any  = localStorage.getItem("token");
     
      this.params ={
        searchText: searchText,
        approval_status: "Hold By Business",
        renewal_flag:"N",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.holdbuisness_url, paramValue)
     
    } 

    getapplicationbusinessholdRENEWAL(searchText:any){
    
      var token: any  = localStorage.getItem("token");
     
      this.params ={
        searchText: searchText,
        approval_status: "Hold By Business",
        renewal_flag:"Y",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.holdbuisness_url, paramValue)
     
    }

    getapplicationbusinessholdENHANCEMENT(searchText:any){
    
      var token: any  = localStorage.getItem("token");
     
      this.params ={
        searchText: searchText,
        approval_status: "Hold By Business",
        renewal_flag:"N",
        enhancement_flag: "Y"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.holdbuisness_url, paramValue)
     
    }
    getapplicationcredithold(searchText:any){
    
      var token: any  = localStorage.getItem("token");
     
      this.params ={
        searchText: searchText,
        approval_status: "Hold By Credit",
        renewal_flag:"N",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.holdcredit_url, paramValue)
     
    } 

    getapplicationcreditholdRENEWAL(searchText:any){
    
      var token: any  = localStorage.getItem("token");
     
      this.params ={
        searchText: searchText,
        approval_status: "Hold By Credit",
        renewal_flag:"Y",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.holdcredit_url, paramValue)
     
    } 
    getapplicationcreditholdENHANCEMENT(searchText:any){
    
      var token: any  = localStorage.getItem("token");
     
      this.params ={
        searchText: searchText,
        approval_status: "Hold By Credit",
        renewal_flag:"N",
        enhancement_flag: "Y"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.holdcredit_url, paramValue)
     
    } 
    getapplicationcchold(searchText:any){
      var token: any  = localStorage.getItem("token");
    
      this.params ={
        searchText: searchText,
        approval_status: "Hold By CC",
        renewal_flag:"N",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.holdcc_url, paramValue)
     
    }

    getapplicationccholdRENEWAL(searchText:any){
      var token: any  = localStorage.getItem("token");
    
      this.params ={
        searchText: searchText,
        approval_status: "Hold By CC",
        renewal_flag:"Y",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.holdcc_url, paramValue)
     
    }

    getapplicationccholdENHANCEMENT(searchText:any){
      var token: any  = localStorage.getItem("token");
    
      this.params ={
        searchText: searchText,
        approval_status: "Hold By CC",
        renewal_flag:"N",
        enhancement_flag: "Y"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.holdcc_url, paramValue)
     
    }
    

    getapplicationbusinessrejected(searchText:any){
      var token: any  = localStorage.getItem("token");
    
      this.params ={
        searchText: searchText,
        approval_status: "Rejected By Business",
        renewal_flag:"N",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.rejectedbuisness_url, paramValue)

    }
    getapplicationbusinessrejectedRENEWAL(searchText:any){
      var token: any  = localStorage.getItem("token");
    
      this.params ={
        searchText: searchText,
        approval_status: "Rejected By Business",
        renewal_flag:"Y",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.rejectedbuisness_url, paramValue)

    } 
    getapplicationbusinessrejectedENHANCEMENT(searchText:any){
      var token: any  = localStorage.getItem("token");
    
      this.params ={
        searchText: searchText,
        approval_status: "Rejected By Business",
        renewal_flag:"N",
        enhancement_flag: "Y"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.rejectedbuisness_url, paramValue)

    } 
    getapplicationcreditrejected(searchText:any){
      debugger
      var token: any  = localStorage.getItem("token");
    
      this.params ={
        searchText: searchText,
        approval_status:"'Rejected By Credit'|'Rejected by Credit Manager'",
        renewal_flag:"N",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.rejectedcredit_url, paramValue)

    }
    getapplicationcreditrejectedRENEWAL(searchText:any){
      var token: any  = localStorage.getItem("token");
    
      this.params ={
        searchText: searchText,
        approval_status:"'Rejected By Credit'|'Rejected by Credit Manager'",
        renewal_flag:"Y",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.rejectedcredit_url, paramValue)

    } 
    getapplicationcreditrejectedENHANCEMENT(searchText:any){
      var token: any  = localStorage.getItem("token");
    
      this.params ={
        searchText: searchText,
        approval_status:"'Rejected By Credit'|'Rejected by Credit Manager'",
        renewal_flag:"N",
        enhancement_flag: "Y"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.rejectedcredit_url, paramValue)

    }  
    getapplicationccrejected(searchText:any){
    
      var token: any  = localStorage.getItem("token");
      this.params ={
        searchText: searchText,
        approval_status: "CC Rejected",
        renewal_flag:"N",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.rejectedcredit_url, paramValue)

    }
    getapplicationccrejectedRENEWAL(searchText:any){
    
      var token: any  = localStorage.getItem("token");
      this.params ={
        searchText: searchText,
        approval_status: "CC Rejected",
        renewal_flag:"Y",
        enhancement_flag: "N"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.rejectedcredit_url, paramValue)

    }
    getapplicationccrejectedENHANCEMENT(searchText:any){
    
      var token: any  = localStorage.getItem("token");
      this.params ={
        searchText: searchText,
        approval_status: "CC Rejected",
        renewal_flag:"N",
        enhancement_flag: "Y"
      }
      var paramValue= JSON.stringify(this.params)
      console.log(paramValue)
      return this.http.post(this.rejectedcredit_url, paramValue)

    }
  
    applicationcount(){
    
      var token: any  = localStorage.getItem("token");
      return this.http.get(this.applicationcounts, {headers: new HttpHeaders({'Authorization': token})})
    } 

    getexportexcel(){
      return this.http.get(this.exportexcel) //{observe:'response', responseType:'blob'})
    } 
    downloadfile(path:any, file_name:any){
      this.params ={
        file_path: path,
        file_name: file_name
      }
      return this.http.post(this.azuredownload, this.params)
     
    }
   



  }