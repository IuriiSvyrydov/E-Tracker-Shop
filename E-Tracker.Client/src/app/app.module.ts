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
    {provide:"SocialAuthServiceConfig",
      useValue:{
        autoLogin:false,
        providers:[
          { id: GoogleLoginProvider.PROVIDER_ID,
            plugin_name:'etracker',
            provider: new GoogleLoginProvider("915094679290-c41ge18liqp1u992huqrji93bv9t6c5r.apps.googleusercontent.com")}
        ]
      }}
  ],
  schemas:[
    CUSTOM_ELEMENTS_SCHEMA
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
