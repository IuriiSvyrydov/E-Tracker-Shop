import { FacebookLoginProvider } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SocialAuthService, SocialUser } from 'angularx-social-login';
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
    private activateRoute: ActivatedRoute,private router: Router, private socialAuthService: SocialAuthService)
   { 
    super(spinner)
    socialAuthService.authState.subscribe( async (user: SocialUser)=>{
      console.log(user);
      this.showSpinner(SpinnerType.BallScaleMultiple);
      switch (user.provider) {
        case "GOOGLE":
          await this.userService.googleLogin(user,()=>{
        this.authService.identityCheck();
        this.hideSpinner(SpinnerType.BallScaleMultiple)
      });
          break;
       case "FACEBOOK": 
          await this.userService.faceBookLogin(user,()=>{
            this.authService.identityCheck();
            this.hideSpinner(SpinnerType.BallScaleMultiple);
          });
          break;
        default:
          break;
      }
    });
  }
  facebookLogin(){
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
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
