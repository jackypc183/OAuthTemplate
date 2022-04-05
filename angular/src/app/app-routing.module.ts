import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { HomeComponent } from './home/home.component';
import { LineLoginRedirectUriComponent } from './line-login-redirect-uri/line-login-redirect-uri.component';
import { LineNotifyRedirectUriComponent } from './line-notify-redirect-uri/line-notify-redirect-uri.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '',redirectTo:'Login' , pathMatch: 'full' },
  { path: 'Login', component: LoginComponent},
  { path: 'LineLoginCallBack', component: LineLoginRedirectUriComponent},
  { path: 'LineNotifyCallBack', component: LineNotifyRedirectUriComponent},
  { path: 'Home', component: HomeComponent,
  canActivate: [AuthGuard]  
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
