import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { firstValueFrom, Observable } from 'rxjs';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClientService: HttpClientService,private toastr: CustomToastrService) { }
   async create(user: User): Promise<Create_User> {
    const observable: Observable<Create_User | User> = this.httpClientService.post<Create_User | User>({
      controller: "users"
    }, user);
    
    return await firstValueFrom(observable) as Create_User
}
async login(userNameOrEmail: string,password:string,callBackFunction?:()=>void): Promise<any>{
  const observable: Observable<any | TokenResponse> = this.httpClientService.post<any | TokenResponse>({
      controller: "users",
      action: "login"
    }, { userNameOrEmail, password })

    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;
    if (tokenResponse){
      
     //   localStorage.setItem("accessToken",tokenResponse.token.accessToken);
     localStorage.setItem("accessToken",tokenResponse.tokenDto.accessToken)

    } 
  this.toastr.message("user login successfully","login successfull",{
    position: ToastrPosition.TopRight,
    messageType: ToastrMessageType.Success
  });
    
  callBackFunction();
}
}