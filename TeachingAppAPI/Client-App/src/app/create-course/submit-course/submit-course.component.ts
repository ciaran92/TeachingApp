import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';

@Component({
  selector: 'app-submit-course',
  templateUrl: './submit-course.component.html',
  styleUrls: ['../create-course.component.css']
})
export class SubmitCourseComponent implements OnInit {

  currentStep: number;

  constructor(private courseService: CreateCourseService) { }

  ngOnInit() {
    this.courseService.currentStep.subscribe(step => this.currentStep = step);
    this.courseService.changeStep(5);
  }

  SubmitCourse() {
    console.log("called");
    /*const formData: FormData = new FormData();
    formData.append("CourseName", sessionStorage.getItem("CourseName"));
    formData.append("Subtitle", sessionStorage.getItem("Subtitle"));
    formData.append("CourseDescription", sessionStorage.getItem("CourseDescription"));
    formData.append("CourseThumbnail", sessionStorage.getItem("thumbnail_img"));   

    this.courseService.uploadImage(formData).subscribe(res => {
    console.log("Success: " + res);
    })
    */
   let CourseName = sessionStorage.getItem("CourseName");
   let Subtitle = sessionStorage.getItem("Subtitle");
   let CourseDescription = sessionStorage.getItem("CourseDescription");
   let CourseThumbnailURL = sessionStorage.getItem("thumbnail_img");
   this.courseService.updateCourse(CourseName, Subtitle, CourseDescription, CourseThumbnailURL);
  }
}
