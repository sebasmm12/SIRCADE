import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportFieldUpdateDialogComponent } from './sport-field-update-dialog.component';

describe('SportFieldUpdateDialogComponent', () => {
  let component: SportFieldUpdateDialogComponent;
  let fixture: ComponentFixture<SportFieldUpdateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SportFieldUpdateDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SportFieldUpdateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
