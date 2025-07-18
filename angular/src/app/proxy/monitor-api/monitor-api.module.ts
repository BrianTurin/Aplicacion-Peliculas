import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { MonitoringsComponent } from './monitorings/monitorings.component';
import { MonitorRoutingModule } from './monitor-api-routing.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
@NgModule({
  declarations: [MonitoringsComponent],
  imports: [
    CommonModule, 
    SharedModule, 
    MonitorRoutingModule, 
    NgxDatatableModule
  ]
})
export class MonitorApiModule { }