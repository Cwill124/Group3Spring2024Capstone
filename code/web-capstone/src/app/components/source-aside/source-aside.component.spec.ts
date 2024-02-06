import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SourceAsideComponent } from './source-aside.component';

describe('SourceAsideComponent', () => {
  let component: SourceAsideComponent;
  let fixture: ComponentFixture<SourceAsideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SourceAsideComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SourceAsideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
