import { Component, OnInit } from '@angular/core';
import { GetApiDataService } from 'src/get-api-data-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private _GetApiDataService: GetApiDataService) { }

  ngOnInit(): void {
  }

  LineLogin(){
    this._GetApiDataService.LineLoginAuthorizeUrl()
    .subscribe(
      (x:any)=>
      {
        window.location.href = x;
      });
  }

  FacebookLogin(){
    alert('尚未實作');
  }

  GoogleLogin(){
    alert('尚未實作');
  }
}
