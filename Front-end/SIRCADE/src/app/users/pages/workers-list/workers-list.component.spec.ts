import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkersListComponent } from './workers-list.component';

describe('WorkersListComponent', () => {
  let component: WorkersListComponent;
  let fixture: ComponentFixture<WorkersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkersListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
