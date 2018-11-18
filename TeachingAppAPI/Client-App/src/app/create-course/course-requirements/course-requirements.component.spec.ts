import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseRequirementsComponent } from './course-requirements.component';

describe('CourseRequirementsComponent', () => {
  let component: CourseRequirementsComponent;
  let fixture: ComponentFixture<CourseRequirementsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CourseRequirementsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
