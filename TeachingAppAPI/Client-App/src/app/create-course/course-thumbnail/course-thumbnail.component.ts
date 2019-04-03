import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { ReplaySubject, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-course-thumbnail',
  templateUrl: './course-thumbnail.component.html',
  styleUrls: ['../create-course.component.css', './course-thumbnail.component.css']
})
export class CourseThumbnailComponent implements OnInit {

  currentStep: number;
  filesToUpload: File;
  filestring: string;
  image64: string;
  constructor(private courseService: CreateCourseService, private router: ActivatedRoute, private route: Router) { }

  ngOnInit() {
    this.courseService.currentStep.subscribe(step => this.currentStep = step);
    this.courseService.changeStep(2);
  }

  handleFileInput(event){
    console.log("working")
    let file = event.target.files[0];
    console.log(file);
    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (): void => {
      this.image64 = reader.result as string;
      console.log(this.image64);
      const res: string = (reader.result as string).match(
        /.+;base64,(.+)/
          )[1];
          sessionStorage.setItem("thumbnail_img", res);
    };
    reader.onerror = function (error) {
      console.log('Error: ', error);
    };
    
  }
  
  goBack(){
    this.route.navigate(['create-course/course-info', this.getCourseId()]);
  }

  next(){
    this.route.navigate(['create-course/content', this.getCourseId()]);
  }

  getCourseId(){
    return parseInt(this.router.snapshot.paramMap.get('id'));
  }

}
