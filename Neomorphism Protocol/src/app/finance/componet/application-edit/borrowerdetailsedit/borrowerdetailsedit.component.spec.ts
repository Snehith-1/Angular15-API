import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowerdetailseditComponent } from './borrowerdetailsedit.component';

describe('BorrowerdetailseditComponent', () => {
  let component: BorrowerdetailseditComponent;
  let fixture: ComponentFixture<BorrowerdetailseditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BorrowerdetailseditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BorrowerdetailseditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
