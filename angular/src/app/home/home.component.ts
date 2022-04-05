import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GetApiDataService } from 'src/get-api-data-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  sendNotifytext:string = '';
  constructor(private route: Router,
    private _GetApiDataService: GetApiDataService) { }

  ngOnInit(): void {

  }

  NotifySub(): void {
    this._GetApiDataService.LineNotifyAuthorizeUrl()
    .subscribe(
      (x:any)=>
      {
        window.location.href = x;
      });
  }

  SendNotifyAll(): void {
    this._GetApiDataService.SendAllLineNotify(this.sendNotifytext)
    .subscribe(
      (x:any)=>
      {
        if(x.state)
          alert('發送訊息成功')
        else
          alert('發送訊息失敗')
      });
  }

  Logout(): void {
    
    this._GetApiDataService.RevokeLineNotify()
    .subscribe(
      (x:any)=>
      {
        localStorage.removeItem('API_Token');
        this.route.navigateByUrl('/Login');
      });
  }
}
