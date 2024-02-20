import { Component, EventEmitter, OnInit, Output, VERSION, Renderer2, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoanManagementModel } from 'src/app/finance/model/loan-management.model';
import { TextInputValidator } from 'src/app/shared/validators/customFormValidators';
import { ApplicationCreationService } from 'src/app/finance/services/application-creation.service';
import { SocketService } from 'src/app/shared/services/socket.service';
import { AppComponent } from 'src/app/app.component';
import { UikitComponent } from 'src/app/shared/component/uikit/uikit.component';
import { debounceTime } from 'rxjs/operators';

interface IGeneralDetails {
  vertical_gid: any;
  applicantName : string,
  vertical : string,
  program : string,
  AppFrmSA : string,
  SAM_Associate_Id_Or_Name:string,
  creditGroup:string,
  vernacularLanguage : Array<any>,
  productTableForm: FormGroup <any>
}
interface ISPoCDetails {
  firstName:String,
  middleName:String,
  lastName:String,
  designation:String,
  mobWtSts:boolean,
  mobileNumber:Number,
  mailID:String,
}
interface IBusinessActivities{
  businessDescription: string,
  productTable : Array<any>
}

@Component({
  selector: 'app-general-info',
  templateUrl: './general-info.component.html',
  styleUrls: ['./general-info.component.scss']
})

export class GeneralInfoComponent implements OnInit {
[x: string]: any;
  @Output() childItemEvent = new EventEmitter<any>();
  @Output() generalDetailsFormEvent = new EventEmitter<any>();
  newobj= {};
  VersionName = VERSION.major;
  generalForm!: FormGroup;
  generalInfoDetails!: FormGroup;
  SPOCDetails!: FormGroup;
  businessActivities!:FormGroup;
  formsObject = {
  }
  generalDetails:IGeneralDetails;
  businessActivity:IBusinessActivities;
  formSubmitted : boolean = false;
  formInvalid:boolean = false;
  generalDetailsFormInvalid:boolean = false;
  SPOCDetailsFormInvalid:boolean = false;
  BusinessActivityFormInvalid:boolean = false;
  subMenuTitle: string = 'General-Details';

  wtStsChecked:boolean = false;
  searchVertical:any;
  searchcreditGroup:any;
  verticalDropdown: any ;
  isVerticalSelected: boolean = false;
  verticals:any;
  isProgramSelected:boolean = false;
  programDropdown:any;
  searchProgram:any;
  programs:any;
  isCreditGroupSelected:boolean = false;
  creditGroupDropdown:any;
  searchCreditGroup:any;
  creditGroups:any;
  isLanguageSelected:boolean = false;
  languageDropdown:any= [];
  searchLanguages:any;
  languages:any;
  languagesBackUp:any;
  isSAMdropDownSelected:boolean = false;
  samDropdown:any;
  searchSamNames:any;
  samNames:any;
  isProductSelected:boolean = false;
  productDropDown:any;
  products :any;
  productTableData:any = [
    {
      product:'',
      verity: []
    }
  ];
  product_name:any;
  varity_select_list : any = []
  productTableInvalid = false;
  isVeritySelected:boolean = false;
  verityDropDown:any;
  varityIndex:any;
  verities:any;
  isDesignationSelected:boolean = false;
  designationDropDown:any;
  searchDesignation:any;
  designations:any;
  isContactTypeSelected:boolean = false;
  contactsTypeDropDown:any;
  contactsType:any;


  isSASelected:boolean = false;
  isSASelectedStatus = true;
  close:boolean = false;
  tableData:any;
  appMenuArr:any = [];
  checked:string = '';
  subMenuArray:any = [];
  subMenuIndex:number = 0;
  title2:string = 'SPOC Details';
  title3:string = 'Business Activity';
  cardTitles:any;
  fragmentIndex:number = 0;
  mainMenu:any=[];
  formValue:any;

  businessActivitiesTableHeader:any;
  response:any;
  txtsector_name: any;
  product: any;
  sbu: any;
  category: any;
  verity: any = [];
  sbu_gid: any;
  category_gid: any;
  productTableFormgroup: any;

  //Added needed file for new table 
  _form!: FormGroup;

  _genders = ['Male', 'Female']
  listproduct: any=[];
  listvalue: any=[];
  productTableForm: any;
  n = 1;
  verity_length : number =0;
  product_length : number | any;
  vernacular_language : any=[];
  product_value:boolean=false;
  verity_value:boolean=false;
  creditgroup_gid:any;
  verticalname_gid: any | string;
samassociate_gid: any;
  term:any;
  filterverticalTypes: { vertical_name: string }[] = []; 
  filterCreditFlow : { creditgroup_name: string }[] = [];
  filterSamNames : { name: string }[] = [];
  filterProgram : { program_name: string}[] = [];
  filterLanguage : { vernacular_language: string}[] = [];
  constructor(public loanManagementModel:LoanManagementModel, private fB:FormBuilder, public route:ActivatedRoute, 
  public router:Router, public socketservice:SocketService,public application:AppComponent, public notify:AppComponent,
  public renderer:Renderer2, public cmnFunctionService:UikitComponent) 
  {
    this.listproduct=[];
    this._buildForm();
    this.formsObject = {
      generalDetailsForm : this.generalInfoDetails,
      businessActivitiesFrom : this.businessActivities,
      
    }
    this.mainMenu = loanManagementModel.ApplicationCreationMenu;
    this.generalDetails = {} as IGeneralDetails;
    this.businessActivity = {} as IBusinessActivities;
    
    //this.verticals = loanManagementModel.verticals;
    //this.programs = loanManagementModel.programs;
    this.creditGroups = loanManagementModel.creditGroups;
// this.languagesBackUp = loanManagementModel.languages;
//this.samNames = loanManagementModel.samNames;
//this.products = loanManagementModel.products;
this.verities = loanManagementModel.verities;
this.designations = loanManagementModel.designations;
this.contactsType = loanManagementModel.contactsType;
this.cardTitles = loanManagementModel.generalInfoCardTitles;
this.businessActivitiesTableHeader = loanManagementModel.businessActivitiesTableHeader;

    route.fragment.subscribe((fragment:any)=>{
      if(fragment !== null){
        this.subMenuTitle = fragment;
        this.generalForm;
        this.subMenuArray = this.mainMenu[0].subMenu;
        this.subMenuIndex = this.subMenuArray.indexOf(this.subMenuTitle);
        this.cardTitles;
        this.fragmentIndex = this.cardTitles.indexOf(fragment);
      }
    });
    this.loadForm();
}

loadForm(){
  this.generalInfoDetails = new FormGroup({
    applicantName: new FormControl('', [Validators.required,Validators.pattern(/^[a-zA-Z0-9\s]+$/)]),
    vertical : new FormControl('', [Validators.required]),
    searchvertical : new FormControl(''),
    searchcreditGroup: new FormControl(''),
    searchSamNames: new FormControl(''),
    searchProgram: new FormControl(''),
    searchLanguages: new FormControl(''),
    vertical_gid : new FormControl(),
    program_gid : new FormControl(),
    program : new FormControl('', [Validators.required]),
    AppFrmSA : new FormControl(true, [Validators.required]),
    SAM_Associate_Id_Or_Name : new FormControl('',[this.isSASelectedStatus ? Validators.required : Validators.nullValidator]),
    creditGroup : new FormControl(this.generalDetails.creditGroup, [Validators.required]),
    vernacularLanguage : new FormControl(this.generalDetails.vernacularLanguage, [Validators.required]),
    description: new FormControl('', [Validators.required,Validators.maxLength(500),]),
    //productTable: new FormControl([],[Validators.required]),
    creditgroup_gid : new FormControl(),
  })
  this.productTableFormgroup = new FormGroup({})

  this.generalForm = this.fB.group({
    generalInfoDetails : this.generalInfoDetails,
    businessActivities : this.businessActivities
  });
}
  get applicantName() {
    return this.generalInfoDetails.get('applicantName')!;
  }
  get vertical() {
    return this.generalInfoDetails.get('vertical')!;
  }
  setVertical(){
    return this.vertical.setValue(this.verticalDropdown)!;
  }
  get verticalfn_gid() {
    return this.generalInfoDetails.get('vertical_gid')!;
  }
  //setVertical_gid(){
  //  return this.verticalfn_gid.setValue(this.verticalname_gid)!;
 // }
  get program() {
    return this.generalInfoDetails.get('program')!;
  }
  setProgram(){
    return this.program.setValue(this.programDropdown)!;
  }
  get AppFrmSA() {
    return this.generalInfoDetails.get('AppFrmSA')!;
  }
  get SAM_Associate_Id_Or_Name() {
    return this.generalInfoDetails.get('SAM_Associate_Id_Or_Name')!;
  }
  setSAM_Associate_Id_Or_Name(){
    return this.SAM_Associate_Id_Or_Name.setValue(this.samDropdown)!;
  }
  get creditGroup() {
    return this.generalInfoDetails.get('creditGroup')!;
  }
  setCreditGroup(){
    return this.creditGroup.setValue(this.creditGroupDropdown)!;
  }
  get vernacularLanguage() {
    return this.generalInfoDetails.get('vernacularLanguage')!;
  }
  setVernacularLanguage(language: any,i?:any){
    this.languageDropdown.push(language);
    this.filterLanguage.splice(i,1);
    this.generalForm.value.generalInfoDetails.vernacularLanguage = this.languageDropdown;
    this.vernacularLanguage.setValue(this.languageDropdown);
  }
  
  
  get businessDescription(){
    return this.generalInfoDetails.get('businessDescription');
  }
  get productTable(){
    return this.generalInfoDetails.get('productTable')!;
  }
  setProductTable(){
      return this.productTable.setValue(this.productTableData)
  }


inputCustomValidation(control: FormControl){
  const pattern:RegExp = /^[a-zA-Z ]*$/;
  if(!pattern.test(control.value)){
    return { patternError: true };
  }else{
    return null;
  }

}

  
  selectVertical(vertical:any){
      this.verticalDropdown = vertical.vertical_name;
      this.isVerticalSelected = true;
      this.setVertical();
      try{
        this.childItemEvent.emit(this.verticalDropdown);
      }catch(e){
        
      };
      //this.vertical_gid.setValue(this.verticalDropdown)!;
      //this.generalInfoDetails.setValue('SAM_Associate_Id_Or_Name')!;
  }
  selectProgram(program:any){
    this.isProgramSelected = true;
    this.programDropdown = program.program_name;
    this.setProgram();
    this.generalInfoDetails.get('program_gid')?.setValue(program.program_gid);
  }
  selectCreditGroup(creditGroup:any){
    this.isCreditGroupSelected = true;
    this.creditGroupDropdown = creditGroup.creditgroup_name;
    this.setCreditGroup();
    this.creditgroup_gid=creditGroup.creditmapping_gid;
  }

  selectLanguage(language: any, i: number) {
    this.isLanguageSelected = true;
   // this.vernacular_language.push(language);
    this.filterLanguage.splice(i, 1);
    // Update languages array to remove the selected language from the dropdown
    this.languages = this.languages.filter((lang:any) => lang.vernacular_language !== language.vernacular_language);
let obj = {
      vernacularlanguage_gid: language.vernacularlanguage_gid,
      vernacular_language: language.vernacular_language,
    };
    this.vernacular_language.push(obj);  }
 


  saChange(event: any) {
    if (event.target.checked) {
      this.SAM_Associate_Id_Or_Name.setValidators(Validators.required);
      this.SAM_Associate_Id_Or_Name.updateValueAndValidity();
      this.isSASelectedStatus=true;
    } else {
      this.SAM_Associate_Id_Or_Name.clearValidators();
      this.SAM_Associate_Id_Or_Name.updateValueAndValidity();
      this.isSASelectedStatus=false;
      this.SAM_Associate_Id_Or_Name.setValue(null)!;
      this.isSAMdropDownSelected = false;
    }
  }

  selectSamName(samName:any){
    this.isSAMdropDownSelected = true;
    this.samDropdown = samName.name;
    this.samassociate_gid = samName.associatemaster_gid;
    this.setSAM_Associate_Id_Or_Name();
  }
  selectProduct( product:string, i?:number){
    this.isProductSelected = true;
    this.productDropDown = product;
    if(i !== undefined){
      this.productTableData[i].product =product;
    }
    this.setProductTable();
  }
  selectVerity(verity:string,i:number, j:number){
    this.isVeritySelected = true;
    this.verityDropDown = verity;
    
      this.productTableData[i].verity.push(verity);
      this.setProductTable()
  }

  removeFormMultiSelectVariety(removedLanguage:string, i:any){
    
    this.productTableData[i].verity.splice(i,1);
    this.setProductTable()
  }

  onInputChanged(value: any, rowIndex: number, propertyKey: string): void {
      const newValue = this.productTableData.map((row: any, index: number) => {
        return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
      })
      this.productTableData = newValue;
      this.setProductTable();
  }

  removeVeraity(p:string, i:number, k:number)
  {
    this.productTableData[i].verity.splice(k,1);
    this.verities.push(p);
  }


  selectDesignation(designation:any){
    this.isDesignationSelected = true;
    this.designationDropDown = designation;
  }
  selectContactType(type:any){
    this.isContactTypeSelected = true;
    this.contactsTypeDropDown = type;
  }
  trackByFn(index:any) {
    return index; // or item.id
  }
  
  addProduct(product:any){
    
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
  deleteProductTableData(i:number){
    this.productTableData.splice(i,1);
    this.setProductTable();
  }


  nextButton(){
    this.generalInfoDetails.get('vernacularLanguage')?.setValue(this.vernacular_language);
    if(this.generalInfoDetails.invalid,this.product_length === 0){
      this.product_value = this.verity_value = true;
      this.generalDetailsFormInvalid = true;
      return;
    }
   
    else if(this.listproduct==null){
      this.notify.showToastMessage('warning','Enter Product Details');
      return;
    }
    
    else
    {
      this.generalDetailsFormInvalid = false;
      
      const form_values = this.generalInfoDetails;
      
      var params = {
        customer_name:  form_values.value.applicantName,
        vertical_gid: this.verticalname_gid,
        vertical_name: form_values.value.vertical,
        program_gid: form_values.value.program_gid,
        program_name: form_values.value.program,
        sa_status: form_values.value.AppFrmSA,
        saname_gid: this.samassociate_gid,
        sa_name: form_values.value.SAM_Associate_Id_Or_Name,
        vernacularlanguage_list: this.vernacular_language,
        creditgroup_gid: this.creditgroup_gid,
        creditgroup_name: form_values.value.creditGroup,
        business_activities : form_values.value.description,
      }
      
      var url = 'MstNgApplicationAdd/SubmitGeneralDtl';
      this.application.uilock();
      debugger
      this.socketservice.post(url, params).subscribe((result:any)=>{
        
        if(result.status == true){
          this.notify.showToastMessage('success',result.message);
        const application_gid = (result.application_gid);

    const encryptedParameter = this.cmnFunctionService.encryptURL('lsapplication_gid=' + application_gid);

    const url = 'app/application-creation/Borrower Details?hash=' + encryptedParameter;
    this.loanManagementModel.application_gid = application_gid;
   

   

    this.router.navigateByUrl(url);
          // this.router.navigateByUrl('app/application-creation/Borrower Details#Institution?lsapplication_gid='+ result.application_gid);
          
        }
        else{
          this.notify.showToastMessage('warning',result.message);
        }
      });
    }
  }

  saveDraft(){

  }

  removeFormMultiSelect(removedLanguage: any, i: number) {
    this.languages.push({ vernacular_language: removedLanguage.vernacular_language });
    this.vernacular_language.splice(i, 1);
	this.vernacularLanguage.setValue(this.languageDropdown)
  }



  // Onselect Vertical send vertical gid and get program list
  OnchangeVertical(cbovertical:any) {
	this.verticalname_gid=null;
    this.verticalname_gid=cbovertical.vertical_gid;
    var params = {
        vertical_gid: cbovertical.vertical_gid,
        lstype: '',
        lstypegid: ''
    }
    var url = 'SystemMaster/GetVerticalProgramList';
    this.application.uilock();
    this.socketservice.getparams(url, params).subscribe((result:any)=> {
       this.programs =result.program_list;
       this.filterProgram = this.programs
    });
    this.application.uiunlock();

    this.generalInfoDetails.controls['searchProgram'].valueChanges
    .pipe(debounceTime(300))
    .subscribe((value: string) => {
      this.filterProgram = this.programs.filter(
        (PROG: { program_name: string }) =>
          PROG.program_name.toLowerCase().includes(value.toLowerCase())
      );
    });

  }

  // Onselect product send vertical gid and get variety list, sector name, category name
  productname_change(cboproduct_name:any) {
    debugger
    this.product = cboproduct_name;
    this.product_name =  cboproduct_name.product_name;
    var params = {
        product_gid: cboproduct_name.product_gid
    }
    var url = 'MstApplicationAdd/GetSectorcategory';
    this.application.uilock();
    this.socketservice.getparams(url, params).subscribe((result:any)=> {
      this.sbu_gid = result.businessunit_gid;
      this.sbu = result.businessunit_name;
      this.category_gid = result.valuechain_gid;
      this.category = result.valuechain_name;
      this.verities = result.varietyname_list;
    });
    this.verity = [];
    this.verity_length = 0;
    this.product_value = false;
    this.application.uiunlock();
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
  // New Table add function
  _addRow() {
    const formArray: FormArray = this._form.get("data") as FormArray;

    const form = this.fB.group({
      name: ["", Validators.required],
      surname: ["", Validators.required],
      gender: [null, Validators.required]
    });

    formArray.push(form);
  }

  //New Table delete function
  _removeRow(index: number) {
    const formArray: FormArray = this._form.get("data") as FormArray;

    formArray.removeAt(index);
  }

  _buildForm() {
    this._form = this.fB.group({
      data: this.fB.array([])
    });
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
      var url = 'MstNgApplicationAdd/PostProductDetailAdd';
      this.application.uilock();
      this.socketservice.post(url, params).subscribe((result:any)=>{
        this.response=result; 
        this.application.uiunlock();
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
    this.application.uilock();
    var url = 'MstNgApplicationAdd/GetProductDetailList';
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
    this.application.uiunlock();
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

  //set verties
  selectveriety(verties:any,i?:any){
    this.isLanguageSelected = true;
    this.languageDropdown.push(verties);
    this.languages.splice(i,1);
    this.generalForm.value.generalInfoDetails.vernacularLanguage = this.languageDropdown;
    this.vernacularLanguage.setValue(this.languageDropdown);
  }

locationBack()
  {
    this.router.navigateByUrl('app/applicationsummary');
  }  ngOnInit() {
    
    this.route.fragment.subscribe((fragment:any)=>{
      if(fragment !== null){
        this.subMenuTitle = fragment;
      }
    });
    // Getting List from API for vertical, credit, product, vernacular Language
    this.application.uilock();
    this.socketservice.get("MstApplicationAdd/GetDropDown").subscribe((result:any)=>{
      this.response=result; 
      this.verticals = this.response.vertical_list;
      this.languages = this.response.vernacularlang_list;
      this.creditGroups = this.response.creditgrouplist;
      this.products = this.response.productname_list;
      this.filterverticalTypes = this.verticals;
      this.filterCreditFlow = this.creditGroups;
      // this.filterProgram = this.products;
     this.filterLanguage = this.languages;
    });
    this.application.uiunlock();

   

    this.generalInfoDetails.controls['searchLanguages'].valueChanges
    .pipe(debounceTime(300))
    .subscribe((value: string) => {
      this.filterLanguage = this.languages.filter(
        (LANG: { vernacular_language: string }) =>
          LANG.vernacular_language.toLowerCase().includes(value.toLowerCase())
      );
    });

 
    this.generalInfoDetails.controls['searchvertical'].valueChanges
    .pipe(debounceTime(300))
    .subscribe((value: string) => {
      this.filterverticalTypes = this.verticals.filter(
        (VERTICAL: { vertical_name: string }) =>
          VERTICAL.vertical_name.toLowerCase().includes(value.toLowerCase())
      );
    });

    this.generalInfoDetails.controls['searchcreditGroup'].valueChanges
    .pipe(debounceTime(300))
    .subscribe((value: string) => {
      this.filterCreditFlow = this.creditGroups.filter(
        (credit: { creditgroup_name: string }) =>
          credit.creditgroup_name.toLowerCase().includes(value.toLowerCase())
      );
    });
    // Getting List from API for SA - Samunnati Associate 
    this.application.uilock();
    this.socketservice.get("MstApplication360/GetAssociateMasterASC").subscribe((result:any)=>{
      this.samNames = result.saassociatemaster_list
      this.filterSamNames = this.samNames;
    });
    this.application.uiunlock();

    this.generalInfoDetails.controls['searchSamNames'].valueChanges
    .pipe(debounceTime(300))
    .subscribe((value: string) => {
      this.filterSamNames = this.samNames.filter(
        (NAME: { name: string }) =>
          NAME.name.toLowerCase().includes(value.toLowerCase())
      );
    });

    // To set the length of the verity multiple value length
    if(this.verity == '' || this.verity == undefined || this.verity == null)
    { this.verity_length =0}

    this.product_length=this.listproduct.length;

    //Temp clear for product
    this.application.uilock();
    this.socketservice.get('MstNgApplicationAdd/GetTempApp').subscribe((result:any)=>{
      this.application.uiunlock();
    });
  }
}
