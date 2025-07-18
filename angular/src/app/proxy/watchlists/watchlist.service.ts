import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { SerieDto } from '../series/models';

@Injectable({
  providedIn: 'root',
})
export class WatchlistService {
  apiName = 'Default';
  

  addSerie = (serieId: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/watchlist/serie/${serieId}`,
    },
    { apiName: this.apiName,...config });

    deleteSerie = (ImdbID: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/watchlist/delete-serie',
      params: { imdbID: ImdbID },
    },
    { apiName: this.apiName,...config });
  

  showSeries = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, SerieDto[]>({
      method: 'POST',
      url: '/api/app/watchlist/show-series',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
