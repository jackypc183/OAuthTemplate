import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { GetApiDataService } from 'src/get-api-data-service.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LineLoginRedirectUriComponent } from './line-login-redirect-uri/line-login-redirect-uri.component';
import { LineNotifyRedirectUriComponent } from './line-notify-redirect-uri/line-notify-redirect-uri.component';
import { FormsModule } from '@angular/forms'
import { ReqErrorHandler } from './error_handler';
import { RequestInterceptor } from './http_interceptor';
import { AuthGuard } from './auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    LineLoginRedirectUriComponent,
    LineNotifyRedirectUriComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    AuthGuard,
    GetApiDataService,
    ReqErrorHandler,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
