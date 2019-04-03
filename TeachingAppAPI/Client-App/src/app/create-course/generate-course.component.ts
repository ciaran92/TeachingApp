import { Component, OnInit, Input } from '@angular/core';
import { CreateCourseService } from '../services/create-course.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-course',
  templateUrl: './generate-course.component.html',
  styleUrls: ['./generate-course.component.css']
})
export class GenerateCourseComponent implements OnInit{

  courseDetails: any;

  constructor(private courseService: CreateCourseService, private route: Router) {}

  ngOnInit() {

  }

  createCourse(courseName: string){
    console.log("Course Name: " + courseName);
    this.courseService.createCourse(courseName).subscribe((response) => 
    {
      this.courseDetails = response;
      console.log("courses list: " + JSON.stringify(response));
      //console.log("courses list: " + this.courseDetails.courseId);
      this.route.navigate(['/create-course/course-info', this.courseDetails.courseId]);
    }, err => {
      console.log("error: " + err.error);
    });
  }
}
