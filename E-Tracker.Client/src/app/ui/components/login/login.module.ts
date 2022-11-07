import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { RouterModule } from '@angular/router';
import { SocialAuthService } from 'angularx-social-login/socialauth.service';



@NgModule({
  declarations: [
    /*LoginComponent*/
  ],
  providers:[],

  imports: [
    CommonModule,
    RouterModule.forChild([
      {path:"", component:LoginComponent}
    ])
  ]
})
export class LoginModule { }
