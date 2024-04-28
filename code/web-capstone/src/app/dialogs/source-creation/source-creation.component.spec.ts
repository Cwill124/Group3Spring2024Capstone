import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SourceCreationComponent } from './source-creation.component';

describe('SourceCreationComponent', () => {
  let component: SourceCreationComponent;
  let fixture: ComponentFixture<SourceCreationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SourceCreationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SourceCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
