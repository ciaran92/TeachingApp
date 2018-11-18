import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitCourseComponent } from './submit-course.component';

describe('SubmitCourseComponent', () => {
  let component: SubmitCourseComponent;
  let fixture: ComponentFixture<SubmitCourseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubmitCourseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmitCourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
