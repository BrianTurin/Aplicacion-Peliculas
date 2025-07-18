import { HttpResponse } from '@angular/common/http';
import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { SerieDto, SerieService } from '@proxy/series';

@Component({
  selector: 'app-series',
  templateUrl: './series.component.html',
  styleUrl: './series.component.scss'
})
export class SeriesComponent {
  series = [] as SerieDto[];
  serieTitle: string ="";
  saved: {[key: string]: boolean} = {};

  constructor(private serieService: SerieService) {

  }

  public searchSeries() {
    if (this.serieTitle.trim()) {
      this.serieService.search(this.serieTitle.trim(), "").subscribe(
        response => {
          this.series = response || [];  // Asignación segura
          console.log('Series encontradas:', this.series);  // Ahora mostrará los resultados correctos
        },
        error => {
          console.error('Error en la búsqueda:', error);  // Manejador de errores
        }
      );
    }
  }

  public saveSerie (serie: SerieDto){
    console.log('Comenzando a persistir la serie en saveSerie: ', serie);


    if (!serie.imdbId){
      console.error("No hay imdbId");
      return;
    }
    if(!serie.seasons){
        serie.seasons = [];
    }
    if (!serie.ratings){
      serie.ratings = [];
    }
    
    this.serieService.saveSerie([serie]).subscribe(()=>{
      console.log('Se persistió la serie:', serie.title);

    }),
    (error: HttpErrorResponse) => {
      console.error('Error al persistir serie: ', error.message);
      console.error('More error: ', error.error)
    }

  }

}
