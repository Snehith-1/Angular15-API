import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CardAnimation } from 'src/app/animation';
import { NumberInputValidator, TextInputValidator, CustomEmailValidator, MultiSeclectDropdownValidatior, TableValidator } from 'src/app/shared/validators/customFormValidators';
import { LoanManagementModel } from '../../../../model/loan-management.model';
import { EventEmitter } from 'events';
import { validateBasis } from '@angular/flex-layout';

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
tan:string,
tanState:string,
udhayamRegistration:string,
categoryAML:string,
categoryTan:string,
categoryBusiness:string,
nearestSamunnatiBranch:string,
lmsUrnStatus:string,
lmsUrnValue:string
}

interface IAddressDetails {
  addressType:string,
  primaryStatus:boolean,
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
  selector: 'app-stack-institution',
  templateUrl: './institution.component.html',
  styleUrls: ['./institution.component.scss'],
  animations : [CardAnimation]
})
export class StackInstitutionComponent extends EventEmitter implements OnInit {
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
  listData:any =[];
  gstRegistered:string = 'No';
  mobileNumdata:any[]= [
    {
      mobNumber : 'null',
      WhatsappNumber : 'null',
      PrimaryStatus : 'null'
    }
  ];
  isShow:boolean = false;
  maildata:any[]= [];
  addressdata:any[]= [];
  equipmentData:any[] = [];
  liveStokeData:any[]=[];
  liveStokeInsSts= 'No';
  recivableData:any[]=[];
  docUploadData:any[]=[];
  licenseData:any[]=[];
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
  creditRatingAgency:any;
  stakeholderType:any;
  searchCreditRating:any;
  creditRating :any;
  searchAMLCategory:any;
  searchBusinessCategory:any;
  StakeholderListSelectionTableHeader :any;
  
  searchDesignations:any;
  designations:any;
  searchAddressType:any;
  addressTypes:any
  searchSamunnatiBranches:any;
  samunnatiBranches :any
  searchState:any;
  states :any;
  equiqmentNames :any
  liveStokeNames :any
  searchDocTitles:any;
DocTitles:any
searchLicenseTypes : any;
  licenseTypes :any
  searchInternalRating:any;
  internalRating :any
  isContactTypeSelected:boolean = false;
  contactsTypeDropDown:any;
  contactsData:any =[
  ];
  contactObj = {

  };
  searchContactType:any;
  contactsType :any


  tableData = [
    {
      code:'',
      Status:'',
      Observation:''
    }
  ]
  geneticCodes:any
  Companyname:any
  PANname:any
  geneticCodeDropdown:any = '';
  isGeneticCodeSelected:boolean = false;
  geneticCodeData:any = [];
  geneticObj = {

  }
  searchCalamitiesProneCities:any
  CalamitiesProneCities :any
  CalamitiesProneCitiesDropdown:any = [];
  isCalamitiesProneCitiesSelected:boolean = false;
  isNonCalamitiesProneCitiesSelected:boolean = false;
  searchNonCalamitiesProneCities:any;
  NonCalamitiesProneCities :any
  NonCalamitiesProneCitiesDropdown:any = [];

  stakeholder: any;
  isStakeholderTypeSelected: boolean = false;
  CRagency: any;
  isCreditRatingAgencySelected: boolean = false;
  isCreditRatingSelected: boolean = false;
  creditRatingDropdown: any;
  AMLCategoryDropdown: any;
  isAMLCategorySelected: boolean = false;
  bCategoryDropdown: any;
  isbCategorySelected: boolean = false;
  designationDropdown: any;
  isDesignationSelected: boolean = false;
  addressTypesDropdown: any;
  TANstateCategoryDropdown: any;
  isAddressTypeSelected: boolean = false;
  isTANCategorySelected: boolean = false;
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
  addObj = {
    addressType : '',
    addLine1 : '',
    addLine2 : '',
    taluka : '',
    district : '',
    state : '',
    country : 'India',
    city : '',
    pinCode : '',
    landMark : '',
    lat : '',
    lon :'',
    PrimaryStatus : null
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

stackholderFormGroup!:FormGroup;
institutionFormDetails:IInstitutionFormDetails;

addressDetailsFormGroup!:FormGroup;
addressDetailsTableFormGroup!:FormGroup;
addressDetailsFormDetails:IAddressDetails;
listDetailsFormGroup!:FormGroup;  
contactPersonDetailsFormGroup!:FormGroup;
contactPersonDetails:IContactPersonDetails;

borrowerDetailsFormGroup!:FormGroup;
documentUploadFormGroup!:FormGroup;
documentUploadDetails:Array<any>;
documentObject:any;
addressTable:any = [
]
/** Paralax card effect */
cardTitles:any;
fragmentIndex:number = 0;
subMenuTitle: string = 'List';

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
documentUploadFormInvalid:boolean = false;

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

gstTableHeader :any

contactTableHeader:any

documentuploadHeader :any

geneticCodeHeader:any

equipmentTableHeader:any

liveStokeTableHeader:any

licenseTableHeader:any

  docUpload:any[]=[];
  wtContactStsChecked:boolean = false;
gstselectedOption: any = {
  id: 1,
  name: 'Madhya Pradesh',
};

  address :any

// Readbutton
isReadMore: boolean = true;




primarylTableView: boolean = true;
additionalTableView: boolean = false;
gstTabTypeIndex = 0;
gstTableIndex = 0;

expand: boolean = false;
anchor: string = 'Read more';

headerOptionsArr: any ;

additionalAddressDetails:any;

primaryAddressDetails :any;

AMLCategory:any
businessCategory :any
dataFromGSTportal:any;
  contactTableInvalid: boolean = false;
  TANCategory:any
  constructor( public datePipe:DatePipe,private formBuilder:FormBuilder, private router:Router, private route:ActivatedRoute, private loanManagementModel:LoanManagementModel){
    super();
    this.dataFromGSTportal = loanManagementModel.dataFromGSTportal;
    this.GSTdata.push(this.dataFromGSTportal);
    this.additionalAddressDetails = loanManagementModel.additionalAddressDetails;
    this.companyType = loanManagementModel.companyType;
    this.creditRatingAgency = loanManagementModel.creditRatingAgency;
    this.stakeholderType = loanManagementModel.stakeholderType;
    this.creditRating = loanManagementModel.creditRating;
    this.headerOptionsArr = loanManagementModel.headerOptionsArr
    this.geneticCodes = loanManagementModel.geneticCodes
    this.Companyname = loanManagementModel.Companyname
    this.PANname = loanManagementModel.PANname
    this.cardTitles = loanManagementModel.stackholderInstitutionCardTitles
    this.gstTableHeader = loanManagementModel.gstTableHeader1
    this.NonCalamitiesProneCities = loanManagementModel.NonCalamitiesProneCities
    this.CalamitiesProneCities = loanManagementModel.CalamitiesProneCities
    this.internalRating = loanManagementModel.internalRating
    this.contactsType = loanManagementModel.contactsType
    this.licenseTypes = loanManagementModel.licenseTypes
    this.DocTitles = loanManagementModel.DocTitles
    this.states = loanManagementModel.states
    this.equiqmentNames = loanManagementModel.equiqmentNames
    this.designations = loanManagementModel.designations
    this.AMLCategory = loanManagementModel.AMLCategory
    this.TANCategory = loanManagementModel.TANCategory
    this.businessCategory = loanManagementModel.businessCategory
    this.primaryAddressDetails = loanManagementModel.primaryAddressDetails
    this.addressTypes = loanManagementModel.addressTypes
    this.address = loanManagementModel.addressDropdown
    this.samunnatiBranches = loanManagementModel.samunnatiBranches
    this.equiqmentNames = loanManagementModel.equiqmentNames
    this.liveStokeNames     = loanManagementModel.liveStokeNames
    this.contactTableHeader = loanManagementModel.contactTableHeader
    this.documentuploadHeader  = loanManagementModel.documentuploadHeader
    this.geneticCodeHeader  = loanManagementModel.geneticCodeHeader
    this.equipmentTableHeader  = loanManagementModel.equipmentTableHeader
    this.liveStokeTableHeader = loanManagementModel.liveStokeTableHeader
    this.licenseTableHeader = loanManagementModel.licenseTableHeader
    this.StakeholderListSelectionTableHeader = loanManagementModel.StakeholderListSelectionTableHeader


    this.router.navigate( [ 'app/application-creation/Stakeholder Details' ], { fragment: this.subMenuTitle } );
    loanManagementModel.ApplicationCreationMenu[2].subMenu = [
      {
        subMenuTitle:'List',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:"Stakeholder",
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:"GST",
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:"Address",
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:"Contact Person",
        hasError:false,
        completed:false,
      }
    ]
    this.customEvent = true;
    this.mainMenu = loanManagementModel.ApplicationCreationMenu;
    this.institutionFormDetails = {} as IInstitutionFormDetails;
    this.addressDetailsFormDetails = {} as IAddressDetails;
    this.contactPersonDetails = {} as IContactPersonDetails;
    this.documentUploadDetails = [{
      title:null,
      value:null,
      attached: new FileReader()
    }];
    this.documentObject = {} ;

    route.fragment.subscribe((fragment:any)=>{
      if(fragment !== null){
        this.subMenuTitle = fragment;
        this.stackholderFormGroup;
        this.subMenuArray = this.mainMenu[1].subMenu;
        this.subMenuIndex = this.subMenuArray.indexOf(this.subMenuTitle);
        this.cardTitles;
        this.fragmentIndex = this.cardTitles.indexOf(fragment);
      }
    });
    this.loadForm();

  }

  addressDetailAutopopulate(value:any){
    if(value.length == 6){
      this.state.setValue('Tamil Nadu');
    this.country.setValue('India');
    this.city.setValue('city');
    this.district.setValue('district');
    this.taluka.setValue('taluka');
    }
  }
  selectListType(type:any,index:any){
    this.isContactTypeSelected = true;
    this.contactsTypeDropDown = type;
    this.listData[index].type = type;
  }
  delList(i:any){
    this.listData.splice(i,1);
  }
 
dropViewFunction(selectedOption: any) {
  this.gstselectedOption = selectedOption;
}

gstShowText() {
  this.isReadMore = !this.isReadMore;
}
trackByFn(index:any) {
  return index;
}

gstTabType(i: number) {
  this.gstTabTypeIndex = i
}
  tracker= (i: any) => i;
  onInputChanged(value: any, rowIndex: number, propertyKey: string): void {
      const newValue = this.GSTdata.map((row: any, index: number) => {
        return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
      })
      this.GSTdata = newValue;
      this.setGstTable();
  }
  onInputChangedListDetails(value: any, rowIndex: number, propertyKey: string): void {
    const newValue = this.listData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.listData = newValue;
  }

  onInputChangedContactDetails(value: any, rowIndex: number, propertyKey: string): void {
    const newValue = this.contactsData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.contactsData = newValue;
  }

  onInputChangedGeneticCode(value: any, rowIndex: number, propertyKey: string): void {
    const newValue = this.geneticCodes.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.geneticCodes = newValue;
    this.setGeneticCodesTable();
  }
  onInputCompanyname(value: any, rowIndex: number, propertyKey: string): void {
    const newValue = this.Companyname.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.Companyname = newValue;
    // this.setGeneticCodesTable();
  }
  onInputPANname(value: any, rowIndex: number, propertyKey: string): void {
    const newValue = this.PANname.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.PANname = newValue;
    // this.setGeneticCodesTable();
  }

  onInputChangedDocUpload(value: any, rowIndex: number, propertyKey: string): void {

    const newValue = this.documentUploadDetails.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.documentUploadDetails = newValue;
    this.setFileDetails();
  }
  onInputChangedEquipmentDetails(value: any, rowIndex: number, propertyKey: string): void {

    const newValue = this.equipmentData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.equipmentData = newValue;
    this.setEquipmentDetailsTable();
  }

  onInputChangedLiveStokeDetails(value: any, rowIndex: number, propertyKey: string): void {

    const newValue = this.liveStokeData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.liveStokeData = newValue;
    this.setLivestockTable();
  }

  onInputChangedLicenseDetails(value: any, rowIndex: number, propertyKey: string): void {

    const newValue = this.licenseData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.licenseData = newValue;
    this.setLicenseDetailsTable();
  }

  loadForm(){

    this.stackholderFormGroup = new FormGroup({
      cin : new FormControl('',[Validators.required,Validators.maxLength(21),Validators.minLength(21),Validators.pattern(/^[UL]{1}[0-9]{5}[A-Z]{2}[0-9]{4}[A-Z]{3}[0-9]{6}$/)]),
      cinDate : new FormControl(),
      businessStartDate : new FormControl(null),
      businessVintage : new FormControl(),
      panValue : new FormControl('',[Validators.required,Validators.maxLength(10),Validators.minLength(10),Validators.pattern(/^[A-Z]{3}[CHFATBLJG]{1}[A-Z]{1}[0-9]{4}[A-Z]{1}$/)]),
      legalTradeName : new FormControl('',[Validators.required,Validators.maxLength(300)]),
      tan : new FormControl(null,[
        Validators.maxLength(10),Validators.minLength(10),
        Validators.pattern(/^[A-Z]{4}[0-9]{5}[A-Z]{1}$/)
      ]),
      tanState : new FormControl(),
      categoryTan : new FormControl()
    })
    

    this.gstDetailsFormGroup = new FormGroup({
      gstTable: new FormControl([],[Validators.required, TableValidator()])
    })

    this.listDetailsFormGroup = new FormGroup({
      SampleName:new FormControl(null,[
        Validators.required,Validators.pattern(/^[a-zA-Z ]*$/)
      ]),
      AddSampleName:new FormControl(null,[
        Validators.required,Validators.pattern(/^[a-zA-Z ]*$/)
      ]),
      SampleDesignation:new FormControl(null,[
        Validators.required,Validators.pattern(/^[a-zA-Z ]*$/)
    
      ]),
      AddSampleDesignation:new FormControl(null,[
        Validators.required,Validators.pattern(/^[a-zA-Z ]*$/)
    
      ]),
      panvalue:new FormControl(null,[
        Validators.required,Validators.maxLength(10),Validators.pattern(/^[A-Z]{3}[P]{1}[A-Z]{1}[0-9]{4}[A-Z]{1}$/)
    
      ]),
      AddPanValue:new FormControl(null,[
        Validators.required,Validators.pattern(/^[A-Z]{3}[P]{1}[A-Z]{1}[0-9]{4}[A-Z]{1}$/)
    
      ]),
    
     })
    

    this.addressDetailsFormGroup = new FormGroup({
      addressType:new FormControl('',[Validators.required]),
      primaryStatus:new FormControl('true',[Validators.required]),
      postalCode:new FormControl('',[Validators.required,Validators.maxLength(6),Validators.minLength(6),Validators.pattern(/^[1-9]{1}[0-9]{5}$/)]),
      country:new FormControl(),
      state:new FormControl(),
      city:new FormControl(),
      district:new FormControl(),
      taluka:new FormControl(),
      addressLine1:new FormControl('',[Validators.required,Validators.maxLength(135)]),
      addressLine2:new FormControl(null,[Validators.maxLength(135)]),
      landMark:new FormControl('',[Validators.maxLength(100)]),
     })

     this.addressDetailsTableFormGroup = new FormGroup({
      addressDetailsTable: new FormControl([],[Validators.required, TableValidator()])
     })

     this.contactPersonDetailsFormGroup = new FormGroup({
      firstName:new FormControl(null,[
        Validators.required,
        Validators.required,
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z ]*$/)
      ]),
      middleName:new FormControl(null,[
        
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z ]*$/)
      ]),
      lastName:new FormControl(null,[
        
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z ]*$/)
      ]),
      // designation:new FormControl(),
      mobileNumber:new FormControl(null,[
        Validators.required,
        Validators.minLength(0),
        Validators.maxLength(10),
        Validators.pattern(/^[0-9]*$/)
      ]),
      mobileNumberPrimaryStatus:new FormControl(true,[
        Validators.required
      ]),
      mobWtSts:new FormControl(true,[
        Validators.required
      ]),
      email:new FormControl(null,[
        Validators.required,
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|in|co\.in|org|co)$/)
      ]),
      emailPrimaryStatus:new FormControl(true,[
        Validators.required,
      ])
     })



     this.documentUploadFormGroup = new FormGroup({
      fileDetailsdata : new FormControl()
     })

     this.borrowerDetailsFormGroup = this.formBuilder.group({
      institutionDetailsData:this.stackholderFormGroup,
      gstDetailsData:this.gstDetailsFormGroup,
      addressDetailsData:this.addressDetailsTableFormGroup,
      contactPersonDetailsData:this.contactPersonDetailsFormGroup,
      documentUploadDetails:this.documentUploadFormGroup
     })
  }

  get cin(){
    return this.stackholderFormGroup.get('cin')!;
  }
  get cinDate(){
    return this.stackholderFormGroup.get('cinDate')!;
  }
  get businessStartDate(){
    return this.stackholderFormGroup.get('businessStartDate')!;
  }

  setBusinessStartDate(){
    return this.businessStartDate.setValue("");
  }
  get businessVintage(){
    return this.stackholderFormGroup.get('businessVintage')!;
  }
  get panValue(){
    return this.stackholderFormGroup.get('panValue')!;
  }
  get legalTradeName(){
    return this.stackholderFormGroup.get('legalTradeName')!;
  }
  setLegalTradeName(){
    return this.legalTradeName.setValue("Auto Populate");
  }

  get tan(){
    return this.stackholderFormGroup.get('tan')!;
  }
  setcategoryTan(){
    this.categoryTan.setValue(this.TANstateCategoryDropdown)
  }
  get tanState(){
    return this.stackholderFormGroup.get('tanState')!;
  }
  setTanState(){
    this.tanState.setValue(this.statesDropdown)
  }

  get fileDetailsData(){
    return this.documentUploadFormGroup.get('fileDetails')!;
  }
  setFileDetails(){
    this.fileDetailsData.setValue(this.documentUploadDetails);
  }


  get addressType(){
    return this.addressDetailsFormGroup.get('addressType')!;
  }
  setAddressType(){
    this.addressType.setValue(this.addressTypesDropdown);
  }
  get categoryTan(){
    return this.stackholderFormGroup.get('categoryTan')!;
  }

  
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
  // get designation(){
  //   return this.contactPersonDetailsFormGroup.get('designation')!;
  // }
  // setDesignation(){
  //   this.designation.setValue(this.designationDropdown);
  // }
  get mobileNumber(){
    return this.contactPersonDetailsFormGroup.get('mobileNumber')!;
  }
  get mobileNumberPrimaryStatus(){
    return this.contactPersonDetailsFormGroup.get('mobileNumberPrimaryStatus')!;
  }
  get email(){
    return this.contactPersonDetailsFormGroup.get('email')!;
  }
  get emailPrimaryStatus(){
    return this.contactPersonDetailsFormGroup.get('emailPrimaryStatus')!;
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

  
  get gstTable(){
    return this.gstDetailsFormGroup.get('gstTable')!;
  }
  setGstTable(){
    this.gstTable.setValue(this.GSTdata);
  }

  get SampleName(){
    return this.listDetailsFormGroup.get('SampleName')!;
  }
  get AddSampleName(){
    return this.listDetailsFormGroup.get('AddSampleName')!;
  }
  get panvalue(){
    return this.listDetailsFormGroup.get('panvalue')!;
  }
  get AddPanValue(){
    return this.listDetailsFormGroup.get('AddPanValue')!;
  }
  get SampleDesignation(){
    return this.listDetailsFormGroup.get('SampleDesignation')!;
  }
  get AddSampleDesignation(){
    return this.listDetailsFormGroup.get('AddSampleDesignation')!;
  }

newGstDetails(){
  return new FormGroup({
    gstNumber:new FormControl(),
    gstSate:new FormControl(),
    gstHeadOffice:new FormControl()
  })
}


  // selectDesignations(designation:any){
  //   this.designationDropdown = designation;
  //   this.isDesignationSelected = true;
  //   this.setDesignation();
  // }
  selectAddressTypes(type:any){
    this.addressTypesDropdown = type;
    this.isAddressTypeSelected = true;
    this.setAddressType();
  }
  selectTANCategory(tan:any){
    this.TANstateCategoryDropdown = tan;
    this.isTANCategorySelected = true;
    this.setcategoryTan()
  }

  selectDocTitle(title:any){
    this.DocTitleDropdown = title;
    this.isDocTitleSelected = true;
  }

  selectContactType(type:any,index:any){
    this.isContactTypeSelected = true;
    this.contactsTypeDropDown = type;
    this.contactsData[index].type = type;
  }

  checkBox(e:any){
    if(e.target.checked){
     this.gstRegistered = 'Yes';
    }else{
      this.gstRegistered = 'No';
    }
  }
  addGST(index:any){
    this.gstObj ={
      gstHeadOffice:true,
      gstState:''
    };

    this.GSTdata.forEach((data,index)=>{
      if(data.gstNumber == null || data.gstNumber == ''){
        this.gstTableInValid = true;
      }else{
        this.gstTableInValid = false;
      }
    });


    if(!this.gstTableInValid){
      this.GSTdata.push(this.gstObj);
      this.GSTdata[this.GSTdata.length-1].gstHeadOffice = true;
    }

  }
  gstDateAutoPopulate(rowIndex:number){
    this.GSTdata[rowIndex].gstState = "Auto Populate"
  }

  delGSTData(i:number){
    this.GSTdata.splice(i,1);
  }

  addAddressDetails(){
    var addressArray = [
      this.addressDetailsFormGroup.value.addressLine1,
      this.addressDetailsFormGroup.value.addressLine2,
      this.addressDetailsFormGroup.value.taluka,
      this.addressDetailsFormGroup.value.district,
      this.addressDetailsFormGroup.value.postalCode
    ]

    const addressDetailsObject = {
      type:this.addressDetailsFormGroup.value.addressType,
      address: addressArray,
      landMark: this.addressDetailsFormGroup.value.landMark,
      primaryStatus:this.addressDetailsFormGroup.value.primaryStatus
    }

    switch (this.addressDetailsFormGroup.invalid) {
      case true:
        this.addressDetailsFormInvalid = true;
        break;
      case false:
        this.addressTable.push(addressDetailsObject);
        this.setAddressDetailsTable();
        break;
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
  delAddData(i:number){
    this.addressTable.splice(i,1);
  }

  delContact(i:any){
    this.contactsData.splice(i,1);
  }
  addContact(){
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
        }
        break;
      case false:
        this.contactTableInvalid = true;
        break;
    }

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
    for(let i=0; i<this.documentUploadDetails.length; i++){
      if(
        this.documentUploadDetails[i].title == null || this.documentUploadDetails[i].value == null || this.documentUploadDetails[i].attached == null
        || this.documentUploadDetails[i].title == '' || this.documentUploadDetails[i].value == '' || this.documentUploadDetails[i].attached == ''){
        this.documentUploadFormInvalid = true;
        break;
      }else{
        this.documentUploadFormInvalid = false;
      }
    }

    if(!this.documentUploadFormInvalid){
      this.documentUploadDetails.push(this.documentObject);
    }

  }





  /** Paralax card effect */

  parallaxCardClick(cardFragment:any){
    this.router.navigate( [ 'app/application-creation/Stakeholder Details' ], { fragment: cardFragment } );
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
  thirdCard(){
    if (this.fragmentIndex + 1 > this.cardTitles.length - 1) {
      return this.cardTitles[this.cardTitles.length - this.fragmentIndex];
    } else if (this.fragmentIndex + 1 == this.cardTitles.length - 1) {
      return this.cardTitles[0];
    } else {
      return this.cardTitles[this.fragmentIndex + 2];
    }
  }
  // --------------------------- Individual Page ---------------------------




  checked(e:any){

  }

  nextButton(){

    if(this.subMenuTitle == 'Stakeholder Details'){
      switch (this.stackholderFormGroup.invalid) {
        case true:
          this.institutionFormInValid = true;
          break;
        case false:
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].completed = true;
          break;
      }
    }

    if(this.subMenuTitle == 'GST Details'){

      for(let i=0; i<this.GSTdata.length; i++){
        if(this.GSTdata[i].gstNumber == null){
          this.gstTableInValid = true;
          break;
        }else{
          this.gstTableInValid = false;
        }
      }

      if(!this.gstTableInValid && this.GSTdata.length !== 0){
        this.parallaxCardClick(this.secondCard());
        this.loanManagementModel.ApplicationCreationMenu[2].subMenu[1].completed = true;
        this.loanManagementModel.ApplicationCreationMenu[2].subMenu[1].hasError = false;
      }

    }

    if(this.subMenuTitle == 'Address Details'){
      switch (this.addressTable.length == 0) {
        case true:
          break;
        case false:
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[2].subMenu[2].completed = true;
          this.loanManagementModel.ApplicationCreationMenu[2].subMenu[2].hasError = false;
          break;
      }
    }

    if(this.subMenuTitle == 'Contact Person Details'){
      switch (this.contactPersonDetailsFormGroup.invalid ) {
        case true:
          this.contactPersonDetailsFormInvalid = true;
          break;
        case false:
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[2].subMenu[0].completed = true;
          this.loanManagementModel.ApplicationCreationMenu[2].subMenu[0].hasError = false;
          break;
      }
    }





  }

  quickLinkErrorIndications(){
    this.loanManagementModel.ApplicationCreationMenu[2].subMenu[1].hasError = this.gstDetailsFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[2].subMenu[2].hasError = this.addressDetailsTableFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[2].subMenu[3].hasError = this.contactPersonDetailsFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[2].subMenu[4].hasError = this.documentUploadFormGroup.invalid;
    this.loanManagementModel.ApplicationCreationMenu[2].subMenu[0].hasError = this.stackholderFormGroup.invalid;
  }

  nextSection(){


    if(this.subMenuTitle == 'Document Upload'){
      for(let i=0; i<this.documentUploadDetails.length; i++){
        if(this.documentUploadDetails[i].title == null || this.documentUploadDetails[i].value == null || this.documentUploadDetails[i].attached == null){
          this.documentUploadFormInvalid = true;
          break;
        }
      }
      if(!this.documentUploadFormInvalid && this.documentUploadDetails.length !== 0){
          this.documentUploadFormInvalid = false;
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[1].completed = true;
        }else{
          this.documentUploadFormInvalid = true;
        }

        if(this.borrowerDetailsFormGroup.valid){
          this.router.navigateByUrl('app/application-creation/Stakeholder Details');
        }


    }
    this.quickLinkErrorIndications();
  }
  checkBoxEvent(){}
  docUploadAlert(){
    alert('file uploaded successfully.')
  }
  ngOnInit(): void {
  }

  addList (){
    for(let i=0; i<this.listData.length;i++){
      if(this.listData[i].type == null){
        this.contactTableInvalid = true;
      }
      else{
        this.contactTableInvalid = false;
      }
    }

    switch ( this.SampleName.valid) {
      case true:
        if(!this.contactTableInvalid || this.listData.length == 0){
          this.contactTableInvalid = false;
          this.listData.push(this.contactObj);
        }
        break;
      case false:
        this.contactTableInvalid = true;
        break;
    }
  }


}