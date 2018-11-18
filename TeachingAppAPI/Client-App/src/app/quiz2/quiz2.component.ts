import { Component, OnInit } from '@angular/core';
import { Quiz2Service } from '../services/quiz2.service';

@Component({
  selector: 'app-quiz2',
  templateUrl: './quiz2.component.html',
  styleUrls: ['./quiz2.component.css']
})
export class Quiz2Component implements OnInit {

  private topicQuizzes: any;
  constructor(private quiz2Service: Quiz2Service) { }

  ngOnInit() {
    this.quiz2Service.getQuizzes().subscribe((response) => {this.topicQuizzes = response})
  }

}
