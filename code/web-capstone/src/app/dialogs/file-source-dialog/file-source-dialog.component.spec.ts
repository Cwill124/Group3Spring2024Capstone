import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileSourceDialogComponent } from './file-source-dialog.component';

describe('FileSourceDialogComponent', () => {
  let component: FileSourceDialogComponent;
  let fixture: ComponentFixture<FileSourceDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FileSourceDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FileSourceDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
