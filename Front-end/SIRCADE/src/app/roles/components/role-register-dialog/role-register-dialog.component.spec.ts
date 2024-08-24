import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleRegisterDialogComponent } from './role-register-dialog.component';

describe('RoleRegisterDialogComponent', () => {
  let component: RoleRegisterDialogComponent;
  let fixture: ComponentFixture<RoleRegisterDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoleRegisterDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoleRegisterDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
