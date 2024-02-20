import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StackIndividualComponent } from './individual.component';

describe('IndividualComponent', () => {
  let component: StackIndividualComponent;
  let fixture: ComponentFixture<StackIndividualComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StackIndividualComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StackIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
