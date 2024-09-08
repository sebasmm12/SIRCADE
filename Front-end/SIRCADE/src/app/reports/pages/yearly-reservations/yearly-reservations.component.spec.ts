import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YearlyReservationsComponent } from './yearly-reservations.component';

describe('YearlyReservationsComponent', () => {
  let component: YearlyReservationsComponent;
  let fixture: ComponentFixture<YearlyReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YearlyReservationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YearlyReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
