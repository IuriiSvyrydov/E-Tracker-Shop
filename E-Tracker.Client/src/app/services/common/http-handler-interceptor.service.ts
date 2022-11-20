import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../ui/custom-toastr.service';
import { AuthUserService } from './model/authUser.service';

@Injectable({
  providedIn: 'root'
})
export class HttpHandlerInterceptorService implements HttpInterceptor {

  constructor(private toastr: CustomToastrService,private userAuthService: AuthUserService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(catchError(error=>{
      switch (error.status) {
        case HttpStatusCode.Unauthorized:
          this.toastr.message("You are not unauthorized","Unauthorized",{
            messageType: ToastrMessageType.Warning,
            position: ToastrPosition.BottomFullWidth
          });
          this.userAuthService.refreshToken(localStorage.getItem("refreshToken"))
                .then(data=>{
                  
                });
          break;
          
          case HttpStatusCode.InternalServerError:
            this.toastr.message("Unternal server","Internal service",{
            messageType: ToastrMessageType.Error,
            position: ToastrPosition.BottomFullWidth
          });
          break;
          case HttpStatusCode.BadRequest:
            this.toastr.message("Bad request","BadRequest",{
            messageType: ToastrMessageType.Warning,
            position: ToastrPosition.BottomFullWidth
          });
          break;
          case HttpStatusCode.NotFound :
          break;
          default:
            this.toastr.message("UnKnown Error","Error",{
            messageType: ToastrMessageType.Error,
            position: ToastrPosition.BottomFullWidth
          });
            break;
      }
      return of(error);
    }));
  }
}
