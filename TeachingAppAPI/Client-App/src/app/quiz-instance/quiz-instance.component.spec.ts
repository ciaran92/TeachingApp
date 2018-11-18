import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizInstanceComponent } from './quiz-instance.component';

describe('QuizInstanceComponent', () => {
  let component: QuizInstanceComponent;
  let fixture: ComponentFixture<QuizInstanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizInstanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizInstanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
