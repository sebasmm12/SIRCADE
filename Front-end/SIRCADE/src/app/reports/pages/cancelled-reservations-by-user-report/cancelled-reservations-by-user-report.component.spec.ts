import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CancelledReservationsByUserReportComponent } from './cancelled-reservations-by-user-report.component';

describe('CancelledReservationsByUserReportComponent', () => {
  let component: CancelledReservationsByUserReportComponent;
  let fixture: ComponentFixture<CancelledReservationsByUserReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CancelledReservationsByUserReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CancelledReservationsByUserReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
