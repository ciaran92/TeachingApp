import { Component, OnInit } from '@angular/core';
import { CourseService } from '../services/course.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {

  public courseDetails: any;
  constructor(private authService: AuthenticationService, private courseService: CourseService, private route: Router, private cookie: CookieService) { }
  
  ngOnInit() {
    this.displayCourseList();
    //console.log(JSON.parse(this.cookie.get("data")).email);
  }

  GoToCourse(courseId: number){
    //this.courseService.getCourse(courseId);
    this.route.navigate(['/courses', courseId]);
  }

  displayCourseList(){
    this.courseService.getCourses().subscribe((response) => 
    {
      this.courseDetails = response;
      console.log("courses list: " + response[0]);
    }, err => {
        console.log("error: " + err);
        if(err == 401){
          this.authService.RefreshToken().subscribe(
            (response: any) => {
              console.log(response);
              if(response.status = 200){
                console.log("refresh token still valid, keep browsing: " + response.token);
                this.authService.SetJwtToken(response.token);
                this.displayCourseList();
              }else{
                this.authService.logOut();
                console.log("something went wrong");
                this.route.navigate(['home']);
            }              
          },
          err => {
              this.authService.logOut();
              this.route.navigate(['home']);
              console.log("status" + err.status);      
            }      
          );
        }else{
          this.authService.logOut();
          console.log("something went wrong");
          this.route.navigate(['home']);
        }
    });
  }
  
}

   
