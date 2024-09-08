import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationWidgetsComponent } from './reservation-widgets.component';

describe('ReservationWidgetsComponent', () => {
  let component: ReservationWidgetsComponent;
  let fixture: ComponentFixture<ReservationWidgetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationWidgetsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservationWidgetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
