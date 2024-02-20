import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FacilityAndChargesComponent } from './facility.component';

describe('FacilityComponent', () => {
  let component: FacilityAndChargesComponent;
  let fixture: ComponentFixture<FacilityAndChargesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FacilityAndChargesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FacilityAndChargesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  
});
