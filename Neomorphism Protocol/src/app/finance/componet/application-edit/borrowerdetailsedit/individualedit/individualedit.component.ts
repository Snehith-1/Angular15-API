import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CardAnimation } from 'src/app/animation';
import { NumberInputValidator, TextInputValidator, CustomEmailValidator, MultiSeclectDropdownValidatior, TableValidator } from 'src/app/shared/validators/customFormValidators';
import { LoanManagementModel } from '../../../../model/loan-management.model';
import { EventEmitter } from 'events';
import { ApplicationCreationService } from 'src/app/finance/services/application-creation.service';
import { SocketService } from 'src/app/shared/services/socket.service';
import { debounceTime } from 'rxjs/operators';
import { AppComponent } from 'src/app/app.component';




interface IgstDetails {
  gstNumber:string,
  gstSate:string,
  gstHeadOffice:string
  }
  
  interface IBasicFormDetails {
    lmsUrnStatus:boolean,
    lmsUrn:string,
    panValue:string,
    aadharNumber:string,
    politicallyVerifiedStatus:boolean,
    politicallyVerified:string,
    designation:string,
    dateOfBirth:string,
    age:string,
    gender:string,
    martialStatus:string,
    physicalStatus:string,
    nearestSamunnatiBranch:string
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
  
  interface IOccupationDetails{
    educationalQualification:string,
    mainOccupation:string,
    incomeType:string,
    annualIncome:string,
    totalLandInAcres:string,
    cultivatedLand:string,
    previouseCrop:string,
    proposedCrop:string,
    landOwnerShipType:string,
    landInTheNameOf:string,
    residenceType:string,
    residenceInTheNameOf:string,
    yearsInCurrentResidence:string,
    nearestSamunnatiBranch2:string,
  }
  
  // interface IGeneticCode{
  
  // }
 
  

@Component({
  selector: 'app-individualedit',
  templateUrl: './individualedit.component.html',
  styleUrls: ['./individualedit.component.scss']
})


  export class IndividualeditComponent extends EventEmitter implements OnInit {
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
    familyData:any[]=[];
    showUrn:boolean | undefined ;
    isUrn:string = 'No';
    showIns:boolean | undefined ;
    showInsEquip:boolean | undefined;
    equipInsSts = 'No'
    showInsLiveStoke:boolean | undefined;
    
    mobAlert:boolean = false;
    mailAlert:boolean = false;
    companyType = [
      'Registered', 'Un Registered'
    ];
    isCompanyTypeSelected:boolean = false;
    company: any;
    searchCreditRatingAgency:any;
    creditRatingAgency:any;
    stakeholderType:any;
    searchCreditRating:any;
    creditRating:any;
    searchAMLCategory:any;
    AMLCategory :any;
    searchBusinessCategory:any;
    businessCategory:any;
    searchDesignations:any;
    designations:any;
    searchPANstatus:any;
    panstatus:any;
    searchAddressType:any;
    addressTypes :any;
    searchSamunnatiBranches:any;
    samunnatiBranches :any;
    searchState:any;
    states :any;
    equiqmentNames:any;
    liveStokeNames :any;
    searchDocTitles:any;
    searchfamilyTypes : any;
    searchInternalRating:any;
    isContactTypeSelected:boolean = false;
    contactsTypeDropDown:any;
    contactsData:any =[ ];
    contactObj = {};
    searchContactType:any;
    contactsType:any;
    params:any;
  
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
    CalamitiesProneCities:any;
    CalamitiesProneCitiesDropdown:any = [];
    isCalamitiesProneCitiesSelected:boolean = false;
    isNonCalamitiesProneCitiesSelected:boolean = false;
    
    searchNonCalamitiesProneCities:any;
    NonCalamitiesProneCities:any;
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
    panstatusDropdown: any;
    isPANstatusSelected: boolean = false;
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
    familyDropdown: any;
    isfamilySelected: boolean = false;
    gstApproved:boolean = false;
    gstApprovedStatus = 'No';
    isFutureDate:boolean = false;
    gstObj = {}
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
    familyObj = {    
      nominee:true,
      middleName:'',
      lastName:'',
      dateOfBirth:'',
      age:'',
      relationshipType:null
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
  
  basicFormGroup!:FormGroup;
  basicFormDetails:IBasicFormDetails;
  
  addressDetailsFormGroup!:FormGroup;
  addressDetailsTableFormGroup!:FormGroup;
  addressDetailsFormDetails:IAddressDetails;
  
  contactDetailsFormGroup!:FormGroup;
  contactPersonDetails:IContactPersonDetails;
  
  borrowerDetailsFormGroup!:FormGroup;
  documentUploadFormGroup!:FormGroup;
  documentUploadDetails:Array<any>;
  documentObject:any;
  addressTable:any = []
  /** Paralax card effect */
  cardTitles:any;
  fragmentIndex:number = 0;
  subMenuTitle: string = 'Basic';
  basicFormInValid:boolean = false;
  gstFormInValid:boolean = false;
  gstDataValid:boolean = true;
  equipmentDataValid:boolean = true;
  livestokeDataValid:boolean = true;
  gstTableInValid:boolean = false;
  liveStokeTableInValid:boolean = false;
  familyTableInValid:boolean = false;
  equipmentTableInValid:boolean = false;
  addressDetailsFormInvalid:boolean = false;
  contactPersonDetailsFormInvalid:boolean = false;
  documentUploadFormInvalid:boolean = false;
  
  geneticCodeFormGroup!:FormGroup;
  geneticCodeFormInvalid:boolean = false;
  
  othersDetailsFormGroup!:FormGroup;
  othersDetailsFormInvalid:boolean = false;
  
  familyDetailsFormGroup!:FormGroup;
  familyDetailsFormInvalid:boolean = false;
  
  occupationDetailsFormGroup!:FormGroup;
  occupationFormDetails:IOccupationDetails;
  occupationDetailsFormInvalid:boolean = false;
  
  FPOcoverageAreaFormGroup!:FormGroup;
  FPOcoverageFormInvalid:boolean = false;
  
  subMenuArray:any = [];
  subMenuIndex:number = 0;
  mainMenu:any=[];
  
  gstDetailsFormArray!:FormArray<FormGroup>;
  
  gstDetailsFormGroup!:FormGroup;
  gstIndex:number = 0;
  
  
  
  contactTableHeader :any;
  
  
  
  geneticCodeHeader :any;
  
  equipmentTableHeader:any;
  
  liveStokeTableHeader:any;
  
  // Readbutton
  isReadMore: boolean = true;
  
  
  
  
  primarylTableView: boolean = true;
  additionalTableView: boolean = false;
  gstTabTypeIndex = 0;
  gstTableIndex = 0;
  
  
  
  isPanValueSelected: boolean = false;
  panValueDropdown: any ;
  PANvalues: any = [];
  wtContactStsChecked:boolean = false;
  docUpload:any[]=[];
  
  docObjUpload:any={};
  
  isGenderSelected: boolean = false;
  genderDropdown: any ;
  
  isLSMURNSelectedStatus:string = 'No';
  isMartialStatusSelected: boolean = false;
  martialStatusDropdown: any;
  isPhysicalStatusSelected: boolean = false;
  physicalStatusDropdown: any;
  
  isNearestSamunnatiBranchSelected: boolean = false;
  nearestSamunnatiBranchDropdown: any;
  
  
  
  isInternalRatingSelected: boolean = false;
  internalRatingDropdown: any;
  internalRatings: any = [];
  isEducationalQualificationSelected: boolean = false;
  educationalQualificationDropdown: any;
  
  
  nearestSamunnatiBranches: any;
  martialStatusArray:any;
  genders: any;
  physicalStatusArray: any ;
  educationalQualifications: any ;
  incomeTypes: any;
  previousCrops: any ;
  Proposedcrop: any ;
  landOwnershipTypes: any;
  landNames: any;
  residenceTypes: any;
  relationshipTypes: any;
  familyDetailsHeader:any;
  
  
  
  
  
  isIncomeTypeSelected: boolean = false;
  incomeTypeDropdown: any;
  isPreviousCropSelected: boolean = false;
  isProposedcropSelected: boolean = false;
  previousCropDropdown: any;
  previousCropsDropdown: any;
  
  proposedCropDropdown: any;
  isLandOwnershipTypeSelected: boolean = false;
  landOwnershipTypeDropdown: any;
  isLandNameSelected: boolean = false;
  landNameDropdown: any;
  isResidenceTypeSelected: boolean = false;
  residenceTypeDropdown: any;
  
    isRelationshipSelected: boolean = false;
    relationshipTypeDropdown: any;
    contactTableInvalid: boolean = false;
  
    //Form_60 Reason
    selectedForm_60: string[] = [];
    isForm_60Selected: boolean = false;
    Form_60Array: string[] = [];
    Form_60Dropdown: any =[];
    
    response: any;
    designationType: any;
    genderName: any;
    martialStatusName: any;
    physicalStatusName: any;
    branchName: any;
    addressName: any;
    educationalQualificationName: any;
    incomeTypeName: any;
    landOwnershipName: any;
    propertyinName: any;
    residenceTypeName: any;
    searchphysicalStatus:any;
    searchnearestSamunnatiBranch: any;
    searchresidenceType:any;
    searchIncome:any;
    searcheducationalQualification: any;
    searchlandOwnerShipType: any;
    searchlandInTheNameOf: any;
    filteraddressTypes: { address_type: string }[] = [];
    filterDesgination: { designation_type: string}[] = [];
    filterphysicalStatus: { physicalstatus_name: string}[] = [];
    filternearestSamunnatiBranchTypes: { branch_name: string }[] = [];
    filterEducationalQualification: { educationalqualification_name: string }[] = [];
    filterIncomeTypes: { incometype_name: string }[] = [];
    flterLandOwnershipType: { ownershiptype_name: string }[] = [];
    filterLandsNames: { propertyin_name: string }[] = [];
    filterResidenceType: {residencetype_name: string}[] = [];
    firstname: any;
    lastname: any;
    middlename: any;
    lmsUrnStatus1: any;
    politicallyVerifiedStatus1: any;
    designation_gid1: any;
    genderGid: any;
    maritalstatus_Gid: any;
    physicalstatusGid: any;
    branch_Gid: any;
      constructor( public datePipe:DatePipe,private formBuilder:FormBuilder, private router:Router, public applicationcreation:SocketService,private socketService: SocketService, private route:ActivatedRoute, private loanManagementModel:LoanManagementModel,private appComponent: AppComponent,public application:AppComponent,public socketservice:SocketService,public notify:AppComponent){
      super();
      this.contactsType = loanManagementModel.contactsType;
      this.NonCalamitiesProneCities = loanManagementModel.NonCalamitiesProneCities;
      this.CalamitiesProneCities = loanManagementModel.CalamitiesProneCities;
      this.geneticCodes = loanManagementModel.geneticCodes;
      this.AMLCategory = loanManagementModel.AMLCategory;
      this.businessCategory = loanManagementModel.businessCategory;
      //this.addressTypes = loanManagementModel.addressTypes;
      this.samunnatiBranches = loanManagementModel.samunnatiBranches;
      this.states = loanManagementModel.states;
      this.equiqmentNames = loanManagementModel.equiqmentNames;
      this.liveStokeNames = loanManagementModel.liveStokeNames;
      this.creditRating = loanManagementModel.creditRating;
      this.creditRatingAgency = loanManagementModel.creditRatingAgency;
      this.stakeholderType = loanManagementModel.stakeholderType;
      this.mobileNumdata = loanManagementModel.mobileNumdata;
      //this.designations = loanManagementModel.borrowerInstitutionDesignations;
      this.panstatus = loanManagementModel.IndividualPANstatus;
      this.contactTableHeader =loanManagementModel.contactTableHeader
      this.geneticCodeHeader = loanManagementModel.geneticCodeHeader
      this.equipmentTableHeader = loanManagementModel.equipmentTableHeader
      this.liveStokeTableHeader =loanManagementModel.liveStokeTableHeader
      this.companyType = loanManagementModel.companyType;
  this.cardTitles = loanManagementModel.borrowerdetailsIndividualcardTitles;
  
  //this.nearestSamunnatiBranches = loanManagementModel.nearestSamunnatiBranches
  //this.martialStatusArray = loanManagementModel.martialStatusArray
  //this.genders = loanManagementModel.genders
  //this.physicalStatusArray = loanManagementModel.physicalStatusArray
  //this.educationalQualifications = loanManagementModel.educationalQualifications
  //this.incomeTypes = loanManagementModel.incomeTypes
  this.previousCrops = loanManagementModel.previousCrops
  this.Proposedcrop = loanManagementModel.Proposedcrop
  //this.landOwnershipTypes = loanManagementModel.landOwnershipTypes
  //this.landNames = loanManagementModel.landNames
  //this.residenceTypes = loanManagementModel.residenceTypes
  this.relationshipTypes = loanManagementModel.relationshipTypes
  this.familyDetailsHeader = loanManagementModel.familyDetailsHeader
  this.Form_60Array = loanManagementModel.Form_60Array
  
  
  
      this.router.navigate( [ 'app/application-edit/borrowerdetailsedit' ], { fragment: this.subMenuTitle } );
      loanManagementModel.ApplicationCreationMenu[1].subMenu = [
        {
          subMenuTitle:'Basic',
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'Address',
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'Contact',
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'Occupation',
          hasError:false,
          completed:false,
        },
        {
          subMenuTitle:'Family',
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
      ]
      this.customEvent = true;
      this.mainMenu = loanManagementModel.ApplicationCreationMenu;
      this.basicFormDetails = {} as IBasicFormDetails;
      this.addressDetailsFormDetails = {} as IAddressDetails;
      this.contactPersonDetails = {} as IContactPersonDetails;
      this.occupationFormDetails = {} as IOccupationDetails;
      this.documentUploadDetails = [];
      this.documentObject = {} ;
  
      route.fragment.subscribe((fragment:any)=>{
        if(fragment !== null){
          this.subMenuTitle = fragment;
          this.basicFormGroup;
          this.subMenuArray = this.mainMenu[1].subMenu;
          this.subMenuIndex = this.subMenuArray.indexOf(this.subMenuTitle);
          this.cardTitles;
          this.fragmentIndex = this.cardTitles.indexOf(fragment);
        }
      });
      this.loadForm();
  
    }
  
  
  
    tracker= (i: any) => i;
  
    onInputChanged(value: any, rowIndex: number, propertyKey: string): void {
        const newValue = this.GSTdata.map((row: any, index: number) => {
          return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
        })
        this.GSTdata = newValue;
        this.setGstTable();
      
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
  
    onInputChangedDocUpload(value: any, rowIndex: number, propertyKey: string): void {
  
      const newValue = this.documentUploadDetails.map((row: any, index: number) => {
        return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
      })
      
      this.documentUploadDetails = newValue;
      
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
  
    onInputChangedfamilyDetails(value: any, rowIndex: number, propertyKey: string): void {
  
      const newValue = this.familyData.map((row: any, index: number) => {
        return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
      })
      this.familyData = newValue;
      this.setfamilyDetailsTable();
    }
  
    LMSURNChange(event:any)
    {
      if(event.target.checked)
      {
        this.isLSMURNSelectedStatus = 'Yes';
      }
      else
      {
        this.isLSMURNSelectedStatus = 'No';
      }
    }
    checkBoxEvent(){
      this.wtContactStsChecked = !this.wtContactStsChecked;
    }
    loadForm(){
  
      this.basicFormGroup = new FormGroup({
        lmsUrnStatus: new FormControl(true, [Validators.required]),
        lmsUrn: new FormControl(''),
        panValue: new FormControl('',[Validators.required,Validators.maxLength(10),Validators.minLength(10),Validators.pattern(/^[A-Z]{3}[P]{1}[A-Z]{1}[0-9]{4}[A-Z]{1}$/)]),
        aadharNumber: new FormControl('', [Validators.required,Validators.maxLength(12),Validators.minLength(12),Validators.pattern(/^[2-9]{1}[0-9]{3}[0-9]{4}[0-9]{4}$/)]),
        politicallyVerifiedStatus: new FormControl(true, [Validators.required]),
        politicallyVerified: new FormControl(''),
        firstName: new FormControl('', [Validators.required,Validators.maxLength(30),Validators.pattern(/^[a-zA-Z]+$/)]),
        middleName: new FormControl('',[Validators.maxLength(30),Validators.pattern(/^[a-zA-Z]+$/)]),
        lastName: new FormControl('', [Validators.required,Validators.maxLength(30),Validators.pattern(/^[a-zA-Z]+$/)]),
        designation: new FormControl('', [Validators.required]),
        designation_gid : new FormControl(),
        searchDesignation: new FormControl(),
        dateOfBirth: new FormControl('', [Validators.required]),
        age: new FormControl('', [Validators.required,Validators.pattern(/^[0-9]*$/)]),
        gender: new FormControl('', [Validators.required]),
        gender_gid : new FormControl(),
        martialStatus: new FormControl('', [Validators.required]),
        martialStatus_gid : new FormControl(),
        physicalStatus: new FormControl('', [Validators.required]),
        physicalStatus_gid : new FormControl(),      
        nearestSamunnatiBranch: new FormControl('', [Validators.required]),
        nearestSamunnatiBranch_gid : new FormControl(),
        Form_60: new FormControl(''),
        searchphysicalStatus: new FormControl(''),
        searchnearestSamunnatiBranch: new FormControl(''),
    
      })
  
  
      this.addressDetailsFormGroup = new FormGroup({
        addressType:new FormControl('',[Validators.required]),
        addressType_gid : new FormControl(),
        primaryStatus:new FormControl('true',[Validators.required]),
        postalCode:new FormControl('',[Validators.required,Validators.maxLength(6),Validators.minLength(6),Validators.pattern(/^[1-9]{1}[0-9]{5}$/)]),
        country:new FormControl(),
        state:new FormControl(),
        city:new FormControl(),
        district:new FormControl(),
        taluka:new FormControl(),
        addressLine1:new FormControl('',[Validators.required,Validators.maxLength(135)]),
        addressLine2:new FormControl('',[Validators.maxLength(135)]),
        landMark:new FormControl('',[Validators.maxLength(100)]),
        searchAddressType: new FormControl(''),
       })
  
       this.addressDetailsTableFormGroup = new FormGroup({
        addressDetailsTable: new FormControl([],[Validators.required, TableValidator()])
       })
  
       this.contactDetailsFormGroup = new FormGroup({
        mobileNumber:new FormControl(null,[
          Validators.required,
          NumberInputValidator(10)
        ]),
        mobileNumberPrimaryStatus:new FormControl(true,[
          Validators.required
        ]),           
        mobWtSts:new FormControl(true,[
          Validators.required
        ]),
        email:new FormControl(null,[
          Validators.required,
          CustomEmailValidator(100),Validators.pattern(/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/)
        ]),
        emailPrimaryStatus:new FormControl(true,[
          Validators.required,
        ])
       })
  
       this.occupationDetailsFormGroup = new FormGroup({
        searchIncome: new FormControl(''),
        searcheducationalQualification: new FormControl(''),
        searchlandOwnerShipType: new FormControl(''),
        searchlandInTheNameOf: new FormControl(''),
        searchresidenceType: new FormControl(''),
          educationalQualification: new FormControl('',[Validators.required]),
          educationalQualification_gid: new FormControl(),
          mainOccupation: new FormControl('',[Validators.required,Validators.pattern(/^[a-zA-Z]+$/)]),
          incomeType: new FormControl(''),
          incomeType_gid: new FormControl(''),
  
          annualIncome: new FormControl('',[Validators.required,Validators.pattern(/^[0-9]*$/)]),
          totalLandInAcres: new FormControl(''),
          cultivatedLand: new FormControl(''),
          previouseCrop: new FormControl(''),
          proposedCrop: new FormControl(''),
          landOwnerShipType: new FormControl(''),
          landOwnerShipType_gid: new FormControl(''),
          landInTheNameOf: new FormControl(''),
          landInTheNameOf_gid: new FormControl(''),
          residenceType: new FormControl(''),
          residenceType_gid: new FormControl(''),
          residenceInTheNameOf: new FormControl(''),
          yearsInCurrentResidence: new FormControl('',[Validators.required]),
          nearestSamunnatiBranch2: new FormControl('',[Validators.required])
       })
  
       this.geneticCodeFormGroup = new FormGroup({
        geneticCodesTable: new FormControl([],[Validators.required, TableValidator()])
       })
  
       this.othersDetailsFormGroup = new FormGroup({
        livestockTable: new FormControl([],[Validators.required, TableValidator()]),
        equipmentDetailsTable: new FormControl([],[Validators.required, TableValidator()])
       })
  
       this.familyDetailsFormGroup = new FormGroup({
        familyFirstName: new FormControl('', [Validators.required,Validators.maxLength(30),Validators.pattern(/^[a-zA-Z]+$/)]),
        familyMiddleName: new FormControl('',[Validators.maxLength(30),Validators.pattern(/^[a-zA-Z]+$/)]),
        familyLastName: new FormControl('', [Validators.maxLength(30),Validators.pattern(/^[a-zA-Z]+$/)]),
        familyDateOfBirth: new FormControl(''),
        familyNominee: new FormControl(true),
        familyAge: new FormControl('', [Validators.pattern(/^[0-9]*$/)]),
        familyDetailsTable: new FormControl()
       })
  
       this.FPOcoverageAreaFormGroup = new FormGroup({
        CalamitiesProneCitiesData: new FormControl([],[Validators.required, MultiSeclectDropdownValidatior()]),
        NonCalamitiesProneCitiesData: new FormControl([],[Validators.required, MultiSeclectDropdownValidatior()])
       })
  
       this.documentUploadFormGroup = new FormGroup({
        fileDetails : new FormControl()
       })
  
       this.borrowerDetailsFormGroup = this.formBuilder.group({
        institutionDetailsData:this.basicFormGroup,
        gstDetailsData:this.gstDetailsFormGroup,
        addressDetailsData:this.addressDetailsTableFormGroup,
        contactPersonDetailsData:this.contactDetailsFormGroup,
        geneticCodeByBusinessData:this.geneticCodeFormGroup,
        otherDetailsData:this.othersDetailsFormGroup,
        familyDetailsData:this.familyDetailsFormGroup,
        fpoCoverageAreaData:this.FPOcoverageAreaFormGroup
       })
  
    }
  
  
  
    get lmsUrnStatus(){
      return this.basicFormGroup.get('lmsUrnStatus')!;
    }
    get lmsUrn(){
      return this.basicFormGroup.get('lmsUrn')!;
    }
    get panValue(){
      return this.basicFormGroup.get('panValue')!;
    }
    get aadharNumber(){
      return this.basicFormGroup.get('aadharNumber')!;
    }
    get politicallyVerifiedStatus(){
      return this.basicFormGroup.get("politicallyVerifiedStatus")!;
    }
    get politicallyVerified(){
      return this.basicFormGroup.get("politicallyVerified")!;
    }
    get firstName(){
      return this.basicFormGroup.get("firstName")!;
    }
    get middleName(){
      return this.basicFormGroup.get("middleName")!;
    }
    get lastName(){
      return this.basicFormGroup.get("lastName")!;
    }
    get designation(){
      return this.basicFormGroup.get("designation")!;
    }
    setDesignation(){
      this.designation.setValue(this.designationDropdown);
    }
    get dateOfBirth(){
      return this.basicFormGroup.get("dateOfBirth")!;
    }
    get age(){
      return this.basicFormGroup.get("age")!;
    }
    ageAutoPapulate(dateValue: string){
  
      const selectedDate = new Date(dateValue);
      console.log(selectedDate);
    
      const today = new Date();
      console.log('Today:', today);
    
      const diffInYears = today.getFullYear() - selectedDate.getFullYear();
      console.log('Difference in Years:', diffInYears);
    
      const isBeforeBirthday =
      today.getMonth() < selectedDate.getMonth() ||
      (today.getMonth() === selectedDate.getMonth() &&
        today.getDate() < selectedDate.getDate());
    console.log('Is Before Birthday:', isBeforeBirthday);
    
    const age = isBeforeBirthday ? diffInYears - 1 : diffInYears;
    console.log('Calculated Age:', age);
    
    this.age.setValue(age);
      
    }
    get gender(){
      return this.basicFormGroup.get("gender")!;
    }
    setGender(){
      this.gender.setValue(this.genderDropdown);
    }
    get martialStatus(){
      return this.basicFormGroup.get("martialStatus")!;
    }
    setMartialStatus(){
      this.martialStatus.setValue(this.martialStatusName);
    }
    get physicalStatus(){
      return this.basicFormGroup.get("physicalStatus")!;
    }
    setPhysicalStatus(){
      this.physicalStatus.setValue(this.physicalStatusDropdown);
    }
    get nearestSamunnatiBranch(){
      return this.basicFormGroup.get("nearestSamunnatiBranch")!;
    }
    setNearestSamunnatiBaranch(){
      this.nearestSamunnatiBranch.setValue(this.branchsDropdown);
    }
    get mobileNumber(){
      return this.contactDetailsFormGroup.get('mobileNumber')!;
    }
    get mobileNumberPrimaryStatus(){
      return this.contactDetailsFormGroup.get('mobileNumberPrimaryStatus')!;
    }
    get mobWtSts(){
      return this.contactDetailsFormGroup.get('mobWtSts')!;
    }
    get email(){
      return this.contactDetailsFormGroup.get('email')!;
    }
    get emailPrimaryStatus(){
      return this.contactDetailsFormGroup.get('emailPrimaryStatus')!;
    }
    get addressType(){
      return this.addressDetailsFormGroup.get('addressType')!;
    }
    setAddressType(){
      this.addressType.setValue(this.addressTypesDropdown);
    }
    get primaryStatus(){
      return this.addressDetailsFormGroup.get('primaryStatus')!;
    }
    get postalCode(){
      return this.addressDetailsFormGroup.get('postalCode')!;
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
    get educationalQualification(){
      return this.occupationDetailsFormGroup.get("educationalQualification")!;
    }
    setEducationalQualification(){
      this.educationalQualification.setValue(this.educationalQualificationDropdown)
    }
    get mainOccupation(){
      return this.occupationDetailsFormGroup.get("mainOccupation")!;
    }
    get incomeType(){
      return this.occupationDetailsFormGroup.get("incomeType")!;
    }
    setIncomeType(){
      this.incomeType.setValue(this.incomeTypeDropdown)
    }
    get annualIncome(){
      return this.occupationDetailsFormGroup.get("annualIncome")!;
    }
    get totalLandInAcres(){
      return this.occupationDetailsFormGroup.get("cultivatedLand")!;
    }
    get cultivatedLand(){
      return this.occupationDetailsFormGroup.get("cultivatedLand")!;
    }
    get previouseCrop(){
      return this.occupationDetailsFormGroup.get("previouseCrop")!;
    }
    setPreviouseCrop(){
      this.previouseCrop.setValue(this.previousCropDropdown);
    }
    get proposedCrop(){
      return this.occupationDetailsFormGroup.get("proposedCrop")!;
    }
    setProposedCrop(){
      this.proposedCrop.setValue(this.proposedCrop)
    }
    get landOwnerShipType(){
      return this.occupationDetailsFormGroup.get("landOwnerShipType")!;
    }
    setLandOwnerShipType(){
      this.landOwnerShipType.setValue(this.landOwnershipTypeDropdown);
    }
    get landInTheNameOf(){
      return this.occupationDetailsFormGroup.get("landInTheNameOf")!;
    }
    setLandInTheNameOf(){
      this.landInTheNameOf.setValue(this.landNameDropdown)
    }
    get residenceType(){
      return this.occupationDetailsFormGroup.get("residenceType")!;
    }
    setResidenceType(){
      this.residenceType.setValue(this.residenceTypeDropdown);
    }
    get residenceInTheNameOf(){
      return this.occupationDetailsFormGroup.get("residenceInTheNameOf")!;
    }
    get yearsInCurrentResidence(){
      return this.occupationDetailsFormGroup.get("yearsInCurrentResidence")!;
    }
    get nearestSamunnatiBranch2(){
      return this.occupationDetailsFormGroup.get("nearestSamunnatiBranch2")!;
    }
    
    get cin(){
      return this.basicFormGroup.get('cin')!;
    }
    get cinDate(){
      return this.basicFormGroup.get('cinDate')!;
    }
    get businessStartDate(){
      return this.basicFormGroup.get('businessStartDate')!;
    }
  
    setBusinessStartDate(){
      return this.businessStartDate.setValue("2023-01-01");
    }
    get businessVintage(){
      return this.basicFormGroup.get('businessVintage')!;
    }
    get legalTradeName(){
      return this.basicFormGroup.get('legalTradeName')!;
    }
    setLegalTradeName(){
      return this.legalTradeName.setValue("Auto Papulate");
    }
    get tan(){
      return this.basicFormGroup.get('tan')!;
    }
    get tanState(){
      return this.basicFormGroup.get('tanState')!;
    }
    setTanState(){
      this.tanState.setValue(this.statesDropdown)
    }
    get udhayamRegistration(){
      return this.basicFormGroup.get('udhayamRegistration')!;
    }
    get categoryAml(){
      return this.basicFormGroup.get('categoryAml')!;
    }
    setCategoryAml(){
      this.categoryAml.setValue(this.AMLCategoryDropdown)
    }
    get categoryBusiness(){
      return this.basicFormGroup.get('categoryBusiness')!;
    }
    setCategoryBusiness(){
      this.categoryBusiness.setValue(this.bCategoryDropdown);
    }
  
  
    get gstTable(){
      return this.gstDetailsFormGroup.get('gstTable')!;
    }
    setGstTable(){
      this.gstTable.setValue(this.GSTdata);
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
  
  
  
    get fileDetails(){
      return this.documentUploadFormGroup.get('fileDetails')!;
    }
    setFileDetails(){
      this.fileDetails.setValue(this.documentUploadFormGroup);
    }
  
  get familyFirstName(){
    return this.familyDetailsFormGroup.get('familyFirstName')!;
  }
  get familyMiddleName(){
    return this.familyDetailsFormGroup.get('familyMiddleName')!;
  }
  get familyLastName(){
    return this.familyDetailsFormGroup.get('familyLastName')!;
  }
  get familyDateOfBirth(){
    return this.familyDetailsFormGroup.get('familyDateOfBirth')!;
  }
  get familyNominee(){
    return this.familyDetailsFormGroup.get('familyNominee')!;
  }
  get familyAge(){
    return this.familyDetailsFormGroup.get('familyAge')!;
  }
  
  
  
    get familyDetailsTable(){
      return this.familyDetailsFormGroup.get('familyDetailsTable')!;
    }
    setfamilyDetailsTable(){
      this.familyDetailsTable.setValue(this.familyData);
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
  
    selectgender(gender:any){
      this.genderDropdown = gender.gender_name;
      this.genderName = gender.gender_name;
      this.isGenderSelected = true;
      this.setGender()
    }
    selectMartialStatus(martialStatus:any){
      this.martialStatusDropdown = martialStatus.maritalstatus_gid;    
      this.martialStatusName = martialStatus.maritalstatus_name;
      this.isMartialStatusSelected = true;
      this.setMartialStatus()
    }
    selectPhysicalStatus(physicalStatus:any){
      this.physicalStatusDropdown = physicalStatus.physicalstatus_name;
      this.physicalStatusName = physicalStatus.physicalstatus_name;
      this.isPhysicalStatusSelected = true;
      this.setPhysicalStatus()
    }
  
    selectEducationalQualification(educationalQualification:any){
      this.educationalQualificationDropdown = educationalQualification.educationalqualification_name;
      this.educationalQualificationName = educationalQualification.educationalqualification_name;
      this.isEducationalQualificationSelected = true;    
      this.setEducationalQualification();
    }
    selectIncomeType(incomeType:any){
      this.incomeTypeDropdown = incomeType.incometype_name;
      this.incomeTypeName = incomeType.incometype_name;
      this.isIncomeTypeSelected = true;
      this.setIncomeType();
    }
    selectPreviousCrop(value:any,i?:any){
      this.isPreviousCropSelected = true;
      this.previousCropDropdown = value;
      this.setPreviouseCrop();
      
    }
    selectProposedcrop(value:any,i?:any){
      this.isProposedcropSelected = true;
      this.proposedCropDropdown = value;
      this.setProposedCrop();
      
    }
    selectLandOwnershipType(landOwnershipType:any){
      this.landOwnershipTypeDropdown = landOwnershipType.ownershiptype_name;
      this.landOwnershipName = landOwnershipType.ownershiptype_name;
      this.isLandOwnershipTypeSelected = true;
      this.setLandOwnerShipType();
    }
    selectLandName(landName:any){
      this.landNameDropdown = landName.propertyin_name;
      this.propertyinName = landName.propertyin_name;
      this.isLandNameSelected = true;
      this.setLandInTheNameOf();
    }
    selectResidenceType(residenceType:any){
      this.residenceTypeDropdown = residenceType.residencetype_name;
      this.residenceTypeName = residenceType.residencetype_name;
      this.isResidenceTypeSelected = true;    
      //this.residenceTypes.slice(i,1);
      this.setResidenceType();
    }
  
    selectRelationshipType(value:any,i:any){    
      this.isRelationshipSelected = true;
      this.relationshipTypeDropdown = value;
      this.familyData[i].relationshipType = value
      this.setfamilyDetailsTable();
    }
  
    
    selectAMLCategory(aml:any){
      this.AMLCategoryDropdown = aml;
      this.isAMLCategorySelected = true;
      this.setCategoryAml()
    }
    
    selectDesignations(designation:any){
      debugger
      this.designationDropdown = designation.designation_type;
      this.designationType = designation.designation_type;
      this.isDesignationSelected = true;
      this.setDesignation();
    }
  
    selectPANstatus(PANstatus:any){
      this.panstatusDropdown = PANstatus;
      this.isPANstatusSelected = true;
      this.setPANstatus();
    }
    setPANstatus() {
      throw new Error('Method not implemented.');
    }
    selectAddressTypes(addresstype:any){
      this.addressTypesDropdown = addresstype.address_type;
      this.addressName = addresstype.address_type;
      this.isAddressTypeSelected = true;
      this.setAddressType();
    }
    selectSamunnatiBranches(branches:any){
      this.branchsDropdown = branches.branch_name;
      this.branchName = branches.branch_name;
      this.isBranchSelected = true;
      this.setNearestSamunnatiBaranch();
    }
  
    selectContactType(type:any,index:any){
      this.isContactTypeSelected = true;
      this.contactsTypeDropDown = type;
      this.contactsData[index].type = type;
    }
  
    trackByFn(index:any) {
      
      return index; // or item.id
  
    }
  
    addAddressDetails(){
      var addressArray = [
        this.addressDetailsFormGroup.value.addressLine1,
        this.addressDetailsFormGroup.value.addressLine2,
        this.addressDetailsFormGroup.value.taluka,
        this.addressDetailsFormGroup.value.district,
        '-'+this.addressDetailsFormGroup.value.postalCode
      ]
      if(this.addressDetailsFormGroup.value.taluka==null){
        this.addressDetailsFormGroup.value.taluka = '-'
      }
      const addressDetailsObject = {
        type:this.addressDetailsFormGroup.value.addressType,
        address: addressArray,
        landMark: this.addressDetailsFormGroup.value.landMark,
        primaryStatus: this.addressDetailsFormGroup.value.primaryStatus
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
    delAddData(i:number){
      this.addressTable.splice(i,1);
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
  
      if(!this.equipmentTableInValid){
        this.equipmentData.push(this.equipObj);
      }
    }
  
    delEquipmentHolding(i:any){
      this.equipmentData.splice(i,1);
    }
    addLiveStokeDetails(liveStokeDetails?: any) {
  
      this.liveStokeObj = {
        liveinsuranceDetails: true
      }
      this.liveStokeData.forEach((data, index) => {
        if (data.name == null || data.name == '') {
          this.liveStokeTableInValid = true;
        } else {
          this.liveStokeTableInValid = false;
        }
      });
  
      if (!this.liveStokeTableInValid) {
        this.liveStokeData.push(this.liveStokeObj);
      }
  
    }
    delLiveStoke(i:any){
      this.liveStokeData.splice(i,1);
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
  
    
  
    
    addfamilyDetails(familyDetails?:any){
      for(let i=0; i<this.familyData.length; i++){
        if(this.familyData[i].relationshipType == null || this.familyData[i].firstName == null || this.familyData[i].lastName == null || this.familyData[i].nominee == null || this.familyData[i].dateOfBirth == null || this.familyData[i].age == null){
          this.familyTableInValid = true;
          break;
        }else{
          this.familyTableInValid = false;
        }
      }
      if(this.familyDetailsFormGroup.invalid){
        this.familyTableInValid = true;
      }
      if(!this.familyTableInValid && this.familyDetailsFormGroup.valid){
        this.familyData.push(this.familyObj);
      }
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
      this.router.navigate( [ 'app/application-edit/borrowerdetailsedit' ], { fragment: cardFragment } );
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
  
  
    saveDraft(formData:any){
      
    }
  
    equipHoldInsStatusToggle(event:any){
      if(event.target.checked){
        this.equipInsSts = 'Yes';
      }else{
        this.equipInsSts = 'No';
      }
    }
  
    nextButton(){
      if(this.subMenuTitle == 'Basic'){
        
        if(this.lmsUrnStatus.value=='true'||this.lmsUrnStatus.value=="true"){
          this.lmsUrnStatus1=='Yes';
        }
        else{
          this.lmsUrnStatus1=='No';
        }
        if(this. politicallyVerifiedStatus.value=='true'||this. politicallyVerifiedStatus.value=="true"){
          this. politicallyVerifiedStatus1=='Yes'
        }
        else{
          this.politicallyVerifiedStatus1=='No';
        }
        var params={
          lsapplication_gid:'APPC20230619731',
          urn_status:this.lmsUrnStatus1,
          urn:this.lmsUrn.value,
          aadhar_no:this.aadharNumber.value,
          pan_status :this.panstatusDropdown,
          pan_no  : this.panValue.value,
          panabsencereason_selectedlist :this.Form_60.value,
          first_name:this.firstName.value,
          middle_name :this.middleName.value,
          last_name:this.lastName.value,
          designation_name :this.designationType,
          designation_gid :this.designation_gid1,
          individual_dob :this.dateOfBirth.value,
          age:this.age.value,
          gender_gid:this.genderGid,
          gender_name:this.genderName,
          maritalstatus_name: this.martialStatusName,
          maritalstatus_gid:this.maritalstatus_Gid,
          physicalstatus_name:this.physicalStatusName,
          physicalstatus_gid:this.physicalstatusGid,
          pep_status:  this.politicallyVerifiedStatus1,
          pepverified_date:this.politicallyVerified.value,
          nearsamunnatiabranch_name:this.branchName,
          nearsamunnatiabranch_gid: this.branch_Gid
      
        }
        var url = 'MstNgApplicationAdd/IndividualSubmit';
        //lockUI();
        this.applicationcreation.post(url, params).subscribe((result:any)=>{
          this.response=result; 
          //unlockUI();
          if (result.status == true) {
          
            this.notify.showToastMessage('success',result.message);
            this.basicFormInValid = false;
            this.parallaxCardClick(this.secondCard());
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].completed = true;
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].hasError = false;
            
          }
          else {
            this.notify.showToastMessage('warning',result.message);
          }
        });
        switch (this.basicFormGroup.invalid) {
          case true:
            this.basicFormInValid = true;
            break;
          case false:
            this.basicFormInValid = false;
            this.parallaxCardClick(this.secondCard());
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].completed = true;
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].hasError = false;
            break;
        }
      }
  
      if(this.subMenuTitle == 'Address'){
        console.log('Address from individual');
        switch (this.addressTable.length == 0) {
          case true:
            alert("Please add Address Details");
            break;
          case false:
            this.parallaxCardClick(this.secondCard());
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[1].completed = true;
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[1].hasError = false;
            break;
        }
      }
  
      if(this.subMenuTitle == 'Contact'){
        
        switch (this.contactDetailsFormGroup.invalid) {
          case true:
            this.contactPersonDetailsFormInvalid = true;
            break;
          case false:
            this.parallaxCardClick(this.secondCard());
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[2].completed = true;
            this.loanManagementModel.ApplicationCreationMenu[1].subMenu[2].hasError = false;
            break;
        }
      }
      if(this.subMenuTitle == 'Genetic code by Business'){
        
        for(let i=0; i<this.geneticCodes.length; i++){
          if(this.geneticCodes[i].observation == ''){
            this.geneticCodeFormInvalid = true;
            break;
          }else{
            this.geneticCodeFormInvalid = false;
          }
        }
  
        if(!this.geneticCodeFormInvalid){
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[4].completed = true;
          this.loanManagementModel.ApplicationCreationMenu[1].subMenu[4].hasError = false;
        }else{
          
        }
  
  
      }
  
  
      if(this.subMenuTitle == 'Occupation'){
  
          
            if(this.occupationDetailsFormGroup.valid){
              this.parallaxCardClick(this.secondCard());
              this.loanManagementModel.ApplicationCreationMenu[1].subMenu[3].completed = this.familyDetailsFormGroup.valid;
              this.loanManagementModel.ApplicationCreationMenu[1].subMenu[3].hasError = false;
              this.occupationDetailsFormInvalid = false;
            }else{
              this.occupationDetailsFormInvalid = true;
            }
  
      }
  
  
      if(this.subMenuTitle == 'Family'){
        
            if(this.familyDetailsFormGroup.valid){
              this.familyTableInValid = false;
              this.parallaxCardClick(this.secondCard());
              this.loanManagementModel.ApplicationCreationMenu[1].subMenu[4].completed = this.familyDetailsFormGroup.valid;
              this.loanManagementModel.ApplicationCreationMenu[1].subMenu[4].hasError = false;
            }else{
              this.familyTableInValid = true;
            }
      }
  
  
    }
  
    quickLinkErrorIndications(){
      this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].hasError = this.basicFormGroup.invalid;
      this.loanManagementModel.ApplicationCreationMenu[1].subMenu[1].hasError = this.addressDetailsTableFormGroup.invalid;
      this.loanManagementModel.ApplicationCreationMenu[1].subMenu[2].hasError = this.contactDetailsFormGroup.invalid;
      this.loanManagementModel.ApplicationCreationMenu[1].subMenu[5].hasError = this.geneticCodeFormGroup.invalid;
      this.loanManagementModel.ApplicationCreationMenu[1].subMenu[6].hasError = this.othersDetailsFormGroup.invalid;
      this.loanManagementModel.ApplicationCreationMenu[1].subMenu[4].hasError = this.familyDetailsFormGroup.invalid;
      this.loanManagementModel.ApplicationCreationMenu[1].subMenu[3].hasError = this.occupationDetailsFormGroup.invalid;
  
    }
  
  
    nextSection(){
      if(this.subMenuTitle == 'Other'){
        
  
        switch (this.othersDetailsFormGroup.valid) {
          case true:
            
              this.loanManagementModel.ApplicationCreationMenu[1].subMenu[6].completed = true;
              this.loanManagementModel.ApplicationCreationMenu[1].subMenu[6].hasError = false;
              
              this.router.navigateByUrl('app/application-edit/Stakeholder Details');
            
            break;
          case false:
            this.othersDetailsFormInvalid = true;
  
            this.liveStokeTableInValid = true;
            this.equipmentTableInValid = true;
            break;
        }
      }
      this.quickLinkErrorIndications();
    }
    locationBack()
    {
      this.router.navigateByUrl('app/application-edit/Generalinfoedit');
    }
    panValueAutoPopulate(){
      var urlFirstName = "Kyc/PANNumber";
        this.params = {
          pan: this.panValue.value,
        }
        this.socketService.post(urlFirstName, this.params).subscribe((responseFirstName: any) => {
          var parts = responseFirstName.result.name.split(" ");
          debugger
          if  (parts.length == 3) {
            this.firstName.setValue(parts[0]);
            this.middleName.setValue(parts[1]);
            this.lastName.setValue(parts[2]);
        } else if (parts.length ==4) {
          this.firstName.setValue(parts[0]);
            this.middleName.setValue(parts[1]);
            this.lastName.setValue(parts[2]);
            this.lastName.setValue(parts[2]);
        } else {
          this.firstName.setValue(parts[0]);
          this.lastName.setValue(parts[1]);
        }
        }
        )
    }
    ngOnInit(): void {
     
      // Getting List from API for Designation
        this.applicationcreation.get("MstApplication360/GetDesignationList").subscribe((result:any)=>{
        this.response=result; 
        this.designations = this.response.designation_list;
        this.filterDesgination = this.designations ;
      });
  
      this.basicFormGroup.controls['searchDesignation'].valueChanges
      .pipe(debounceTime(300))
      .subscribe((value: string) => {
        this.filterDesgination = this.designations.filter(
          (DESC: { designation_type: string }) =>
            DESC.designation_type.toLowerCase().includes(value.toLowerCase())
        );
      });
  
      // Getting List from API for GenderList
      this.applicationcreation.get("MstApplication360/GenderList").subscribe((result:any)=>{
        this.response=result; 
        this.genders = this.response.application_list;
      });
  
      // Getting List from API for MaritalStatusList
      this.applicationcreation.get("MstApplication360/GetMaritalStatusList").subscribe((result:any)=>{
        this.response=result; 
        this.martialStatusArray = this.response.application_list;
      });
  
      // Getting List from API for PhysicalStatusList
      this.applicationcreation.get("MstApplication360/GetPhysicalStatusList").subscribe((result:any)=>{
        this.response=result; 
        this.physicalStatusArray = this.response.physicalstatus_list;
        this.filterphysicalStatus = this.physicalStatusArray;
      });
  
      this.basicFormGroup.controls['searchphysicalStatus'].valueChanges
      .pipe(debounceTime(300))
      .subscribe((value: string) => {
        this.filterphysicalStatus = this.physicalStatusArray.filter(
          (PHYIS: { physicalstatus_name: string }) =>
            PHYIS.physicalstatus_name.toLowerCase().includes(value.toLowerCase())
        );
      });
  
      // Getting List from API for SamunnatiBranchList
      this.applicationcreation.get("MstApplication360/GetSamunnatiBranchList").subscribe((result:any)=>{
        this.response=result; 
        this.nearestSamunnatiBranches = this.response.samunnatibranch_list;
        this.filternearestSamunnatiBranchTypes =  this.nearestSamunnatiBranches
      });
  
      this.basicFormGroup.controls['searchnearestSamunnatiBranch'].valueChanges
  .pipe(debounceTime(300))
  .subscribe((value: string) => {
    this.filternearestSamunnatiBranchTypes = this.nearestSamunnatiBranches.filter(
      (DName: { branch_name: string }) =>
        DName.branch_name.toLowerCase().includes(value.toLowerCase())
    );
  });
  
      // Getting List from API for AddressType
      this.applicationcreation.get("AddressType/GetAddressTypeASC").subscribe((result:any)=>{
        this.response=result; 
        this.addressTypes = this.response.addresstype_list;
  
        this.filteraddressTypes = this.addressTypes
      });
  
  
      this.addressDetailsFormGroup.controls['searchAddressType'].valueChanges
      .pipe(debounceTime(300))
      .subscribe((value: string) => {
        this.filteraddressTypes = this.addressTypes.filter(
          (Adress: { address_type: string }) =>
            Adress.address_type.toLowerCase().includes(value.toLowerCase())
        );
      });
  
      // Getting List from API for EducationalQualificationList
      this.applicationcreation.get("MstApplication360/EducationalQualificationList").subscribe((result:any)=>{
        this.response=result; 
        this.educationalQualifications = this.response.application_list;
        this.filterEducationalQualification = this.educationalQualifications
      });
  
      this.occupationDetailsFormGroup.controls['searcheducationalQualification'].valueChanges
      .pipe(debounceTime(300))
      .subscribe((value: string) => {
        this.filterEducationalQualification = this.educationalQualifications.filter(
          (EDUCATION: { educationalqualification_name: string }) =>
          EDUCATION.educationalqualification_name.toLowerCase().includes(value.toLowerCase())
        );
      });
  
      // Getting List from API for IncomeTypeList 
      this.applicationcreation.get("MstApplication360/IncomeTypeList").subscribe((result:any)=>{
        this.response=result; 
        this.incomeTypes = this.response.application_list;
        this.filterIncomeTypes = this.incomeTypes 
      });
  
      this.occupationDetailsFormGroup.controls['searchIncome'].valueChanges
      .pipe(debounceTime(300))
      .subscribe((value: string) => {
        this.filterIncomeTypes = this.incomeTypes.filter(
          (type: { incometype_name: string }) =>
          type.incometype_name.toLowerCase().includes(value.toLowerCase())
        );
      });
  
      // Getting List from API for OwnershipTypeList
      this.applicationcreation.get("MstApplication360/OwnershipTypeList").subscribe((result:any)=>{
        this.response=result; 
        this.landOwnershipTypes = this.response.application_list;
        this.flterLandOwnershipType = this.landOwnershipTypes
      });
  
      this.occupationDetailsFormGroup.controls['searchlandOwnerShipType'].valueChanges
      .pipe(debounceTime(300))
      .subscribe((value: string) => {
        this.flterLandOwnershipType = this.landOwnershipTypes.filter(
          (type: { ownershiptype_name: string }) =>
          type.ownershiptype_name.toLowerCase().includes(value.toLowerCase())
        );
      });
  
      // Getting List from API for Land in the Name of
      this.applicationcreation.get("MstApplication360/GetPropertyinNameList").subscribe((result:any)=>{
        this.response=result; 
        this.landNames = this.response.application_list;
        this.filterLandsNames = this.landNames
      });
  
  
      this.occupationDetailsFormGroup.controls['searchlandInTheNameOf'].valueChanges
      .pipe(debounceTime(300))
      .subscribe((value: string) => {
        this.filterLandsNames = this.landNames.filter(
          (land: { propertyin_name: string }) =>
          land.propertyin_name.toLowerCase().includes(value.toLowerCase())
        );
      });
  
      // Getting List from API for ResidenceTypeList
      this.applicationcreation.get("MstApplication360/ResidenceTypeList").subscribe((result:any)=>{
        this.response=result; 
        this.residenceTypes = this.response.application_list;
        this.filterResidenceType =  this.residenceTypes;
      });
  
      
      this.occupationDetailsFormGroup.controls['searchresidenceType'].valueChanges
      .pipe(debounceTime(300))
      .subscribe((value: string) => {
        this.filterResidenceType = this.residenceTypes.filter(
          (type: { residencetype_name: string }) =>
          type.residencetype_name.toLowerCase().includes(value.toLowerCase())
        );
      });
    }
  
    agePopulate(dateValue: string) {
      const selectedDate = new Date(dateValue);
      console.log(selectedDate);
    
      const today = new Date();
      console.log('Today:', today);
    
      const diffInYears = today.getFullYear() - selectedDate.getFullYear();
      console.log('Difference in Years:', diffInYears);
    
      const isBeforeBirthday =
      today.getMonth() < selectedDate.getMonth() ||
      (today.getMonth() === selectedDate.getMonth() &&
        today.getDate() < selectedDate.getDate());
    console.log('Is Before Birthday:', isBeforeBirthday);
    
    const age = isBeforeBirthday ? diffInYears - 1 : diffInYears;
    console.log('Calculated Age:', age);
    
    this.familyAge.setValue(age);
    
    }
  
    get Form_60() {
      return this.basicFormGroup.get("Form_60")!;
    }
    setForm_60() {
      this.Form_60.setValue(this.selectedForm_60);
    }
  
  isSelectedForm_60(value: string) {
      return this.selectedForm_60.includes(value);
    }
  
    toggleForm_60(value: string) {
      const index = this.selectedForm_60.indexOf(value);
      if (index === -1) {
        this.selectedForm_60.push(value);
      } else {
        this.selectedForm_60.splice(index, 1);
      }
      this.isForm_60Selected = true;
      this.setForm_60();
    }
  
    removeFormMultiSelect(removedForm_60:string, i:any){
      this.selectedForm_60.splice(i, 1);
      this.setForm_60();
    }
  }
  