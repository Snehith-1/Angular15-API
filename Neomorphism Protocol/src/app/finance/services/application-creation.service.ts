import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApplicationCreationService {
  response:any;
  constructor(private http:HttpClient) { }
  get(api:string){
    return this.http.get(api);
  } 
  getparams(api:string, params:any){
    return this.http.get(api,{params:params});
  }
}
