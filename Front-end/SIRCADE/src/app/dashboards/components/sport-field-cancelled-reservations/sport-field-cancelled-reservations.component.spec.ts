import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportFieldCancelledReservationsComponent } from './sport-field-cancelled-reservations.component';

describe('SportFieldCancelledReservationsComponent', () => {
  let component: SportFieldCancelledReservationsComponent;
  let fixture: ComponentFixture<SportFieldCancelledReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SportFieldCancelledReservationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SportFieldCancelledReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
