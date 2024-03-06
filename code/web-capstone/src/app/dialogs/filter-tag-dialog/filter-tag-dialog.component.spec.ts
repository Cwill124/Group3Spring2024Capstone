import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterTagDialogComponent } from './filter-tag-dialog.component';

describe('UrlSourceDialogComponent', () => {
  let component: FilterTagDialogComponent;
  let fixture: ComponentFixture<FilterTagDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FilterTagDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FilterTagDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
