import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';

@Component({
  selector: 'teacher-dashboard',
  templateUrl: './teacher-dashboard.component.html',
  styleUrls: ['./teacher-dashboard.component.css']
})
export class TeacherDashboardComponent implements OnInit {

  private courseListStatus1: Response;
  private courseListStatus2: any;

  constructor(private courseService: CreateCourseService) { }

  ngOnInit() {
    this.displayUsersCreatedCourses();
  }

  displayUsersCreatedCourses(){
    this.courseService.getTeacherCourseList().subscribe((response) => 
    {
      this.courseListStatus1 = response.status1;
      console.log(JSON.stringify(this.courseListStatus1))
      this.courseListStatus2 = response.status2;
    }, err => {
        console.log("error: " + err);
    });
  }

}
