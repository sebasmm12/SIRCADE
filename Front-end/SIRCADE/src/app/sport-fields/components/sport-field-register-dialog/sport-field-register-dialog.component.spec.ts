import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportFieldRegisterDialogComponent } from './sport-field-register-dialog.component';

describe('SportFieldRegisterDialogComponent', () => {
  let component: SportFieldRegisterDialogComponent;
  let fixture: ComponentFixture<SportFieldRegisterDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SportFieldRegisterDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SportFieldRegisterDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
