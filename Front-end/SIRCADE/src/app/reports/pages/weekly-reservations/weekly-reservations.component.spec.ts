import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeeklyReservationsComponent } from './weekly-reservations.component';

describe('WeeklyReservationsComponent', () => {
  let component: WeeklyReservationsComponent;
  let fixture: ComponentFixture<WeeklyReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeeklyReservationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeeklyReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
