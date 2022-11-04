import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NgxFileDropEntry } from "ngx-file-drop";
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { FileUploadDialogComponent, FileUploadDialogState } from 'src/app/dialogs/file-upload-dialog/file-upload-dialog.component';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { DialogService } from '../dialog.service';
import { HttpClientService } from '../http-client.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent  {
  constructor(private httpClientService: HttpClientService,
    private alertifyService: AlertifyService,
    private customToastrService : CustomToastrService,
    private dialog: MatDialog,
    private spinner: NgxSpinnerService,
  private dialogService: DialogService){
  }

 public files: NgxFileDropEntry[];
 @Input()options: Partial<FileUploadOptions>;

  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData: FormData = new FormData();
    for(const file of files) {
      (file.fileEntry as FileSystemFileEntry).file((_file: File)=>{
        fileData.append(_file.name, _file,file.relativePath);
      });
    }
    this.dialogService.openDialog({
      componetType: FileUploadDialogComponent,
      data: FileUploadDialogState.Yes,
      afterClosed:()=> {
        this.spinner.show(SpinnerType.BallScaleMultiple),
        this.httpClientService.post({
          controller:this.options.controller,
          action:this.options.action,
          queryString : this.options.queryString,
          headers: new HttpHeaders({"responseType":"blob"})
        },fileData).subscribe(data=>{
          const message: string ="file uplouded succssesfully";
          if (this.options.isAdminPage) {
            this.alertifyService.message(message,{
              messageType: MessageType.Success,
              position: Position.TopRight
            })
          }else{
            this.customToastrService.message(message,"Successful",{
              messageType:ToastrMessageType.Success,
              position:ToastrPosition.TopRight
            })
          }
        })
        
      },
    });
   }


}
export class FileUploadOptions{
  controller?:string;
  action?:string;
  queryString?: string;
  explanation?:string;
  accept?:string;
  isAdminPage?: boolean = false;
}