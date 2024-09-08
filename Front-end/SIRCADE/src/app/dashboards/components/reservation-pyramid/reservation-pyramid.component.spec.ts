import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationPyramidComponent } from './reservation-pyramid.component';

describe('ReservationPyramidComponent', () => {
  let component: ReservationPyramidComponent;
  let fixture: ComponentFixture<ReservationPyramidComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationPyramidComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservationPyramidComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
