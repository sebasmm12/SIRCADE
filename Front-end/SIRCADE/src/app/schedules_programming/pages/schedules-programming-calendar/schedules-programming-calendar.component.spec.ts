import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchedulesProgrammingCalendarComponent } from './schedules-programming-calendar.component';

describe('SchedulesProgrammingCalendarComponent', () => {
  let component: SchedulesProgrammingCalendarComponent;
  let fixture: ComponentFixture<SchedulesProgrammingCalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SchedulesProgrammingCalendarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SchedulesProgrammingCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
