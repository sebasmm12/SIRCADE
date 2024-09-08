import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradesReservationsComponent } from './grades-reservations.component';

describe('GradesReservationsComponent', () => {
  let component: GradesReservationsComponent;
  let fixture: ComponentFixture<GradesReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GradesReservationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GradesReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
