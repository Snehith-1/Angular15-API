import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';



@Injectable({
  providedIn: 'root'
})
export class SocketService {

  constructor(private http:HttpClient) { }
  get(api:string){
    return this.http.get(api);
  } 
  getparams(api:string, params:any){

    return this.http.get(api,{params:params});
  }
  post(api:string, params:any){
    return this.http.post(api,params);
  }
}
