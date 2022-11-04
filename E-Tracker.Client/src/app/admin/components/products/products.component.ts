import { Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import {  NgxSpinnerService} from "ngx-spinner";
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Create_Product } from 'src/app/contracts/product';
import { ListComponent } from './list/list.component';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService,private httpClentService: HttpClientService) { 
    super(spinner)
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallScaleMultiple);
   
  }
  @ViewChild(ListComponent) listComponents: ListComponent
  createProduct(createdProduct: Create_Product){
    this.listComponents.getProducts();
  }

}
