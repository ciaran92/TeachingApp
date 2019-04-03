import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { JwtHelperService } from '@auth0/angular-jwt';


@Component({
  selector: 'my-courses',
  templateUrl: './my-courses.component.html',
  styleUrls: ['./my-courses.component.css']
})
export class MyCoursesComponent implements OnInit {

  private name: any;
  private myCoursesList: any;

  constructor(private courseService: CourseService, private route: Router, private cookie: CookieService) { }

  ngOnInit() {
    
    /*this.auth.GetUsername().subscribe((response) => {
      this.name = response;
      console.log("name: " + this.name[0].name);
    })*/
    this.DisplayCourseList();
  }

  DisplayCourseList(){
    this.courseService.GetCoursesEnrolledIn().subscribe((response) => 
    {
      console.log("error: 401 nigga", JSON.stringify(response));
      if(response.ok){
        this.myCoursesList = response;
        console.log("MyCourses: " + this.myCoursesList[0].courseName);
      }
      if(response.status == 401){
        console.log("error: 401 nigga");
      }
    }, error => {
      console.log("error: " + (error.status));
      this.route.navigate(['/home']);
    });
  }

}     
