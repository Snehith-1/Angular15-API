import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StackInstitutionComponent } from './institution.component';

describe('InstitutionComponent', () => {
  let component: StackInstitutionComponent;
  let fixture: ComponentFixture<StackInstitutionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StackInstitutionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StackInstitutionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
