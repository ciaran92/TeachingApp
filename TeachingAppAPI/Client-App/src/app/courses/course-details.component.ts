import { Component, OnInit } from '@angular/core';
import { CourseService } from '../services/course.service';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrls: ['./course-details.component.css']
})
export class CourseDetailsComponent implements OnInit {

  routeId: any;
  courseDetails: any;
  constructor(private courseService: CourseService, private authService: AuthenticationService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getCourseInfo();
  }

  getCourseInfo(){
    this.courseService.getCourse(this.getCourseIdFromURL()).subscribe((response) => {
      this.courseDetails = response;
      console.log("deets: " + this.courseDetails[0].courseName);
    });
  }

  enrol(){
    this.courseService.enrolInCourse(this.getCourseIdFromURL()).subscribe((response) => {
      
      console.log("response from API: " + response);
    });
  }

  getCourseIdFromURL(){
    return parseInt(this.route.snapshot.paramMap.get('id'));
  }

}
