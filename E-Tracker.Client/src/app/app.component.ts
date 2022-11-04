import { Component } from '@angular/core';
import { data } from 'jquery';
import { ToastrService } from "ngx-toastr";
import { MessageType } from './services/admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'E-Tracker.Client';

  /**
   *
   */
  constructor(private toastrService: CustomToastrService) {
   /*toastrService.message("success","E-Tracker",{
    messageType: ToastrMessageType.Info,
    position: ToastrPosition.BottomFullWidth
   });*/
   
  }

}
// $.get("https://localhost:7068/api/products",data=>{
//   console.log(data);
//});
