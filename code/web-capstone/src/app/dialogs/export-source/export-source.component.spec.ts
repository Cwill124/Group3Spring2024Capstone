import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExportSourceComponent } from './export-source.component';

describe('ExportSourceComponent', () => {
  let component: ExportSourceComponent;
  let fixture: ComponentFixture<ExportSourceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExportSourceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExportSourceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
