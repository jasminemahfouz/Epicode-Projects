import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UncompletedComponent } from './uncompleted.component';

describe('UncompletedComponent', () => {
  let component: UncompletedComponent;
  let fixture: ComponentFixture<UncompletedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UncompletedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UncompletedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
