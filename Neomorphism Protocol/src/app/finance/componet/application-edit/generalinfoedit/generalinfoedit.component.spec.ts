import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralinfoeditComponent } from './generalinfoedit.component';

describe('GeneralinfoeditComponent', () => {
  let component: GeneralinfoeditComponent;
  let fixture: ComponentFixture<GeneralinfoeditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GeneralinfoeditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GeneralinfoeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
