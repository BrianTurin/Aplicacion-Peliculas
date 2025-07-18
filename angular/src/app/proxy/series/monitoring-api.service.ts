import type { APIMonitoringDto, APIStatisticsDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class APIMonitoringAppService {
  apiName = 'Default';
  

  showMonitorings = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, APIMonitoringDto[]>({
      method: 'POST',
      url: '/api/app/monitoreo-api/show-monitorings',
    },
    { apiName: this.apiName,...config });
  

  getStatistics = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, APIStatisticsDto>({
      method: 'POST',
      url: '/api/app/monitoreo-api/get-statistics',
    },
    { apiName: this.apiName,...config });
  

  saveMonitoring = (monitoringApiDTO: APIMonitoringDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/monitoreo-api/save-monitoring',
      body: monitoringApiDTO,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
