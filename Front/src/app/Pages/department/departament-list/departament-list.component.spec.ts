import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartamentListComponent } from './departament-list.component';

describe('DepartamentListComponent', () => {
  let component: DepartamentListComponent;
  let fixture: ComponentFixture<DepartamentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartamentListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DepartamentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
