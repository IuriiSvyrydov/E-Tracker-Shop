import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from './layout/layout.module';
import { ComponentsModule } from './components/components.module';
import { DashboardModule } from './components/dashboard/dashboard.module';
import { NgxSpinnerModule } from 'ngx-spinner';
import { OrderModule } from './components/order/order.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    LayoutModule,
     ComponentsModule,
    DashboardModule,
    NgxSpinnerModule
    
  ],
  exports:[
    LayoutModule
    
  
  ]
})
export class AdminModule { }
