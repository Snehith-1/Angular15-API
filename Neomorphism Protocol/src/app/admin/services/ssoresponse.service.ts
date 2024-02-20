import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SsoresponseService {
  apiurl= 'Login/LoginReturn'; 
  constructor(private http:HttpClient) {

   }
   ssologin(code: any){
    return this.http.post(this.apiurl,code);
  }
}
