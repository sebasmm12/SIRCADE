import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyReservationsComponent } from './monthly-reservations.component';

describe('MonthlyReservationsComponent', () => {
  let component: MonthlyReservationsComponent;
  let fixture: ComponentFixture<MonthlyReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MonthlyReservationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlyReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
