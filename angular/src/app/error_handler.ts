import { Injectable, OnDestroy } from '@angular/core';
import { HttpErrorResponse, HttpRequest, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { takeWhile } from 'rxjs/operators';

@Injectable()
export class ReqErrorHandler implements OnDestroy {
    ngOnDestroy(): void {
        this.api_subscribe = false;
    }
    api_subscribe = true; //ngOnDestroy時要取消
    KeyMan: string = ''
    constructor(private router: Router,
    ) {
    }

    checkEverRequestError: boolean = false

    public handleError(request: HttpRequest<any>, error: HttpErrorResponse) {
        this.CheckError(request, error, false);
    }

    private CheckError(request: HttpRequest<any>, error: HttpErrorResponse, ifTimeOut: boolean) {
       alert('登入逾時，請重新登入');
    //    localStorage.removeItem('API_Token');
    //    this.router.navigateByUrl('/Login');
    }
}
