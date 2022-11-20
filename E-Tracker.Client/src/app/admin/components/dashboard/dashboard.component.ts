import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import {  NgxSpinnerService} from "ngx-spinner";
import { SignalrService } from 'src/app/services/common/signalr.service';
import { ReceiveFunction } from 'src/app/constants/receive-function';
import { HubUrl } from 'src/app/constants/hubs/hub-url';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit {

  constructor(private alertify: AlertifyService,  spinner: NgxSpinnerService,
   private signalrService: SignalrService) { 
    super(spinner)
    signalrService.start(HubUrl.ProductHub);
  }

  ngOnInit(): void {
    this.signalrService.on(ReceiveFunction.ProductReceiveMessage,message=>{
      this.alertify.message(message,{
        messageType: MessageType.Notify,
        position: Position.TopRight
      });

    });
      
  }
  m(){
     this.alertify.message("Success",{
      messageType: MessageType.Success,
      position: Position.BottomLeft,
      delay :5

     });
  }
  d(){
   // this.alertify.dismiss()
  }

}
