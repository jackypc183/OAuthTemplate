import { Injectable, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GetApiDataService {
  constructor(private http: HttpClient) {
  }
  localUrl = 'https://test/apipath'
  GetHeader() {    
    var _credentials = localStorage.getItem('API_Token');
    // console.log(_credentials)
    var _basic = "Bearer " + _credentials;
    var _header = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': _basic
    })
    return _header
  }

  /**
   * @todo LineLogin
   */
  LineLoginAuthorizeUrl() {
    var _URL = this.localUrl + 'api/LineLogin/AuthorizeUrl'
    return this.http.get(_URL.toString(), {
      headers: this.GetHeader()
    })
  }

  /**
   * @todo LineLoginToken
   */
   LineLoginToken(code:string,state:string) {
    return this.http.post(this.localUrl + 'api/LineLogin/Token', JSON.stringify({code:code,state:state}), {
      headers: this.GetHeader()
    })
  }
  
  /**
   * @todo LineNotify
   */
   LineNotifyAuthorizeUrl() {
    var _URL = this.localUrl + 'api/LineNotify/AuthorizeUrl'
    return this.http.get(_URL.toString(), {
      headers: this.GetHeader()
    })
  }

  /**
   * @todo LineNotifyToken
   */
   LineNotifyToken(code:string,state:string) {
    return this.http.post(this.localUrl + 'api/LineNotify/Token', JSON.stringify({code:code,state:state}), {
      headers: this.GetHeader()
    })
  }

  /**
   * @todo CheckUser
   */
   CheckUser() {
    var _URL = this.localUrl + 'api/Users'
    return this.http.get(_URL.toString(), {
      headers: this.GetHeader()
    })
  }

  /**
   * @todo sendAllLineNotify
   */
   SendAllLineNotify(msg:string) {
    msg = `"${msg.toString()}"`;
    return this.http.post(this.localUrl + 'api/LineNotify/SendAllNotify', msg, {
      headers: this.GetHeader()
    })
  }

  /**
   * @todo RevokeLineNotify
   */
   RevokeLineNotify() {
    var _URL = this.localUrl + 'api/LineNotify/RevokeLineNotify'
    return this.http.get(_URL.toString(), {
      headers: this.GetHeader()
    })
  }
}




















