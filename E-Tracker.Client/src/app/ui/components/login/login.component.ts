import { Component, OnInit } from '@angular/core';
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

  constructor(private userService: UserService, spinner: NgxSpinnerService,private authService:AuthService) { 
    super(spinner)
  }

  ngOnInit(): void {
  }
  async login(userNameOrEmail:string,password:string){
    this.showSpinner(SpinnerType.BallScaleMultiple);
   await  this.userService.login(userNameOrEmail,password,()=>{
    this.authService.identityCheck();
    this.hideSpinner(SpinnerType.BallScaleMultiple)
   });
  }

}
