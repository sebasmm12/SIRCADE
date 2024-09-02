import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserStatusUpdateDialogComponent } from './user-status-update-dialog.component';

describe('UserStatusUpdateDialogComponent', () => {
  let component: UserStatusUpdateDialogComponent;
  let fixture: ComponentFixture<UserStatusUpdateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserStatusUpdateDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserStatusUpdateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
