import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SsoresponseComponent } from './ssoresponse.component';

describe('SsoresponseComponent', () => {
  let component: SsoresponseComponent;
  let fixture: ComponentFixture<SsoresponseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SsoresponseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SsoresponseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
