import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleProgrammingCancelationConfirmationComponent } from './schedule-programming-cancelation-confirmation.component';

describe('ScheduleProgrammingCancelationConfirmationComponent', () => {
  let component: ScheduleProgrammingCancelationConfirmationComponent;
  let fixture: ComponentFixture<ScheduleProgrammingCancelationConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleProgrammingCancelationConfirmationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleProgrammingCancelationConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
