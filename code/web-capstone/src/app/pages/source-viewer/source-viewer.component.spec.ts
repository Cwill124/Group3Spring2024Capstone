import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SourceViewerComponent } from './source-viewer.component';

describe('SourceViewerComponent', () => {
  let component: SourceViewerComponent;
  let fixture: ComponentFixture<SourceViewerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SourceViewerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SourceViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
