import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class CustomToastrService {

  constructor(private toastr: ToastrService) { 
  }
  message(message: string, title: string,toastrOptions: Partial<ToastrOptions>){
messageType:ToastrMessageType;
    this.toastr[toastrOptions.messageType](message,title,{
      positionClass:toastrOptions.position
    });
  }
}
export enum ToastrMessageType{
  Success = "success",
  Info = "info",
  Warning = "warning",
  Error  = "error" 
}

export enum ToastrPosition{
  TopRight = "toastr-top-right",
  BottomRight = "toastr-bottom-right",
  BottomLeft = "toastr-bottom-left",
  TopLeft = "toastr-top-left",
  TopFullWidth = "toastr-top-full-width",
  BottomFullWidth = "toast-bottom-full-width",
  TopCenter = "toastr-top-center",
  BottomCenter = "toastr-bottom-center"
}
export class ToastrOptions {
messageType:ToastrMessageType;
position:ToastrPosition
}
