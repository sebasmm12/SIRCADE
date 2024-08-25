import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleProgrammingRegisterComponent } from './schedule-programming-register.component';

describe('ScheduleProgrammingRegisterComponent', () => {
  let component: ScheduleProgrammingRegisterComponent;
  let fixture: ComponentFixture<ScheduleProgrammingRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleProgrammingRegisterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleProgrammingRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
