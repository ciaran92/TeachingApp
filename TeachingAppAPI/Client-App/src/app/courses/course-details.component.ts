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
    // from the current route snapshot, get the id parameter from the url
    let id = parseInt(this.route.snapshot.paramMap.get('id'));
    console.log("id: " + id);
    this.courseService.getCourse(id).subscribe((response) => {
      this.courseDetails = response;
      console.log("deets: " + this.courseDetails[0].courseName);
    });
    //console.log("efgegege " + this.courseService.courseDetails[0]);
  }

  public loggedIn(): boolean{

    return false;
  }

}
