import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MonitoringsComponent } from './monitorings/monitorings.component';
const routes: Routes = [
    {
        path: '',
        component: MonitoringsComponent,
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MonitorRoutingModule { }
