import type { CreateUpdateSerieDto, SerieDto, SeasonDto, RatingDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SerieService {
  apiName = 'Default';
  

  create = (input: CreateUpdateSerieDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SerieDto>({
      method: 'POST',
      url: '/api/app/serie',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/serie/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SerieDto>({
      method: 'GET',
      url: `/api/app/serie/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SerieDto>>({
      method: 'GET',
      url: '/api/app/serie',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  
  
  search = (title: string, gender?: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SerieDto[]>({
      method: 'POST',
      url: '/api/app/serie/search-serie',
      params: { title, gender },
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateSerieDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SerieDto>({
      method: 'PUT',
      url: `/api/app/serie/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}

  saveSerie = (seriesDto: SerieDto[], config?: Partial<Rest.Config>)=>
    this.restService.request<any,SerieDto>({
      method: 'POST',
      url: '/api/app/serie/save-series',
      body: seriesDto,
    },
    {apiName: this.apiName,...config});

    getSeries = (config?: Partial<Rest.Config>) =>
      this.restService.request<any, SerieDto[]>({
        method: 'POST',
        url: '/api/app/serie/saved-series',
      },
      { apiName: this.apiName,...config });
}
