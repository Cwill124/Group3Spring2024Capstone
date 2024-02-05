import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UrlSourceDialogComponent } from './url-source-dialog.component';

describe('UrlSourceDialogComponent', () => {
  let component: UrlSourceDialogComponent;
  let fixture: ComponentFixture<UrlSourceDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UrlSourceDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UrlSourceDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
