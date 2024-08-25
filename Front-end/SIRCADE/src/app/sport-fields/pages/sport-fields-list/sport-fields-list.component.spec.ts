import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportFieldsListComponent } from './sport-fields-list.component';

describe('SportFieldsListComponent', () => {
  let component: SportFieldsListComponent;
  let fixture: ComponentFixture<SportFieldsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SportFieldsListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SportFieldsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
