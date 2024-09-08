import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportFieldRescheduledReservationsComponent } from './sport-field-rescheduled-reservations.component';

describe('SportFieldRescheduledReservationsComponent', () => {
  let component: SportFieldRescheduledReservationsComponent;
  let fixture: ComponentFixture<SportFieldRescheduledReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SportFieldRescheduledReservationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SportFieldRescheduledReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
