import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleDeletionDialogComponent } from './role-deletion-dialog.component';

describe('RoleDeletionDialogComponent', () => {
  let component: RoleDeletionDialogComponent;
  let fixture: ComponentFixture<RoleDeletionDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoleDeletionDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoleDeletionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
