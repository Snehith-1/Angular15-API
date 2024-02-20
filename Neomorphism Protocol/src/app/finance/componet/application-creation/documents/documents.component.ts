import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { LoanManagementModel } from '../../../model/loan-management.model';
import { CardAnimation } from "src/app/animation";
import { AppComponent } from 'src/app/app.component';
import { SocketService } from 'src/app/shared/services/socket.service';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.scss'],
  animations : [CardAnimation]
})

export class DocumentsComponent implements OnInit{
  @Input() application_gid : string | undefined;

  stackHolderDocuments :any;
  cardTitles:any;
  fragmentIndex:number = 0;
  uploadedDocuments:any;
  borrowerDocuments :any
  subMenuTitle: string = 'Borrower Documents';
  checklistView:boolean = false;
  documentsArray:any;
  checkListIndex:any;
  borrower_list: any[] = [];
  stackholder_list: any[] = [];
  borrower_checklist: any[] = [];
  stackholder_checklist: any[] = [];
  BorrowerView:any| boolean;
  stackholderView:any| boolean;
  constructor(private router:Router,private route:ActivatedRoute,public loanManagementModel:LoanManagementModel,
    public application:AppComponent, public socketservice:SocketService){
    this.application_gid = this.loanManagementModel.application_gid;
    this.uploadedDocuments= [
      {
        id:1,
        documentName: 'Company PAN',
        value: 'AAUCS6880M',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:2,
        documentName: 'Past 3 Years IT Statement',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:3,
        documentName: 'YTD Sales Figures',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:4,
        documentName: 'List of ARF Proposed Buyers',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:5,
        documentName: 'Credit Bureau Authorization',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:6,
        documentName: 'Bank statement for last 6 Months from all operating accounts',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:7,
        documentName: 'Last 6 Months Bank statement of Active Accounts',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:8,
        documentName: 'YTD P&L & (Projections /CMA if available)',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:9,
        documentName: 'Product wise break up of revenues (last 2 fiscal)',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:10,
        documentName: 'Debtors Ageing  latest month end.',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:11,
        documentName: 'Creditors Ageing Analysis for latest month end - for trade lines',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:12,
        documentName: 'Top 5 Suppliers /Vintage / transaction value last 6 months',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:13,
        documentName: 'Top 5 Buyers/Vintage/Transaction Value last 6 months',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:14,
        documentName: 'Existing Sanction Letter of other Banks',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
      {
        id:15,
        documentName: 'Sanction Limit & O/s for both WC & TL',
        value: '-',
        uploadedOn: '20-Mar-2023 11:00 AM',
        isUploaded: false
      },
    ]


    this.borrowerDocuments= [
      {
        borrowerName : 'Sammunati Finance Intermediation and services private limited',
        documentUploadStatus : 6,
        uploadedDocumentsData : this.uploadedDocuments
      }
    ]

    this.stackHolderDocuments = [
      {id:'1', guarantorDetails: 'Mahendran Balachandran',
          documentUploadStatus : 6,
          uploadedDocumentsData : this.uploadedDocuments},
      {id:'2', guarantorDetails: 'Anil Somanapalli Kumar Gopala  Krishna',
          documentUploadStatus : 6,
          uploadedDocumentsData : this.uploadedDocuments},
      {id:'3', guarantorDetails: 'Anil Somanapalli Kumar Gopala  Krishna',
          documentUploadStatus : 6,
          uploadedDocumentsData : this.uploadedDocuments},
      {id:'4', guarantorDetails: 'Arogya Jerald William K',
          documentUploadStatus : 6,
          uploadedDocumentsData : this.uploadedDocuments},
      {id:'5', guarantorDetails: 'Mahendran Balachandran',
          documentUploadStatus : 6,
          uploadedDocumentsData : this.uploadedDocuments},
      {id:'6', guarantorDetails: 'Anil Somanapalli Kumar Gopala  Krishna',
          documentUploadStatus : 6,
          uploadedDocumentsData : this.uploadedDocuments},
      {id:'7', guarantorDetails: 'Anil Somanapalli Kumar Gopala  Krishna',
          documentUploadStatus : 6,
          uploadedDocumentsData : this.uploadedDocuments},
      {id:'8', guarantorDetails: 'Arogya Jerald William K',
          documentUploadStatus : 6,
          uploadedDocumentsData : this.uploadedDocuments},
    ]
    this.cardTitles = loanManagementModel.documentCardDetails
    //Documents
    this.router.navigate( [ 'app/application-creation/Documents' ], { fragment: this.subMenuTitle } );
    loanManagementModel.ApplicationCreationMenu[5].subMenu = [
      {
        subMenuTitle:'Borrower Documents',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:'Stakeholder Documents',
        hasError:false,
        completed:false,
      }
    ]
    route.fragment.subscribe((fragment:any)=>{
      if(fragment !== null){
        this.subMenuTitle = fragment;
       // this.DetailsFormGroup;
        this.cardTitles;
        this.fragmentIndex = this.cardTitles.indexOf(fragment);
      }
    });
  }

  ngOnInit(): void {
    var application_gid = this.application_gid;
    var params = {
      application_gid:application_gid
    }
    this.application.uilock();
    this.socketservice.getparams('MstNgApplicationAdd/GetDocumentSummary',params).subscribe((result:any)=>{
      if(result.status == true){
        this.borrower_list = [];
        this.stackholder_list = [];
        for(var i=0; i<result.applicationdocument_list.length;i++){
          if(result.applicationdocument_list[i].stakeholder_type == 'Applicant'){
            this.borrower_list.push(result.applicationdocument_list[i]);
          }
          else{
            this.stackholder_list.push(result.applicationdocument_list[i]);
          }
        }
      }
      this.application.uiunlock();
    });
  }
  parallaxCardClick(cardFragment:any){
    this.router.navigate( [ 'app/application-creation/Documents' ], { fragment: cardFragment } );
  }

  secondCard(){
    if (this.fragmentIndex > this.cardTitles.length - 1) {
      return this.cardTitles[this.cardTitles.length - this.fragmentIndex - 1];
    } else if (this.fragmentIndex == this.cardTitles.length - 1) {
      return this.cardTitles[0];
    } else {
      return this.cardTitles[this.fragmentIndex + 1];
    }
  }
 
  checkListAction(stackholder_gid:any,stackholder_type:any,index:any){
    this.checklistView = true;
    //this.documentsArray = uploadDocuments;
    this.checkListIndex = index;
  }

  BorrowerDocView(application_gid:string,stackholder_type:string,stackholder_gid:string){
    this.BorrowerView = true;
    this.checklistView = true;
    //this.documentsArray = uploadDocuments;
    
    var params = {
      application_gid : application_gid,
      type : stackholder_type,
      stackholder_gid :stackholder_gid
    }
    this.socketservice.getparams('MstNgApplicationAdd/GetDocumentListSummary',params).subscribe((result:any)=>{
      if(result.status == true){
        this.borrower_checklist = [];
        this.borrower_checklist =  result.applicationlistdocument_list;
      }
    });
  }

  StackholderDocView(application_gid:string,stackholder_type:string, stackholder_gid:string){
    this.stackholderView = true;
    this.checklistView = true;
    //this.documentsArray = uploadDocuments;
    //this.checkListIndex = index;
    var params = {
      application_gid : application_gid,
      type : stackholder_type,
      stackholder_gid : stackholder_gid
    }
    this.socketservice.getparams('MstNgApplicationAdd/GetDocumentListSummary',params).subscribe((result:any)=>{
      if(result.status == true){
        this.stackholder_checklist = [];
        this.stackholder_checklist =  result.applicationlistdocument_list;
      }
    });
  }

  fileUploadFunction(index:any){
    
    this.documentsArray[index].isUploaded = true;
  }
// urls=[]
//  selectfiles(index:any){
  
//   if(index.target.selectFiles)
//   {
   
//     for(var i=0;i<File.length;i++)
//     {
//       var reader=new FileReader()
//       reader.readAsDataURL(index.target.files[i])
//       reader.onload=(index:any)=>{
//         this.urls.push
//       }
//     }
    
//   }
//   this.documentsArray[index].isUploaded = true;
//  }




  deleteDocument(index:any){
    this.documentsArray[index].isUploaded = false;
  }

  backButtonBorrower(){
    this.checklistView = false;
    this.BorrowerView = false;
    this.documentsArray = this.uploadedDocuments;
    // this.borrowerDocuments[this.checkListIndex].
  }
  backButtonStackholder(){
    this.checklistView = false;
    this.stackholderView = false;
    this.documentsArray = this.uploadedDocuments;
    // this.borrowerDocuments[this.checkListIndex].
  }
  nextButton(){
    this.router.navigateByUrl('app/application-creation/Bureau Details');
  }
  
}
