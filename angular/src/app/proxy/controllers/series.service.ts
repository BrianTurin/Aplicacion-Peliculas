import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IActionResult } from '../microsoft/asp-net-core/mvc/models';

@Injectable({
  providedIn: 'root',
})
export class SeriesService {
  apiName = 'Default';
  

  searchSeries = (title: string, genre: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IActionResult>({
      method: 'GET',
      url: '/api/Series/search',
      params: { title, genre },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
