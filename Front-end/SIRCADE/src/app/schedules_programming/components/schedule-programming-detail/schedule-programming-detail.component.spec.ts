import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleProgrammingDetailComponent } from './schedule-programming-detail.component';

describe('ScheduleProgrammingDetailComponent', () => {
  let component: ScheduleProgrammingDetailComponent;
  let fixture: ComponentFixture<ScheduleProgrammingDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleProgrammingDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleProgrammingDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
