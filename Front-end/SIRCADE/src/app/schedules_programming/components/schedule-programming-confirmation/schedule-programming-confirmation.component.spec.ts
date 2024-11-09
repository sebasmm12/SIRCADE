import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleProgrammingConfirmationComponent } from './schedule-programming-confirmation.component';

describe('ScheduleProgrammingConfirmationComponent', () => {
  let component: ScheduleProgrammingConfirmationComponent;
  let fixture: ComponentFixture<ScheduleProgrammingConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleProgrammingConfirmationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleProgrammingConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
