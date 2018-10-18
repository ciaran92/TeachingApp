import { Component, OnInit } from '@angular/core';
import { CourseService } from '../services/course.service';


@Component({
  selector: 'app-my-courses',
  templateUrl: './my-courses.component.html',
  styleUrls: ['./my-courses.component.css']
})
export class MyCoursesComponent implements OnInit {

  //private courseDetails: any;
  constructor(private courseService: CourseService) { }

  ngOnInit() {
    
    //this.courseService.getCourses().subscribe((response) => {this.courseDetails = response;});
    
    //this.courseDetails = this.courseService.getCourses();
    //console.log("test courseDetails: " + this.courseDetails[0].courseId);

  }

}     
