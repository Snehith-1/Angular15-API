import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StakeholderDetailsComponent } from './stakeholder-details.component';

describe('StakeholderDetailsComponent', () => {
  let component: StakeholderDetailsComponent;
  let fixture: ComponentFixture<StakeholderDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StakeholderDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StakeholderDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
