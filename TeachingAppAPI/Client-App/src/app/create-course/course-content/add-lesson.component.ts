import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { Lesson } from './lesson';

@Component({
  selector: 'add-lesson',
  templateUrl: './add-lesson.component.html',
  styleUrls: ['../create-course.component.css', './course-content.component.css']
})
export class AddLessonComponent implements OnInit {

    constructor(private courseService: CreateCourseService) { }

    ngOnInit() {

    }
}
