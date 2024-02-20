import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndividualeditComponent } from './individualedit.component';

describe('IndividualeditComponent', () => {
  let component: IndividualeditComponent;
  let fixture: ComponentFixture<IndividualeditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndividualeditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndividualeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
