import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { SeriesComponent } from './series/series.component';
import { SerieRoutingModule } from './serie-routing.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { WatchlistComponent } from './watchlist/watchlist.component';

@NgModule({
  declarations: [SeriesComponent,WatchlistComponent],
  imports: [
    CommonModule,
    SharedModule,
    SerieRoutingModule,
    NgxDatatableModule
  ]
})
export class SerieModule { }
