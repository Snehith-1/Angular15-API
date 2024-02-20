import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductSamagroComponent } from './product-samagro.component';

describe('ProductSamagroComponent', () => {
  let component: ProductSamagroComponent;
  let fixture: ComponentFixture<ProductSamagroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductSamagroComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductSamagroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
