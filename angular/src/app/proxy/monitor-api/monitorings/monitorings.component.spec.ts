import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonitoringsComponent } from './monitorings.component';

describe('MonitotingsComponent', () => {
  let component: MonitoringsComponent;
  let fixture: ComponentFixture<MonitoringsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MonitoringsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonitoringsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
