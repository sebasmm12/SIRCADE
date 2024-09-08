import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FrequentlyUsersReportComponent } from './frequently-users-report.component';

describe('FrequentlyUsersReportComponent', () => {
  let component: FrequentlyUsersReportComponent;
  let fixture: ComponentFixture<FrequentlyUsersReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FrequentlyUsersReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FrequentlyUsersReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
