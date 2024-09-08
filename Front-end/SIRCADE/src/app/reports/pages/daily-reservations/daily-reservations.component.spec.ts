import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyReservationsComponent } from './daily-reservations.component';

describe('DailyReservationsComponent', () => {
  let component: DailyReservationsComponent;
  let fixture: ComponentFixture<DailyReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DailyReservationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DailyReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
