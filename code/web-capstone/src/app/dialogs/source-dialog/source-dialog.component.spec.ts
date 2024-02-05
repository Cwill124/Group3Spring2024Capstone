import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SourceDialogComponent } from './source-dialog.component';

describe('SourceDialogComponent', () => {
  let component: SourceDialogComponent;
  let fixture: ComponentFixture<SourceDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SourceDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SourceDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
