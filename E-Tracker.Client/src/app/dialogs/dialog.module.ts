import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadDialogComponent } from './file-upload-dialog/file-upload-dialog.component';
import {MatDialogModule} from '@angular/material/dialog';
import {  MatButtonModule} from "@angular/material/button";
import { FileUploadModule } from '../services/common/file-upload/file-upload.module';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { SelectImageProductDialogComponent } from './select-image-product-dialog/select-image-product-dialog.component';
import { MatCardModule } from "@angular/material/card";


@NgModule({
  declarations: [
  //  SelectImageProductDialogComponent,
    DeleteDialogComponent,
    
  ],
  imports: [
    CommonModule,
    MatButtonModule,
    FileUploadModule,
    MatDialogModule,
    MatCardModule
  ]
})
export class DialogModule { }
