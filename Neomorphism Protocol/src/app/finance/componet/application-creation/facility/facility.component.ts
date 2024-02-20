import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CardAnimation } from 'src/app/animation';
import { AppService } from 'src/app/service.service';
import { LoanManagementModel } from '../../../model/loan-management.model';
import * as converter from "number-to-words";
import{ CurrencyPipe} from'@angular/common';
import { UdpCurrencyMaskPipePipe } from 'src/app/shared/pipes/udp-currency-mask-pipe.pipe';
import { SocketService } from 'src/app/shared/services/socket.service';
import { AppComponent } from 'src/app/app.component';
import { UikitComponent } from 'src/app/shared/component/uikit/uikit.component';

interface OverallRecommendationOfLimitDetails
{
  limit:number,
  validityPeriod:number,
  modeYearSelect:boolean,
  modeMonthSelect:boolean,
  modeDaySelect:boolean
}

interface OverallRecommendationOfLimitDetailsTwo
{
  limit:number,
  validityPeriod:number,
  modeYearSelect:boolean,
  modeMonthSelect:boolean,
  modeDaySelect:boolean
}

interface AddFacilityOrProduct
{
  facilityType:string,
  facilityPurpose:string,
  facilityClassification:boolean,
  natureOfTheFacility:boolean,
  validityOfFacility:string,
  rateOfInterest:number,
  penelInterest:number,
  margin:number,
  creditPeriod:string,
  limitDetailsPurpose:string,
  moratorium:boolean,
  moratoriumSelect:string,
  moratoriumStartDate:string,
  moratoriumEndDate:string,
  principleFrequency:string,
  interestFrequency:string,
  interestDeductedUpfront:boolean,
  securityClassification:boolean,


  existingFacilitySelect:boolean,
  securedSelect:boolean,
  unsecuredSelect:boolean,
  facilityLoanAmount:string,


  revolvingSelect:boolean,
  nonRevolvingSelect:boolean,
  nonApplicableSelect:boolean,

  validityTimePeriodYearSelect:boolean,
  validityTimePeriodMonthSelect:boolean,
  validityTimePeriodDaySelect:boolean,

  creditTimePeriodYearSelect:boolean,
  creditTimePeriodMonthSelect:boolean,
  creditTimePeriodDaySelect:boolean,




}


@Component({
  selector: 'app-facility',
  templateUrl: './facility.component.html',
  styleUrls: ['./facility.component.scss'],
  animations : [CardAnimation],
  providers:[UdpCurrencyMaskPipePipe]
})
export class FacilityAndChargesComponent implements OnInit {
  
  withoutCommasValue: any;
  withoutCommasValuelimit: any;
  withoutCommasValueloan: any;
  Comma_separate: any;
  Comma_separate_limit: any;
  year_mon_day:any;
  Amount:any;
  Comma_separate_Loan_Amount: any;
  TypeofLoanflag:boolean=false;
  response:any;
  converter1 = converter;
  overallRecomendationDetails!: FormGroup;
  overallRecomendationDetailsInvalid = false;
  collateralDetails!: FormGroup;
  collateralDetailsInvalid = false;
  hypothecationDetailsInvalid = false;
  overallRecomendationDetailsTwo!: FormGroup;
  remainingAmount:any;

  hypothecationDetails!: FormGroup;
  facilityAndChargesPage! : FormGroup;
  facilityOrProductDetails!: FormGroup;
  facilityOrProductDetailsInvalid=false;
  formsObject!:FormGroup;

  overallDetails:OverallRecommendationOfLimitDetails;
  overallDetailsTwo:OverallRecommendationOfLimitDetailsTwo;
  facilityOrproduct:AddFacilityOrProduct;

  todayDate = new Date();
  
  isProductTypeSelected:boolean = false;
  ProductType:any;
  productSubTypes = [

  ];
  isProductSubTypeSelected:boolean = false;
  ProductSubType:any;
  LoanTypes = [

  ];
  isLoanTypeSelected:boolean = false;
  LoanType:any;
  isFacilityTypeSelected:boolean = false;
  FacilityTypeValue:any;

  isfilterFacilitySelected:boolean=false;
  isFacilitymodeSelected:boolean=false;
  isLoantypeSelected:boolean=false;



  isFacilityPurposeTypeSelected:boolean = false;
  FacilityPurposeValue:any;
  ishypothecationDetailsTitleTypeSelected:boolean = false;
  hypothecationDetailsTitleTypeValue:any;


  isMoratoriumTypesSelected:boolean = false;
  moratoriumValue:any

  isSourceTypesSelected:boolean = false;
  sourceTypeValue:any;

  isSecurityTypeSelected:boolean = false;
  SecurityType:any;
  listproduct: any=[];

  facilityModes = [

  ];
  isFacilityModeSelected:boolean = false;
  FacilityMode:any;



  isPrincipalFrequencySelected:boolean = false;
  PrincipalFrequency:any;

  isInterestFrequencySelected:boolean = false;
  InterestFrequency:any;
  programs = [

  ];
  isProgramSelected:boolean = false;
  Program:any;
  isMoratoriumTypeSelected:boolean = false;
  MoratoriumType:any;
  isProductSelected:boolean = false;
  productDropDown:any;
  product:any;

  productTableData:any = [{
    product:'',
    verity:[]
  }];
  isVeritySelected:boolean = false;
  verityDropDown:any;
  product_name:any;
  varity_select_list : any = []
  productTableInvalid = false;  
  varityIndex:any;
  verities:any;
  addedCharges:any = [];
  chargeObj = {}
  isChargeSelected:boolean = false;
  selectedCharge:any;

  DocTitleDropdown: any;
  isDocTitleSelected: boolean = false;
  docObj = {
    docTitle : null,
    docId : null,
    docName : null
  }
   txtcalculationoveralllimit_validity:any;
  isMoratoriumSelected = 'No';

  addNewRow:any = [
    {
      id:1,
      name: 'Mukil'
    }
  ];
  newObj = {};
  dummy = 1;
/** Paralax card effect */


fragmentIndex:number = 0;
subMenuTitle: string = 'Limit Details';

  isCollateralDocumentUploadTitleSelected = false;
  collateralDocumentUploadTitleValue:any;


  isProductDetailsDataSelected = false;
  productDetailsDataValue:any;


  isProductDetailsVarietySelected = false;
  productDetailsVarietyValue:any;




  collatralDocumentTableData:any = [{
    title:null,
  }]
  collatralDocumentTableDataInvalid:boolean = false;
  collatralDocumentObject:any = {
    title:'',
  }

  hypothecationDocumentTableData:any = [{
    title:null
  }]
  hypothecationDocumentTableDataInvalid:boolean = false;
  hypothecationDocumentObject:any = {}
  addFacilityPage: boolean = false;
  chargesFormGroup!:FormGroup;
  chargesDetailsInvalid: boolean = false;
  overallRecomendationDetailsTwoInvalid: boolean = false;
  cardTitles:any;
  businessActivitiesTableHeader:any;
  products :any;
  product_value:boolean=false;
  verity_value:boolean=false;
  collateralDocumentUploadTitle :any;
  productDetailsData :any;
  productDetailsVariety :any;
  collatralDocumentUploadTableHeader :any;
  hypothecationDocumentUploadTableHeader :any;
  interestFrequencies :any;
  charges :any;
  productTypes :any;
  facilityTypes :any;
  facilityPurposeTypes :any;
  hypothecationDetailsTitleTypes :any;
  moratoriumTypes :any;
  sourceTypes :any;
  securityTypes :any;
  principalFrequencies :any;
  filterTerm: any;
  texts: string[] = ['Year', 'Month', 'Day'];
  currentIndex: number = 0;
  currentValidity: string = 'Year';
  currentPeriod: string = 'Year';
  term: any;
  Loanproductname: any;
  loanproduct_gid:any;
  filterFacility:any;
  Fatype:any;
  Facilitymode:any;
  Famode:any;
  Loantype:any;
  TypeofLoan:any;
  sbu: any;
  category: any;
  verity: any = [];
  sbu_gid: any;
  category_gid: any;
  verity_length : number =0;
  product_length : number | any;
  period: any;
  dayfield: any;
  dayfield1: any;
  dayfield2: any;
  dayfield3: any;
  validperiod: any;
  period1: any;
  period2: any;
  period3: any;
  validityperiod: any;
  amountvalue: any;
  amountvalue2: any;
  remainingAmounts: any;
  overalllimit_amount: any;
  loanfacility_amount: any;
  overalllimit_amount1: any;
  loanfacility_amount1: any;
  Facilitysub_gid: any;
  loan_gid: any;
  PrincipalFrequency_gid: any;
  Interestfrequency_gid: any;
  Comma_separate_Loan_Amount1: any;
  application_gid: any;
  filteredSourceTypes:any;
  application2loan_gid: any;
  security_gid: any;
  TypeofLoannonsecure: any;

  totalCollectable = 0;
  totalDeductable = 0;  

  constructor(public service:AppService,private loanManagementModel:LoanManagementModel, private fB:FormBuilder, public route:ActivatedRoute, public router:Router,
    public socketservice:SocketService,public application:AppComponent, public notify:AppComponent,public cmnfunctionService:UikitComponent ) {
    this.router.navigate( [ 'app/application-creation/Facility & Charges' ], { fragment: this.subMenuTitle } );


  this.cardTitles= loanManagementModel.facilitiesAndChargesCardTitles;
  this.businessActivitiesTableHeader= loanManagementModel.businessActivitiesTableHeader
  // this.products = loanManagementModel.products
  this.verities = loanManagementModel.verities
  this.collateralDocumentUploadTitle = loanManagementModel.collateralDocumentUploadTitle
  this.productDetailsData = loanManagementModel.productDetailsData
  this.productDetailsVariety     = loanManagementModel.productDetailsVariety
  this.collatralDocumentUploadTableHeader     = loanManagementModel.collatralDocumentUploadTableHeader
  this.hypothecationDocumentUploadTableHeader   = loanManagementModel.hypothecationDocumentUploadTableHeader
  //this.interestFrequencies    = loanManagementModel.interestFrequencies
  this.charges   = loanManagementModel.charges
  this.productTypes = loanManagementModel.productTypes
  //this.facilityTypes = loanManagementModel.facilityTypes
  //this.facilityPurposeTypes = loanManagementModel.facilityPurposeTypes
  this.hypothecationDetailsTitleTypes     = loanManagementModel.hypothecationDetailsTitleTypes
   this.moratoriumTypes = loanManagementModel.moratoriumTypes
  this.sourceTypes = loanManagementModel.sourceTypes
  // this.securityTypes = loanManagementModel.securityTypes
  this.filterFacility = loanManagementModel.filterFacility
  this.Facilitymode = loanManagementModel.Facilitymode
  this.filteredSourceTypes = loanManagementModel.filteredSourceTypes

 // this.principalFrequencies = loanManagementModel.principalFrequencies

  // this.moratoriumTypes = [
  //   {level:'Principal'},
  //   {level:'Principal and interest'}
 
  // ]


   loanManagementModel.ApplicationCreationMenu[3].subMenu = [
      {
        subMenuTitle:"Limit Details",
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:"Charges",
        hasError:false,
        completed:false,
      }
    ]
    this.formsObject = fB.group({
      overallRecomendationFormData : this.overallRecomendationDetails,
      overallRecomendationTwoFormData : this.overallRecomendationDetailsTwo,
      collateralDetailsFormData : this.collateralDetails,
      hypothecationDetailsFormData : this.hypothecationDetails,
      addFacilityOrProductFormData : this.facilityOrProductDetails,
    })
    this.overallDetails = { } as OverallRecommendationOfLimitDetails;
    this.overallDetailsTwo = { } as OverallRecommendationOfLimitDetailsTwo;
    this.facilityOrproduct = { } as AddFacilityOrProduct;
    route.fragment.subscribe((fragment:any)=>{
      if(fragment !== null){
        if(fragment == 'Charges'){
          this.addFacilityPage = false;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu = [
            {
              subMenuTitle:"Limit Details",
              hasError:false,
              completed:false,
            },
            {
              subMenuTitle:"Charges",
              hasError:false,
              completed:false,
            }
          ];
          this.cardTitles= this.loanManagementModel.facilitiesAndChargesCardTitles;
        }
        this.subMenuTitle = fragment;
        this.cardTitles;
        this.fragmentIndex = this.cardTitles.indexOf(fragment);
      }
    });
    this.loadOverAllDetailsForm();

  }


   /** Paralax card effect */

   parallaxCardClick(cardFragment:any){
    this.router.navigate( [ 'app/application-creation/Facility & Charges' ], { fragment: cardFragment } );
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

  loadOverAllDetailsForm()
  {
    this.overallRecomendationDetails = new FormGroup({
      limit:new FormControl('', [ Validators.required,Validators.pattern(/^[0-9,]+$/),Validators.maxLength(19)],),
      validityPeriodSelect:new FormControl(this.year_mon_day),
      validityPeriod:new FormControl(null,[Validators.pattern(/^[0-9]*$/)]),
    })

    this.facilityOrProductDetails = new FormGroup({
      facilityType:new FormControl('',[Validators.required,]),
      facilityPurpose:new FormControl('',[Validators.required]),
      facilityClassification:new FormControl('new',[Validators.required,]),
      natureOfTheFacility:new FormControl('secured',[Validators.required,]),
      facilityLoanAmount:new FormControl('',[Validators.required,Validators.pattern(/^[0-9,]+$/),Validators.maxLength(19)]),
      validityOfFacility:new FormControl('',[Validators.required,Validators.pattern(/^[0-9]+$/)]),
      rateOfInterest:new FormControl('',[Validators.required,Validators.pattern(/^[0-9%]+$/),Validators.maxLength(10)]),
      penelInterest:new FormControl('',[Validators.required,Validators.pattern(/^[0-9%]+$/),Validators.maxLength(10)]),
      margin:new FormControl('',[Validators.required,Validators.pattern(/^[0-9%]+$/),Validators.maxLength(10)]),
      creditPeriod:new FormControl('',[Validators.required,Validators.pattern(/^[0-9%]+$/),Validators.maxLength(10)]),
      moratorium:new FormControl('true'),
      moratoriumSelect:new FormControl(''),
      moratoriumStartDate:new FormControl('',[Validators.required,]),
      moratoriumEndDate:new FormControl('',[Validators.required,]),
      principleFrequency:new FormControl(''),
      interestFrequency:new FormControl(''),
      interestDeductedUpfront:new FormControl('yes'),
      securityClassification:new FormControl('secured',[Validators.required,]),
      limitDetailsPurpose:new FormControl('',[Validators.required]),
      productTable: new FormControl(null,[Validators.required])

    })

    this.collateralDetails = new FormGroup({
      sourceType:new FormControl('',[Validators.required]),
     
      guidelineValue:new FormControl(''),
      guidelineValueAssessedOn:new FormControl(''),
      marketValue:new FormControl(''),
      marketValueAssessedOn:new FormControl(''),
      forcedSourceValue:new FormControl(''),
      collateralFSV:new FormControl(''),
      forcedValueAssessedOn:new FormControl(''),
      collateralObservationSummary:new FormControl('',[Validators.pattern(/^[a-zA-Z ]*$/)]),
      collateralDocuments:new FormControl(null,[Validators.required])

    })

    this.hypothecationDetails = new FormGroup({

      securityType:new FormControl('',[Validators.required]),
      securityValue:new FormControl('',[Validators.maxLength(20),Validators.pattern(/^[0-9]+$/)]),
      securityAssessedOn:new FormControl(''),
      assetID:new FormControl('',[Validators.maxLength(20),Validators.pattern(/^[a-zA-Z0-9]+$/)]),
      ROCFillingID:new FormControl('',[Validators.maxLength(20),Validators.pattern(/^[a-zA-Z0-9]+$/)]),
      CERSAIFillingID:new FormControl('',[Validators.maxLength(20),Validators.pattern(/^[a-zA-Z0-9]+$/)]),
      securityDescription:new FormControl('',[Validators.pattern(/^[a-zA-Z0-9]+$/)]),
      hypothecationObservationSummary:new FormControl('',[Validators.pattern(/^[a-zA-Z0-9]+$/)]),
      primarySecurity:new FormControl('',[Validators.pattern(/^[a-zA-Z0-9]+$/)]),
      hypothecationDocuments : new FormControl(null,[Validators.required])
    })

    this.overallRecomendationDetailsTwo = new FormGroup({
      limitTwo:new FormControl('', [
        Validators.required,Validators.maxLength(19),Validators.pattern(/^[\d,]+$/),
      ]),
       validityPeriodTwoSelect:new FormControl(this.validityperiod),
      //this.overallRecomendationDetailsTwo.get("validityPeriodTwoSelect")?.setValue(this.validityperiod);
      validityPeriodTwo:new FormControl('',[Validators.pattern(/^[0-9]*$/)]),

    })

    this.chargesFormGroup = new FormGroup({
      processingFee: new FormControl('collect'),
      processingFeeValue: new FormControl('',[Validators.required,Validators.maxLength(20),Validators.pattern(/^[0-9]*$/)]),
      documentationCharges: new FormControl('collect'),
      documentationChargesValue: new FormControl('',[Validators.required,Validators.maxLength(20),Validators.pattern(/^[0-9]*$/)]),
      fieldVisitCharges: new FormControl('collect'),
      fieldVisitChargesValue: new FormControl('',[Validators.required,Validators.maxLength(20),Validators.pattern(/^[0-9]*$/)]),
      termLifeInsurance: new FormControl('collect'),
      termLifeInsuranceValue: new FormControl('',[Validators.required,Validators.maxLength(20),Validators.pattern(/^[0-9]*$/)]),
      personalAccidentInsurance: new FormControl('collect'),
      personalAccidentInsuranceValue: new FormControl('',[Validators.required,Validators.maxLength(20),Validators.pattern(/^[0-9]*$/)]),
      adhocFee: new FormControl('collect'),
      adhocFeeValue: new FormControl('',[Validators.required,Validators.maxLength(20),Validators.pattern(/^[0-9]*$/)]),
    })


    this.facilityAndChargesPage = this.fB.group({
      overallRecomendationFormDetails : this.overallRecomendationDetails,
      overallRecomendationTwoFormDetails : this.overallRecomendationDetailsTwo,
      collateralDetailsFormDetails : this.collateralDetails,
      hypothecationDetailsFormDetails : this.hypothecationDetails,
      addFacilityOrProductFormDetails : this.facilityOrProductDetails
    });


  }
  get limit() {
    return this.overallRecomendationDetails.get('limit')!;
  }

  get validityPeriodSelect() {
    return this.overallRecomendationDetails.get('validityPeriodSelect')!;
  }

  get validityPeriod() {
    return this.overallRecomendationDetails.get('validityPeriod')!;
  }


  get facilityType(){
    return this.facilityOrProductDetails.get('facilityType')!;
  }

  setFacilityType(){
    return this.facilityType.setValue(this.FacilityTypeValue)!;
  }

  get facilityPurpose(){
    return this.facilityOrProductDetails.get('facilityPurpose')!;
  }

  setFacilityPurpose(){
    return this.facilityPurpose.setValue(this.FacilityPurposeValue);
  }

  get facilityClassification(){
    return this.facilityOrProductDetails.get('facilityClassification')!;
  }

  get natureOfTheFacility(){
    return this.facilityOrProductDetails.get('natureOfTheFacility')!;
  }

  get facilityLoanAmount(){
    return this.facilityOrProductDetails.get('facilityLoanAmount')!;
  }

  get validityOfFacility(){
    return this.facilityOrProductDetails.get('validityOfFacility')!;
  }

  get rateOfInterest(){
    return this.facilityOrProductDetails.get('rateOfInterest')!;
  }

  get penelInterest(){
    return this.facilityOrProductDetails.get('penelInterest')!;
  }

  get margin(){
    return this.facilityOrProductDetails.get('margin')!;
  }

  get creditPeriod(){
    return this.facilityOrProductDetails.get('creditPeriod')!;
  }

  get moratorium(){
    return this.facilityOrProductDetails.get('moratorium')!;
  }

  // get moratoriumSelect(){
  //   return this.facilityOrProductDetails.get('moratoriumSelect')!;
  // }

  // setmoratoriumSelect(){
  //   //return this.moratoriumSelect.setValue(this.moratoriumSelect);
  //   throw new Error('Method not implemented.');
  // }

  get moratoriumStartDate(){
    return this.facilityOrProductDetails.get('moratoriumStartDate')!;
  }

  get moratoriumEndDate(){
    return this.facilityOrProductDetails.get('moratoriumEndDate')!;
  }

  get principleFrequency(){
    return this.facilityOrProductDetails.get('principleFrequency')!;
  }

  setPricipleFrequency(){
    return this.principleFrequency.setValue(this.PrincipalFrequency);
  }

  get interestFrequency(){
    return this.facilityOrProductDetails.get('interestFrequency')!;
  }

  setInterestFrequency(){
    return this.interestFrequency.setValue(this.InterestFrequency);
  }

  get interestDeductedUpfront(){
    return this.facilityOrProductDetails.get('interestDeductedUpfront')!;
  }

  get securityClassification(){
    return this.facilityOrProductDetails.get('securityClassification')!;
  }

  get limitDetailsPurpose(){
    return this.facilityOrProductDetails.get('limitDetailsPurpose')!;
  }

  get productTable(){
    return this.facilityOrProductDetails.get('productTable')!;
  }

  setProductTable(){
    this.productTable.setValue(this.productTableData)
  }
  purposeSetValue(event:any){
    this.limitDetailsPurpose.setValue(event.target.value)
  }
  get sourceType() {
    return this.collateralDetails.get('sourceType')!;
  }

  setSourceType(){
    return this.sourceType.setValue(this.sourceTypeValue)!;
  }

  get guidelineValue() {
    return this.collateralDetails.get('guidelineValue')!;
  }

  get guidelineValueAssessedOn() {
    return this.collateralDetails.get('guidelineValueAssessedOn')!;
  }

  get marketValue() {
    return this.collateralDetails.get('marketValue')!;
  }

  get marketValueAssessedOn() {
    return this.collateralDetails.get('marketValueAssessedOn')!;
  }

  get forcedSourceValue() {
    return this.collateralDetails.get('forcedSourceValue')!;
  }

  get collateralFSV() {
    return this.collateralDetails.get('collateralFSV')!;
  }

  get forcedValueAssessedOn() {
    return this.collateralDetails.get('forcedValueAssessedOn')!;
  }

  get collateralObservationSummary() {
    return this.collateralDetails.get('collateralObservationSummary')!;
  }

  get collateralDocuments(){
    return this.collateralDetails.get('collateralDocuments')!;
  }

  setCollateralDocuments(){
    return this.collateralDocuments.setValue(this.collatralDocumentTableData);
  }

  get securityType(){
    return this.hypothecationDetails.get('securityType')!;
  }

  setSecurityType(){
    this.securityType.setValue(this.SecurityType)!;
  }

  get securityValue(){
    return this.hypothecationDetails.get('securityValue')!;
  }

  get securityAssessedOn(){
    return this.hypothecationDetails.get('securityAssessedOn')!;
  }
  get assetID(){
    return this.hypothecationDetails.get('assetID')!;
  }

  get ROCFillingID(){
    return this.hypothecationDetails.get('ROCFillingID')!;
  }

  get CERSAIFillingID(){
    return this.hypothecationDetails.get('CERSAIFillingID')!;
  }

  get securityDescription(){
    return this.hypothecationDetails.get('securityDescription')!;
  }

  get hypothecationObservationSummary(){
    return this.hypothecationDetails.get('hypothecationObservationSummary')!;
  }

  get primarySecurity(){
    return this.hypothecationDetails.get('primarySecurity')!;
  }

  get hypothecationDocuments(){
    return this.hypothecationDetails.get('hypothecationDocuments')!;
  }

  setHypothecationDocuments(){
    this.hypothecationDocuments.setValue(this.hypothecationDocumentTableData);
  }


  get limitTwo() {
    return this.overallRecomendationDetailsTwo.get('limitTwo')!;
  }

  get validityPeriodTwoSelect() {
    return this.overallRecomendationDetailsTwo.get('validityPeriodTwoSelect')!;
  }

  get validityPeriodTwo() {
    return this.overallRecomendationDetailsTwo.get('validityPeriodTwo')!;
  }

  get processingFee(){
    return this.chargesFormGroup.get('processingFee')!;
  }
  get processingFeeValue(){
    return this.chargesFormGroup.get('processingFeeValue')!;
  }
  get documentationCharges(){
    return this.chargesFormGroup.get('documentationCharges')!;
  }
  get documentationChargesValue(){
    return this.chargesFormGroup.get('documentationChargesValue')!;
  }
  get fieldVisitCharges(){
    return this.chargesFormGroup.get('fieldVisitCharges')!;
  }
  get fieldVisitChargesValue(){
    return this.chargesFormGroup.get('fieldVisitChargesValue')!;
  }
  get termLifeInsurance(){
    return this.chargesFormGroup.get('termLifeInsurance')!;
  }
  get termLifeInsuranceValue(){
    return this.chargesFormGroup.get('termLifeInsuranceValue')!;
  }
  get personalAccidentInsurance(){
    return this.chargesFormGroup.get('personalAccidentInsurance')!;
  }
  get personalAccidentInsuranceValue(){
    return this.chargesFormGroup.get('personalAccidentInsuranceValue')!;
  }
  get adhocFee(){
    return this.chargesFormGroup.get('adhocFee')!;
  }
  get adhocFeeValue(){
    return this.chargesFormGroup.get('adhocFeeValue')!;
  }

  onInputChanged(value: any, rowIndex: number, propertyKey: string): void {
    const newValue = this.productTableData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.productTableData = newValue;
    this.setProductTable();

}
onInputChangedCollatralDocument(value: any, rowIndex: number, propertyKey: string): void {
  const newValue = this.collatralDocumentTableData.map((row: any, index: number) => {
    return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
  })
  this.collatralDocumentTableData = newValue;
  this.setCollateralDocuments();

}
onInputChangedHypothecationDocument(value: any, rowIndex: number, propertyKey: string): void {
  const newValue = this.hypothecationDocumentTableData.map((row: any, index: number) => {
    return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
  })
  this.hypothecationDocumentTableData = newValue;

  this.setHypothecationDocuments();

}
productname_change(cboproduct_name:any) {
  this.product = cboproduct_name;
  this.product_name =  cboproduct_name.product_name;
  var params = {
      product_gid: cboproduct_name.product_gid
  }
  var url = 'MstApplicationAdd/GetSectorcategory';
  this.socketservice.getparams(url, params).subscribe((result:any)=> {
    this.sbu_gid = result.businessunit_gid;
    this.sbu = result.businessunit_name;
    this.category_gid = result.valuechain_gid;
    this.category = result.valuechain_name;
    this.verities = result.varietyname_list;
  });
  this.product_value = false;
}
verityname_change(cboverity_name:any){
  this.varity_select_list.variety_gid = cboverity_name.varity_gid;
  this.varity_select_list.variety_name = cboverity_name.variety_name;
  let obj = {
    variety_gid: cboverity_name.variety_gid,
    variety_name: cboverity_name.variety_name,
  };
  this.verity.push(obj);
  this.verity_length = this.verity_length + 1;
  this.verity_value = false;
}
removeSelectVariety(cboverity_name:any){
  this.verity.splice(cboverity_name);
  this.verity_length = this.verity_length - 1;
}
  get timePeriodTwo() {
    return this.overallRecomendationDetailsTwo.get('timePeriod')!;
  }
  get modeYearSelect() {
    return this.overallRecomendationDetails.get('modeYearSelect')!;
  }
  get modeYearSelectTwo() {
    return this.overallRecomendationDetailsTwo.get('modeYearSelect')!;
  }

  get modeMonthSelect() {
    return this.overallRecomendationDetails.get('modeMonthSelect')!;
  }
  get modeMonthSelectTwo() {
    return this.overallRecomendationDetailsTwo.get('modeMonthSelect')!;
  }

  get modeDaySelect() {
    return this.overallRecomendationDetails.get('modeDaySelect')!;
  }
  get modeDaySelectTwo() {
    return this.overallRecomendationDetailsTwo.get('modeDaySelect')!;
  }


  get existingFacilitySelect(){
    return this.facilityOrProductDetails.get('existingFacilitySelect')!;
  }

  get securedSelect(){
    return this.facilityOrProductDetails.get('securedSelect')!;
  }

  get unsecuredSelect(){
    return this.facilityOrProductDetails.get('unsecuredSelect')!;
  }

  get revolvingSelect(){
    return this.facilityOrProductDetails.get('revolvingSelect')!;
  }

  get nonRevolvingSelect(){
    return this.facilityOrProductDetails.get('nonRevolvingSelect')!;
  }

  get nonApplicableSelect(){
    return this.facilityOrProductDetails.get('nonApplicableSelect')!;
  }
  get validityTimePeriodYearSelect(){
    return this.facilityOrProductDetails.get('validityTimePeriodYearSelect')!;
  }

  get validityTimePeriodMonthSelect(){
    return this.facilityOrProductDetails.get('validityTimePeriodMonthSelect')!;
  }

  get validityTimePeriodDaySelect(){
    return this.facilityOrProductDetails.get('validityTimePeriodDaySelect')!;
  }

  get creditTimePeriodYearSelect(){
    return this.facilityOrProductDetails.get('creditTimePeriodYearSelect')!;
  }

  get creditTimePeriodMonthSelect(){
    return this.facilityOrProductDetails.get('creditTimePeriodMonthSelect')!;
  }

  get creditTimePeriodDaySelect(){
    return this.facilityOrProductDetails.get('creditTimePeriodDaySelect')!;
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

  selectFacilityType(FacilityTypeValue:any){
    this.isFacilityTypeSelected = true;
    this.FacilityTypeValue = FacilityTypeValue.loanproduct_name;
    this.setFacilityType();
   
  }
  selectLoantype(Loantype:any){
    this.isLoantypeSelected = true;
    this.TypeofLoan = Loantype.loan_type;
    this.loan_gid=Loantype.loantype_gid;
    if( this.TypeofLoan=='Secured'){
      this.TypeofLoanflag=true;
    }
    else{
      this.TypeofLoanflag=false;
    }
    // this.setFacilityType();
   
  }
  selectFaType(filterFacility:any){
    
    this.isfilterFacilitySelected = true;
    this.Fatype = filterFacility;
    // this.setFacilityType();
   
  }
  selectFaMode(Facilitymode:any){
    
    this.isFacilitymodeSelected = true;
    this.Famode = Facilitymode;
    // this.setFacilityType();
   
  }

  selectFacilityPurpose(FacilityPurposeValue:any)
  {
    this.isFacilityPurposeTypeSelected = true;
    this.FacilityPurposeValue = FacilityPurposeValue.loansubproduct_name;
    this.Facilitysub_gid=FacilityPurposeValue.loansubproduct_gid;
    this.setFacilityPurpose();
  }

  selectSourceType(sType:any)
  {
    this.isSourceTypesSelected = true;
    this.sourceTypeValue = sType;
    this.setSourceType();
  }
  



  selectPrincipalFrequency(PrincipalFrequency:any){

    this.isPrincipalFrequencySelected = true;
    this.PrincipalFrequency = PrincipalFrequency.principalfrequency_name;
    this.PrincipalFrequency_gid=PrincipalFrequency.principalfrequency_gid;

    this.setPricipleFrequency();
  }
  selectInterestFrequency(InterestFrequency:any){
    this.isInterestFrequencySelected = true;
    this.InterestFrequency = InterestFrequency.interestfrequency_name;
    this.Interestfrequency_gid=InterestFrequency.interestfrequency_gid;
    this.setInterestFrequency();
  }
  selectProgram(f:any){
    this.isProgramSelected = true;
    this.Program = f;
  }
  selectMoratoriumType(moratoriumTypes:any){
    
    this.moratoriumValue = moratoriumTypes;
    this.isMoratoriumTypesSelected = true;
    // this.setmoratoriumSelect();
  }

  selectVerity(verity:string,i:number, j:number){
    this.isVeritySelected = true;
    this.verityDropDown = verity;

      this.productTableData[i].verity.push(verity);
  }
  removeFormMultiSelectVariety(removedLanguage:string, i:any){

    this.productTableData[i].verity.splice(i,1);
    this.setProductTable()
  }
  selectProduct( product:string, i?:number){
    this.isProductSelected = true;
    this.productDropDown = product;
    if(i !== undefined){
      this.productTableData[i].product = product;
    }
    this.setProductTable();

  }

  selectCollateralDocumentUploadTitle( title:string, i:number){
      this.collatralDocumentTableData[i].title = title;
      this.setCollateralDocuments();
  }
  selectHypothecationDocumentUploadTitle( title:string, i:number){
      this.hypothecationDocumentTableData[i].title = title;
      this.setHypothecationDocuments();
  }
  addProduct(){
    this.productTableData.forEach((element:any) => {
      if(element.product == null || element.verity == null || element.sbu == null || element.category == null ){
        this.productTableInvalid = true;

      }else{
        this.productTableInvalid = false;
      }
    });

    if(!this.productTableInvalid ){
      this.productTableData.push(
        {
          product:'',
          verity:[]
        }
      )
    }
  };

  addCollatralDocumentTableRow(){
    this.collatralDocumentTableData.forEach((element:any) => {
      if(element.title == null){
        this.collatralDocumentTableDataInvalid = true;

      }else{
        this.collatralDocumentTableDataInvalid = false;
      }
    });

    if(!this.collatralDocumentTableDataInvalid ){
      this.collatralDocumentTableData.push({
        title:null,
      })
    }
  }

  addHypothecationDocumentTableRow(){
    this.hypothecationDocumentTableData.forEach((element:any) => {
      if(element.title == null){
        this.hypothecationDocumentTableDataInvalid = true;

      }else{
        this.hypothecationDocumentTableDataInvalid = false;
      }
    });

    if(!this.hypothecationDocumentTableDataInvalid ){
      this.hypothecationDocumentTableData.push({
        title:null
      })
    }
  }

  deleteProductTableData(i:number){
    this.productTableData.splice(i,1);
    this.setProductTable();
  }

  deleteCollatralTableData(i:number){
    this.collatralDocumentTableData.splice(i,1);
    this.setCollateralDocuments();
  }

  deleteHypothecationTableData(i:number){
    this.hypothecationDocumentTableData.splice(i,1);
    this.setHypothecationDocuments();
  }


  selectSecurityType(p:any){
    this.isSecurityTypeSelected = true;
    this.SecurityType = p.security_type;
    this.security_gid=p.securitytype_gid;
    this.setSecurityType()
  }



  trackByFn(index:any) {
    return index; // or item.id
  }
  saveDraft(){
  }
  // product based subproduct
  OnchangeFacilitytype(cboFacilityTypeValue:any) {
    this.isFacilityPurposeTypeSelected=false;
    
    this.loanproduct_gid=cboFacilityTypeValue.loanproduct_gid;

    var params = {
      loanproduct_gid: cboFacilityTypeValue.loanproduct_gid,
      application_gid:'',
      application2loan_gid: ''
      
    }
    
   // var url = 'api/MstApplicationAdd/GetLoanSubProduct';
    var url = 'MstApplicationAdd/GetLoanSubProduct';
    this.socketservice.getparams(url, params).subscribe((result:any)=> {
      this.facilityPurposeTypes =result.application_list;
   });
  }
  MoratoriumChange(event:any)
  {
    if(event.target.checked)
    {
      this.isMoratoriumSelected = 'Yes';
    }
    else
    {
      this.isMoratoriumSelected = 'No';
      this.moratoriumValue='';
    }
  }

  addNew()
  {

    this.addNewRow.push(this.newObj);
    this.dummy++;
  }

  delThis(i:any)
  {
    this.addNewRow.splice(i,1);
  }

  delRow(){
    this.addNewRow.pop(this.newObj);
  }



  getFieldValidationClass() {
    return this.validityOfFacility.invalid && (this.validityOfFacility.dirty || this.validityOfFacility.touched) ? 'errorFeedBack' : '';
  }

  addFacility(){

    if(this.Comma_separate==null||this.Comma_separate==undefined ){
     
      this.notify.showToastMessage('warning','Kindly Enter Limit');
      return;
    }
    if(  this.validityPeriodTwo.value==null||  this.validityPeriodTwo.value==undefined||this.validityPeriodTwo.value=='' ){
      this.notify.showToastMessage('warning','Kindly Enter Validity Period');
      return;
    }

  else {

    let overallamount = parseInt(this.Comma_separate.replace(/[\s,]+/g, '').trim());
    let lsloanfacility_amount=0;
    if (overallamount < lsloanfacility_amount) {
      this.notify.showToastMessage('Amount Exceeded from entered loan facility amount', 'warning');
  }
  if(this.validityPeriodTwoSelect.value=='years'){
   var  txtvalidityoveralllimit_year = this.validityPeriodTwo.value;
   txtvalidityoveralllimit_day=0;
   txtvalidityoveralllimit_month=0;
  this.txtcalculationoveralllimit_validity =(txtvalidityoveralllimit_year+'-Year,'+txtvalidityoveralllimit_month+'-Month,'+txtvalidityoveralllimit_day+'-Day');
  }
 else  if(this.validityPeriodTwoSelect.value=='months'){
    var  txtvalidityoveralllimit_month = this.validityPeriodTwo.value;
    txtvalidityoveralllimit_year=0;
    txtvalidityoveralllimit_day=0;
 this.txtcalculationoveralllimit_validity=(txtvalidityoveralllimit_year+'-Year,'+txtvalidityoveralllimit_month+'-Month,'+txtvalidityoveralllimit_day+'-Day');
   }
  else  if(this.validityPeriodTwoSelect.value=='days'){
    var  txtvalidityoveralllimit_day = this.validityPeriodTwo.value;
    txtvalidityoveralllimit_month=0;
    txtvalidityoveralllimit_year=0;
 this.txtcalculationoveralllimit_validity=(txtvalidityoveralllimit_year+'-Year,'+txtvalidityoveralllimit_month+'-Month,'+txtvalidityoveralllimit_day+'-Day');
     }

   
    var params = {
     overalllimit_amount: overallamount,
    validityoveralllimit_year: txtvalidityoveralllimit_year,
    validityoveralllimit_month: txtvalidityoveralllimit_month,
     validityoveralllimit_days: txtvalidityoveralllimit_day,
     calculationoveralllimit_validity:this.txtcalculationoveralllimit_validity,
     application_gid:this.application_gid,
   csa_applicability:'',
    csaactivity_gid: '',
     csaactivity_name: '',
     percentageoftotal_limit:''
    }
    var url = 'MstNgProductChargesAddEdit/SubmitOverallLimit';
    //lockUI();
    this.socketservice.post(url, params).subscribe((result:any)=>{
      this.response=result; 
      //unlockUI();
      if (result.status == true) {
      
        this.notify.showToastMessage('success',result.message);
      }
      else {
        this.notify.showToastMessage('warning',result.message);
      }
    });
    this.addFacilityPage = true;
    this.TypeofLoanflag=false;
    this.validityPeriodTwoSelect.value;  
  this.product_length=0;
    if(this.product_length === 0){
      this.product_value = this.verity_value = true;
    }
    this.isFacilityTypeSelected = false;
    this.isLoantypeSelected = false;
   this.isfilterFacilitySelected = false;
  this.isFacilitymodeSelected = false;
  this.isFacilityPurposeTypeSelected = false;
   this.isPrincipalFrequencySelected = false;
    this.isInterestFrequencySelected = false;
    this.isMoratoriumSelected = 'Yes';      
    this.isMoratoriumTypesSelected=false;
    this.product_value = false;
    this.verity_value = false;
    this.socketservice.get("MstApplicationAdd/GetproductDropDown").subscribe((result:any)=>{
      this.response=result; 
      debugger;
      this.facilityTypes = this.response.loanproductlist;  
      this.principalFrequencies = this.response.principalfrequencylist;  
      this.interestFrequencies = this.response.interestfrequencylist; 
      this.Loantype = this.response.loantypelist;  
  
  
    });
    this.socketservice.get('MstNgApplicationAdd/GetTempProduct').subscribe((result:any)=>{
      this.application.uiunlock();
    });
  
    this.cardTitles = [
      'Limit Details','Collateral Details','Hypothecation Details'
    ];
    this.isLoantypeSelected = false;
    // this.Comma_separate='';
    // this.validityPeriodTwo==null;
    // this.validityPeriodTwo.value=='';
    var param = {
      application_gid: this.application_gid,
    }
    var url = 'MstNgProductChargesAddEdit/GetProductChargesEdit';
    //lockUI();
    this.socketservice.getparams(url, param).subscribe((result:any)=>{
      this.response=result; 
      //unlockUI();
      if (result.status == true) {
      this.Comma_separate_limit = this.response.overalllimit_amount;  
      if (this.Comma_separate_limit) {
        this.withoutCommasValuelimit = this.removeCommas(this.Comma_separate_limit);
     
        this.Comma_separate_limit = this.formatCurrencyIndianLimit(this.Comma_separate_limit);
        this.amountvalue2=this.convertToIndianStandard(this.withoutCommasValuelimit)
      }
      this.year_mon_day = this.response.lsyearmonthday;  
      this.period1 = this.response.validityoveralllimit_year; 
      this.period2 = this.response.validityoveralllimit_month; 
      this.period3 = this.response.validityoveralllimit_days;    
      if( this.period1!="0"){
        this.validperiod=this.response.validityoveralllimit_year;
       // this.overallRecomendationDetailsTwo.get('validityPeriodTwo')?.setValue(this.response.validityoveralllimit_year);
      } 
      else if(this.period2!="0"){
        this.validperiod=this.response.validityoveralllimit_month; 
      }
      else if(this.period3 !="0"){
        this.validperiod=this.response.validityoveralllimit_days; 
      }
      }
      
    });
   
 
}
   
  }

  nextButton(){

    if(this.addFacilityPage){

      
      if(this.subMenuTitle == 'Limit Details'){
       //this.product = cboproduct_name;
      //this.product_name =  cboproduct_name.product_name;
      var params = {
         //product_gid: cboproduct_name.product_gid
        application_gid: this.application_gid
      }
      var url = 'MstNgProductChargesAddEdit/GetEditLimit';
      this.socketservice.getparams(url, params).subscribe((result:any)=> {
        this.overalllimit_amount1 = result.overalllimit_amount;
        this.loanfacility_amount1 = result.loanfacility_amount;
      
        if( this.loanfacility_amount1==null||  this.loanfacility_amount1==undefined||  this.loanfacility_amount1==''||this.loanfacility_amount1==""){
          this.loanfacility_amount=="0";
        }
        else{
          this.loanfacility_amount = this.removeCommas(this.loanfacility_amount1);
        }
        this.overalllimit_amount = this.removeCommas(this.overalllimit_amount1);
        if((this.Comma_separate_Loan_Amount)>(this.overalllimit_amount-this.loanfacility_amount)){
          this.notify.showToastMessage('warning','Amount Exceeded from Overall Limit');
          return;
        }
        if(this.Comma_separate_Loan_Amount=="0"||this.Comma_separate_Loan_Amount=='0'){
          this.notify.showToastMessage('warning','Loan Facility Amount should not be 0');
          return;
       }
       else{
        // if(this.Comma_separate_Loan_Amount==null||this.Comma_separate_Loan_Amount==''){
         if(this.Comma_separate_Loan_Amount==null||this.Comma_separate_Loan_Amount==''||this.FacilityTypeValue==null||this.FacilityTypeValue==''||
        this.FacilityPurposeValue==null||this.FacilityPurposeValue==''||this.Fatype==null||this.Fatype==''||this.Famode==null||this.Famode==''||
         this.validityOfFacility==null||this.validityOfFacility.value==''||this.validityOfFacility.value==null||this.rateOfInterest.value==null||this.rateOfInterest.value==''||
         this.penelInterest.value==''||this.penelInterest.value==null||this.margin.value==''||this.margin.value==null||this.creditPeriod.value==''||
         this.creditPeriod.value==null){
          this.notify.showToastMessage('warning','Kindly fill all mandatory values');
        }
        else{
          if(this.isMoratoriumSelected=='Yes'){
            if(this.moratoriumValue==null||this.moratoriumValue==''||this.moratoriumStartDate.value==null
            ||this.moratoriumStartDate.value==''||this.moratoriumEndDate.value==null||this.moratoriumEndDate.value==''){

            }
            else{
              // var lsloanproduct_name = '';
              // var lsloanproduct_gid = '';
              // if(this.FacilityTypeValue!=null||this.FacilityTypeValue!=''){

              // }
             // const form_values = this.facilityOrProductDetails;
             if(this.currentValidity=='Year'){
             var lsvalidityOfFacilityyear= this.validityOfFacility.value;
             lsvalidityOfFacilitymonth=0;
             lsvalidityOfFacilityday=0;
             }
             else if(this.currentValidity=='Month'){
              var lsvalidityOfFacilitymonth= this.validityOfFacility.value;
              lsvalidityOfFacilityday=0;
              lsvalidityOfFacilityyear=0;
             }
             else if(this.currentValidity=='Day'){
              var lsvalidityOfFacilityday= this.validityOfFacility.value;
              lsvalidityOfFacilitymonth=0;
              lsvalidityOfFacilityyear=0;
             }
             if( this.currentPeriod=='Year'){
             var lscreditPeriodyear=this.creditPeriod.value;
             lscreditPeriodmonth=0;
             lscreditPeriodday=0;
             }
             else if( this.currentPeriod=='Month'){
              var lscreditPeriodmonth=this.creditPeriod.value;
              lscreditPeriodday=0;
              lscreditPeriodyear=0;
             }
             else if( this.currentPeriod=='Day'){
              var lscreditPeriodday=this.creditPeriod.value;
              lscreditPeriodmonth=0;
              lscreditPeriodyear=0;
             }
             this.Comma_separate_Loan_Amount1 = this.removeCommas(this.Comma_separate_Loan_Amount);
              var params={
                application_gid: this.application_gid,
                product_type: this.FacilityTypeValue,
                producttype_gid: this.loanproduct_gid,
                productsub_type:this.FacilityPurposeValue,
                productsubtype_gid:this.Facilitysub_gid,
                facility_type:this.Fatype,
                facility_mode: this.Famode,
                facilityloan_amount:this.Comma_separate_Loan_Amount1,
                facilityvalidity_year: lsvalidityOfFacilityyear,
                facilityvalidity_month: lsvalidityOfFacilitymonth,
                facilityvalidity_days:lsvalidityOfFacilityday,
                rate_interest: this.rateOfInterest.value,
                margin:this.margin.value,
                penal_interest: this.penelInterest.value,
                tenureproduct_year: lscreditPeriodyear,
                tenureproduct_month: lscreditPeriodmonth,
                tenureproduct_days: lscreditPeriodday,
                moratorium_status:this.isMoratoriumSelected,
                moratorium_type:this.moratoriumValue,
                moratorium_startdate :this.moratoriumStartDate.value,
                moratorium_enddate:this.moratoriumEndDate.value,
                principalfrequency_name :this.PrincipalFrequency,
                principalfrequency_gid:this.PrincipalFrequency_gid,
                interestfrequency_gid:this.Interestfrequency_gid,
                interestfrequency_name:this.InterestFrequency,
                interest_status :this.interestDeductedUpfront.value,
                enduse_purpose:this.limitDetailsPurpose.value,
                loantype_gid: this.loan_gid,
                loan_type: this.TypeofLoan,
                facilityrequested_date: '',                               
                program: '',
                program_gid: '',                      
             
                source_type: '',
                guideline_value: '',
                guideline_date: '',
                market_value: '',
                marketvalue_date:'',
                forcedsource_value: '',
                collateralSSV_value: '',
                forcedvalueassessed_on: '',
                collateralobservation_summary: '',                    
               
                product_gid: '',
                product_name: '',
                variety_gid: '',
                variety_name: '',
                sector_name:'',
                category_name: '',
                botanical_name:'',
                alternative_name:''
                
                //rate_interest:
              }
              var url = 'MstNgProductChargesAddEdit/PostLoanDtl';
              this.socketservice.post(url, params).subscribe((result:any)=> {
                if (result.status == true) {
      
                  this.notify.showToastMessage('success',result.message);
                
                    this.parallaxCardClick(this.secondCard());
                    this.overallRecomendationDetailsInvalid = false;
                    this.facilityOrProductDetailsInvalid = false;
                    this.loanManagementModel.ApplicationCreationMenu[3].subMenu[0].completed = true;
                    this.application2loan_gid=result.application2loan_gid;
                    if(result.loan_type=='Secured'){
                      this.TypeofLoannonsecure=true;
                      //this.isSourceTypesSelected=true;
                    }
                    else{
                      this.TypeofLoannonsecure=false;
                      // this.isSourceTypesSelected=false;
                    }
                    //this.filteredSourceTypes = loanManagementModel.filteredSourceTypes
                }
                else {
                  this.notify.showToastMessage('warning',result.message);
                }
              });
             // console.log(form_values.value.loanproduct_gid);
            }
          }
          else{
            // var lsloanproduct_name = '';
            // var lsloanproduct_gid = '';
            // if(this.FacilityTypeValue!=null||this.FacilityTypeValue!=''){

            // }
           // const form_values = this.facilityOrProductDetails;
           if(this.currentValidity=='Year'){
           var lsvalidityOfFacilityyear= this.validityOfFacility.value;
           lsvalidityOfFacilitymonth=0;
           lsvalidityOfFacilityday=0;
           }
           else if(this.currentValidity=='Month'){
            var lsvalidityOfFacilitymonth= this.validityOfFacility.value;
            lsvalidityOfFacilityday=0;
            lsvalidityOfFacilityyear=0;
           }
           else if(this.currentValidity=='Day'){
            var lsvalidityOfFacilityday= this.validityOfFacility.value;
            lsvalidityOfFacilitymonth=0;
            lsvalidityOfFacilityyear=0;
           }
           if( this.currentPeriod=='Year'){
           var lscreditPeriodyear=this.creditPeriod.value;
           lscreditPeriodmonth=0;
           lscreditPeriodday=0;
           }
           else if( this.currentPeriod=='Month'){
            var lscreditPeriodmonth=this.creditPeriod.value;
            lscreditPeriodday=0;
            lscreditPeriodyear=0;
           }
           else if( this.currentPeriod=='Day'){
            var lscreditPeriodday=this.creditPeriod.value;
            lscreditPeriodmonth=0;
            lscreditPeriodyear=0;
           }
          
            var params={
              application_gid: this.application_gid,
              product_type: this.FacilityTypeValue,
              producttype_gid: this.loanproduct_gid,
              productsub_type:this.FacilityPurposeValue,
              productsubtype_gid:this.Facilitysub_gid,
              facility_type:this.Fatype,
              facility_mode: this.Famode,
              facilityloan_amount: this.Comma_separate_Loan_Amount1,
              facilityvalidity_year: lsvalidityOfFacilityyear,
              facilityvalidity_month: lsvalidityOfFacilitymonth,
              facilityvalidity_days:lsvalidityOfFacilityday,
              rate_interest: this.rateOfInterest.value,
              margin:this.margin.value,
              penal_interest: this.penelInterest.value,
              tenureproduct_year: lscreditPeriodyear,
              tenureproduct_month: lscreditPeriodmonth,
              tenureproduct_days: lscreditPeriodday,
              moratorium_status:this.isMoratoriumSelected,
              moratorium_type:this.moratoriumValue,
              moratorium_startdate :this.moratoriumStartDate.value,
              moratorium_enddate:this.moratoriumEndDate.value,
              principalfrequency_name :this.PrincipalFrequency,
              principalfrequency_gid:this.PrincipalFrequency_gid,
              interestfrequency_gid:this.Interestfrequency_gid,
              interestfrequency_name:this.InterestFrequency,
              interest_status :this.interestDeductedUpfront.value,
              enduse_purpose:this.limitDetailsPurpose.value,
              loantype_gid: this.loan_gid,
              loan_type: this.TypeofLoan,
              facilityrequested_date:'',                               
              program: '',
              program_gid: '',                      
           
              source_type: '',
              guideline_value: '',
              guideline_date: '',
              market_value: '',
              marketvalue_date:'',
              forcedsource_value: '',
              collateralSSV_value: '',
              forcedvalueassessed_on: '',
              collateralobservation_summary: '',                    
             
              product_gid: '',
              product_name: '',
              variety_gid: '',
              variety_name: '',
              sector_name:'',
              category_name: '',
              botanical_name:'',
              alternative_name:''
              
              //rate_interest:
            }
            var url = 'MstNgProductChargesAddEdit/PostLoanDtl';
            this.socketservice.post(url, params).subscribe((result:any)=> {
              if (result.status == true) {
    
                this.notify.showToastMessage('success','Limit Details Added Successfully');
           
          this.parallaxCardClick(this.secondCard());
          this.overallRecomendationDetailsInvalid = false;
          this.facilityOrProductDetailsInvalid = false;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[0].completed = true;
       

              }
              else {
                this.notify.showToastMessage('warning','Error Occurred While Adding Limit Details');
              }
            });
           // console.log(form_values.value.loanproduct_gid);
          }
        }
       }
       
      });

        if(this.overallRecomendationDetails.invalid || this.facilityOrProductDetails.invalid){
          this.overallRecomendationDetailsInvalid = true;
          this.facilityOrProductDetailsInvalid = true;
        
        }else{
          this.parallaxCardClick(this.secondCard());
          this.overallRecomendationDetailsInvalid = false;
          this.facilityOrProductDetailsInvalid = false;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[0].completed = true;
        }


      }

      if(this.subMenuTitle == 'Collateral Details'){
        if(this.TypeofLoan.value=='Secured'){
          this.TypeofLoannonsecure=false;
          var param ={
            application2loan_gid :this.application2loan_gid,          
            source_type: this.sourceTypeValue,
            guideline_value: this.guidelineValue.value,
            guideline_date: this.guidelineValueAssessedOn.value,
            market_value: this.marketValue.value,
            marketvalue_date:this.marketValueAssessedOn.value,
            forcedsource_value: this.forcedSourceValue.value,
            collateralSSV_value: this.collateralFSV.value,
            forcedvalueassessed_on: this.forcedValueAssessedOn.value,
            collateralobservation_summary:this.collateralObservationSummary.value,  
            application_gid: this.application_gid
          }
          var url = 'MstNgProductChargesAddEdit/PostLoanDtlCollateral';
          this.socketservice.post(url, param).subscribe((result:any)=> {
            if (result.status == true) {
  
              this.notify.showToastMessage('success',result.message);
            
              this.collateralDetailsInvalid = false;
              this.parallaxCardClick(this.secondCard());
              this.loanManagementModel.ApplicationCreationMenu[3].subMenu[1].completed = true;
              this.socketservice.get("MstApplicationAdd/GetproductDropDown").subscribe((result:any)=>{
                this.response=result; 
               
                this.securityTypes = this.response.securitytype_list;
              
              });
                //this.filteredSourceTypes = loanManagementModel.filteredSourceTypes
            }
            else {
              this.notify.showToastMessage('warning',result.message);
            }
          });
        }
       else{
        this.TypeofLoannonsecure=true;
        this.collateralDetailsInvalid = false;
              this.parallaxCardClick(this.secondCard());
              this.loanManagementModel.ApplicationCreationMenu[3].subMenu[1].completed = true;
              this.socketservice.get("MstApplicationAdd/GetproductDropDown").subscribe((result:any)=>{
                this.response=result; 
               
                this.securityTypes = this.response.securitytype_list;
              
              });
       }
        if(this.collateralDetails.invalid){
          this.collateralDetailsInvalid = true;
        }else{
          this.collateralDetailsInvalid = false;
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[1].completed = true;
        }
      }


    }else{

      if(this.subMenuTitle == 'Limit Details'){

        if(this.overallRecomendationDetailsTwo.invalid){
          this.overallRecomendationDetailsTwoInvalid = true;
          // this.socketservice.get("MstApplicationAdd/GetproductDropDown").subscribe((result:any)=>{
          //   this.response=result; 
          //   debugger;
          //   this.facilityTypes = this.response.loanproductlist;    
          // });
        }else{
          this.overallRecomendationDetailsTwoInvalid = false;
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[0].completed = true;
        }

      }

    }
  }

  nextSection(){
    if(this.subMenuTitle == 'Charges'){
      if(this.chargesFormGroup.invalid){
        this.chargesDetailsInvalid = true;
      }else{
        this.chargesDetailsInvalid = false;

        if(this.overallRecomendationDetailsTwo.valid && this.chargesFormGroup.valid){
          this.router.navigateByUrl('app/application-creation/Documents');
        }else{
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[0].hasError = this.overallRecomendationDetailsTwo.invalid;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[1].hasError = this.chargesFormGroup.invalid;
        }
        this.loanManagementModel.ApplicationCreationMenu[3].subMenu[1].completed = true;
      }
    }

    if(this.subMenuTitle == 'Hypothecation Details'){

      var params={
        securitytype_gid:this.security_gid,
        security_type: this.securityType.value,
        security_description: this.securityDescription.value,
        security_value: this.securityValue.value,
        securityassessed_date: this.securityAssessedOn.value,
        asset_id: this.assetID.value,
        roc_fillingid: this.ROCFillingID.value,
        CERSAI_fillingid:this.CERSAIFillingID.value,
        hypoobservation_summary: this.hypothecationObservationSummary.value,
        primary_security: this.primarySecurity.value,
        application_gid: this.application_gid
      }
      var url = 'MstNgProductChargesAddEdit/PostHypothecation';
      this.socketservice.post(url, params).subscribe((result:any)=> {
        if (result.status == true) {

          this.notify.showToastMessage('success',result.message);
        
          this.hypothecationDetailsInvalid = false;
        this.parallaxCardClick(this.secondCard());
        if(this.overallRecomendationDetails.valid && this.collateralDetails.valid && this.hypothecationDetails.valid){
          this.addFacilityPage = false;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu = [
           
            {
              subMenuTitle:"Charges",
              hasError:false,
              completed:false,
            }
          ];
          this.cardTitles= this.loanManagementModel.facilitiesAndChargesCardTitles;
        }else{
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[0].hasError = this.overallRecomendationDetails.invalid;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[1].hasError = this.collateralDetails.invalid;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[2].hasError = this.hypothecationDetails.invalid;
        }
        this.loanManagementModel.ApplicationCreationMenu[3].subMenu[2].completed = true;
            //this.filteredSourceTypes = loanManagementModel.filteredSourceTypes
        }
        else {
          this.notify.showToastMessage('warning',result.message);
        }
      });
      if(this.hypothecationDetails.invalid){
        this.hypothecationDetailsInvalid = true;
      }else{
        this.hypothecationDetailsInvalid = false;
        this.parallaxCardClick(this.secondCard());
        if(this.overallRecomendationDetails.valid && this.collateralDetails.valid && this.hypothecationDetails.valid){
          this.addFacilityPage = false;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu = [
            {
              subMenuTitle:"Limit Details",
              hasError:false,
              completed:false,
            },
            {
              subMenuTitle:"Charges",
              hasError:false,
              completed:false,
            }
          ];
          this.cardTitles= this.loanManagementModel.facilitiesAndChargesCardTitles;
        }else{
          this.parallaxCardClick(this.secondCard());
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[0].hasError = this.overallRecomendationDetails.invalid;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[1].hasError = this.collateralDetails.invalid;
          this.loanManagementModel.ApplicationCreationMenu[3].subMenu[2].hasError = this.hypothecationDetails.invalid;
        }
        this.loanManagementModel.ApplicationCreationMenu[3].subMenu[2].completed = true;
      }
    }
  }

  backButton(){

    if(this.addFacilityPage){
      if(this.subMenuTitle == 'Limit Details'){
        var param = {
          application_gid: this.application_gid,
        }
        var url = 'MstNgProductChargesAddEdit/GetProductChargesEdit';
        //lockUI();
        this.socketservice.getparams(url, param).subscribe((result:any)=>{
          this.response=result; 
          //unlockUI();
          if (result.status == true) {
          this.Comma_separate = this.response.overalllimit_amount; 
          if (this.Comma_separate) {
            this.withoutCommasValuelimit = this.removeCommas(this.Comma_separate);
         
            this.Comma_separate = this.formatCurrencyIndianLimit(this.Comma_separate);
            this.amountvalue=this.convertToIndianStandard(this.withoutCommasValuelimit)
          }
          this.validityperiod = this.response.lsyearmonthday;  
          if(this.validityperiod=="year"){
            this.validityperiod='years';
            this.overallRecomendationDetailsTwo.get("validityPeriodTwoSelect")?.setValue(this.validityperiod);
    
          }
        else if(this.validityperiod=="month"){
        this.validityperiod='months';
        this.overallRecomendationDetailsTwo.get("validityPeriodTwoSelect")?.setValue(this.validityperiod);
       }
       else if(this.validityperiod=="day"){
        this.validityperiod='days';
        this.overallRecomendationDetailsTwo.get("validityPeriodTwoSelect")?.setValue(this.validityperiod);
       }
          this.dayfield1 = this.response.validityoveralllimit_year; 
    
          this.dayfield2 = this.response.validityoveralllimit_month; 
          this.dayfield3= this.response.validityoveralllimit_days;
          if( this.dayfield1!="0") {
            this.dayfield = this.response.validityoveralllimit_year; 
            
          }  
          else if (this.dayfield2!="0") {
            this.dayfield = this.response.validityoveralllimit_month; 
          
          } 
          else if( this.dayfield3!="0"){
            this.dayfield= this.response.validityoveralllimit_days;
          }
         
          }
          
        });
    
        this.addFacilityPage = false;
        this.loanManagementModel.ApplicationCreationMenu[3].subMenu = [
          {
            subMenuTitle:"Limit Details",
            hasError:false,
            completed:false,
          },
          {
            subMenuTitle:"Charges",
            hasError:false,
            completed:false,
          }
        ];
        this.cardTitles= this.loanManagementModel.facilitiesAndChargesCardTitles;
        this.Comma_separate='';
        this.validityPeriodTwo==null;
        this.validityPeriodTwo.value=='';
      }
    }
}

addnewProduct(){
  if(this.product === '' || this.product === null || this.product === undefined){
    this.product_value = true;
    if(this.verity.length === 0){
      this.verity_value = true;
    }
  }
  else if(this.verity.length === 0){
    this.verity_value = true;
  }
  else{
    var params = {
      product_gid: this.product.product_gid,
      product_name: this.product.product_name,
      variety_list: this.verity,
      sector_name: this.sbu,
      category_name: this.category
    }
    var url = 'MstNgApplicationAdd/PostProduct';
    //lockUI();
    this.socketservice.post(url, params).subscribe((result:any)=>{
      this.response=result; 
      //unlockUI();
      if (result.status == true) {
        this.productdetaillist();
        this.notify.showToastMessage('success',result.message);
      }
      else {
        this.notify.showToastMessage('warning',result.message);
      }
    });
}
  
  this.product =  null;
  this.product_name = null;
  this.sbu = this.category = '-'
  this.verity = [];
  this.verity_length = 0;
}

productdetaillist(){
  var url = 'MstNgApplicationAdd/GetProductDetails';
  //lockUI();
  this.socketservice.get(url).subscribe((result:any)=>{
    this.listproduct = result.mstproduct_list;
    if(this.listproduct == null){
      this.product_length = 0;
    }
    else{
      this.product_length=this.listproduct.length;
    }
   
  });
  return;
}
product_delete(application2product_gid: any) {
  this.application.uilock();
  var params ={
    application2product_gid: application2product_gid
  }
  var url = 'MstNgApplicationAdd/DeleteProductDetail';
  this.socketservice.getparams(url, params).subscribe((result:any)=> {
    this.application.uiunlock();
    if(result.status == true){
      this.productdetaillist();
      this.notify.showToastMessage('success',result.message);
    }
    else{
      this.notify.showToastMessage('warning',result.message);
    }
    
  });
}

addserviceCharges() {    

  this.totalCollectable = parseInt(this.processingFeeValue.value) + parseInt(this.documentationChargesValue.value) + parseInt(this.fieldVisitChargesValue.value) + parseInt(this.adhocFeeValue.value) + parseInt(this.termLifeInsuranceValue.value) + parseInt(this.personalAccidentInsuranceValue.value);

  this.totalDeductable = parseInt(this.processingFeeValue.value) + parseInt(this.documentationChargesValue.value) + parseInt(this.fieldVisitChargesValue.value) + parseInt(this.adhocFeeValue.value) + parseInt(this.termLifeInsuranceValue.value) + parseInt(this.personalAccidentInsuranceValue.value); 


  var params = {
    application_gid: this.application_gid,
    processing_fee: this.processingFee.value,
    processing_collectiontype: this.processingFeeValue.value,
    doc_charges: this.documentationCharges.value,
    doccharge_collectiontype: this.documentationChargesValue.value,
    fieldvisit_charge: this.fieldVisitCharges.value,
    fieldvisit_collectiontype: this.fieldVisitChargesValue.value,
    adhoc_fee: this.adhocFee.value,
    adhoc_collectiontype: this.adhocFeeValue.value,
    life_insurance: this.termLifeInsurance.value,
    lifeinsurance_collectiontype: this.termLifeInsuranceValue.value,
    acct_insurance: this.personalAccidentInsurance.value,
    acctinsurance_collectiontype: this.personalAccidentInsuranceValue.value,
    total_collect: this.totalCollectable,
    total_deduct: this.totalDeductable,
  }

  var url = 'MstNgProductChargesAddEdit/PostServiceCharges';

  this.socketservice.post(url, params).subscribe((result: any) => {
    if (result.status == true) {
      this.notify.showToastMessage('success', result.message);
      this.processingFeeValue.reset();
      this.documentationChargesValue.reset();
      this.fieldVisitChargesValue.reset();
      this.adhocFeeValue.reset();
      this.termLifeInsuranceValue.reset();
      this.personalAccidentInsuranceValue.reset();
      this.totalCollectable = 0;
      this.totalDeductable = 0;
    }
    else {
      this.notify.showToastMessage('warning', result.message);
    }
  });
} 


  convertToWords(){

  }

  ngOnInit() {
    // this.route.queryParams.subscribe(params => {

    //   const hash = params['hash'];  

    //   if (hash) {

    //     const searchObject = this.cmnfunctionService.decryptURL(hash);        

    //     this.application_gid = searchObject.lsapplication_gid;

    //     }

    //   });

    

    this.route.fragment.subscribe((fragment:any)=>{
      if(fragment !== null){
        this.subMenuTitle = fragment;
      }
    });
    this.socketservice.get("MstApplicationAdd/GetproductDropDown").subscribe((result:any)=>{
      this.response=result; 
      debugger;
      this.facilityTypes = this.response.loanproductlist;  
      this.principalFrequencies = this.response.principalfrequencylist;  
      this.interestFrequencies = this.response.interestfrequencylist; 
      this.Loantype = this.response.loantypelist;  
 

    });
    this.socketservice.get("MstApplicationAdd/GetDropDown").subscribe((result:any)=>{
      this.response=result; 
     
      this.products = this.response.productname_list;
    
    });
    var param = {
      application_gid: this.application_gid,
    }
    var url = 'MstNgProductChargesAddEdit/GetProductChargesEdit';
    //lockUI();
    this.socketservice.getparams(url, param).subscribe((result:any)=>{
      this.response=result; 
      //unlockUI();
      if (result.status == true) {
      this.Comma_separate = this.response.overalllimit_amount; 
      if (this.Comma_separate) {
        this.withoutCommasValuelimit = this.removeCommas(this.Comma_separate);
     
        this.Comma_separate = this.formatCurrencyIndianLimit(this.Comma_separate);
        this.amountvalue=this.convertToIndianStandard(this.withoutCommasValuelimit)
      }
      this.validityperiod = this.response.lsyearmonthday;  
      if(this.validityperiod=="year"){
        this.validityperiod='years';
        this.overallRecomendationDetailsTwo.get("validityPeriodTwoSelect")?.setValue(this.validityperiod);

      }
    else if(this.validityperiod=="month"){
    this.validityperiod='months';
    this.overallRecomendationDetailsTwo.get("validityPeriodTwoSelect")?.setValue(this.validityperiod);
   }
   else if(this.validityperiod=="day"){
    this.validityperiod='days';
    this.overallRecomendationDetailsTwo.get("validityPeriodTwoSelect")?.setValue(this.validityperiod);
   }
      this.dayfield1 = this.response.validityoveralllimit_year; 

      this.dayfield2 = this.response.validityoveralllimit_month; 
      this.dayfield3= this.response.validityoveralllimit_days;
      if( this.dayfield1!="0") {
        this.dayfield = this.response.validityoveralllimit_year; 
        
      }  
      else if (this.dayfield2!="0") {
        this.dayfield = this.response.validityoveralllimit_month; 
      
      } 
      else if( this.dayfield3!="0"){
        this.dayfield= this.response.validityoveralllimit_days;
      }
     
      }
      
    });



  }

  ChangeValidityValue() {
    switch (this.currentValidity) {
      case 'Year':
        this.currentValidity = 'Month';
        this.validityOfFacility.setValidators([Validators.required, Validators.min(1), Validators.max(9999)]);
        break;
      case 'Month':
        this.currentValidity = 'Day';
        this.validityOfFacility.setValidators([Validators.required, Validators.min(1), Validators.max(9999)]);
        break;
      case 'Day':
        this.currentValidity = 'Year';
        this.validityOfFacility.setValidators([Validators.required, Validators.min(1), Validators.max(9999)]);
        break;
    }
    this.validityOfFacility.updateValueAndValidity();
  }

  ChangePeriodValue() {
    switch (this.currentPeriod) {
      case 'Year':
        this.currentPeriod = 'Month';
        this.creditPeriod.setValidators([Validators.required, Validators.min(1), Validators.max(9999)]);
        break;
      case 'Month':
        this.currentPeriod = 'Day';
        this.creditPeriod.setValidators([Validators.required, Validators.min(1), Validators.max(9999)]);
        break;
      case 'Day':
        this.currentPeriod = 'Year';
        this.creditPeriod.setValidators([Validators.required, Validators.min(1), Validators.max(9999)]);
        break;
    }
    this.creditPeriod.updateValueAndValidity();
  }

  numberCheck (args: { key: string; }) {
    if (args.key === 'e' || args.key === '+' || args.key === '-') {
      return false;
    } else {
      return true;
    }}
  
CommaFormatted(event: { which: number; }) {
     // skip for arrow keys
     if (event.which >= 37 && event.which <= 40) return;
     if (this.Comma_separate) {
       this.withoutCommasValue = this.removeCommas(this.Comma_separate);
       this.Comma_separate = this.formatCurrencyIndian(this.Comma_separate);
     }
   }

 formatCurrencyIndian(number: any) {
     const formattedNumber = number.toString().replace(/\D/g, "");
     const commaFormattedNumber = formattedNumber.replace(/(\d{2})(?=\d{2})/g, "$1,");
     return commaFormattedNumber;
   }

CommaFormattedlimit(event: { which: number; }) {
    if (event.which >= 37 && event.which <= 40) return;

    // format number
    if (this.Comma_separate_limit) {
      this.withoutCommasValuelimit = this.removeCommas(this.Comma_separate_limit);
   
      this.Comma_separate_limit = this.formatCurrencyIndianLimit(this.Comma_separate_limit);
    }
  }
 
   formatCurrencyIndianLimit(number: any) {
     const formattedNumber = number.toString().replace(/\D/g, "");
     const commaFormattedNumber = formattedNumber.replace(/(\d{2})(?=\d{2})/g, "$1,");
     return commaFormattedNumber;
   }
 
   CommaFormattedLoanAmount(event: { which: number; }) {
    if (event.which >= 37 && event.which <= 40) return;

    // format number
    if (this.Comma_separate_Loan_Amount) {
      this.withoutCommasValueloan = this.removeCommas(this.Comma_separate_Loan_Amount);
      

      this.Comma_separate_Loan_Amount = this.formatCurrencyIndianLoanAmount(this.Comma_separate_Loan_Amount);
      this.remainingAmounts=this.RemainingAmount(this.withoutCommasValueloan);
    }
  }
 
   removeCommas(value: string) {
     return value.replace(/,/g, "");
   }
 
   formatCurrencyIndianLoanAmount(number: any) {
     const formattedNumber = number.toString().replace(/\D/g, "");
     const commaFormattedNumber = formattedNumber.replace(/(\d{2})(?=\d{2})/g, "$1,");
     return commaFormattedNumber;
   }
 
   RemainingAmount(number:any){
    this.withoutCommasValuelimit = this.removeCommas(this.Comma_separate_limit);
  this.remainingAmount=( this.withoutCommasValuelimit-this.withoutCommasValueloan)
   }
 
   updateShowValue(event: any) {
     this.Comma_separate_Loan_Amount = event.target.value;
   }
 
   // finize
   convertToIndianStandard(number: number): string {
     const units = ['', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen'];
     const tens = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
 
     if (number === 0) {
       return 'Zero';
     }
 
     let words = '';
 
     if (number < 20) {
       words = units[number];
     } else if (number < 100) {
       words = tens[Math.floor(number / 10)] + ' ' + units[number % 10];
     } else if (number < 1000) {
       words = units[Math.floor(number / 100)] + ' Hundred ' + this.convertToIndianStandard(number % 100);
     } else if (number < 100000) {
       words = this.convertToIndianStandard(Math.floor(number / 1000)) + ' Thousand ' + this.convertToIndianStandard(number % 1000);
     } else if (number < 10000000) {
       words = this.convertToIndianStandard(Math.floor(number / 100000)) + ' Lakh ' + this.convertToIndianStandard(number % 100000);
     } else if (number < 1000000000) {
       words = this.convertToIndianStandard(Math.floor(number / 10000000)) + ' Crore ' + this.convertToIndianStandard(number % 10000000);
     } else if (number < 100000000000) {
       words = this.convertToIndianStandard(Math.floor(number / 1000000000)) + ' Hundred Crore ' + this.convertToIndianStandard(number % 1000000000);
     } else if (number < 10000000000000) {
       words = this.convertToIndianStandard(Math.floor(number / 100000000000)) + ' Thousand Crore ' + this.convertToIndianStandard(number % 100000000000);
     }
 
     return words.trim();
   }



}
