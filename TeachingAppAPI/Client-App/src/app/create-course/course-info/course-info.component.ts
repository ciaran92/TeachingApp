import { Component, OnInit, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-course-info',
  templateUrl: './course-info.component.html',
  styleUrls: ['../create-course.component.css']
})
export class CourseInfoComponent implements OnInit, AfterViewInit {

  editor: any;
  currentStep: number;

  constructor(private courseService: CreateCourseService, private route: Router) { }

  ngOnInit() {
    this.courseService.currentStep.subscribe(step => this.currentStep = step);
    this.courseService.changeStep(1);
  }

  ngAfterViewInit(): void {
    
    this.editor = window.document.getElementsByName('Course_Desc')[0];
    this.editor.contentDocument.designMode = "on";
  }

  execCmd(command: string){
    this.editor.contentDocument.execCommand(command, false, null);
  }

  OnSubmit(CourseName: string, CourseSubtitle: string){
    sessionStorage.setItem("CourseName", CourseName);
    sessionStorage.setItem("Subtitle", CourseSubtitle);
    sessionStorage.setItem("CourseDescription", this.editor.contentDocument.body.innerHTML);

    this.route.navigate(['create-course/thumbnail']);
    //this.courseService.createCourse(CourseName, CourseSubtitle, this.editor.body.innerHTML);
    
  }

  /*FormatHTML(unformattedHTML: string): string{
    let output = "<p>";
    output += unformattedHTML;

    var replaceDiv = /<div>/gi;
    var replaceEndDiv = /<\/div>/gi;

    var format1 = output.replace(replaceDiv, "</p><div><p>");
    var format2 = format1.replace(replaceEndDiv, "</p></div>");
    output = format2;
    output += "</p>";

    return output;
  }*/

}
