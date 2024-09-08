import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportFieldReservationsComponent } from './sport-field-reservations.component';

describe('SportFieldReservationsComponent', () => {
  let component: SportFieldReservationsComponent;
  let fixture: ComponentFixture<SportFieldReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SportFieldReservationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SportFieldReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
