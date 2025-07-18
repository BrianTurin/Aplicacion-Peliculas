import { Component, OnInit } from '@angular/core';
import { SerieDto, SerieService } from '@proxy/series';

@Component({
  selector: 'app-watchlist',
  templateUrl: './watchlist.component.html',
  styleUrl: './watchlist.component.scss'
})
export class WatchlistComponent implements OnInit{
  savedSeries: SerieDto[] = [];

  constructor(private serieService: SerieService) {}

  ngOnInit(): void {
    this.loadSavedSeries();
  }

  loadSavedSeries(){
    this.serieService.getSeries().subscribe(
      response=>{
        this.savedSeries = response || [];
      },
      error => console.error('Error al cargar watchlist', error)
    );
  }

}
