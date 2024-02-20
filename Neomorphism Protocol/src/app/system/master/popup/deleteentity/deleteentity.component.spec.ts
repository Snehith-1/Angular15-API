import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteentityComponent } from './deleteentity.component';

describe('DeleteentityComponent', () => {
  let component: DeleteentityComponent;
  let fixture: ComponentFixture<DeleteentityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteentityComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteentityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
