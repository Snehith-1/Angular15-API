import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Token } from '@angular/compiler';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  apiurl=  'user/topmenu'; 
  apimenuurl = 'user/sidemenu';
  ipurl = 'UserType/Getipandlogintime'
  constructor(private http: HttpClient) { }

  getPosts() {
    var user_gid = localStorage.getItem('user_gid');
    return this.http.get(this.apiurl + '?user_gid=' + user_gid)
  }
  getmenuPosts(module_gid: string) {
    var user_gid = localStorage.getItem('user_gid');
    var module_gid = module_gid;
    return this.http.get(this.apimenuurl + '?user_gid=' + user_gid + '&module_gid='+module_gid)
  }
  getiplog() {
    return this.http.get(this.ipurl)
  }
}