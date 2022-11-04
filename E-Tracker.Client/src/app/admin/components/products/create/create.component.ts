import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component'; 
import { Create_Product } from 'src/app/contracts/product';
import { AlertifyOptions, AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { FileUploadOptions } from 'src/app/services/common/file-upload/file-upload.component';
import { ProductService } from 'src/app/services/common/model/product.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent extends BaseComponent implements OnInit {

  constructor( spiner: NgxSpinnerService,   private productService: ProductService ,private alertify:AlertifyService ) { 
    super(spiner);
  }

  ngOnInit(): void {
  }

@Output() createProduct: EventEmitter<Create_Product> = new EventEmitter();
// @Output()fileUploadOptions:Partial<FileUploadOptions> = {
//   action: "upload",
//   controller:"products",
//   explanation: "Choose File ",
//   isAdminPage: true,
//   accept:".png, .jpg, .jpeg, .json"
// }

  create(name: HTMLInputElement,stock: HTMLInputElement,price: HTMLInputElement){
    this.showSpinner(SpinnerType.BallScaleMultiple);
    const create_product:Create_Product = new Create_Product();
    create_product.name = name.value;
    create_product.stock = parseInt(stock.value);
    create_product.price = parseFloat(price.value);

    if (!name.value) {
      this.alertify.message("Field can not be empty",{
        messageType:MessageType.Error,
        position: Position.TopRight
      });
      this.createProduct.emit(create_product);
      return;
    }

    if (parseInt(stock.value)<0) {
      this.alertify.message("Stock can not be less 0",{
        messageType:MessageType.Error,
        position: Position.TopRight
      });
      return;
    }
     if (parseFloat(price.value)<=0) {
      this.alertify.message("Price can not be less 0",{
        messageType:MessageType.Error,
        position: Position.TopRight
      });
      return;
    }

    this.productService.create(create_product,()=>{
      this.hideSpinner(SpinnerType.BallScaleMultiple);
      this.alertify.message("product created successfully",{
        messageType: MessageType.Success,
        position: Position.TopRight
      });
    },errorMessage=>{
      this.alertify.message(errorMessage,{
        messageType: MessageType.Error,
        position: Position.TopRight
      })
    });
  }

}
