import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PDFSourceComponent } from './pdfsource.component';

describe('PDFSourceComponent', () => {
  let component: PDFSourceComponent;
  let fixture: ComponentFixture<PDFSourceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PDFSourceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PDFSourceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
