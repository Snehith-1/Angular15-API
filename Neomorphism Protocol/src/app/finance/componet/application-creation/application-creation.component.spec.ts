import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationCreationComponent } from './application-creation.component';

describe('ApplicationCreationComponent', () => {
  let component: ApplicationCreationComponent;
  let fixture: ComponentFixture<ApplicationCreationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationCreationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplicationCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
