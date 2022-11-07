import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SocialAuthService, SocialAuthServiceConfig, SocialUser } from 'angularx-social-login';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AuthService } from 'src/app/services/common/auth.service';
import { UserService } from 'src/app/services/common/model/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent implements OnInit {

  constructor(private userService: UserService, spinner: NgxSpinnerService,private authService:AuthService,
    private activateRoute: ActivatedRoute,private router: Router, socialAuthService: SocialAuthService)
   { 
    super(spinner)
    socialAuthService.authState.subscribe((user: SocialUser)=>{
      console.log(user);
    });
  }

  ngOnInit(): void {
  }
  async login(userNameOrEmail:string,password:string){
    this.showSpinner(SpinnerType.BallScaleMultiple);
   await  this.userService.login(userNameOrEmail,password,()=>{
    this.authService.identityCheck();
    this.activateRoute.queryParams.subscribe(params=>{
      const returnUrl :string= params["returnUrl"];

      if (returnUrl) 
        this.router.navigate([returnUrl]);
    })
    this.hideSpinner(SpinnerType.BallScaleMultiple)
   });
  }

}
