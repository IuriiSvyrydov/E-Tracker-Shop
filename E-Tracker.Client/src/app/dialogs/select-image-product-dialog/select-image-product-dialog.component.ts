import { Component, Inject, Output,OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { Product_List_Image } from 'src/app/contracts/product_List_image';
import { DialogService } from 'src/app/services/common/dialog.service';
import { FileUploadOptions } from 'src/app/services/common/file-upload/file-upload.component';
import { ProductService } from 'src/app/services/common/model/product.service';
import { BaseDialog } from '../base/base-dialog';
import { DeleteDialogComponent, DeleteState } from '../delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-select-image-product-dialog',
  templateUrl: './select-image-product-dialog.component.html',
  styleUrls: ['./select-image-product-dialog.component.scss']
})
export class SelectImageProductDialogComponent extends BaseDialog<SelectImageProductDialogComponent> implements OnInit{

  constructor(private matDialogRef: MatDialogRef<SelectImageProductDialogComponent>
    ,@Inject(MAT_DIALOG_DATA)public data: 
  SelectedImageDataState| string, private productService: ProductService,
  private dialogService: DialogService,
  private spinner: NgxSpinnerService) { 
    super(matDialogRef)
  }
  images: Product_List_Image[];
  // x = [1 ,3 ,5 ,8,21,65,56,23,67];
   @Output() options: Partial<FileUploadOptions> = {
    accept: ".png, .jpg, .jpeg, .gif",
    action: "upload",
    controller: "products",
    explanation: "Select or drag the product image here..",
    isAdminPage: true ,
    queryString: `id=${this.data}`

  };

   async ngOnInit() {
    this.spinner.show(SpinnerType.BallScaleMultiple);
      this.images = await this.productService.
      readInages(this.data as string,()=>this.spinner.hide(SpinnerType.BallScaleMultiple)) 
  }
 async deleteImage(imageId:string, event:any){
    this.dialogService.openDialog({
      componetType: DeleteDialogComponent,
      data:DeleteState.Yes,
      afterClosed: async()=>{
          this.spinner.show(SpinnerType.BallScaleMultiple);
    await  this.productService.deleteImage(this.data as string, imageId,()=>{
      this.spinner.hide(SpinnerType.BallScaleMultiple);
      var card = $(event.srcElement).parent().parent();
    });
      }
    });

  
  }
}
export enum SelectedImageDataState{
Close
}
 