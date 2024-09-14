import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportFieldTypesByTurnComponent } from './sport-field-types-by-turn.component';

describe('SportFieldTypesByTurnComponent', () => {
  let component: SportFieldTypesByTurnComponent;
  let fixture: ComponentFixture<SportFieldTypesByTurnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SportFieldTypesByTurnComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SportFieldTypesByTurnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
