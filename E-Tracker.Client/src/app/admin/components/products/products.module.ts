import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products.component';
import { RouterModule } from '@angular/router';
import {MatSidenavModule} from '@angular/material/sidenav';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatTableModule} from '@angular/material/table';
import {  MatPaginatorModule} from "@angular/material/paginator";
import { DeleteDirective } from 'src/app/directives/admin/delete.directive';
import{MatDialog, MatDialogModule} from '@angular/material/dialog'
import { FileUploadModule } from 'src/app/services/common/file-upload/file-upload.module';
import { DialogModule } from '@angular/cdk/dialog';
import { DialogService } from 'src/app/services/common/dialog.service';
import { DeleteDialogComponent } from 'src/app/dialogs/delete-dialog/delete-dialog.component';
import { SelectImageProductDialogComponent } from 'src/app/dialogs/select-image-product-dialog/select-image-product-dialog.component';


@NgModule({
  declarations: [
    ProductsComponent,
    CreateComponent,
    DeleteDirective,
    ListComponent,
    DeleteDialogComponent
    
  ],
  providers:[
      DialogService
  ],
  imports: [
    CommonModule,
    MatSidenavModule,
    MatFormFieldModule,
    MatTableModule,
    MatInputModule,
    MatPaginatorModule,
    DialogModule,
    MatDialogModule,
    MatButtonModule,

    //FileUploadModule,
    //FileUploadModule,
    RouterModule.forChild([ 
      {path:"", component:ProductsComponent}
    ])
  ]
})
export class ProductsModule { }
