import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { FormGroup, FormControl, Validators } from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class EntityService {

  form: FormGroup = new FormGroup({
    entity_gid: new FormControl(null),
    entity_code: new FormControl(''),
    entity_description: new FormControl(''),
    designation_gid: new FormControl(''),
    designation_name: new FormControl(''),
    designation_description: new FormControl(''),
    entity_name: new FormControl('', Validators.required),
    created_date: new FormControl(''),
    created_by: new FormControl(''),
    status: new FormControl(''),
    message: new FormControl(''),
  });

  initializeFormGroup() {
    this.form.setValue({
      entity_gid: 0,
      entity_code: '',
      entity_description: '',
      entity_name: '',
      designation_description: '',
      designation_gid:'',
      designation_name:'',
      created_date: '',
      created_by: '',
      status: '',
      message:'',

            });
  }
   httpOptions = {
    headers: new HttpHeaders({
    'Content-Type': 'application/json'
    })
    }

  //private API_URL = environment.API_URL;
  // menuurl= 'User/menu' ;
  url= 'Entity/PostEntity' ;
  url1='Entity/GetEntitySummary' ;
  url2='Entity/Getupdateentitydetails' ;
  url3='Entity/Getdeleteentitydetails' ;
  
  constructor(private http:HttpClient) { }
  PostEntity(entity:any){
    return this.http.post(this.url,entity);
  }


  getEntity(){
    var user_gid = localStorage.getItem('user_gid');
    return this.http.get(this.url1 );
    

  }
  populateForm(Entity:any) {
    this.form.setValue(Entity);
  }
  updatentity(val : any)  {
    return this.http.post<any>(this.url2,val);
  }
  deleteEntityadd(val :any) {
    return this.http.get<any>(this.url3 + '?entity_gid=' + val.entity_gid);
  }
}
