import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { map } from 'rxjs';
import { GetApiDataService } from 'src/get-api-data-service.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private route: Router,
        private _GetApiUserService: GetApiDataService, ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        
        if (localStorage.getItem('API_Token')){
            return this._GetApiUserService.CheckUser()
            .pipe(map(
                (x:any)=>{
                    if(x){
                        return true;
                    }else{
                        this.route.navigate(['/Login']);
                        return false;
                    }
                }
            )
            )
        }else{
            // alert('無token')
            this.route.navigate(['/Login']);
            return false;
        }
    }
}