import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationsByMonthComponent } from './reservations-by-month.component';

describe('ReservationsByMonthComponent', () => {
  let component: ReservationsByMonthComponent;
  let fixture: ComponentFixture<ReservationsByMonthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationsByMonthComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservationsByMonthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
