import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvanceHomePageContentComponent } from './advance-home-page-content.component';

describe('AdvanceHomePageContentComponent', () => {
  let component: AdvanceHomePageContentComponent;
  let fixture: ComponentFixture<AdvanceHomePageContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdvanceHomePageContentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdvanceHomePageContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
