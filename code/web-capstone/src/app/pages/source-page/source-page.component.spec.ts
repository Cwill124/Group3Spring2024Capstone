import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SourcePageComponent } from './source-page.component';

describe('SourcePageComponent', () => {
  let component: SourcePageComponent;
  let fixture: ComponentFixture<SourcePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SourcePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SourcePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
