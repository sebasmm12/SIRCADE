import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchedulesProgrammingComponent } from './schedules-programming.component';

describe('SchedulesProgrammingComponent', () => {
  let component: SchedulesProgrammingComponent;
  let fixture: ComponentFixture<SchedulesProgrammingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SchedulesProgrammingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SchedulesProgrammingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
