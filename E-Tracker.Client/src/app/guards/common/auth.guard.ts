import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { SpinnerType } from 'src/app/base/base.component';
import { AuthService, isAuthenticate } from 'src/app/services/common/auth.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private jetHelper: JwtHelperService, private router: Router,
    private toastr: CustomToastrService,private spinner: NgxSpinnerService,
    private authService: AuthService){
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
      this.spinner.show(SpinnerType.BallScaleMultiple);
       debugger;
      if (isAuthenticate) {
         this.router.navigate(["login"],{queryParams:{returnUrl:state.url}});

       }

    return true;
  }
  
}