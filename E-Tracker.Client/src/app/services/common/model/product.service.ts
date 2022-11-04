import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable ,firstValueFrom} from 'rxjs';
import { List_Product } from 'src/app/contracts/list_product';
import { Create_Product } from 'src/app/contracts/product';
import { Product_List_Image } from 'src/app/contracts/product_List_image';

import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

    constructor(private httpClientService: HttpClientService) { }

    create(product: Create_Product, successCallBack?:() => void, errorCallBack?: (errorMessage: string) => void) {
    this.httpClientService.post({
      controller: "products"
    }, product)
      .subscribe(result => {
        successCallBack();
      }, (errorResponse: HttpErrorResponse) => {
        const _error: Array<{ key: string, value: Array<string> }> = errorResponse.error;
        let message = "";
        _error.forEach((v, index) => {
          v.value.forEach((_v, _index) => {
            message += `${_v}<br>`;
          });
        });
        errorCallBack(message);
      });
}
async read(page: number = 0,size:number = 0, successCallBack:()=>void,errorCallBack:
(errorMessage:string)=>void): Promise<{totalCount: number,products:List_Product[]}>{
const promiseData: Promise<{totalCount: number, products:List_Product[]}> = 
  this.httpClientService.get<{totalCount:number,products:List_Product[]}>({
    controller:"products",
    queryString:`page=${page}&size=${size}`
  }).toPromise();
  promiseData.then(d=>successCallBack())
  .catch((errorResponse: HttpErrorResponse)=>errorCallBack(errorResponse.message))
 return await promiseData;
}
async delete(id: string){
  const obs:Observable<any> =  
  this.httpClientService.delete<any>({
    controller:"products"
  },id); 
  await firstValueFrom(obs);
}

async readInages(id: string,successCallBack?:()=>void, errorCallBack?:(errorMessage:string)=>void): Promise<Product_List_Image[]>{
  const getObservable: Observable<Product_List_Image[]> =  this.httpClientService.get<Product_List_Image[]>({
    action:"GetProductImages",
    controller:"products"
  },id);
    const images: Product_List_Image[] =  await firstValueFrom(getObservable);
    successCallBack();
    return images;
}
async deleteImage(id: string,imageId:string,successCallBack?:()=>void){
const deleteImage = this.httpClientService.delete({
  action:"DeleteImage",
  controller:"products",
  queryString: `imageId=${imageId}`
},id)
await firstValueFrom(deleteImage);
successCallBack();
}

}
