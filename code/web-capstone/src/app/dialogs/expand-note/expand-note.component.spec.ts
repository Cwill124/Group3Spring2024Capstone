import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpandNoteComponent } from './expand-note.component';

describe('ExpandNoteComponent', () => {
  let component: ExpandNoteComponent;
  let fixture: ComponentFixture<ExpandNoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExpandNoteComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExpandNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
