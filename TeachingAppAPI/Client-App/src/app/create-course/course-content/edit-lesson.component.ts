import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { Lesson } from './lesson';

@Component({
  selector: 'edit-lesson',
  templateUrl: './edit-lesson.component.html',
  styleUrls: ['../create-course.component.css', './course-content.component.css']
})
export class EditLessonComponent implements OnInit {

    constructor(private courseService: CreateCourseService) { }

    ngOnInit() {

    }
}
