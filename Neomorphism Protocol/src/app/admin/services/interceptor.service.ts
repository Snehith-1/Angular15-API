import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler, HttpEventType, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable()
export class Interceptor implements HttpInterceptor {
  constructor( public route:Router){}
  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
   
    var token = localStorage.getItem('token');
    var url = environment.API_URL;
    if (token == '' || token == null) {
      const req = httpRequest.clone({
        url: url + httpRequest.url,
        headers: httpRequest.headers.set('Access-Control-Allow-Origin', '*')
      });
      return next.handle(req).pipe(
        tap({
          next:(event)=>{
            if(event instanceof HttpResponse)
            {
              if(event.status == 500)
              {
                this.route.navigate(['/page/500']);
              }
              else if(event.status == 200)
              {
                //this.route.navigate(['/page/500']);
              }
              else if(event.status == 401)
              {
                //this.route.navigate(['/page/500']);
              }
              return;
            }
          },
          error: (error) => {
            if(error.status === 401) {
              //this.route.navigate(['/page/500']);
            }
            else if(error.status === 404) {
              this.route.navigate(['/page/500']);
            }
          }
        })
      )
     
    }
    else {
      const req = httpRequest.clone({
        url: url + httpRequest.url,
        headers: httpRequest.headers.set('Access-Control-Allow-Origin', '*')
          .set('Authorization', token)
          .set('Content-Type', 'application/json')
      });
      return next.handle(req).pipe(
        tap({
          next:(event)=>{
            if(event instanceof HttpResponse)
            {
              if(event.status == 500)
              {
                this.route.navigate(['/page/500']);
              }
              else if(event.status == 401)
              {
                //this.route.navigate(['/page/500']);
              }
              else if(event.status === 404) {
                this.route.navigate(['/page/500']);
              }
            }
          },
          error: (error) => {
            if(error.status === 401) {
              //this.route.navigate(['/page/500']);
            }
            else if(error.status === 404) {
              this.route.navigate(['/page/500']);
            }
          }
        })
      )
    }
  }
}