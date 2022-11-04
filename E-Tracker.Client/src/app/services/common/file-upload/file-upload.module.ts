import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadComponent } from './file-upload.component';
import { NgxFileDropModule } from 'ngx-file-drop';
import { DialogModule } from '@angular/cdk/dialog';
import { FileUploadDialogComponent } from 'src/app/dialogs/file-upload-dialog/file-upload-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { SelectImageProductDialogComponent } from 'src/app/dialogs/select-image-product-dialog/select-image-product-dialog.component';
import { MatCardModule } from '@angular/material/card';
import { BrowserModule } from '@angular/platform-browser';



@NgModule({
  declarations: [
    FileUploadComponent,
    FileUploadDialogComponent,
    SelectImageProductDialogComponent
  ],
  imports: [
    CommonModule,
    
    NgxFileDropModule,
    MatDialogModule,
    MatButtonModule,
    MatCardModule
    
    
    
  ],
  exports:[
    FileUploadComponent
  ]
})
export class FileUploadModule { }
