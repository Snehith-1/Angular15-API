import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  url= 'Currency/PostCurrency' ;
  url1= 'Currency/GetCurrencySummary' ;
  url2= 'Currency/deleteCurrencySummary' ;
  url3= 'Currency/GeteditCurrencySummary' ;
  url4= 'Currency/UpdatedCurrency' ;
  url5= 'Currency/Getcountrydropdown' ;
  
  url6= 'Currency/Getbreadcrumb' ;
  
  
  form: FormGroup = new FormGroup({
    currencyexchange_gid: new FormControl(null),
    currency_code: new FormControl('', Validators.required),
    exchange_rate: new FormControl('', Validators.required),
    country: new FormControl(''),
    country_name: new FormControl('', Validators.required),
    created_date: new FormControl(''),
    created_by: new FormControl(''),
    status: new FormControl(''),
    message: new FormControl(''),

  });

  initializeFormGroup() {
    this.form.setValue({
      currencyexchange_gid: 0,
      currency_code: '',
      exchange_rate: '',
      country: '',
      country_name: '',
      created_date: '',
      created_by: '',
      status: '',
      message:'',
        });
  }
  Getbreadcrumb(Getbreadcrumb: string){
    var user_gid = localStorage.getItem('user_gid');

    return this.http.get<any>(this.url6 + '?user_gid=' + user_gid+'&module_gid=' + Getbreadcrumb); 

  }

  populateForm(Currency:any) {
    this.form.setValue(Currency);
  }
  constructor(private http:HttpClient) { }
  GetCurrencySummary(){
 
    var user_gid = localStorage.getItem('user_gid');
    return this.http.get(this.url1 + '?user_gid=' + user_gid);
    
  }
  Getcountrydropdown(){
 
    var user_gid = localStorage.getItem('user_gid');
    return this.http.get(this.url5 + '?user_gid=' + user_gid);
    
  }
  
  PostCurrency(val:any){
    return this.http.post(this.url,val);
  }
  GeteditCurrencySummary(val :any) {
    return this.http.get<any>(this.url3 + '?currencyexchange_gid=' + val);
  }

  UpdatedCurrency(val : any)  {
    return this.http.post<any>(this.url4,val);
  }

  deleteCurrencySummary(val :any) {
    return this.http.get<any>(this.url2 + '?currencyexchange_gid=' + val.currencyexchange_gid);
  }
}
