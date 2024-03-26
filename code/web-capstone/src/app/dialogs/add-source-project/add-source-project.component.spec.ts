import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSourceProjectComponent } from './add-source-project.component';

describe('AddSourceProjectComponent', () => {
  let component: AddSourceProjectComponent;
  let fixture: ComponentFixture<AddSourceProjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddSourceProjectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddSourceProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
