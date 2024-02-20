import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable()
export class Interceptor implements HttpInterceptor {
  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
   
    var token = localStorage.getItem('token');
    var url = environment.API_URL;
    if (token == '' || token == null) {
      const req = httpRequest.clone({
        url: url + httpRequest.url,
        headers: httpRequest.headers.set('Access-Control-Allow-Origin', '*')
      });
      return next.handle(req)
    }
    else {
      const req = httpRequest.clone({
        url: url + httpRequest.url,
        headers: httpRequest.headers.set('Access-Control-Allow-Origin', '*')
          .set('Authorization', token)
          .set('Content-Type', 'application/json')
      });
      return next.handle(req)
    }
  }
}