import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { Topic } from './topic';

@Component({
  selector: 'edit-topic',
  templateUrl: './edit-topic.component.html',
  styleUrls: ['../create-course.component.css', './course-content.component.css']
})
export class EditTopicComponent implements OnInit {

  constructor(private courseService: CreateCourseService) { }

  ngOnInit() {

  }

}
