
import { Component, OnInit, Input } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'create-quiz',
  templateUrl: '../create-quiz/create-quiz.component.html',
  styleUrls: ['../../create-course.component.css', '../course-content.component.css']
})
export class CreateQuizComponent implements OnInit {

    constructor(private courseService: CreateCourseService, private router: ActivatedRoute, private route: Router) { }

    ngOnInit() {
    }

}
