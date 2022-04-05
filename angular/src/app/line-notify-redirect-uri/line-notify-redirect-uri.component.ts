import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GetApiDataService } from 'src/get-api-data-service.service';
import { queryStrings } from '../line-login-redirect-uri/line-login-redirect-uri.component';

@Component({
  selector: 'app-line-notify-redirect-uri',
  templateUrl: './line-notify-redirect-uri.component.html',
  styleUrls: ['./line-notify-redirect-uri.component.css']
})
export class LineNotifyRedirectUriComponent implements OnInit {

  constructor(private route: Router,
    private _GetApiDataService: GetApiDataService) { }

  ngOnInit(): void {
    var urlQuery: any = queryStrings();
    if(urlQuery.code != null){
      this._GetApiDataService.LineNotifyToken(urlQuery.code,urlQuery.state)
      .subscribe((x:any)=>{
        if(x.state){
          alert("訂閱成功!")
        }else{
          alert("訂閱失敗!")
        }
        this.route.navigateByUrl('/Home')
      })
    }else{
      this.route.navigateByUrl('/Home')
    }
    // console.log(urlQuery.code);
  }

}
