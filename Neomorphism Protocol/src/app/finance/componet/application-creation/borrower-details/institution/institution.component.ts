import { DatePipe } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CardAnimation } from 'src/app/animation';
import { NumberInputValidator, TextInputValidator, CustomEmailValidator, MultiSeclectDropdownValidatior, TableValidator } from 'src/app/shared/validators/customFormValidators';
import { LoanManagementModel } from '../../../../model/loan-management.model';
import { EventEmitter } from 'events';
import { SocketService } from 'src/app/shared/services/socket.service';
import { HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import { stringToKeyValue } from '@angular/flex-layout/extended/style/style-transforms';
import { UikitComponent } from 'src/app/shared/component/uikit/uikit.component';
import { AppComponent } from 'src/app/app.component';
import { debounceTime } from 'rxjs/operators';
import { ApplicationCreationService } from 'src/app/finance/services/application-creation.service';
//import { LoginComponent } from 'src/app/admin/componets/signin/signin.component';

interface IgstDetails {
gstNumber:string,
gstSate:string,
gstHeadOffice:string
}

interface IInstitutionFormDetails {
cin:string,
cinDate:string,
businessStartDate:string,
businessVintage:string,
panValue:string,
legelTradeName:string,
// gstDetails:Array<IgstDetails>,
// gstDetails:Array<any>,
tan:string,
tanState:string,
udhayamRegistration:string,
categoryAML:string,
categoryBusiness:string,
categoryTan:string,
nearestSamunnatiBranch:string,
lmsUrnStatus:string,
lmsUrnValue:string
}

interface IAddressDetails {
  addressType:string,
  primaryStatus:string,
  postalCode:string,
  country:string,
  state:string,
  city:string,
  district:string,
  taluka:string,
  addressLine1:string,
  addressLine2:string,
  landMark:string
}

interface IContactPersonDetails {
  firstName:string,
  middleName:string,
  lastName:string,
  designation:string,
  mobileNumber:string,
  mobileNumberPrimaryStatus:boolean,
  email:string,
  emailPrimaryStatus:boolean
}

interface IFileObject{
  documentTitle: string,
  documentId: string,
  document:File
}

@Component({
  selector: 'app-institution',
  templateUrl: './institution.component.html',
  styleUrls: ['./institution.component.scss'],
  animations : [CardAnimation]
})
export class InstitutionComponent extends EventEmitter implements OnInit {

customEvent:boolean;
  isInstitutionPage:boolean = true;
  isIndividualPage:boolean = false;
  borrowerPage:boolean | undefined;
  borrower:string = '';
  isOpen:boolean = false;
  todayDate = new Date();
  form:FormGroup | undefined;
  openModal:boolean =false;
  hideAfterTime:number = 1000;
  errorMessage:string | undefined;
  GSTdata:any[]= [];
  isprecheck: boolean | undefined;
  gstRegistered:string = 'No';
  mobileNumdata:any;
  isShow:boolean = false;
  maildata:any[]= [];
  addressdata:any[]= [];
  equipmentData:any[] = [
    {}
  ];
  liveStokeData:any[]=[
    {}
  ];
  liveStokeInsSts= 'No';
  recivableData:any[]=[];
  docUploadData:any[]=[];
  licenseData:any[]=[
    {
      type:null
    }
  ];
  showUrn:boolean | undefined ;
  isUrn:string = 'No';
  showIns:boolean | undefined ;
  showInsEquip:boolean | undefined;
  equipInsSts = 'No'
  showInsLiveStoke:boolean | undefined;
  alert:boolean = false;
  mobAlert:boolean = false;
  mailAlert:boolean = false;
  companyType:any;
  isCompanyTypeSelected:boolean = false;
  company: any;
  searchCreditRatingAgency:any;
  creditRatingAgency:any
  stakeholderType:any
  searchCreditRating:any;
  creditRating:any;
  searchAMLCategory:any;  
  searchBusinessCategory:any;

  AMLCategory:any;
  businessCategory:any;
  addressTypes:any;
  samunnatiBranches:any;
  states:any;
  equiqmentNames:any;
  liveStokeNames:any;

  searchDesignations:any;
  designations:any;
  searchAddressType:any;
  searchSamunnatiBranches:any;
  searchState:any;
  searchDocTitles:any;

searchLicenseTypes : any;
  
  isContactTypeSelected:boolean = false;
  contactsTypeDropDown:any;
  contactsData:any = [];
  contactObj:any = {

  };
  searchContactType:any;
  licenseTypes:any;
  contactsType:any;
  params: any;


  tableData = [
    {
      code:'',
      Status:'',
      Observation:''
    }
  ]
  geneticCodes:any;
  geneticCodeDropdown:any = '';
  isGeneticCodeSelected:boolean = false;
  geneticCodeData:any = [];
  geneticObj = {

  }
  searchCalamitiesProneCities:any
  
  CalamitiesProneCitiesDropdown:any = [];
  isCalamitiesProneCitiesSelected:boolean = false;
  isNonCalamitiesProneCitiesSelected:boolean = false;
  //isCPCitySelected:boolean
  searchNonCalamitiesProneCities:any;
  NonCalamitiesProneCities:any = ['option 1','option 2','option 4','option 5','option 6','option 7'];
  CalamitiesProneCities:any = ['option 1','option 2','option 4','option 5','option 6','option 7'];
  NonCalamitiesProneCitiesDropdown:any = [];

  stakeholder: any;
  isStakeholderTypeSelected: boolean = false;
  CRagency: any;
  isCreditRatingAgencySelected: boolean = false;
  isCreditRatingSelected: boolean = false;
  creditRatingDropdown: any;
  AMLCategoryDropdown: any;
  isAMLCategorySelected: boolean = false;
  TANstateCategoryDropdown: any;
  isTANCategorySelected: boolean = false;

  bCategoryDropdown: any;
  isbCategorySelected: boolean = false;
  designationDropdown: any;
  isDesignationSelected: boolean = false;
  addressTypesDropdown: any;
  isAddressTypeSelected: boolean = false;
  branchsDropdown: any;
  isBranchSelected: boolean = false;
  statesDropdown: any;
  isStateSelected: boolean = false;
  intRatingDropdown: any;
  isIntRatingSelected: boolean = false;
  equiqmentNameDropdown: any;
  isEquiqmentNameSelected: boolean = false;
  liveStokeDropdown: any;
  isLiveStokeSelected: boolean = false;
  DocTitleDropdown: any;
  isDocTitleSelected: boolean = false;
  licenseDropdown: any;
  isLicenseSelected: boolean = false;
  gstApproved:boolean = false;
  gstApprovedStatus = 'No';
  isFutureDate:boolean = false;
  geneticcodedetails: any;
  geneticCodesDetails: any;
  isgenetic: any;
  showgentic: boolean | undefined;
  geneticCodeStatus: any;
  gst_no_add:any;
  gstObj = {
  }
  mobObj = {
    mobNumber : null,
    PrimaryStatus : null
  }
  emailObj = {
    email : null,
    emailPrySts : null
  }
  docObj = {
    docTitle : null,
    docId : null,
    docName : null
  }
  licenseObj = {
  }
  recivableObj = {

  }
  equipObj = {

  }
  liveStokeObj = {

  }
  SCwordCount: any;
  SCwordsCount: any;
  TCwordCount: any;
  TCwordsCount: any;

institutionFormGroup!:FormGroup;
institutionFormDetails:IInstitutionFormDetails;

addressDetailsFormGroup!:FormGroup;
addressDetailsTableFormGroup!:FormGroup;
addressDetailsFormDetails:IAddressDetails;

contactPersonDetailsFormGroup!:FormGroup;
contactPersonDetails:IContactPersonDetails;

borrowerDetailsFormGroup!:FormGroup;
documentUploadFormGroup!:FormGroup;
documentUploadDetails:Array<any>;
documentObject:any;
addressTable:any = []
addressTableTemplate:any = []
cardTitles:any;
fragmentIndex:number = 0;
subMenuTitle: string = 'Institution';
institutionFormInValid:boolean = false;
gstFormInValid:boolean = false;
gstDataValid:boolean = true;
equipmentDataValid:boolean = true;
livestokeDataValid:boolean = true;
gstTableInValid:boolean = false;
liveStokeTableInValid:boolean = false;
licenseTableInValid:boolean = false;
equipmentTableInValid:boolean = false;
addressDetailsFormInvalid:boolean = false;
contactPersonDetailsFormInvalid:boolean = false;
contactTableInvalid:boolean = false;
documentUploadFormInvalid:boolean = false;
photoNotFound: boolean | undefined;
photoUrlList: any[] | undefined;
photoFound: boolean | undefined;
geneticCodeFormGroup!:FormGroup;
geneticCodeFormInvalid:boolean = false;

othersDetailsFormGroup!:FormGroup;
othersDetailsFormInvalid:boolean = false;

licenseDetailsFormGroup!:FormGroup;
licenseDetailsFormInvalid:boolean = false;

FPOcoverageAreaFormGroup!:FormGroup;
FPOcoverageFormInvalid:boolean = false;
subMenuArray:any = [];
subMenuIndex:number = 0;
mainMenu:any=[];

gstDetailsFormArray!:FormArray<FormGroup>;
gstDetailsFormGroup!:FormGroup;
gstIndex:number = 0;

gstTableHeader :any;

contactTableHeader:any;

documentuploadHeader:any;

geneticCodeHeader:any;

equipmentTableHeader:any;

liveStokeTableHeader:any;

licenseTableHeader:any;

fpoCoverageAreaTableHeader:any;

gstselectedOption: any;




// Readbutton
isReadMore: boolean = true;


tabs = [
  {
    id: 1,
    tabName: 'Primary Address'
  },
  {
    id: 2,
    tabName: 'Additional Address'
  },
]


primarylTableView: boolean = true;
additionalTableView: boolean = false;
gstTabTypeIndex = 0;
gstTableIndex = 0;

expand: boolean = false;
anchor: string = 'Read more';

headerOptionsArr: any = [
  {
    id: 1,
    name: 'Madhya Pradesh',
  },
  {
    id: 2,
    name: 'Commerce',
  },
  {
    id: 3,
    name: 'Foundation',
  },


];



primaryAddressDetails:any;
additionalAddressDetails:any;
dataFromGSTportal:any;

term:any;
searchCategoryTan: any;
searchCategoryAml: any;
searchCategoryBusiness: any;
searchnearestSamunnatiBranch: any;
filtercategoryTanTypes: { state_name: string }[] = [];
filtercategoryAmlTypes: { amlcategory_name: string }[] = [];
filtercategoryBusinessTypes: { businesscategory_name: string }[] = []; 
filternearestSamunnatiBranchTypes: { branch_name: string }[] = [];
filteraddressTypes: { address_type: string }[] = [];
filterdesignationTypes: { designation: string }[] = [];
TANCategory: any;
  gstNumber: any;
  gst_code: any;
  GSTheadoffice: any;
  NotGSTheadoffice: any;
  postalCodeValue: any;
  addresstype: any;
  AddressTypeValue: any;
  BusinessCategory: any;
  BranchesDropdown: any;
  staticmapImgUrl: any;
  latitude: any;
  longitude: any;
  addressparams: any;
  photoUrlArray: any=[];
  contact: any=[];
  sharedData: any;
  gstsource: any;
  leirenewaldate: any;
  cinyear: any;
  designationType: any;
  response: any;
  amlcategoryName: any;
  businesscategoryName: any;
  branchName: any;
  tanstateName: any;
  amlcategorygid: any;
  businesscategorygid: any;
  branchNamegid: any;
  tanstategid: any;
  socketservice: any;
  activatedRoute: any;
  application_gid: string | undefined;
  gst_state: any;
  gst_no: any;
  gstfullcode: any;
  headoffice_status: any;
  gststatename: any;
  institution_gid: any;
  encryptedParameter: string | undefined;




  constructor( public datePipe:DatePipe,private formBuilder:FormBuilder, private router:Router, private route:ActivatedRoute, private loanManagementModel:LoanManagementModel, private socketService: SocketService, private appComponent: AppComponent,public application:AppComponent,public notify:AppComponent,public cmnfunctionService:UikitComponent ){
    super();
   
    //this.geneticCodes = loanManagementModel.geneticCodes;
    this.licenseTypes = loanManagementModel.licenseTypes;
    this.contactsType = loanManagementModel.contactsType;
    this.states = loanManagementModel.states;
    this.equiqmentNames = loanManagementModel.equiqmentNames;
    this.liveStokeNames = loanManagementModel.liveStokeNames;
    this.creditRating = loanManagementModel.creditRating;
    this.creditRatingAgency = loanManagementModel.creditRatingAgency;
    this.stakeholderType = loanManagementModel.stakeholderType;
    this.mobileNumdata = loanManagementModel.mobileNumdata;
    this.cardTitles = loanManagementModel.borrowerDetailsInstitutionCardTitles;
    this.gstTableHeader =loanManagementModel.gstTableHeader;
    this.contactTableHeader =loanManagementModel.contactTableHeader
    this.documentuploadHeader = loanManagementModel.documentuploadHeader
    this.geneticCodeHeader = loanManagementModel.geneticCodeHeader
    this.equipmentTableHeader = loanManagementModel.equipmentTableHeader
    this.liveStokeTableHeader =loanManagementModel.liveStokeTableHeader
    this.licenseTableHeader =    loanManagementModel.licenseTableHeader
    this.fpoCoverageAreaTableHeader = loanManagementModel.fpoCoverageAreaTableHeader
    this.gstselectedOption =loanManagementModel.gstselectedOption;
    this.primaryAddressDetails = loanManagementModel.primaryAddressDetails;
    this.additionalAddressDetails = loanManagementModel.additionalAddressDetails;
    this.companyType = loanManagementModel.companyType;

    loanManagementModel.ApplicationCreationMenu[1].subMenu = [
      {
        subMenuTitle:'Institution',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:'GST',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:'Address',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:'Contact Person',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:'Genetic code by Business',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:'Other',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:'License',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:'FPO Coverage Area',
        hasError:false,
        completed:false,
      },
    ]
    //this.router.navigate( [ 'app/application-creation/Borrower Details' ], { fragment: this.subMenuTitle } );
    this.customEvent = true;
    this.mainMenu = loanManagementModel.ApplicationCreationMenu;
    this.institutionFormDetails = {} as IInstitutionFormDetails;
    this.addressDetailsFormDetails = {} as IAddressDetails;
    this.contactPersonDetails = {} as IContactPersonDetails;
    this.documentUploadDetails = [];
    this.documentObject = {} ;

    route.fragment.subscribe((fragment:any)=>{
      if(fragment !== null){
        this.subMenuTitle = fragment;
        this.institutionFormGroup;
        this.subMenuArray = this.mainMenu[1].subMenu;
        this.subMenuIndex = this.subMenuArray.indexOf(this.subMenuTitle);
        this.cardTitles;
        this.fragmentIndex = this.cardTitles.indexOf(fragment);
      }
    });
    this.loadForm();

  }

dropViewFunction(selectedOption: any) {
  this.gstselectedOption = selectedOption;
}

gstShowText() {
  this.isReadMore = !this.isReadMore;
}

gstTabType(i: number) {
  this.gstTabTypeIndex = i
}
  tracker= (i: any) => i;
  onInputChanged(value: any, rowIndex: number, propertyKey: string): void {
    this.GSTdata[rowIndex].headoffice_status = value;
    this.headoffice_status= value;
    
      // const newValue = this.GSTdata.map((row: any, index: number) => {
      //   return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
      // })
      // this.GSTdata = newValue;
      // this.setGstTable();
  }
 

  onInputChangedContactDetails(value: any, rowIndex: number, propertyKey: string): void {
    const newValue = this.contactsData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    // this.dataChanged.emit(newValue);
    this.contactsData = newValue;
    this.setContactTableData();

  }

  onInputChangedGeneticCode(value: any, rowIndex: number, propertyKey: string): void {
    const newValue = this.geneticCodes.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })

    
    this.geneticCodes = newValue;
    if(value.target.value == 'Yes'){
      this.geneticCodes[rowIndex].status = 'No';
    }
    if(value.target.value == 'No'){
      this.geneticCodes[rowIndex].status = 'Yes';
    }
    // this.setGSTDetails()
    this.setGeneticCodesTable();
  }

  onInputChangedDocUpload(value: any, rowIndex: number, propertyKey: string): void {

    const newValue = this.documentUploadDetails.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    // this.dataChanged.emit(newValue);
    this.documentUploadDetails = newValue;
    // this.setGSTDetails()
  }
  onInputChangedEquipmentDetails(value: any, rowIndex: number, propertyKey: string): void {

    const newValue = this.equipmentData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    // this.dataChanged.emit(newValue);
    this.equipmentData = newValue;
    // this.setGSTDetails()
    this.setEquipmentDetailsTable();
  }

  onInputChangedLiveStokeDetails(value: any, rowIndex: number, propertyKey: string): void {

    const newValue = this.liveStokeData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    // this.dataChanged.emit(newValue);
    this.liveStokeData = newValue;
    // this.setGSTDetails()
    this.setLivestockTable();
  }

  onInputChangedLicenseDetails(value: any, rowIndex: number, propertyKey: string): void {

    const newValue = this.licenseData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    // this.dataChanged.emit(newValue);
    this.licenseData = newValue;
    
    this.setLicenseDetailsTable();
  }

  loadForm(){

    this.institutionFormGroup = new FormGroup({
      cin : new FormControl(null,
        [Validators.required,Validators.maxLength(21),Validators.minLength(21),Validators.pattern(/^[a-zA-Z0-9]+$/)]),
      cinDate : new FormControl(),
      businessStartDate : new FormControl(null,[Validators.required]),
      businessVintage : new FormControl(),
      panValue : new FormControl('',[Validators.required,Validators.maxLength(10),Validators.minLength(10),Validators.pattern(/^[A-Z]{3}[CHFATBLJG]{1}[A-Z]{1}[0-9]{4}[A-Z]{1}$/)]),
      legalTradeName : new FormControl('',[Validators.required,Validators.maxLength(300)]),
      lei : new FormControl('',[]),
      tan : new FormControl(null,[
        Validators.maxLength(10),Validators.minLength(10),
        Validators.pattern(/^[A-Z]{4}[0-9]{5}[A-Z]{1}$/)
      ]),
      tanState : new FormControl(),
      tanState_gid : new FormControl(),
      kin : new FormControl(null,[
        Validators.maxLength(14)
      ]),
      udhayamRegistration : new FormControl('',[Validators.maxLength(72)]),
      categoryAml : new FormControl(),
      categoryAml_gid : new FormControl(),
      categoryTan : new FormControl(),
      searchCategoryTan: new FormControl(''),
      searchCategoryAml: new FormControl(''),
      searchCategoryBusiness: new FormControl(''),
      searchnearestSamunnatiBranch: new FormControl(''),
      searchAddressType: new FormControl(''),
      categoryBusiness : new FormControl('',[Validators.required]),
      categoryBusiness_gid : new FormControl(),
      nearestSamunnatiBranch : new FormControl('',[Validators.required]),
      nearestSamunnatiBranch_gid : new FormControl(),
      lmsUrnStatus : new FormControl(true),
      lmsUrnValue : new FormControl()
    })

    this.gstDetailsFormGroup = new FormGroup({
      gstTable: new FormControl([],[Validators.required, TableValidator()])
    })

    this.addressDetailsFormGroup = new FormGroup({
      addressType:new FormControl('',[Validators.required]),
      primaryStatus:new FormControl('Yes',[Validators.required]),
      postalCode:new FormControl('',[
        Validators.required,Validators.maxLength(6),Validators.minLength(6),Validators.pattern(/^[1-9]{1}[0-9]{5}$/)
      ]),
      country:new FormControl(),
      state:new FormControl(),
      city:new FormControl(),
      district:new FormControl(),
      taluka:new FormControl(),
      addressLine1:new FormControl('',[Validators.required,Validators.maxLength(135)]),
      addressLine2:new FormControl('',[Validators.maxLength(135)]),
      landMark:new FormControl('',[Validators.maxLength(100)]),
      searchAddressType:new FormControl(''),
     })

     this.addressDetailsTableFormGroup = new FormGroup({
      addressDetailsTable: new FormControl([],[Validators.required, TableValidator()])
     })

     this.contactPersonDetailsFormGroup = new FormGroup({
      firstName:new FormControl(null,[
        Validators.required,
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z ]*$/)
      ]),
      middleName:new FormControl('',[   
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z ]*$/),
      ]),
      lastName:new FormControl('',[
        // TextInputValidator(50)
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z ]*$/),
      ]),
      designation:new FormControl(null,[Validators.required]),
      designation_gid: new FormControl(''),

      mobileNumber:new FormControl(null,[
        Validators.required,        
        Validators.maxLength(10),
        Validators.minLength(10),
        Validators.pattern(/^[0-9]*$/),
      ]),
      mobileNumberPrimaryStatus:new FormControl(true,[
        Validators.required
      ]),      
      mobWtSts:new FormControl(true),
      email:new FormControl(null,[
        Validators.required,
        Validators.maxLength(100),
        Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|in|co\.in|org|co)$/)
      ]),
      emailPrimaryStatus:new FormControl(true),
      contactTableData: new FormControl(null)
     })

     this.geneticCodeFormGroup = new FormGroup({
      geneticCodesTable: new FormControl([],[Validators.required, TableValidator()])
     })


     this.othersDetailsFormGroup = new FormGroup({
      livestockTable: new FormControl([],[Validators.required, TableValidator()]),
      equipmentDetailsTable: new FormControl([],[Validators.required, TableValidator()])
     })

     this.licenseDetailsFormGroup = new FormGroup({
      licenseDetailsTable: new FormControl([],[Validators.required, TableValidator()])
     })

     this.FPOcoverageAreaFormGroup = new FormGroup({
      CalamitiesProneCitiesData: new FormControl([],[Validators.required, MultiSeclectDropdownValidatior()]),
      NonCalamitiesProneCitiesData: new FormControl([],[Validators.required, MultiSeclectDropdownValidatior()])
     })

     this.documentUploadFormGroup = new FormGroup({
      fileDetails : new FormControl()
     })

     this.borrowerDetailsFormGroup = this.formBuilder.group({
      institutionDetailsData:this.institutionFormGroup,
      gstDetailsData:this.gstDetailsFormGroup,
      addressDetailsData:this.addressDetailsTableFormGroup,
      contactPersonDetailsData:this.contactPersonDetailsFormGroup,
      geneticCodeByBusinessData:this.geneticCodeFormGroup,
      otherDetailsData:this.othersDetailsFormGroup,
      licenseDetailsData:this.licenseDetailsFormGroup,
      fpoCoverageAreaData:this.FPOcoverageAreaFormGroup
     })
  }

  get cin(){
    return this.institutionFormGroup.get('cin')!;
  }
  get kin(){
    return this.institutionFormGroup.get('kin')!;
  }
  get cinDate(){
    return this.institutionFormGroup.get('cinDate')!;
  }
  get businessStartDate(){
    return this.institutionFormGroup.get('businessStartDate')!;
  }
  get categoryTan(){
    return this.institutionFormGroup.get('categoryTan')!;
  }
  setcategoryTan(){
    this.categoryTan.setValue(this.TANstateCategoryDropdown)
  }
  
  get businessVintage(){
    return this.institutionFormGroup.get('businessVintage')!;
  }
  get panValue(){
    return this.institutionFormGroup.get('panValue')!;
  }
  get legalTradeName(){
    return this.institutionFormGroup.get('legalTradeName')!;
  }
  get lei(){
    return this.institutionFormGroup.get('lei')!;
  }
  setLegalTradeName(){
    return this.legalTradeName.setValue("Auto Populate");
  }
  // get gstDetails(){
  //   return this.institutionFormGroup.get('gstDetails')! as FormArray;
  // }

  // setGSTDetails(){
  //   return this.gstDetails.setValue(this.GSTdata);
  // }
  get tan(){
    return this.institutionFormGroup.get('tan')!;
  }
  get tanState(){
    return this.institutionFormGroup.get('tanState')!;
  }
  setTanState(){
    this.tanState.setValue(this.statesDropdown)
  }
  get udhayamRegistration(){
    return this.institutionFormGroup.get('udhayamRegistration')!;
  }
  get categoryAml(){
    return this.institutionFormGroup.get('categoryAml')!;
  }
  setCategoryAml(){
    this.categoryAml.setValue(this.AMLCategoryDropdown)
  }
  get categoryBusiness(){
    return this.institutionFormGroup.get('categoryBusiness')!;
  }
  setCategoryBusiness(){
    this.categoryBusiness.setValue(this.bCategoryDropdown);
  }
  get nearestSamunnatiBranch(){
    return this.institutionFormGroup.get('nearestSamunnatiBranch')!;
  }
  setNearestSamunnatiBaranch(){
    this.nearestSamunnatiBranch.setValue(this.branchsDropdown);
  }
  get lmsUrnStatus(){
    return this.institutionFormGroup.get('lmsUrnStatus')!;
  }
  get lmsUrnValue(){
    return this.institutionFormGroup.get('lmsUrnValue')!;
  }

  get gstTable(){
    return this.gstDetailsFormGroup.get('gstTable')!;
  }
  setGstTable(){
    this.gstTable.setValue(this.GSTdata);
  }


  get addressType(){
    return this.addressDetailsFormGroup.get('addressType')!;
  }
  // setAddressType(){
  //   this.addressType.setValue(this.addressTypesDropdown);
  // }
  get primaryStatus(){
    return this.addressDetailsFormGroup.get('primaryStatus')!;
  }
  get postalCode(){
    return this.addressDetailsFormGroup.get('postalCode')!;
  }
  get country(){
    return this.addressDetailsFormGroup.get('country')!;
  }
  get state(){
    return this.addressDetailsFormGroup.get('state')!;
  }
  get city(){
    return this.addressDetailsFormGroup.get('city')!;
  }
  get district(){
    return this.addressDetailsFormGroup.get('district')!;
  }
  get taluka(){
    return this.addressDetailsFormGroup.get('taluka')!;
  }
  get addressLine1(){
    return this.addressDetailsFormGroup.get('addressLine1')!;
  }
  get addressLine2(){
    return this.addressDetailsFormGroup.get('addressLine2')!;
  }
  get landMark(){
    return this.addressDetailsFormGroup.get('landMark')!;
  }

  get addressDetailsTable(){
    return this.addressDetailsTableFormGroup.get('addressDetailsTable')!;
  }
setAddressDetailsTable(){
  this.addressDetailsTable.setValue(this.addressTable)
}

  get geneticCodesTable(){
    return this.geneticCodeFormGroup.get('geneticCodesTable')!;
  }
setGeneticCodesTable(){
  this.geneticCodesTable.setValue(this.geneticCodes)
}


  get livestockTable(){
    return this.othersDetailsFormGroup.get('livestockTable')!;
  }
setLivestockTable(){
  this.livestockTable.setValue(this.liveStokeData)
}
  get equipmentDetailsTable(){
    return this.othersDetailsFormGroup.get('equipmentDetailsTable')!;
  }
setEquipmentDetailsTable(){
  this.equipmentDetailsTable.setValue(this.equipmentData)
}

  get firstName(){
    return this.contactPersonDetailsFormGroup.get('firstName')!;
  }
  get middleName(){
    return this.contactPersonDetailsFormGroup.get('middleName')!;
  }
  get lastName(){
    return this.contactPersonDetailsFormGroup.get('lastName')!;
  }
  get designation(){
    return this.contactPersonDetailsFormGroup.get('designation')!;
  }
  setDesignation(){
    this.designation.setValue(this.designationDropdown);
  }
  get mobileNumber(){
    return this.contactPersonDetailsFormGroup.get('mobileNumber')!;
  }
  get mobileNumberPrimaryStatus(){
    return this.contactPersonDetailsFormGroup.get('mobileNumberPrimaryStatus')!;
  }
  get mobWtSts(){
    return this.contactPersonDetailsFormGroup.get('mobWtSts')!;
  }
  get email(){
    return this.contactPersonDetailsFormGroup.get('email')!;
  }
  get emailPrimaryStatus(){
    return this.contactPersonDetailsFormGroup.get('emailPrimaryStatus')!;
  }
  get contactTableData(){
    return this.contactPersonDetailsFormGroup.get('contactTableData')!;
  }
  setContactTableData(){
    this.contactTableData.setValue(this.contactsData)
  }

  get fileDetails(){
    return this.documentUploadFormGroup.get('fileDetails')!;
  }
  setFileDetails(){
    this.fileDetails.setValue(this.documentUploadFormGroup);
  }

  get licenseDetailsTable(){
    return this.licenseDetailsFormGroup.get('licenseDetailsTable')!;
  }
  setLicenseDetailsTable(){
    this.licenseDetailsTable.setValue(this.licenseData);
  }


  get CalamitiesProneCitiesData(){
    return this.FPOcoverageAreaFormGroup.get('CalamitiesProneCitiesData')!;
  }
  setCalamitiesProneCities(){
    this.CalamitiesProneCitiesData.setValue(this.CalamitiesProneCitiesDropdown);
  }
  get NonCalamitiesProneCitiesData(){
    return this.FPOcoverageAreaFormGroup.get('NonCalamitiesProneCitiesData')!;
  }
  setNonCalamitiesProneCities(){
    this.NonCalamitiesProneCitiesData.setValue(this.NonCalamitiesProneCitiesDropdown);
  }

newGstDetails(){
  return new FormGroup({
    gstNumber:new FormControl(),
    gstSate:new FormControl(),
    gstHeadOffice:new FormControl()
  })
}

  borrowerChange(e:any){
    this.borrower = e.target.value;
    if(this.borrower == '- Institution'){
      this.isInstitutionPage = true;
      this.isIndividualPage = false;
    }else{
      this.isInstitutionPage = false;
      this.isIndividualPage = true;
    }
  }
  checkBoxURN(e: any) {
    if (e.target.checked) {
      this.showUrn = true;
      this.isUrn = 'Yes';
      console.log(this.isUrn);
    } else {
      this.showUrn = false;
      this.isUrn = 'No';
      console.log(this.isUrn);
    }
  }

  checkgeneticBox(e: any) {
    if (e.target.checked) {
      this.showgentic = true;
      this.geneticCodeStatus = 'Yes';
      console.log(this.showgentic);
    } else {
      this.showgentic = false;
      this.geneticCodeStatus = 'No';
      console.log(this.showgentic);
    }
  }

  isInsurenceLiveStoke(e:any){
    if(e.target.value == 'Yes'){
      this.showInsLiveStoke = true;
    }else{
      this.showInsLiveStoke = false;
    }
  }
  selectCompanyType(type:any){
    
    this.company = type;
    this.isCompanyTypeSelected = true;
  }
  selectStakeholderType(stakeholder:any){
    this.stakeholder = stakeholder;
    this.isStakeholderTypeSelected = true;
  }
  selectCreditRatingAgency(agency:any){
    this.CRagency = agency;
    this.isCreditRatingAgencySelected = true;
  }
  selectCreditRating(CRating:any){
    this.creditRatingDropdown = CRating;
    this.isCreditRatingSelected = true;
  }
  selectAMLCategory(aml:any){
    this.AMLCategoryDropdown = aml.amlcategory_name;
    this.amlcategoryName = aml.amlcategory_name;
    this.amlcategorygid = aml.amlcategory_gid;
    this.isAMLCategorySelected = true;
    this.setCategoryAml()
  }
  selectBusinessCategory(bCategory:any){
    this.bCategoryDropdown = bCategory.businesscategory_name;
    this.businesscategoryName = bCategory.businesscategory_name;
    this.businesscategorygid = bCategory.businesscategory_gid;
    this.isbCategorySelected = true;
    this.setCategoryBusiness();
  }
  selectDesignations(designation:any){
    this.designationDropdown = designation.designation_type;
    this.designationType = designation.designation_type;
    this.isDesignationSelected = true;
    this.setDesignation();
  }
  selectAddressTypes(addresstype:any){
    this.addressType.setValue(addresstype.address_type);
    this.AddressTypeValue=addresstype.address_gid;
    this.isAddressTypeSelected = true;
    //this.setAddressType();

}
  selectSamunnatiBranches(branches:any){
    this.branchsDropdown = branches.branch_name;
    this.branchName = branches.branch_name;
    this.branchNamegid = branches.branch_gid;
    this.isBranchSelected = true;
    this.setNearestSamunnatiBaranch();
  }
  selectState(state:any){
    this.statesDropdown = state;
    this.isStateSelected = true;
    this.setTanState();
  } 
  selectTANCategory(tanState:any){
    this.TANstateCategoryDropdown = tanState.state_name;
    this.tanstateName = tanState.state_name;
    this.tanstategid = tanState.state_gid;
    this.isTANCategorySelected = true;
    this.setcategoryTan()
  }
  selectIntRating(intRating:any){
    this.intRatingDropdown = intRating;
    this.isIntRatingSelected = true;
  }
  selectEquiqmentName(equiqment:any){
    this.equiqmentNameDropdown = equiqment;
    this.isEquiqmentNameSelected = true;
  }
  selectLiveStokeName(liveStoke:any){
    this.liveStokeDropdown = liveStoke;
    this.isLiveStokeSelected = true;
  }
  selectDocTitle(title:any){
    this.DocTitleDropdown = title;
    this.isDocTitleSelected = true;
  }
  selectLicenseType(license:any, index:any){
    this.licenseDropdown = license;
    this.licenseData[index].type = license;
    
    this.isLicenseSelected = true;
    this.setLicenseDetailsTable();
  }
  
  selectContactType(type:any,index:any){
    
    this.contactsTypeDropDown = type;
    this.contactsData[index].type = type;
    
    this.isContactTypeSelected = true;
  }
  selectCPCity(city:any,i?:any){
    this.isCalamitiesProneCitiesSelected = true;
    this.CalamitiesProneCitiesDropdown.push(city);
    this.setCalamitiesProneCities();
    this.CalamitiesProneCities.splice(i,1);
  }
  selectNonCPCity(city:any,i?:any){
    this.isNonCalamitiesProneCitiesSelected = true;
    this.NonCalamitiesProneCitiesDropdown.push(city);
    this.setNonCalamitiesProneCities();
    this.NonCalamitiesProneCities.splice(i,1);
  }
  selectGeneticCode(code:any,i?:any){
    this.isGeneticCodeSelected = true;
    this.geneticCodeDropdown = code;
    this.geneticCodes.slice(i,1);
  }
  trackByFn(index:any) {
    return index; // or item.id

  }

  addressDetailAutopopulate(){
    if(this.postalCode.value.length == 6){
    var urlpostalcodeaddressbind = "Mstbuyer/GetPostalCodeDetails";
    
    this.params = {
      postal_code: this.postalCode.value,
    }
    this.socketService.getparams(urlpostalcodeaddressbind, this.params).subscribe((responseaddressbind:any) => {
    this.application.uiunlock();
    this.city.setValue(responseaddressbind.city),
    this.state.setValue(responseaddressbind.state_name),
    this.country.setValue('India'),
    this.district.setValue(responseaddressbind.district),
    this.taluka.setValue(responseaddressbind.taluka);
  
     
    })
    
}
}

  
 
  addGST(){
  
  if (this.headoffice_status==undefined)
   {
     this.headoffice_status= "No";
   }

   console.log(this.headoffice_status)
    var params = {
      //institution_gid:this.institution_gid,
      gst_no:this.gstfullcode,
      apifetch_flag:'N',
      gst_state:this.gststatename,
      headoffice_status:this.headoffice_status,

    }

    var urlinstitutionadd = "MstNgApplicationAdd/PostInstitutionGST";
    this.socketService.post(urlinstitutionadd,params).subscribe((responseurlinstitutionadd:any)=>{
      if (responseurlinstitutionadd.status == true) {
    
        this.institutionlist();
        this.notify.showToastMessage('success',responseurlinstitutionadd.message);
        this.gstfullcode='',
  this.gst_no='',
  this.gststatename=''
  this.headoffice_status='No'
       
  
    for(let i=0; i<this.GSTdata.length; i++){ 
      
      if(this.GSTdata[i].gst_no == null && this.GSTdata[i].gstsource !='Manually added'){
        this.gstTableInValid = true;
        break;
      }else{
        this.gstTableInValid = false;
      }
      this.gstfullcode='',
      this.gst_no='',
      this.gststatename=''
      this.headoffice_status='No'
  }
  
}
      else {
        this.notify.showToastMessage('warning',responseurlinstitutionadd.message);
        this.institutionlist();
       
       
      }
    });
   
    this.gstfullcode='',
    this.gst_no='',
    this.gststatename=''
 
    }



  checkheadoffice(institution2branch_gid:any, headofficestatus:any){
    var CheckHeadstatus;
     headofficestatus = (headofficestatus == true ? 'Yes' : 'No');
     CheckHeadstatus = this.GSTdata.findIndex(
       a => a.headoffice_status == "Yes" && headofficestatus == "Yes" && a.institution2branch_gid != institution2branch_gid);   
        if(CheckHeadstatus != -1){
          this.GSTdata[CheckHeadstatus].headoffice_status = false;
        }
  }
  
  checknewheadoffice(headofficestatus:any){
    var CheckHeadstatus;
    CheckHeadstatus = this.GSTdata.findIndex(
      a => a.headoffice_status == "Yes");   
       if(CheckHeadstatus != -1){
         this.GSTdata[CheckHeadstatus].headoffice_status = false
       }
       this.headoffice_status=headofficestatus;
      if(headofficestatus == "Yes")
        {
          var urlupdateheadoffice= "MstNgApplicationAdd/UpdateGSTHeadOffice";
          var params2 = {
            institution_gid:this.institution_gid,
        }
          this.socketService.post(urlupdateheadoffice,params2).subscribe((response:any)=>{ 
          });
        }

  }
  
  gstDateAutoPopulate(rowIndex:number,gst_no:any){
    var urlStatebind = "MstApplicationAdd/GetGSTState";
    this.gst_code=gst_no.target.value.substring(0, 2);
    this.gstfullcode=gst_no.target.value;
      this.params = {
        gst_code:this.gst_code
      }

    this.socketService.getparams(urlStatebind, this.params).subscribe((responsestatebind:any) => {
      //this.GSTdata[rowIndex].gst_state = responsestatebind.gst_state;
      this.gststatename=responsestatebind.gst_state;
     
    });

    
  }

  delGSTData(institution2branch_gid:any,index:number){
     this.GSTdata.splice(index,1);
   
    var urldeletegst = "MstApplicationAdd/DeleteInstitutionGST";
   
      this.params = {
        institution2branch_gid:institution2branch_gid
      }

    this.socketService.getparams(urldeletegst, this.params).subscribe((responsedeletegst:any) => {
      
        if (responsedeletegst.data.status == true) {
          this.notify.showToastMessage('success',responsedeletegst.message);
          this.institutionlist();
        }
        else {
          this.notify.showToastMessage('warning',responsedeletegst.message);
        }
      
    });
   
   
    //this.setGstTable();
  }
  


  addAddressDetails(index:any){

    var urladdressadd= "MstApplicationAdd/PostInstitutionAddressDetail";
  
    switch (this.addressDetailsFormGroup.invalid) {
      case true:
        this.addressDetailsFormInvalid = true;
        break;
      case false:

        this.latitude = '';
        this.longitude = '';
        if (this.addressDetailsFormGroup.value.addressLine2 == undefined) {
          var addressString = ''.concat(this.addressDetailsFormGroup.value.addressLine1.toString(), ",", this.addressDetailsFormGroup.value.postalCode.toString());
      } else {
          var addressString = ''.concat(this.addressDetailsFormGroup.value.addressLine1.toString(), ",", this.addressDetailsFormGroup.value.addressLine2.toString(), ",", this.addressDetailsFormGroup.value.postalCode.toString());
      }
      var addressparams = {
          address: addressString
      }
      var geocodeurl = 'GoogleMapsAPINg/GetGeoCoding';
      this.socketService.getparams(geocodeurl,addressparams).subscribe((responsegeocode: any) => {
     
          if (responsegeocode.status == "OK") {
              this.latitude = responsegeocode.results[0].geometry.location.lat.toString();
              this.longitude = responsegeocode.results[0].geometry.location.lng.toString();
              //$scope.geocodingFailed = false;
       
        this.params = {
          addressline1: this.addressDetailsFormGroup.value.addressLine1,
          addressline2: this.addressDetailsFormGroup.value.addressLine2,
          taluka: this.addressDetailsFormGroup.value.taluka,
          city: this.addressDetailsFormGroup.value.city,
          state: this.addressDetailsFormGroup.value.state,
          district: this.addressDetailsFormGroup.value.district,
          postal_code: this.addressDetailsFormGroup.value.postalCode,
          address_type:this.addressDetailsFormGroup.value.addressType,
          address_typegid:this.AddressTypeValue,
          landMark: this.addressDetailsFormGroup.value.landMark,
          primary_status:this.addressDetailsFormGroup.value.primaryStatus,
          country:this.addressDetailsFormGroup.value.country,
          latitude: this.latitude,
          longitude: this.longitude
        }
        this.socketService.post(urladdressadd, this.params).subscribe((responseaddressAdd: any) => {
          if (responseaddressAdd.status == true) {

            this.addresslist();
            this.notify.showToastMessage('success',responseaddressAdd.message);
        }
        else {
          this.notify.showToastMessage('warning',responseaddressAdd.message);
        }
    });
  }
  else if (responsegeocode.status == "ZERO_RESULTS") {
      //$scope.geocodingFailed = true;
  }
})
        
    
        // this.setAddressDetailsTable(index);
        // break;
    }

  }
  addAddressFromGST(gstAddress:any){
    const addressDetailsObject = {
      type:this.addressDetailsFormGroup.value.addressType,
      address: gstAddress.registeredAddress,
      landMark: this.addressDetailsFormGroup.value.landMark,
      primaryStatus:this.addressDetailsFormGroup.value.primaryStatus
    }

    this.addressTable.push(addressDetailsObject);
    
    this.setAddressDetailsTable();
  }
  delAddData(institution2address_gid:any){
    var urldeleteaddress = "MstApplicationAdd/DeleteInstitutionAddressDetail";

      this.params = {
        institution2address_gid:institution2address_gid
      }

    this.socketService.getparams(urldeleteaddress, this.params).subscribe((responsedeleteaddress:any) => {
      //this.ngOnInit();

        if (responsedeleteaddress.status == true) {
          this.addresslist();
          this.notify.showToastMessage('success',responsedeleteaddress.message);
        }
        else {
          this.notify.showToastMessage('warning',responsedeleteaddress.message);
        }
      
    });
    //this.addressTable.splice(i,1);
  }

  addresslist(){
    var urlAddressaddedlist = "MstNgApplicationAdd/GetInstitutionAddressList";  
 

          this.socketService.get(urlAddressaddedlist).subscribe((responseAddressaddedlist: any) => {
            this.addressTable = [];
            for (var i = 0; i < responseAddressaddedlist.mstaddress_list.length; i++) {
              if (responseAddressaddedlist.mstaddress_list[i] != null) {
                  //photoUrlArray[i] = resp.data[i];
             
            var addressArray  = [
              responseAddressaddedlist.mstaddress_list[i].addressline1,
              responseAddressaddedlist.mstaddress_list[i].addressline2,
              responseAddressaddedlist.mstaddress_list[i].taluka,
              responseAddressaddedlist.mstaddress_list[i].district,
              '-'+responseAddressaddedlist.mstaddress_list[i].postal_code
            ]
        
            const addressDetailsObject = {
              institution2address_gid: responseAddressaddedlist.mstaddress_list[i].institution2address_gid,
              address_type: responseAddressaddedlist.mstaddress_list[i].address_type,
              address: addressArray,
              landmark: responseAddressaddedlist.mstaddress_list[i].landmark,
              primaryStatus:responseAddressaddedlist.mstaddress_list[i].primary_status,
              longitude:responseAddressaddedlist.mstaddress_list[i].longitude,
              latitude:responseAddressaddedlist.mstaddress_list[i].latitude,
              addressLine1:responseAddressaddedlist.mstaddress_list[i].addressline1,
              addressLine2:responseAddressaddedlist.mstaddress_list[i].addressline2,
              postalCode:responseAddressaddedlist.mstaddress_list[i].postal_code,
            }
            this.addressTable.push(addressDetailsObject);
          }
        }
        
        });
  }
  submitEquipDetails(equipDetails?:any){
    this.equipObj = {
      insuranceDetails:true,
      isRented:true
    }
    this.equipmentData.forEach((data,index)=>{
      if(data.name == null || data.name == ''){
        this.equipmentTableInValid = true;
      }else{
        this.equipmentTableInValid = false;
      }
    });

    if(!this.equipmentTableInValid || this.equipmentData.length == 0){
      this.equipmentData.push(this.equipObj);
    }
  }

  delEquipmentHolding(i:any){
    this.equipmentData.splice(i,1);
    this.setEquipmentDetailsTable();
  }
  addLiveStokeDetails(liveStokeDetails?:any){
    this.liveStokeObj = {
      liveinsuranceDetails:true
    }
    this.liveStokeData.forEach((data,index)=>{
      if(data.name == null || data.name == ''){
        this.liveStokeTableInValid = true;
      }else{
        this.liveStokeTableInValid = false;
      }
    });

    if(!this.liveStokeTableInValid || this.liveStokeData.length == 0){
      this.liveStokeData.push(this.liveStokeObj);
    }
  }
  delLiveStoke(i:any){
    
    this.liveStokeData.splice(i,1);
    
    this.setLivestockTable()
  }
  removeFormMultiSelectCPcity(i:any,value:any){
    this.CalamitiesProneCitiesDropdown.splice(i,1);
    
    this.CalamitiesProneCities.push(value)
    
  }
  removeFormMultiSelectNonCPcity(i:any,value:any){
    this.NonCalamitiesProneCities.push(value);
    
    this.NonCalamitiesProneCitiesDropdown.splice(i,1);
    

  }
  submitRecivableDetails(recivableDetails?:any){
    this.recivableData.push(this.recivableObj);
  }
  delContact(i:any){
    this.contactsData.splice(i,1);
  }
  addContact(){
    
    this.contact = [
  { "Particulars": this.mobileNumber.value,
    "Primary Status":"on",
   "type" : "Mobile Number"  },
  { "Particulars": this.email.value,
  "Primary Status":"on",
 "type" : "E-Mail" },
];
  
 console.log(this.contact)
  for(let i=0; i<this.contactsData.length;i++){
      if(this.contactsData[i].Particulars == null || this.contactsData[i].type == null){
        this.contactTableInvalid = true;
        
      }else{
        this.contactTableInvalid = false;
      }
    }

    switch (this.mobileNumber.valid && this.email.valid) {
      case true:
        if(!this.contactTableInvalid || this.contactsData.length == 0){
          this.contactTableInvalid = false;
          this.contactsData.push(this.contactObj);
          console.log(this.contactsData)
        }
        break;
      case false:
        this.contactTableInvalid = true;
        break;
    }

  }
  fileUpload(docEvent:any, docDetails:any){
    if(docDetails.invalid){
      
      this.errorMessage = 'Select Doc Title / Enter Doc ID';
      this.alert = true;
      setTimeout(() => this.alert=false, this.hideAfterTime);
    }else{
      var docData = docDetails.form.value;
    var uploadedFiles:any[] = docEvent.target.files;
    for(var i=0; uploadedFiles.length >i; i++){
      this.docObj = {
        docTitle : this.DocTitleDropdown,
      docId : docData.docID,
      docName : uploadedFiles[i].name
      };
      this.docUploadData.push(this.docObj);
    }
    }
    this.docObj.docId = null,
    
    this.DocTitleDropdown = 'Select an Option';
  }

  documentUpload(documentTitle:any, documentId:any, uploadDoc:any, index:any){
    this.documentUploadDetails[index].documentTitle = documentTitle;
    this.documentUploadDetails[index].documentId = documentId;
    this.documentUploadDetails[index].document = uploadDoc.target.files;
    
  }
  delDocDetails(i:any){
    this.documentUploadDetails.splice(i,1);
  }
  addDocumentDetails(){
    this.documentObject={
      title:null,
      value:null,
      attached: new FileReader()
    }
    this.documentUploadDetails.push(this.documentObject);
  }
  addLicenseDetails(licenseDetails?:any){
    this.licenseData.forEach((data,index)=>{
      if(data.number == null || data.type == null || data.issueDate == null || data.expiryDate == null){
        this.licenseTableInValid = true;
      }else{
          this.licenseTableInValid = false;
      }
    });

    if(!this.licenseTableInValid){
      this.licenseData.push({
        type:null
      });
    }
  }
  delLicenseDetails(i:any){
    this.licenseData.splice(i,1);
  }
  dateValidation(e:any){
     var newDate = new Date(e.value);

    if(newDate.getFullYear()<=this.todayDate.getFullYear()){
      if(newDate.getMonth()<=this.todayDate.getMonth()){
        if(newDate.getDate()<=this.todayDate.getDate()){
          return false;
        }else{
        return true;
      }
      }else{
        return true;
      }
    }else{
      return true;
    }
  }

  /** Paralax card effect */

  parallaxCardClick(cardFragment:any){
    this.router.navigate( [ 'app/application-creation/Borrower Details' ], { fragment: cardFragment});
  }

  secondCard(){
    if (this.fragmentIndex > this.cardTitles.length - 1) {
      return this.cardTitles[this.cardTitles.length - this.fragmentIndex - 1];
    } else if (this.fragmentIndex == this.cardTitles.length - 1) {
      return this.cardTitles[0];
    } else {
      return this.cardTitles[this.fragmentIndex + 1] ;
    }
  }
  thirdCard(){
    if (this.fragmentIndex + 1 > this.cardTitles.length - 1) {
      return this.cardTitles[this.cardTitles.length - this.fragmentIndex];
    } else if (this.fragmentIndex + 1 == this.cardTitles.length - 1) {
      return this.cardTitles[0];
    } else {
      return this.cardTitles[this.fragmentIndex + 2];
    }
  }




  saveDraft(formData:any){
    
  }
 

  nextButton(){
    
    if(this.subMenuTitle == 'Institution'){
      switch (this.institutionFormGroup.invalid) {
        case true:
          this.institutionFormInValid = true;
          break;
        case false:
          this.params = {
            application_gid:this.application_gid,
            companypan_no: this.panValue.value,
            company_name: this.legalTradeName.value,
            lei_no: this.lei.value,
            renewaldue_date: this.leirenewaldate,
            cin_no: this.cin.value,
            cin_date: this.cinDate.value,
            businessstartdate: this.businessStartDate.value,
            tan_number: this.tan.value,
            tanstate_gid: this.tanstategid,
            tanstate_name: this.tanstateName,
            kin_no:this.kin.value,
            udhayam_registration: this.udhayamRegistration.value,
            amlcategory_gid: this.amlcategorygid,
            amlcategory_name: this.amlcategoryName,
            businesscategory_gid: this.businesscategorygid,
            businesscategory_name: this.businesscategoryName,         
            nearsamunnatiabranch_gid: this.branchNamegid,
            nearsamunnatiabranch_name: this.branchName,
            urn_status: this.isUrn,
            urn: this.lmsUrnValue.value,       
            
        }
        var institutionsubmiturl = 'MstNgApplicationAdd/SubmitInstitutionDtl';
        this.application.uilock();
      
        this.socketService.post(institutionsubmiturl,this.params).subscribe((result:any) => {
          this.application.uiunlock();
          if(result.status == true){
            this.notify.showToastMessage('success',result.message);
            this.institution_gid=result.institution_gid;
            console.log(this.institution_gid);
            this.institutionlist();
            //this.router.navigateByUrl('app/application-creation/Borrower Details');
            // const institution_gid = (result.institution_gid);
            // const encryptedParameter = this.cmnfunctionService.encryptURL('lsinstitution_gid=' + institution_gid);      
            // const url = 'app/application-creation/Borrower Details?hash=' + encryptedParameter;
            // this.encryptedParameter = encryptedParameter
          } 
          else{
            this.notify.showToastMessage('warning',result.message);
          }
        });
     
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].completed = true;
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].hasError = false;
          break;
      }
    }

    if(this.subMenuTitle == 'GST'){
    this.institutionlist()
    this.params = {
      institution_gid:this.institution_gid,
      
  }
  var gstsubmiturl = 'MstNgApplicationAdd/SubmitGSTDtl';
  this.application.uilock();

  this.socketService.post(gstsubmiturl,this.params).subscribe((result:any) => {
    this.application.uiunlock();
    if(result.status == true){
      this.notify.showToastMessage('success',result.message);
     
    } 
    else{
      this.notify.showToastMessage('warning',result.message);
    }
  });
     

      if(!this.gstTableInValid && this.GSTdata.length !== 0){
        this.parallaxCardClick(this.secondCard());
        this.loanManagementModel.ApplicationCreationMenu[1].subMenu[1].completed = true;
        this.loanManagementModel.ApplicationCreationMenu[1].subMenu[1].hasError = false;
      }



    }

    if(this.subMenuTitle == 'Address'){
      switch (this.addressTable.length == 0) {
        case true:
          alert("Please add Address");
          break;
          this.institutionlist()
          this.params = {
            institution_gid:this.institution_gid,
            
        }
        var addresssubmiturl = 'MstNgApplicationAdd/SubmitInstitutionAddressDtl';
        this.application.uilock();
      
        this.socketService.post(gstsubmiturl,this.params).subscribe((result:any) => {
          this.application.uiunlock();
          if(result.status == true){
            this.notify.showToastMessage('success',result.message);
           
          } 
          else{
            this.notify.showToastMessage('warning',result.message);
          }
        });
        case false:
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[2].completed = true;
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[2].hasError = false;
          break;
      }
    }

    if(this.subMenuTitle == 'Contact Person'){
      switch (this.contactPersonDetailsFormGroup.invalid ) {
        case true:
          this.contactPersonDetailsFormInvalid = true;
          break;
        case false:
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[3].completed = true;
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[3].hasError = false;
          break;
      }
    }

    if (this.subMenuTitle == 'Genetic code by Business') {
      
      if(!this.geneticCodeFormInvalid){
      this.params=
          {           
            GeneticCodes_list:this.geneticCodes,
          }
  var urlGeneticCodeAddList = "MstNgApplicationAdd/PostGeneticCode"; 
  this.socketService.post(urlGeneticCodeAddList,this.params).subscribe((geneticcodeaddresult: any) => {
    if(geneticcodeaddresult.status == true){
      this.notify.showToastMessage('success',geneticcodeaddresult.message);
   

  this.parallaxCardClick(this.secondCard());
  this.loanManagementModel.ApplicationCreationMenu[1].subMenu[4].completed = true;
  this.loanManagementModel.ApplicationCreationMenu[1].subMenu[4].hasError = false;
}

else
{
  this.notify.showToastMessage('warning',geneticcodeaddresult.message);
}
});
      }
}
    

    if(this.subMenuTitle == 'Other'){
      

      switch (this.othersDetailsFormGroup.valid) {
        case true:
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[5].completed = true;
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[5].hasError = false;
            this.parallaxCardClick(this.secondCard());
          break;
        case false:
          this.othersDetailsFormInvalid = true;
          this.liveStokeTableInValid = true;
          this.equipmentTableInValid = true;
          break;
      }
    }

    if(this.subMenuTitle == 'License Details'){

      this.licenseData.forEach((data,index)=>{
        if(data.number == null || data.type == null || data.issueDate == null || data.expiryDate == null){
          this.licenseTableInValid = true;
        }else{
          this.licenseTableInValid = false;
        }
      });

          if(this.licenseDetailsFormGroup.valid){
            this.parallaxCardClick(this.secondCard());
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[6].completed = this.licenseDetailsFormGroup.valid;
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[6].hasError = false;
          }
    }

  }

  quickLinkErrorIndications(){
    this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].hasError = this.institutionFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[1].subMenu[1].hasError = this.gstDetailsFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[1].subMenu[2].hasError = this.addressDetailsTableFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[1].subMenu[3].hasError = this.contactPersonDetailsFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[1].subMenu[4].hasError = this.geneticCodeFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[1].subMenu[5].hasError = this.othersDetailsFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[1].subMenu[6].hasError = this.licenseDetailsFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[1].subMenu[7].hasError = this.FPOcoverageAreaFormGroup.invalid;
  }


  nextSection(){
    if(this.subMenuTitle == 'FPO Coverage Area'){
      if(this.FPOcoverageAreaFormGroup.invalid){
        this.FPOcoverageFormInvalid = true;
      }

      if(this.borrowerDetailsFormGroup.valid){
        this.sharedData.BorrowerDetalisFormInstitution();
        this.router.navigateByUrl('app/application-creation/Stakeholder Details');
    this.loanManagementModel.ApplicationCreationMenu[1].subMenu[7].completed = this.FPOcoverageAreaFormGroup.valid;
      }
    }
    this.quickLinkErrorIndications();
  }

  locationBack()
  {
    this.router.navigateByUrl('app/application-creation/General-Details');
  }

  onchangebusinessstartdate(){
    var urlgetbuisnessvintage = 'Mstbuyer/GetYearsAndMonthsInBusiness'
    this.params = {
      businessstart_date: this.businessStartDate.value,
    }
    this.socketService.getparams(urlgetbuisnessvintage, this.params).subscribe((responsebuisnessvintage: any) => {
      this.businessVintage.setValue(responsebuisnessvintage.year_business+' Years and '+responsebuisnessvintage.month_business+' Months');
    });
     
  }
    
  
  autopopulatebasedonPANvalue(){
    if(this.panValue.valid){
      var urlCIN = "KycNg/GetCINFromPAN";
      var urlLegalName = "KycNg/PANNumber";
      var urlLEI = "KycNg/GetLEINumber";
      
      
      this.params = {
        pan: this.panValue.value,
      }
      this.application.uilock();
      this.socketService.post(urlCIN, this.params).subscribe((responseCIN: any) => {
      
        if(responseCIN.status == true){
          this.notify.showToastMessage('success',responseCIN.result[0].message);
          this.cin.setValue(responseCIN.result[0].entityId);
        this.cinDate.setValue(responseCIN.result[0].dateOfCreation);
        this.application.uiunlock();
          //this.router.navigateByUrl('app/application-creation/Borrower Details?lsapplication_gid=' + result.application_gid);
        }
        else{
          if(responseCIN.result != null){
          this.notify.showToastMessage('warning',responseCIN.result[0].message);
          }
          else{this.notify.showToastMessage('warning',responseCIN.message);}
        }
        //this.cinDate.setValue(this.cinyear +"-01-01");
       
      }
      )

      this.socketService.post(urlLegalName, this.params).subscribe((responseLegalName: any) => {
        this.legalTradeName.setValue(responseLegalName.result.name);

      }
      )
      this.socketService.post(urlLEI, this.params).subscribe((responseLEI: any) => {
        this.lei.setValue(responseLEI.result[0].lei);
        this.leirenewaldate =responseLEI.result[0].registrationNextRenewalDate;
       
   
      }
      )
    }
      this.appComponent.uiunlock()
  }
  autopopulatebasedonCINvalue(){
    if(this.cin.valid){
      //this.cinDate.setValue("2023-01-01");
      //this.businessVintage.setValue("Auto Populate");
    }
  }
  autopopulatebasedonTANvalue(){
    if(this.tan.valid){
      this.tanState.setValue("Auto Populate");
    }
  }

  StaticMap_View(latitude:any,longitude:any)
  {
    var urlStaticMapview="GoogleMapsAPINg/GetStaticMapUrl";
    
    this.params = {
      latitude:latitude,
      longitude:longitude
    }

    this.socketService.getparams(urlStaticMapview,this.params).subscribe((responseStaticMapview: any) => {
     this.staticmapImgUrl = responseStaticMapview
  })
}

Place_View(addressline1:any,addressline2:any,postalCode:any)
{
  if(addressline1=='') {
    var addressString = ''.concat(addressline1.toString(), ",", postalCode.toString());
  }
  else{
    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postalCode.toString());
  }

  
  var urlplaceview="GoogleMapsAPINg/GetPlaceImage";
  this.params = {
      address: addressString  
  }

  this.socketService.getparams(urlplaceview,this.params).subscribe((responseplaceview: any) => {
    var photoUrlArray: string | any[] | undefined = [];
    for (var i = 0; i < responseplaceview.length; i++) {
      if (responseplaceview[i] != null) {
        this.photoUrlArray[i] = responseplaceview[i]; 
      }
    }
      if (photoUrlArray.length == 0) {
        this.photoNotFound = true;
    } else {
        this.photoUrlList = photoUrlArray;
        this.photoFound = true;
    }
});
}
institutionlist()
{

var urlInstitutionGSTList="MstNgApplicationAdd/GetInstitutionGSTList";
this.socketService.get(urlInstitutionGSTList).subscribe((responseInstitutionGSTList: any) => {
  this.GSTdata = responseInstitutionGSTList.mstgst_list;
 if(this.GSTdata!=null)
 {
 for (var i = 0; i < this.GSTdata.length; i++) {
  if (responseInstitutionGSTList.mstgst_list[i].gstsource != 'N') {
    responseInstitutionGSTList.mstgst_list[i].gstsource = "GST Portal"; 
    this.isprecheck=true;
  }
  else
  {
    responseInstitutionGSTList.mstgst_list[i].gstsource = "Manually added";
    this.isprecheck=false;
  }

 }


for(let i=0; i<this.GSTdata.length; i++){
  
  if(this.GSTdata[i].gst_no == null && this.GSTdata[i].gstsource !='Manually added'){
    this.gstTableInValid = true;
    break;
  }else{
    this.gstTableInValid = false;
  }

}

 }
 else
 {
  this.GSTdata =[]

 }

});
return;

}
  ngOnInit(): void {

    this.institutionlist();
    this.route.queryParams.subscribe(params => {
      const hash = params['hash'];  
      if (hash) {
        const searchObject = this.cmnfunctionService.decryptURL(hash);        
        this.application_gid = searchObject.lsapplication_gid;
        //this.institution_gid = searchObject.lsinstitution_gid;
        }
        console.log("hash ",hash);
   
  });
    var urlAddresstypelist = "AddressType/GetAddressTypeASC"; 
    this.socketService.get(urlAddresstypelist).subscribe((responseaddresslist: any) => {
      this.addressTypesDropdown = responseaddresslist.addresstype_list
     this.filteraddressTypes = this.addressTypesDropdown
  });

  this.addressDetailsFormGroup.controls['searchAddressType'].valueChanges
.pipe(debounceTime(300))
.subscribe((value: string) => {
  this.filteraddressTypes = this.addressTypesDropdown.filter(
    (Adress: { address_type: string }) =>
      Adress.address_type.toLowerCase().includes(value.toLowerCase())
  );
});

this.addresslist();

 var urlAMLCategory="MstApplication360/AMLCategoryList";
 this.socketService.get(urlAMLCategory).subscribe((responseamlcategory: any) => {
  this.AMLCategory.push = responseamlcategory.amlcategory_list
});

var urlBuisnessCategory="MstApplication360/BusinessCategoryList";
this.socketService.get(urlBuisnessCategory).subscribe((responseurlBuisnessCategory: any) => {
 this.BusinessCategory.push = responseurlBuisnessCategory.businesscategory_list
});

var urlbranchesDropdown="MstApplication360/GetSamunnatiBranchList";
this.socketService.get(urlbranchesDropdown).subscribe((responsebranchesDropdown: any) => {
 this.BranchesDropdown.push = responsebranchesDropdown.samunnatibranch_list
});

 // Getting List from API for Designation
 this.socketService.get("MstApplication360/GetDesignationList").subscribe((result:any)=>{
  this.response=result; 
  this.designations = this.response.designation_list;
});

// Getting List from API for AMLCategoryList
this.socketService.get("MstApplication360/AMLCategoryList").subscribe((result:any)=>{
  this.response=result; 
  this.AMLCategory = this.response.amlcategory_list;
  this.filtercategoryAmlTypes = this.AMLCategory
});

this.institutionFormGroup.controls['searchCategoryAml'].valueChanges
.pipe(debounceTime(300))
.subscribe((value: string) => {
  this.filtercategoryAmlTypes = this.AMLCategory.filter(
    (AName: { amlcategory_name: string }) =>
      AName.amlcategory_name.toLowerCase().includes(value.toLowerCase())
  );
});

// Getting List from API for BusinessCategoryList
this.socketService.get("MstApplication360/BusinessCategoryList").subscribe((result:any)=>{
  this.response=result; 
  this.businessCategory = this.response.businesscategory_list;
  this.filtercategoryBusinessTypes = this.businessCategory
});

this.institutionFormGroup.controls['searchCategoryBusiness'].valueChanges
.pipe(debounceTime(300))
.subscribe((value: string) => {
  this.filtercategoryBusinessTypes = this.businessCategory.filter(
    (BCName: { businesscategory_name: string }) =>
      BCName.businesscategory_name.toLowerCase().includes(value.toLowerCase())
  );
});

// Getting List from API for GetSamunnatiBranchList
this.socketService.get("MstApplication360/GetSamunnatiBranchList").subscribe((result:any)=>{
  this.response=result; 
  this.samunnatiBranches = this.response.samunnatibranch_list;
  this.filternearestSamunnatiBranchTypes =  this.samunnatiBranches
});

this.institutionFormGroup.controls['searchnearestSamunnatiBranch'].valueChanges
.pipe(debounceTime(300))
.subscribe((value: string) => {
  this.filternearestSamunnatiBranchTypes = this.samunnatiBranches.filter(
    (DName: { branch_name: string }) =>
      DName.branch_name.toLowerCase().includes(value.toLowerCase())
  );
});

// Getting List from API for Tan State
this.socketService.get("customer/state").subscribe((result:any)=>{
  this.response=result; 
  this.TANCategory = this.response.state_list;
  this.filtercategoryTanTypes= this.TANCategory;
});

this.institutionFormGroup.controls['searchCategoryTan'].valueChanges
.pipe(debounceTime(300))
.subscribe((value: string) => {
  this.filtercategoryTanTypes = this.TANCategory.filter(
    (SName: { state_name: string }) =>
      SName.state_name.toLowerCase().includes(value.toLowerCase())
  );
});


//genetic code by business
this.socketService.get("MstNgApplicationAdd/GetGeneticCodeList").subscribe((geneticcoderesult:any)=>{

  this.geneticCodesDetails = geneticcoderesult.geneticbuisness_list;
this.geneticCodes= geneticcoderesult.geneticbuisness_list;

for(var i=0; i<this.geneticCodes.length;i++){
  this.geneticCodes[i].status = 'Yes';
}

});



  
  
  }

  checkBoxEvent(){
    console.log("fdsgt")
  }

}



