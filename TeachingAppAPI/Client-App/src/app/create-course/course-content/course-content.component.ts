import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { Topic } from './topic';

@Component({
  selector: 'app-course-content',
  templateUrl: './course-content.component.html',
  styleUrls: ['../create-course.component.css']
})
export class CourseContentComponent implements OnInit {

  htmlContent: string;
  currentStep: number;
  topics: Topic[] = [];
  addTopic: boolean = false;
  editorConfig = {
    "editable": true,
    "spellcheck": true,
    "height": "400px",
    "minHeight": "0",
    "width": "auto",
    "minWidth": "0",
    "translate": "yes",
    "enableToolbar": true,
    "showToolbar": true,
    "placeholder": "Enter text here...",
    "imageEndPoint": "",
    "toolbar": [
        ["bold", "italic", "underline", "strikeThrough", "superscript", "subscript"],
        ["fontSize"],
        ["justifyLeft", "justifyCenter", "justifyRight", "justifyFull", "indent", "outdent"],
        ["cut", "copy", "delete", "removeFormat", "undo", "redo"],
        ["paragraph", "blockquote", "removeBlockquote", "horizontalLine", "orderedList", "unorderedList"],
        ["link", "unlink", "image", "video"]
    ]
  };

  constructor(private courseService: CreateCourseService) { }

  ngOnInit() {
    this.courseService.currentStep.subscribe(step => this.currentStep = step);
    this.courseService.changeStep(5);
    this.topics.push(new Topic("Introduction"));
    console.log("topic length: " + this.topics.length);
  }

  AddTopic(){
    //this.topics.push(this.topics.length);
  }

  test(){
    console.log(this.htmlContent);
  }

}
