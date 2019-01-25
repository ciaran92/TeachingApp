import { Component, OnInit, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-course-info',
  templateUrl: './course-info.component.html',
  styleUrls: ['../create-course.component.css']
})
export class CourseInfoComponent implements OnInit, AfterViewInit {

  editor: any;
  currentStep: number;
  dataModel: string = "<h1>Hello</h1>";
  courseName: string;
  courseSubtitle: string;

  constructor(private courseService: CreateCourseService, private router: ActivatedRoute, private route: Router) { }

  ngOnInit() {
    this.courseService.currentStep.subscribe(step => this.currentStep = step);
    this.courseService.changeStep(1);
  }

  ngAfterViewInit(): void {
    //this.editor = window.document.getElementsByName('Course_Desc')[0];
    //this.editor.contentDocument.designMode = "on";
  }

  execCmd(command: string){
    this.editor.contentDocument.execCommand(command, false, null);
  }

  addImage(){
    var url = prompt("enter image url");
    this.editor.contentDocument.execCommand("insertImage", false, url);
  }

  OnSubmit(){
    sessionStorage.setItem("CourseName", this.courseName);
    sessionStorage.setItem("Subtitle", this.courseSubtitle);
    sessionStorage.setItem("CourseDescription", this.dataModel);
    console.log(this.dataModel);
    this.route.navigate(['create-course/thumbnail', this.getCourseId()]);
    //this.courseService.createCourse(CourseName, CourseSubtitle, this.editor.body.innerHTML);
  }

  getCourseId(){
    return parseInt(this.router.snapshot.paramMap.get('id'));
  }

}
