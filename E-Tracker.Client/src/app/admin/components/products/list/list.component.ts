import {  Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import {MatTableDataSource  } from '@angular/material/table';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { List_Product } from 'src/app/contracts/list_product';
import { SelectImageProductDialogComponent } from 'src/app/dialogs/select-image-product-dialog/select-image-product-dialog.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { DialogService } from 'src/app/services/common/dialog.service';
import { ProductService } from 'src/app/services/common/model/product.service';

declare var $ : any;

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService,private productSrvice: ProductService,
    private alertifyService: AlertifyService, private dialogService: DialogService) {
    super(spinner)
   }
  
  displayedColumns: string[] = ['name', 'stock', 'price', 'createDate','updateDate','photos','edit','delete'];
   dataSource : MatTableDataSource<List_Product> = null;
   @ViewChild(MatPaginator)paginator: MatPaginator

async getProducts(){
      this.showSpinner(SpinnerType.BallScaleMultiple);

   const allProducts: { totalCount:number, products: List_Product[]} = await this.productSrvice.read(this.paginator?
    this.paginator.pageIndex:0,
    this.paginator?this.paginator.pageSize:5,
    ()=>this.hideSpinner(SpinnerType.BallScaleMultiple),errorMessage=>
    this.alertifyService.message(errorMessage,{
      messageType: MessageType.Error,
      position: Position.TopRight
    }));
    this.dataSource = new MatTableDataSource<List_Product>(allProducts.products);
    this.paginator.length = allProducts.totalCount;
    this.dataSource.paginator = this.paginator;
}
async pageChanged(){
await this.getProducts();
}
async  ngOnInit() {
  await this.getProducts();
  }
  AddProductImages(id: string){
    this.dialogService.openDialog({
      componetType: SelectImageProductDialogComponent,
      data: id,
      options:{
        width: "1400px"
      }
    })
  }
}
