import { Injectable } from '@angular/core';
import { SocialUser } from 'angularx-social-login';
import { firstValueFrom, Observable } from 'rxjs';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class AuthUserService {

  constructor(private httpClientService: HttpClientService,private toastr: CustomToastrService) { }
  async login(userNameOrEmail: string,password:string,callBackFunction?:()=>void): Promise<any>{
  const observable: Observable<any | TokenResponse> = this.httpClientService.post<any | TokenResponse>({
      controller: "auth",
      action: "login"
    }, { userNameOrEmail, password })

    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;
    if (tokenResponse){
      
     //   localStorage.setItem("accessToken",tokenResponse.token.accessToken);
     localStorage.setItem("accessToken",tokenResponse.tokenDto.accessToken);
     localStorage.setItem("refreshToken",tokenResponse.tokenDto.refreshToken);
      this.toastr.message("user login successfully","login successfull",{
    position: ToastrPosition.TopRight,
    messageType: ToastrMessageType.Success
  });
    

    } 
 
  callBackFunction();
}
async refreshToken(refreshToken:string,callBackFunction?:()=>void): Promise<any>{
 const observable: Observable<any| TokenResponse> =this.httpClientService.post<any| TokenResponse>({
  controller:"auth",
  action:"refreshToken"
 },{refreshToken: refreshToken});
 const refreshTokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;
  if (refreshTokenResponse) {
     localStorage.setItem("accessToken",refreshTokenResponse.tokenDto.accessToken);
     localStorage.setItem("refreshToken",refreshTokenResponse.tokenDto.refreshToken);
  //     this.toastr.message("user login successfully","login successfull",{
  //   position: ToastrPosition.TopRight,
  //   messageType: ToastrMessageType.Success
  // }
  // );
    
  }
  callBackFunction();
}
async googleLogin(user:SocialUser,callBackFunction?:()=>void): Promise<any>{
  const observable:Observable<SocialUser|TokenResponse>= this.httpClientService.post<SocialUser|TokenResponse>({
        action:"google-login",
        controller:"auth"
      },user)
      const tokenResponse:TokenResponse =  await firstValueFrom(observable) as TokenResponse

      if (tokenResponse) {
        localStorage.setItem("accessToken",tokenResponse.tokenDto.accessToken);
        localStorage.setItem("refreshToken",tokenResponse.tokenDto.refreshToken);
      }
      this.toastr.message("google login successfully ","google login success, welcome",{
        position:ToastrPosition.TopRight,
        messageType:ToastrMessageType.Success
      });
      callBackFunction();
}
async faceBookLogin(user: SocialUser,callBackFunction?:()=>void): Promise<any>{
  const observable: Observable<SocialUser|TokenResponse> = this.httpClientService.post<SocialUser|TokenResponse>({
    controller:"auth",
    action:"facebook-login"
  },user);
  const tokenResponse =  await firstValueFrom(observable) as TokenResponse
  if (tokenResponse) {
    localStorage.setItem("accessToken",tokenResponse.tokenDto.accessToken);
    localStorage.setItem("refreshToken",tokenResponse.tokenDto.refreshToken);

    this.toastr.message("google login successfully ","google login success, welcome",{
        position:ToastrPosition.TopRight,
        messageType:ToastrMessageType.Success
      });
  } 
      callBackFunction();
   this.toastr.message("facefook login successfully ","facebook login success, welcome",{
        position:ToastrPosition.TopRight,
        messageType:ToastrMessageType.Success
      });
      callBackFunction();

}
  
}
