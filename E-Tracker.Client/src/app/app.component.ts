import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { data } from 'jquery';
import { AuthService } from './services/common/auth.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public authService: AuthService, public toastr:CustomToastrService,private router:Router) {
  this.authService.identityCheck();
  }
  SignOut(){
    localStorage.removeItem("accessToken");
    this.authService.identityCheck();
    this.router.navigate([""]);
    this.toastr.message("session is cloused","Signed out",{
      messageType: ToastrMessageType.Warning,
      position: ToastrPosition.TopLeft
    });

  }

}

