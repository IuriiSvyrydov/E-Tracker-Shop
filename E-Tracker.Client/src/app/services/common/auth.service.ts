import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private jwtHelper: JwtHelperService) { }
  identityCheck(){
    const token: string = localStorage.getItem("accessToken");
      //  const decodedToken = this.jetHelper.decodeToken(token);
      //  const expirationDate: Date = this.jetHelper.getTokenExpirationDate(token);
       let expired: boolean;
       try {
        expired = this.jwtHelper.isTokenExpired(token);
        
      } catch  {
         expired = true;
      }
      isAuthenticate = token!=null && !expired
  }
  
  public get isAuthenticated() : boolean {
    return isAuthenticate;
  }
  
}
export let isAuthenticate:boolean;
