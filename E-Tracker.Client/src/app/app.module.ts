import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AdminModule } from './admin/admin.module';
import { NgxSpinnerModule } from "ngx-spinner";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UiModule } from './ui/ui.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { LoginComponent } from './ui/components/login/login.component';
import { GoogleLoginProvider, SocialLoginModule } from 'angularx-social-login';
import {  FacebookLoginProvider, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AdminModule,
   UiModule,
   NgxSpinnerModule,
   SocialLoginModule,
    BrowserAnimationsModule,
    JwtModule.forRoot({
      config:{
       
       tokenGetter:()=>localStorage.getItem("accessToken"),
        allowedDomains:["localhost:7008"]
      }
    }),
    HttpClientModule,
  
    ToastrModule.forRoot()
  ],
  
  providers: [
    {provide: "baseUrl",useValue:"https://localhost:7008/api",multi: true},
   {
    
         provide: "SocialAuthServiceConfig",
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider("1013805387157-l8k45f5sha52vsii7bvo61v7bat8t441.apps.googleusercontent.com")
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider("784148045978382")
          }
         
        ],
        
        onError: err => console.log(err)
      } as SocialAuthServiceConfig
   }
  ],
  schemas:[
    CUSTOM_ELEMENTS_SCHEMA
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

