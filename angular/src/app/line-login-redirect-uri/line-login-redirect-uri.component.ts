import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GetApiDataService } from 'src/get-api-data-service.service';

@Component({
  selector: 'app-line-login-redirect-uri',
  templateUrl: './line-login-redirect-uri.component.html',
  styleUrls: ['./line-login-redirect-uri.component.css']
})
export class LineLoginRedirectUriComponent implements OnInit {

  constructor(private route: Router,
    private _GetApiDataService: GetApiDataService) { }

  ngOnInit(): void {
    var urlQuery: any = queryStrings();
    if(urlQuery.code != null){
      this._GetApiDataService.LineLoginToken(urlQuery.code,urlQuery.state)
      .subscribe((x:any)=>{
        if(x.state){
          this.route.navigateByUrl('/Home')
          localStorage.setItem('API_Token', x.result)
        }else{
          this.route.navigateByUrl('/Login')
        }
      })
      // this.route.navigateByUrl('/Home')
    }else{
      this.route.navigateByUrl('/Login')
    }
    // console.log(urlQuery.code);
  }

}

export function queryStrings() {
  // This function is anonymous, is executed immediately and
  // the return value is assigned to QueryString!
  var query_string:any = {};
  var query = window.location.search.substring(1);
  var vars = query.split("&");
  for (var i = 0; i < vars.length; i++) {
    var pair = vars[i].split("=");
    // If first entry with this name
    if (typeof query_string[pair[0]] === "undefined") {
      query_string[pair[0]] = pair[1];
      // If second entry with this name
    } else if (typeof query_string[pair[0]] === "string") {
      var arr = [query_string[pair[0]], pair[1]];
      query_string[pair[0]] = arr;
      // If third or later entry with this name
    } else {
      query_string[pair[0]].push(pair[1]);
    }
  }
  return query_string;
}