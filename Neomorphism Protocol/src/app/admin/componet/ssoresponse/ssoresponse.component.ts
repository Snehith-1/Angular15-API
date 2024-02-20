import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { SsoresponseService } from '../../services/ssoresponse.service';
import { CookieService } from 'ngx-cookie-service';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-ssoresponse',
  templateUrl: './ssoresponse.component.html',
  styleUrls: ['./ssoresponse.component.scss']
})
export class SsoresponseComponent implements OnInit {
  code: any;
  response: any;
  interface: any;
  angular: any;
  ssorequest: any;

  constructor(private cookieService: CookieService,private service: SsoresponseService, private route: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((params => {
      this.code = params;
    }
    ));

    const params = new HttpParams()
      .append('code',JSON.stringify(this.code))
      .append('angular', true);

    this.service.ssologin(params).subscribe(result => {
      if (result != null) {
        this.response = result;
        if (this.response.user_gid == null || this.response.user_gid == "") {
          alert('Invalid Credentials');
          this.route.navigate(['page/login'])
        }
        else if (this.response.user_gid != null || this.response.user_gid != "") {
          this.cookieService.set('token', '"' + this.response.token + '"',undefined,'/v1');
          localStorage.setItem('token', this.response.token);
          localStorage.setItem('user_gid', this.response.user_gid);
          this.route.navigate(['app/dashboard'])
        }
      }
    })
  }
}
