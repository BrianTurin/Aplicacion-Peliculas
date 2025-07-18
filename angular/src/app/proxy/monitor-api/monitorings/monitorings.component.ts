import { Component, OnInit } from '@angular/core';
import { APIMonitoringDto, APIMonitoringAppService, APIStatisticsDto } from '@proxy/series';

@Component({
  selector: 'app-monitotings',
  templateUrl: './monitorings.component.html',
  styleUrls: ['./monitorings.component.scss']
})
export class MonitoringsComponent implements OnInit {
  monitorings: APIMonitoringDto[] = [];
  statistics: APIStatisticsDto | null = null;

  constructor(private monitoringService: APIMonitoringAppService) {}

  ngOnInit(): void {
    this.showMonitoringsInDB();
    this.showMonitoringsStatistics();
  }

  public showMonitoringsInDB(): void {    
    this.monitoringService.showMonitorings().subscribe(response => {
      this.monitorings = response || [];
    });
  }

  public showMonitoringsStatistics(): void {    
    this.monitoringService.getStatistics().subscribe(response => {
      this.statistics = response || null;
    });
  }
}